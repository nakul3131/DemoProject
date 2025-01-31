using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/OrganizationFund")]
    public class OrganizationFundController : Controller
    {
        private readonly IOrganizationFundRepository organizationFundRepository;
        private readonly IOrganizationDetailRepository organizationDetailRepository;

        public OrganizationFundController(IOrganizationFundRepository _organizationFundRepository, IOrganizationDetailRepository _organizationDetailRepository)
        {
            organizationFundRepository = _organizationFundRepository;
            organizationDetailRepository = _organizationDetailRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            OrganizationFundViewModel organizationFundViewModel = await organizationDetailRepository.GetFundEntry(StringLiteralValue.Reject);

            // Get Organization Contact Detail In Session Object For Future Use
            HttpContext.Session["OrganizationFund"] = await organizationDetailRepository.GetFundEntries(StringLiteralValue.Reject);

            if (organizationFundViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(organizationFundViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(OrganizationFundViewModel _organizationFundViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_organizationFundViewModel);
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await organizationFundRepository.Amend(_organizationFundViewModel);

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
                    bool result = await organizationFundRepository.Delete(_organizationFundViewModel);

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

            return View(_organizationFundViewModel);
        }

        private void ClearModelStateOfDataTableFields(OrganizationFundViewModel _organizationFundViewModel)
        {
            ModelState[nameof(_organizationFundViewModel.SequenceNumber)]?.Errors?.Clear();
            ModelState[nameof(_organizationFundViewModel.SequenceNumberText)]?.Errors?.Clear();
            ModelState[nameof(_organizationFundViewModel.TransSequenceNumberText)]?.Errors?.Clear();
            ModelState[nameof(_organizationFundViewModel.ActivationDate)]?.Errors?.Clear();
            ModelState[nameof(_organizationFundViewModel.FundId)]?.Errors?.Clear();
        }

        [HttpGet]
        [Route("Change")]
        public async Task<ActionResult> Modify()
        {
            if (await organizationFundRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            OrganizationFundViewModel organizationFundViewModel = await organizationDetailRepository.GetFundEntry(StringLiteralValue.Verify);

            // Get Organization Contact Detail In Session Object For Future Use
            HttpContext.Session["OrganizationFund"] = await organizationDetailRepository.GetFundEntries(StringLiteralValue.Verify);

            return View(organizationFundViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Change")]
        public async Task<ActionResult> Modify(OrganizationFundViewModel _organizationFundViewModel)
        {
            ClearModelStateOfDataTableFields(_organizationFundViewModel);
            if (ModelState.IsValid)
            {
                bool result = await organizationFundRepository.Save(_organizationFundViewModel);

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

            return View(_organizationFundViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<OrganizationFundViewModel> _organizationFund)
        {
            HttpContext.Session.Add("OrganizationFund", _organizationFund);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            OrganizationFundViewModel organizationFundViewModel = await organizationDetailRepository.GetFundEntry(StringLiteralValue.Unverified);

            // Get Organization Contact Detail In Session Object For Future Use
            HttpContext.Session["OrganizationFund"] = await organizationDetailRepository.GetFundEntries(StringLiteralValue.Unverified);

            if (organizationFundViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(organizationFundViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(OrganizationFundViewModel _organizationFundViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _organizationFundViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await organizationFundRepository.Verify(_organizationFundViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home")}, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _organizationFundViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await organizationFundRepository.Reject(_organizationFundViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("Default", "Home")}, JsonRequestBehavior.AllowGet);
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

            return View(_organizationFundViewModel);
        }

        [HttpGet]
        [Route("AuthorizedRecords")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<OrganizationIndexViewModel> organizationFundViewModels = await organizationDetailRepository.GetFundIndex(StringLiteralValue.Verify);

            if (organizationFundViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(organizationFundViewModels);
        }

    }
}