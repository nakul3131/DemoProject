using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/OrganizationLoanType")]
    public class OrganizationLoanTypeController : Controller
    {
        private readonly IOrganizationLoanTypeRepository organizationLoanTypeRepository;
        private readonly IOrganizationDetailRepository organizationDetailRepository;

        public OrganizationLoanTypeController(IOrganizationLoanTypeRepository _organizationLoanTypeRepository, IOrganizationDetailRepository _organizationDetailRepository)
        {
            organizationLoanTypeRepository = _organizationLoanTypeRepository;
            organizationDetailRepository = _organizationDetailRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            OrganizationLoanTypeViewModel organizationLoanTypeViewModel = await organizationDetailRepository.GetLoanTypeEntry(StringLiteralValue.Reject);

            // Get Organization Contact Detail In Session Object For Future Use
            HttpContext.Session["OrganizationLoanType"] = await organizationDetailRepository.GetLoanTypeEntries(StringLiteralValue.Reject);

            if (organizationLoanTypeViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(organizationLoanTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_organizationLoanTypeViewModel);
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await organizationLoanTypeRepository.Amend(_organizationLoanTypeViewModel);

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
                    bool result = await organizationLoanTypeRepository.Delete(_organizationLoanTypeViewModel);

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

            return View(_organizationLoanTypeViewModel);
        }

        private void ClearModelStateOfDataTableFields(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel)
        {
            ModelState[nameof(_organizationLoanTypeViewModel.LoanTypeId)]?.Errors?.Clear();
            ModelState[nameof(_organizationLoanTypeViewModel.MaximumLoanTenure)]?.Errors?.Clear();
            ModelState[nameof(_organizationLoanTypeViewModel.MinimumDownPaymentPercentage)]?.Errors?.Clear();
            ModelState[nameof(_organizationLoanTypeViewModel.SequenceNumber)]?.Errors?.Clear();
            ModelState[nameof(_organizationLoanTypeViewModel.SequenceNumberText)]?.Errors?.Clear();
            ModelState[nameof(_organizationLoanTypeViewModel.TransSequenceNumberText)]?.Errors?.Clear();
            ModelState[nameof(_organizationLoanTypeViewModel.ActivationDate)]?.Errors?.Clear();
        }

        [HttpGet]
        [Route("AuthorizedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<OrganizationLoanTypeViewModel> organizationLoanTypeViewModels = await organizationDetailRepository.GetLoanTypeIndex(StringLiteralValue.Verify);

            if (organizationLoanTypeViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(organizationLoanTypeViewModels);
        }

        [HttpGet]
        [Route("Change")]
        public async Task<ActionResult> Modify()
        {
            if (await organizationLoanTypeRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            OrganizationLoanTypeViewModel organizationLoanTypeViewModel = await organizationDetailRepository.GetLoanTypeEntry(StringLiteralValue.Verify);

            // Get Organization Contact Detail In Session Object For Future Use
            HttpContext.Session["OrganizationLoanType"] = await organizationDetailRepository.GetLoanTypeEntries(StringLiteralValue.Verify);

            return View(organizationLoanTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Change")]
        public async Task<ActionResult> Modify(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel)
        {
            ClearModelStateOfDataTableFields(_organizationLoanTypeViewModel);

            if (ModelState.IsValid)
            {
                bool result = await organizationLoanTypeRepository.Save(_organizationLoanTypeViewModel);

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

            return View(_organizationLoanTypeViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<OrganizationLoanTypeViewModel> _organizationLoanType)
        {
            HttpContext.Session.Add("OrganizationLoanType", _organizationLoanType);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            OrganizationLoanTypeViewModel organizationLoanTypeViewModel = await organizationDetailRepository.GetLoanTypeEntry(StringLiteralValue.Unverified);

            // Get Organization Contact Detail In Session Object For Future Use
            HttpContext.Session["OrganizationLoanType"] = await organizationDetailRepository.GetLoanTypeEntries(StringLiteralValue.Unverified);

            if (organizationLoanTypeViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(organizationLoanTypeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _organizationLoanTypeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await organizationLoanTypeRepository.Verify(_organizationLoanTypeViewModel);

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
                    _organizationLoanTypeViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await organizationLoanTypeRepository.Reject(_organizationLoanTypeViewModel);

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
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_organizationLoanTypeViewModel);
        }

    }
}