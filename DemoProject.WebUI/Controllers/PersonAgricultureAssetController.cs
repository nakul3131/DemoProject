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
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonAgricultureAsset")]
    public class PersonAgricultureAssetController : Controller
    {
        private readonly IPersonAgricultureAssetRepository personAgricultureAssetRepository;
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonAgricultureAssetController(IPersonMasterRepository _personMasterRepository, IPersonAgricultureAssetRepository _personAgricultureAssetRepository, IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository)
        {
            personMasterRepository = _personMasterRepository;
            personAgricultureAssetRepository = _personAgricultureAssetRepository;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["AgricultureAsset"] = await personAgricultureAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonAgricultureAssetViewModel personAgricultureAssetViewModel = new PersonAgricultureAssetViewModel();

            IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModels = (IEnumerable<PersonAgricultureAssetViewModel>)HttpContext.Session["AgricultureAsset"];

            foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModels)
            {
                personAgricultureAssetViewModel = viewModel;
                break;
            }
            return View(personAgricultureAssetViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personAgricultureAssetViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personAgricultureAssetViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personAgricultureAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personAgricultureAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personAgricultureAssetRepository.Amend(_personAgricultureAssetViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonAgricultureAsset");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personAgricultureAssetRepository.VerifyRejectDelete(_personAgricultureAssetViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonAgricultureAsset") }, JsonRequestBehavior.AllowGet);
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

            return View(_personAgricultureAssetViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonAgricultureAssetViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonAgricultureAssetViewModel.PersonAgricultureAssetPrmKey"]?.Errors?.Clear();
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
        [Route("ListOfVerifiedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personAgricultureAssetRepository.GetIndex(StringLiteralValue.Verify);

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
            HttpContext.Session["AgricultureAsset"] = await personAgricultureAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;
            if (await personAgricultureAssetRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonAgricultureAssetViewModel personAgricultureAssetViewModel = new PersonAgricultureAssetViewModel();

            IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModels = (IEnumerable<PersonAgricultureAssetViewModel>)HttpContext.Session["AgricultureAsset"];

            foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModels)
            {
                personAgricultureAssetViewModel = viewModel;
                break;
            }
            return View(personAgricultureAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel)
        {
             ClearModelStateOfDataTableFields(_personAgricultureAssetViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personAgricultureAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personAgricultureAssetViewModel.PersonId);

                bool result = await personAgricultureAssetRepository.Modify(_personAgricultureAssetViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonAgricultureAsset");
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

            return View(_personAgricultureAssetViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personAgricultureAssetRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveAgricultureDataTables")]
        public ActionResult SaveAgricultureDataTables(List<PersonAgricultureAssetViewModel> _agricultureAsset)
        {
            HttpContext.Session.Add("AgricultureAsset", _agricultureAsset);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personAgricultureAssetRepository.GetIndex(StringLiteralValue.Unverified);

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
            PersonAgricultureAssetViewModel personAgricultureAssetViewModel = new PersonAgricultureAssetViewModel();
            HttpContext.Session["AgricultureAsset"] = await personAgricultureAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;


            IEnumerable<PersonAgricultureAssetViewModel> personAgricultureAssetViewModels = (IEnumerable<PersonAgricultureAssetViewModel>)HttpContext.Session["AgricultureAsset"];

            foreach (PersonAgricultureAssetViewModel viewModel in personAgricultureAssetViewModels)
            {
                personAgricultureAssetViewModel = viewModel;
                break;
            }
            return View(personAgricultureAssetViewModel);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonAgricultureAssetViewModel _personAgricultureAssetViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personAgricultureAssetViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personAgricultureAssetViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personAgricultureAssetViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personAgricultureAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personAgricultureAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personAgricultureAssetRepository.VerifyRejectDelete(_personAgricultureAssetViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonAgricultureAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personAgricultureAssetViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personAgricultureAssetRepository.VerifyRejectDelete(_personAgricultureAssetViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonAgricultureAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonAgricultureAsset");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personAgricultureAssetViewModel.PersonId);
        }
    }
}