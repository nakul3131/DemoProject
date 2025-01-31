using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Management.Servant;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.Management.Servant;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.IO;
using System.Web;
using DemoProject.WebUI.Utility;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/Employee")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;
        private readonly IServantDetailRepository servantDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IEmployeeDbContextRepository employeeDbContextRepository;
        public EmployeeController(IEmployeeRepository _employeeRepository, IServantDetailRepository _servantDetailRepository, IManagementDetailRepository _managementDetailRepository, IPersonDetailRepository _personDetailRepository, IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, IEmployeeDbContextRepository _employeeDbContextRepository)
        {
            employeeRepository = _employeeRepository;
            servantDetailRepository = _servantDetailRepository;
            personDetailRepository = _personDetailRepository;
            managementDetailRepository = _managementDetailRepository;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            employeeDbContextRepository = _employeeDbContextRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid EmployeeId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            bool data = await employeeRepository.GetSessionValues(managementDetailRepository.GetEmployeePrmKeyById(EmployeeId), StringLiteralValue.Reject);

            EmployeeViewModel employeeViewModel = await employeeRepository.GetEmployeeEntry(EmployeeId, StringLiteralValue.Reject);

            employeeViewModel.EmployeePhotoViewModel.PhotoCopy = GetEmployeePhotoFromPath(employeeViewModel);

            if (employeeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(EmployeeViewModel _employeeViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                ClearModelStateOfDataTableFields(_employeeViewModel, StringLiteralValue.Amend);
            else
                ClearModelStateOfDataTableFields(_employeeViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await employeeRepository.Amend(_employeeViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "Employee");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await employeeRepository.VerifyRejectDelete(_employeeViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "Employee"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "Employee");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_employeeViewModel);
        }


        private void ClearModelStateOfDataTableFields(EmployeeViewModel _employeeViewModel, string _entryType)
        {
            string errorViewModelName = "EmployeeDocumentViewModel,EmployeeSalaryStructureViewModel";


            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["EmployeeDocumentViewModel.SchemeApplicationParameterPrmKey"]?.Errors?.Clear();
                ModelState["EmployeeSalaryStructureViewModel.SchemeCustomerAccountNumberPrmKey"]?.Errors?.Clear();
                ModelState["EmployeeDepartmentViewModel.SchemeCustomerAccountNumberPrmKey"]?.Errors?.Clear();
                ModelState["EmployeeDesignationViewModel.SchemeCustomerAccountNumberPrmKey"]?.Errors?.Clear();
                ModelState["EmployeePerformanceRatingViewModel.SchemeCustomerAccountNumberPrmKey"]?.Errors?.Clear();
                ModelState["EmployeeWorkingScheduleViewModel.SchemeCustomerAccountNumberPrmKey"]?.Errors?.Clear();
            }
            // Clear DataTable Error
            foreach (var key in ModelState.Keys)
            {
                var viewModelPropertyArray = key.Split('.');
                int arrayLength = viewModelPropertyArray.Length;

                if (arrayLength > 1)
                {
                    var viewModel = viewModelPropertyArray[arrayLength - 2];

                    if (errorViewModelName.Contains(viewModel) || key.Contains("Enable"))
                    {
                        ModelState[key]?.Errors?.Clear();
                    }
                }
                else
                    ModelState[key]?.Errors?.Clear();
            }


        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create()
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(EmployeeViewModel _employeeViewModel)
        {
            ClearModelStateOfDataTableFields(_employeeViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await employeeRepository.Save(_employeeViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "Employee");
                    }

                    return RedirectToAction("Default", "Home");
                }
                else
                {
                    throw new DatabaseException();
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_employeeViewModel);
        }


        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<EmployeeIndexViewModel> employeeViewModel = await employeeRepository.GetEmployeeIndex(StringLiteralValue.Reject);

            if (employeeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(employeeViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetDocumentValidationFields(string _DocumentTypeIdText)
        {
            var data = personDetailRepository.DocumentValidations(_DocumentTypeIdText);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<EmployeeSalaryStructureViewModel> _salaryStructure)
        {
            HttpContext.Session.Add("SalaryStructure", _salaryStructure);

            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("EmployeeImageSaveDataTables")]
        public ActionResult EmployeeImageSaveDataTables(List<EmployeeDocumentViewModel> _EmployeeDocument)
        {
            HttpContext.Session.Add("Document", _EmployeeDocument);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<EmployeeIndexViewModel> employeeViewModel = await employeeRepository.GetEmployeeIndex(StringLiteralValue.Unverified);

            if (employeeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(employeeViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<EmployeeIndexViewModel> employeeViewModel = await employeeRepository.GetEmployeeIndex(StringLiteralValue.Verify);

            if (employeeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(employeeViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid EmployeeId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            bool data = await employeeRepository.GetSessionValues(managementDetailRepository.GetEmployeePrmKeyById(EmployeeId), StringLiteralValue.Unverified);

            EmployeeViewModel employeeViewModel = await employeeRepository.GetEmployeeEntry(EmployeeId, StringLiteralValue.Unverified);

            employeeViewModel.EmployeePhotoViewModel.PhotoCopy = GetEmployeePhotoFromPath(employeeViewModel);

            if (employeeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(EmployeeViewModel _employeeViewModel, string Command)
        {

            if (Command == StringLiteralValue.CommandVerify)
                ClearModelStateOfDataTableFields(_employeeViewModel, StringLiteralValue.Verify);
            else
                ClearModelStateOfDataTableFields(_employeeViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _employeeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await employeeRepository.VerifyRejectDelete(_employeeViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Employee"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await employeeRepository.VerifyRejectDelete(_employeeViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Employee"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("UnverifiedIndex", "Employee");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_employeeViewModel.EmployeeId);
        }

        [NonAction]
        private byte[] GetEmployeePhotoFromPath(EmployeeViewModel _employeeViewModel)
        {
            string filePath;
            string fileName;
            byte[] imagecode = null;
            byte[] fileBytes;

            filePath = _employeeViewModel.EmployeePhotoViewModel.LocalStoragePath;

            //Get Full Destination Path
            string fullFilePath = employeeDbContextRepository.GetFullFilePath(filePath, _employeeViewModel.EmployeePhotoViewModel.NameOfFile);

            //Check File Exist Or Not
            bool fileExistance = employeeDbContextRepository.FileExist(fullFilePath);

            if (fileExistance)
            {
                // Read the file from the specified path
                fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                fileName = Path.GetFileName(filePath);

                // Create an HttpPostedFileBase instance
                HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                _employeeViewModel.EmployeePhotoViewModel.PhotoPath = postedFile;

                Stream photostream = _employeeViewModel.EmployeePhotoViewModel.PhotoPath.InputStream;
                BinaryReader photobinaryreader = new BinaryReader(photostream);
                imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
            }
            else
            {
                // Get Old PhotoPath
                string originalPath = _employeeViewModel.EmployeePhotoViewModel.LocalStoragePath;

                // Find The Last Index Of '/' to Replace File Name
                int lastSlashIndex = originalPath.LastIndexOf('/');

                string path = originalPath.Substring(0, lastSlashIndex);

                // Create New Path By Combining Path and FileName
                string newPath = path + "/" + "default.png";

                filePath = newPath;
                //Get Full Destination Path
                fullFilePath = employeeDbContextRepository.GetFullFilePath(filePath, "default.png");

                // Read the file from the specified path
                fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                fileName = Path.GetFileName(filePath);

                // Create an HttpPostedFileBase instance
                HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                _employeeViewModel.EmployeePhotoViewModel.PhotoPath = postedFile;

                Stream photostream = _employeeViewModel.EmployeePhotoViewModel.PhotoPath.InputStream;
                BinaryReader photobinaryreader = new BinaryReader(photostream);
                imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);

            }
            return imagecode;

        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry(Guid EmployeeId)
        {

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            bool data = await employeeRepository.GetSessionValues(managementDetailRepository.GetEmployeePrmKeyById(EmployeeId), StringLiteralValue.Verify);

            EmployeeViewModel employeeViewModel = await employeeRepository.GetEmployeeEntry(EmployeeId, StringLiteralValue.Verify);

            employeeViewModel.EmployeePhotoViewModel.PhotoCopy = GetEmployeePhotoFromPath(employeeViewModel);

            if (employeeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(employeeViewModel);
        }

    }
}