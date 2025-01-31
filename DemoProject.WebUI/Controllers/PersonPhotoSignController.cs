using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Linq;
using System.IO;
using System.Web;
using DemoProject.WebUI.Utility;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonPhotoSign")]
    public class PersonPhotoSignController : Controller
    {
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonPhotoSignRepository personPhotoSignRepository;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public PersonPhotoSignController(IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, IPersonPhotoSignRepository _personPhotoSignRepository, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            personPhotoSignRepository = _personPhotoSignRepository;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonPhotoSignViewModel personPhotoSignViewModel = await personPhotoSignRepository.GetEntry(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage)
                personPhotoSignViewModel.PhotoCopy = GetPersonPhotoSignFromPath("Photo", personPhotoSignViewModel);

            if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage)
                personPhotoSignViewModel.PersonSign = GetPersonPhotoSignFromPath("Sign", personPhotoSignViewModel);

            if (personPhotoSignViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personPhotoSignViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonPhotoSignViewModel _personPhotoSignViewModel, string command)
        {
            ClearModelStateOfDataTableFields(_personPhotoSignViewModel);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personPhotoSignViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personPhotoSignViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personPhotoSignRepository.Amend(_personPhotoSignViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonPhotoSign");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personPhotoSignRepository.VerifyRejectDelete(_personPhotoSignViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonPhotoSign") }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_personPhotoSignViewModel.PersonId);
        }


        [HttpGet]
        [Route("ListOfVerifiedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personPhotoSignRepository.GetIndex(StringLiteralValue.Verify);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid personId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonPhotoSignViewModel personPhotoSignViewModel = await personPhotoSignRepository.GetEntry(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);

            if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage)
                personPhotoSignViewModel.PhotoCopy = GetPersonPhotoSignFromPath("Photo", personPhotoSignViewModel);

            if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage)
                personPhotoSignViewModel.PersonSign = GetPersonPhotoSignFromPath("Sign", personPhotoSignViewModel);

            if (await personPhotoSignRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            if (personPhotoSignViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personPhotoSignViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonPhotoSignViewModel _personPhotoSignViewModel)
        {
            ClearModelStateOfDataTableFields(_personPhotoSignViewModel);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personPhotoSignViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personPhotoSignViewModel.PersonId);

                bool result = await personPhotoSignRepository.Modify(_personPhotoSignViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonPhotoSign");
                }
                else
                {
                    throw new DatabaseException();
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_personPhotoSignViewModel);
        }
        
        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personPhotoSignRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personPhotoSignRepository.GetIndex(StringLiteralValue.Unverified);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid personId)
        {
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonPhotoSignViewModel personPhotoSignViewModel = await personPhotoSignRepository.GetEntry(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            if (personInformationParameterViewModel.EnablePhotoDocumentUploadInLocalStorage)
                personPhotoSignViewModel.PhotoCopy = GetPersonPhotoSignFromPath("Photo", personPhotoSignViewModel);
            
            if (personInformationParameterViewModel.EnableSignDocumentUploadInLocalStorage)
                personPhotoSignViewModel.PersonSign = GetPersonPhotoSignFromPath("Sign", personPhotoSignViewModel);

            if (personPhotoSignViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(personPhotoSignViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonPhotoSignViewModel _personPhotoSignViewModel, string command)
        {
            ClearModelStateOfDataTableFields(_personPhotoSignViewModel);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personPhotoSignViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personPhotoSignViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personPhotoSignViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personPhotoSignRepository.VerifyRejectDelete(_personPhotoSignViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonPhotoSign"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personPhotoSignViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personPhotoSignRepository.VerifyRejectDelete(_personPhotoSignViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonPhotoSign"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonPhotoSign");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personPhotoSignViewModel.PersonId);
        }

        private void ClearModelStateOfDataTableFields(PersonPhotoSignViewModel _personPhotoSignViewModel)
        {
            if (_personPhotoSignViewModel.PhotoPath == null)
            {
                ModelState[nameof(_personPhotoSignViewModel.PhotoPath)]?.Errors?.Clear();
            }

            if (_personPhotoSignViewModel.SignPath == null)
            {
                ModelState[nameof(_personPhotoSignViewModel.SignPath)]?.Errors?.Clear();
            }
        }



        [NonAction]
        private byte[] GetPersonPhotoSignFromPath(string _photoSign, PersonPhotoSignViewModel _personPhotoSignViewModel)
        {
            string filePath;
            string fileName;
            byte[] imagecode = null;
            byte[] fileBytes;

            if (_photoSign == "Photo")
            {
                filePath = _personPhotoSignViewModel.PhotoLocalStoragePath;
                //filePath = "~/Document/Customer/Photo/RD207c76D1xjBZel8wFbg.png";

                //Get Full Destination Path
                string fullFilePath = personDbContextRepository.GetFullFilePath(filePath, _personPhotoSignViewModel.PhotoNameOfFile);

                //Check File Exist Or Not
                bool fileExistance = personDbContextRepository.FileExist(fullFilePath);

                if (fileExistance)
                {
                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _personPhotoSignViewModel.PhotoPath = postedFile;

                    Stream photostream = _personPhotoSignViewModel.PhotoPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                }
                else
                {
                    // Get Old PhotoPath
                    string originalPath = _personPhotoSignViewModel.PhotoLocalStoragePath;
                    fileName = Path.GetFileName(originalPath);
                    if (originalPath == "None" || originalPath == "Unknown")
                    {
                        originalPath = "~/Document/Customer/Photo/RD207c76D1xjBZel8wFbg.png";
                    }
                    // Find The Last Index Of '/' to Replace File Name
                    int lastSlashIndex = originalPath.LastIndexOf('/');

                    string path = originalPath.Substring(0, lastSlashIndex);

                    // Create New Path By Combining Path and FileName
                    string newPath = path + "/" + "default.png";

                    filePath = newPath;
                    //Get Full Destination Path
                    fullFilePath = personDbContextRepository.GetFullFilePath(filePath, "default.png");

                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _personPhotoSignViewModel.PhotoPath = postedFile;

                    Stream photostream = _personPhotoSignViewModel.PhotoPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);

                }
            }
            else
            {
                filePath = _personPhotoSignViewModel.SignLocalStoragePath;
                //Get Full Destination Path
                string fullFilePath = personDbContextRepository.GetFullFilePath(filePath, _personPhotoSignViewModel.SignNameOfFile);

                //Check File Exist Or Not
                bool fileExistance = personDbContextRepository.FileExist(fullFilePath);

                if (fileExistance)
                {
                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _personPhotoSignViewModel.SignPath = postedFile;

                    Stream photostream = _personPhotoSignViewModel.SignPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                }
                else
                {
                    // Get Old PhotoPath
                    string originalPath = _personPhotoSignViewModel.SignLocalStoragePath;

                    if (originalPath == "None" || originalPath == "Unknown")
                    {
                        originalPath = "~/Document/Customer/Sign/RD207c76D1xjBZel8wFbg.png";
                    }

                    // Find The Last Index Of '/' to Replace File Name
                    int lastSlashIndex = originalPath.LastIndexOf('/');

                    string path = originalPath.Substring(0, lastSlashIndex);

                    // Create New Path By Combining Path and FileName
                    string newPath = path + "/" + "default.png";

                    filePath = newPath;

                    //Get Full Destination Path
                    fullFilePath = personDbContextRepository.GetFullFilePath(filePath, "default.png");

                    // Read the file from the specified path
                    fileBytes = System.IO.File.ReadAllBytes(fullFilePath);
                    fileName = Path.GetFileName(filePath);

                    // Create an HttpPostedFileBase instance
                    HttpPostedFileBase postedFile = new MemoryPostedFile(fileBytes, fileName);

                    _personPhotoSignViewModel.SignPath = postedFile;

                    Stream photostream = _personPhotoSignViewModel.SignPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);

                }
            }
            return imagecode;
        }



    }
}