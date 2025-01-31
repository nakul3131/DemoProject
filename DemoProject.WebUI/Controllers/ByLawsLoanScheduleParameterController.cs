using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.Enterprise.Establishment;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Parameter/ByLawsLoanScheduleParameter")]
    public class ByLawsLoanScheduleParameterController : Controller
    {

        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IByLawsLoanScheduleParameterRepository byLawsLoanScheduleParameterRepository;
        private readonly IOrganizationLoanTypeRepository organizationLoanTypeRepository;
        private readonly ILoanSchemeParameterRepository loanSchemeParameterRepository;
        public ByLawsLoanScheduleParameterController(IAccountDetailRepository _accountDetailRepository, IByLawsLoanScheduleParameterRepository _byLawsLoanScheduleParameterRepository, ILoanSchemeParameterRepository _loanSchemeParameterRepository, IOrganizationLoanTypeRepository _organizationLoanTypeRepository)
        {
            accountDetailRepository = _accountDetailRepository;
            byLawsLoanScheduleParameterRepository = _byLawsLoanScheduleParameterRepository;
            organizationLoanTypeRepository = _organizationLoanTypeRepository;
            loanSchemeParameterRepository = _loanSchemeParameterRepository;
        }

        public ByLawsLoanScheduleParameterController(IByLawsLoanScheduleParameterRepository _byLawsLoanScheduleParameterRepository, IOrganizationLoanTypeRepository _organizationLoanTypeRepository)
        {
            byLawsLoanScheduleParameterRepository = _byLawsLoanScheduleParameterRepository;
            organizationLoanTypeRepository = _organizationLoanTypeRepository;
        }

        [HttpGet]
        [Route("Amend")]
        public async Task<ActionResult> Amend(byte LoanTypePrmKey)
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
            ViewBag.LoanSchemeParameter = loanSchemeParameterViewModel;

            ByLawsLoanScheduleParameterViewModel byLawsLoanScheduleParameterViewModel = await byLawsLoanScheduleParameterRepository.GetRejectedEntry(LoanTypePrmKey);

            if (byLawsLoanScheduleParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(byLawsLoanScheduleParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(ByLawsLoanScheduleParameterViewModel _byLawsLoanScheduleParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await byLawsLoanScheduleParameterRepository.Amend(_byLawsLoanScheduleParameterViewModel);

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
                    bool result = await byLawsLoanScheduleParameterRepository.VerifyRejectDelete(_byLawsLoanScheduleParameterViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "ByLawsLoanScheduleParameter"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "ByLawsLoanScheduleParameter");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_byLawsLoanScheduleParameterViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<OrganizationLoanTypeViewModel> organizationLoanTypeViewModel = await organizationLoanTypeRepository.GetByLawsLoanScheduleParameterIndex();

            return View(organizationLoanTypeViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<OrganizationLoanTypeViewModel> organizationLoanTypeViewModel = await organizationLoanTypeRepository.GetByLawsLoanScheduleParameterIndex();

            return View(organizationLoanTypeViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<OrganizationLoanTypeViewModel> organizationLoanTypeViewModel = await organizationLoanTypeRepository.GetByLawsLoanScheduleParameterIndex();

            return View(organizationLoanTypeViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(byte LoanTypePrmKey)
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
            ViewBag.LoanSchemeParameter = loanSchemeParameterViewModel;

            ByLawsLoanScheduleParameterViewModel _byLawsLoanScheduleParameterViewModel = await organizationLoanTypeRepository.GetVerifiedEntries(LoanTypePrmKey);

            if (await byLawsLoanScheduleParameterRepository.IsAnyAuthorizationPending(_byLawsLoanScheduleParameterViewModel.LoanTypePrmKey))
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            //if (_byLawsLoanScheduleParameterViewModel is null)
            //{
            //    throw new DatabaseException();
            //}

            return View(_byLawsLoanScheduleParameterViewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(ByLawsLoanScheduleParameterViewModel _byLawsLoanScheduleParameterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await byLawsLoanScheduleParameterRepository.Save(_byLawsLoanScheduleParameterViewModel);

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

            return View(_byLawsLoanScheduleParameterViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(byte LoanTypePrmKey)
        {

            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();
            ViewBag.LoanSchemeParameter = loanSchemeParameterViewModel;
            ByLawsLoanScheduleParameterViewModel byLawsLoanScheduleParameterViewModel = await organizationLoanTypeRepository.GetUnVerifiedEntries(LoanTypePrmKey);

            if (byLawsLoanScheduleParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(byLawsLoanScheduleParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(ByLawsLoanScheduleParameterViewModel _byLawsLoanScheduleParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _byLawsLoanScheduleParameterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await byLawsLoanScheduleParameterRepository.VerifyRejectDelete(_byLawsLoanScheduleParameterViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "ByLawsLoanScheduleParameter"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                if (Command == StringLiteralValue.CommandReject)
                {
                    _byLawsLoanScheduleParameterViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await byLawsLoanScheduleParameterRepository.VerifyRejectDelete(_byLawsLoanScheduleParameterViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "ByLawsLoanScheduleParameter"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("UnverifiedIndex", "ByLawsLoanScheduleParameter");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_byLawsLoanScheduleParameterViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry()
        {
            ByLawsLoanScheduleParameterViewModel byLawsLoanScheduleParameterViewModel = await byLawsLoanScheduleParameterRepository.GetActiveEntry();

            if (byLawsLoanScheduleParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(byLawsLoanScheduleParameterViewModel);
        }

    }
}

