using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Linq;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonMovableAsset")]
    public class PersonMovableAssetController : Controller
    {
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonMovableAssetRepository personMovableAssetRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonMovableAssetController(IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, IPersonMasterRepository _personMasterRepository, IPersonMovableAssetRepository _personMovableAssetRepository)
        {
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            personMasterRepository = _personMasterRepository;
            personMovableAssetRepository = _personMovableAssetRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["MovableAsset"] = await personMovableAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Reject);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonMovableAssetViewModel personMovableAssetViewModel = new PersonMovableAssetViewModel();

            IEnumerable<PersonMovableAssetViewModel> personMovableAssetViewModels = (IEnumerable<PersonMovableAssetViewModel>)HttpContext.Session["MovableAsset"];

            foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModels)
            {
                personMovableAssetViewModel = viewModel;
                break;
            }
            return View(personMovableAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonMovableAssetViewModel _personMovableAssetViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personMovableAssetViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personMovableAssetViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                _personMovableAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personMovableAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personMovableAssetRepository.Amend(_personMovableAssetViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonMovableAsset");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personMovableAssetRepository.VerifyRejectDelete(_personMovableAssetViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonMovableAsset") }, JsonRequestBehavior.AllowGet);
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

            return View(_personMovableAssetViewModel.PersonId);
        }

        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonMovableAssetViewModel _personMovableAssetViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonMovableAssetViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonMovableAssetViewModel.PersonMovableAssetPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personMovableAssetRepository.GetIndex(StringLiteralValue.Verify);

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
            HttpContext.Session["MovableAsset"] = await personMovableAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId), StringLiteralValue.Verify);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            if (await personMovableAssetRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }
            PersonMovableAssetViewModel personMovableAssetViewModel = new PersonMovableAssetViewModel();

            IEnumerable<PersonMovableAssetViewModel> personMovableAssetViewModels = (IEnumerable<PersonMovableAssetViewModel>)HttpContext.Session["MovableAsset"];

            foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModels)
            {
                personMovableAssetViewModel = viewModel;
                break;
            }
            return View(personMovableAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonMovableAssetViewModel _personMovableAssetViewModel)
        {
             ClearModelStateOfDataTableFields(_personMovableAssetViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personMovableAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personMovableAssetViewModel.PersonId);

                bool result = await personMovableAssetRepository.Modify(_personMovableAssetViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonMovableAsset");
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

            return View(_personMovableAssetViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personMovableAssetRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveMovableAssetDataTables")]
        public ActionResult SaveMovableAssetDataTables(List<PersonMovableAssetViewModel> _movableAsset)
        {
            HttpContext.Session.Add("MovableAsset", _movableAsset);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personMovableAssetRepository.GetIndex(StringLiteralValue.Unverified);

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
            HttpContext.Session["MovableAsset"] = await personMovableAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonMovableAssetViewModel personMovableAssetViewModel = new PersonMovableAssetViewModel();

            IEnumerable<PersonMovableAssetViewModel> personMovableAssetViewModels = (IEnumerable<PersonMovableAssetViewModel>)HttpContext.Session["MovableAsset"];

            foreach (PersonMovableAssetViewModel viewModel in personMovableAssetViewModels)
            {
                personMovableAssetViewModel = viewModel;
                break;
            }
            return View(personMovableAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonMovableAssetViewModel _personMovableAssetViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personMovableAssetViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personMovableAssetViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personMovableAssetViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personMovableAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personMovableAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personMovableAssetRepository.VerifyRejectDelete(_personMovableAssetViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonMovableAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personMovableAssetViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personMovableAssetRepository.VerifyRejectDelete(_personMovableAssetViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonMovableAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonMovableAsset");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personMovableAssetViewModel.PersonId);
        }
    }
}