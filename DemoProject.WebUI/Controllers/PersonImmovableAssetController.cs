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
    [RoutePrefix("Employee/PersonInformation/PersonImmovableAsset")]
    public class PersonImmovableAssetController : Controller
    {
        private readonly IPersonImmovableAssetRepository personImmovableAssetRepository;
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonImmovableAssetController(IPersonMasterRepository _personMasterRepository, IPersonImmovableAssetRepository _personImmovableAssetRepository, IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository)
        {
            personMasterRepository = _personMasterRepository;
            personImmovableAssetRepository = _personImmovableAssetRepository;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["ImmovableAsset"] = await personImmovableAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Reject);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonImmovableAssetViewModel personImmovableAssetViewModel = new PersonImmovableAssetViewModel();

            IEnumerable<PersonImmovableAssetViewModel> personImmovableAssetViewModels = (IEnumerable<PersonImmovableAssetViewModel>)HttpContext.Session["ImmovableAsset"];

            foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModels)
            {
                personImmovableAssetViewModel = viewModel;
                break;
            }
            return View(personImmovableAssetViewModel);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonImmovableAssetViewModel _personImmovableAssetViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personImmovableAssetViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personImmovableAssetViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personImmovableAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personImmovableAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personImmovableAssetRepository.Amend(_personImmovableAssetViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonImmovableAsset");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personImmovableAssetRepository.VerifyRejectDelete(_personImmovableAssetViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonImmovableAsset") }, JsonRequestBehavior.AllowGet);
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

            return View(_personImmovableAssetViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonImmovableAssetViewModel _personImmovableAssetViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonImmovableAssetViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonImmovableAssetViewModel.PersonImmovableAssetPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personImmovableAssetRepository.GetIndex(StringLiteralValue.Verify);

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
            HttpContext.Session["ImmovableAsset"] = await personImmovableAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Verify);
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;
            if (await personImmovableAssetRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonImmovableAssetViewModel personImmovableAssetViewModel = new PersonImmovableAssetViewModel();

            IEnumerable<PersonImmovableAssetViewModel> personImmovableAssetViewModels = (IEnumerable<PersonImmovableAssetViewModel>)HttpContext.Session["ImmovableAsset"];

            foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModels)
            {
                personImmovableAssetViewModel = viewModel;
                break;
            }
            return View(personImmovableAssetViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonImmovableAssetViewModel _personImmovableAssetViewModel)
        {
             ClearModelStateOfDataTableFields(_personImmovableAssetViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personImmovableAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personImmovableAssetViewModel.PersonId);

                bool result = await personImmovableAssetRepository.Modify(_personImmovableAssetViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonImmovableAsset");
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

            return View(_personImmovableAssetViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personImmovableAssetRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveImmovableDataTables")]
        public ActionResult SaveImmovableDataTables(List<PersonImmovableAssetViewModel> _immovableAsset)
        {
            HttpContext.Session.Add("ImmovableAsset", _immovableAsset);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personImmovableAssetRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["ImmovableAsset"] = await personImmovableAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonImmovableAssetViewModel personImmovableAssetViewModel = new PersonImmovableAssetViewModel();

            IEnumerable<PersonImmovableAssetViewModel> personImmovableAssetViewModels = (IEnumerable<PersonImmovableAssetViewModel>)HttpContext.Session["ImmovableAsset"];

            foreach (PersonImmovableAssetViewModel viewModel in personImmovableAssetViewModels)
            {
                personImmovableAssetViewModel = viewModel;
                break;
            }
            return View(personImmovableAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonImmovableAssetViewModel _personImmovableAssetViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personImmovableAssetViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personImmovableAssetViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personImmovableAssetViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personImmovableAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personImmovableAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personImmovableAssetRepository.VerifyRejectDelete(_personImmovableAssetViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonImmovableAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personImmovableAssetViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personImmovableAssetRepository.VerifyRejectDelete(_personImmovableAssetViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonImmovableAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonImmovableAsset");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personImmovableAssetViewModel.PersonId);
        }
    }
}