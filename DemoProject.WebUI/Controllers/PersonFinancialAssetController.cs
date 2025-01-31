using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using System.Linq;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/PersonInformation/PersonFinancialAsset")]
    public class PersonFinancialAssetController : Controller
    {
        private readonly IPersonFinancialAssetRepository personFinancialAssetRepository;
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;
        private readonly IPersonMasterRepository personMasterRepository;

        public PersonFinancialAssetController(IPersonMasterRepository _personMasterRepository, IPersonFinancialAssetRepository _personFinancialAssetRepository, IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository)
        {
            personMasterRepository = _personMasterRepository;
            personFinancialAssetRepository = _personFinancialAssetRepository;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid personId)
        {
            HttpContext.Session["FinancialAsset"] = await personFinancialAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Reject);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            PersonFinancialAssetViewModel personFinancialAssetViewModel = new PersonFinancialAssetViewModel();

            IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModels = (IEnumerable<PersonFinancialAssetViewModel>)HttpContext.Session["FinancialAsset"];

            foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModels)
            {
                personFinancialAssetViewModel = viewModel;
                break;
            }
            return View(personFinancialAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string command)
        {
            if (command == StringLiteralValue.CommandAmend)
                 ClearModelStateOfDataTableFields(_personFinancialAssetViewModel, StringLiteralValue.Amend);
            else
                 ClearModelStateOfDataTableFields(_personFinancialAssetViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personFinancialAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personFinancialAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandAmend)
                {
                    bool result = await personFinancialAssetRepository.Amend(_personFinancialAssetViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PersonFinancialAsset");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandDelete)
                {
                    bool result = await personFinancialAssetRepository.VerifyRejectDelete(_personFinancialAssetViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PersonFinancialAsset") }, JsonRequestBehavior.AllowGet);
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

            return View(_personFinancialAssetViewModel.PersonId);
        }


        [NonAction]
        private void ClearModelStateOfDataTableFields(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "PersonFinancialAssetViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["PersonFinancialAssetViewModel.PersonFinancialAssetPrmKey"]?.Errors?.Clear();
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
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personFinancialAssetRepository.GetIndex(StringLiteralValue.Verify);

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
            HttpContext.Session["FinancialAsset"] = await personFinancialAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Verify);
            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            if (await personFinancialAssetRepository.IsAnyAuthorizationPending(personMasterRepository.GetPersonPrmKeyById(personId)))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            PersonFinancialAssetViewModel personFinancialAssetViewModel = new PersonFinancialAssetViewModel();

            IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModels = (IEnumerable<PersonFinancialAssetViewModel>)HttpContext.Session["FinancialAsset"];

            foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModels)
            {
                personFinancialAssetViewModel = viewModel;
                break;
            }
            return View(personFinancialAssetViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(PersonFinancialAssetViewModel _personFinancialAssetViewModel)
        {

             ClearModelStateOfDataTableFields(_personFinancialAssetViewModel, StringLiteralValue.Create);
            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personFinancialAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personFinancialAssetViewModel.PersonId);

                bool result = await personFinancialAssetRepository.Modify(_personFinancialAssetViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("VerifiedIndex", "PersonFinancialAsset");
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

            return View(_personFinancialAssetViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personFinancialAssetRepository.GetIndex(StringLiteralValue.Reject);

            if (personIndexViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(personIndexViewModels);
        }

        [HttpPost]
        [Route("SaveFinanceDataTables")]
        public ActionResult SaveFinanceDataTables(List<PersonFinancialAssetViewModel> _financialAsset)
        {
            HttpContext.Session.Add("FinancialAsset", _financialAsset);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfUnverifiedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PersonIndexViewModel> personIndexViewModels = await personFinancialAssetRepository.GetIndex(StringLiteralValue.Unverified);

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
            PersonFinancialAssetViewModel personFinancialAssetViewModel = new PersonFinancialAssetViewModel();
            HttpContext.Session["FinancialAsset"] = await personFinancialAssetRepository.GetEntries(personMasterRepository.GetPersonPrmKeyById(personId),StringLiteralValue.Unverified);

            PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

            ViewBag.PersonInformationParameter = personInformationParameterViewModel;

            IEnumerable<PersonFinancialAssetViewModel> personFinancialAssetViewModels = (IEnumerable<PersonFinancialAssetViewModel>)HttpContext.Session["FinancialAsset"];

            foreach (PersonFinancialAssetViewModel viewModel in personFinancialAssetViewModels)
            {
                personFinancialAssetViewModel = viewModel;
                break;
            }
            return View(personFinancialAssetViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PersonFinancialAssetViewModel _personFinancialAssetViewModel, string command)
        {
            if (command == StringLiteralValue.CommandVerify)
                 ClearModelStateOfDataTableFields(_personFinancialAssetViewModel, StringLiteralValue.Verify);
            else
                 ClearModelStateOfDataTableFields(_personFinancialAssetViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _personFinancialAssetViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];
                _personFinancialAssetViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personFinancialAssetViewModel.PersonId);

                if (command == StringLiteralValue.CommandVerify)
                {
                    bool result = await personFinancialAssetRepository.VerifyRejectDelete(_personFinancialAssetViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonFinancialAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (command == StringLiteralValue.CommandReject)
                {
                    _personFinancialAssetViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await personFinancialAssetRepository.VerifyRejectDelete(_personFinancialAssetViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PersonFinancialAsset"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PersonFinancialAsset");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_personFinancialAssetViewModel.PersonId);
        }
    }
}