using System;
using System.Web.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Linq;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/Organization")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationRepository organizationRepository;

        public OrganizationController(IOrganizationRepository _organizationRepository)
        {
            organizationRepository = _organizationRepository;
        }

        [HttpGet]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            OrganizationViewModel organizationViewModel = await organizationRepository.GetOrganizationEntry(StringLiteralValue.Reject);

            bool data = await organizationRepository.GetSessionValues(StringLiteralValue.Reject);

            if (organizationViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(organizationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(OrganizationViewModel _organizationViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                ClearModelStateOfDataTableFields(_organizationViewModel, StringLiteralValue.Amend);
            else
                ClearModelStateOfDataTableFields(_organizationViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await organizationRepository.Amend(_organizationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("Default", "Home");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await organizationRepository.RejectDelete(_organizationViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_organizationViewModel);
        }


        private void ClearModelStateOfDataTableFields(OrganizationViewModel _organizationViewModel, string _entryType)
        {

            string errorViewModelName = "OrganizationContactDetailViewModel,OrganizationFundViewModel,OrganizationLoanTypeViewModel,OrganizationGSTRegistrationDetailViewModel";

            if (_entryType != StringLiteralValue.Create)
            {
                ModelState["OrganizationContactDetailViewModel.OrganizationContactDetailPrmKey"]?.Errors?.Clear();
                ModelState["OrganizationFundViewModel.OrganizationFundPrmKey"]?.Errors?.Clear();
                ModelState["OrganizationLoanTypeViewModel.OrganizationLoanTypePrmKey"]?.Errors?.Clear();
                ModelState["OrganizationGSTRegistrationDetailViewModel.OrganizationGSTRegistrationDetailPrmKey"]?.Errors?.Clear();
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
        [Route("AuthorizedRecords")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<OrganizationIndexViewModel> organizationViewModels = await organizationRepository.GetOrganizationIndex();

            if (organizationViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(organizationViewModels);
        }

        [HttpGet]
        [Route("Change")]
        public async Task<ActionResult> Modify()
        {
            if (await organizationRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            OrganizationViewModel organizationViewModel = await organizationRepository.GetOrganizationEntry(StringLiteralValue.Verify);

            bool data = await organizationRepository.GetSessionValues(StringLiteralValue.Verify);

            return View(organizationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Change")]
        public async Task<ActionResult> Modify(OrganizationViewModel _organizationViewModel)
        {
            ClearModelStateOfDataTableFields(_organizationViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await organizationRepository.Save(_organizationViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

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

            return View(_organizationViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<OrganizationContactDetailViewModel> _organizationContactDetail, List<OrganizationFundViewModel> _organizationFund, List<OrganizationGSTRegistrationDetailViewModel> _organizationGSTRegistrationDetail, List<OrganizationLoanTypeViewModel> _organizationLoanType)
        {
            HttpContext.Session.Add("OrganizationContactDetail", _organizationContactDetail);
            HttpContext.Session.Add("OrganizationFund", _organizationFund);
            HttpContext.Session.Add("OrganizationGSTRegistrationDetail", _organizationGSTRegistrationDetail);
            HttpContext.Session.Add("OrganizationLoanType", _organizationLoanType);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            OrganizationViewModel organizationViewModel = await organizationRepository.GetOrganizationEntry(StringLiteralValue.Unverified);

            bool data = await organizationRepository.GetSessionValues(StringLiteralValue.Unverified);

            if (organizationViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(organizationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(OrganizationViewModel _organizationViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandVerify)
                ClearModelStateOfDataTableFields(_organizationViewModel, StringLiteralValue.Verify);
            else
                ClearModelStateOfDataTableFields(_organizationViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _organizationViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await organizationRepository.Verify(_organizationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _organizationViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await organizationRepository.RejectDelete(_organizationViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("Default", "Home");
            }
            else
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");

            return View(_organizationViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> ViewEntry(Guid OrganizationId)
        {
            OrganizationViewModel organizationViewModel = await organizationRepository.GetActiveEntry();

            bool data = await organizationRepository.GetSessionValues(StringLiteralValue.Verify);

            if (organizationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(organizationViewModel);
        }
    }
}