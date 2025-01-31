using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Parameter/LoanSchemeParameter")]
    public class LoanSchemeParameterController : Controller
    {
        private readonly ILoanSchemeParameterRepository loanSchemeParameterRepository;

        public LoanSchemeParameterController(ILoanSchemeParameterRepository _loanSchemeParameterRepository)
        {
            loanSchemeParameterRepository = _loanSchemeParameterRepository;
        }

        [HttpGet]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetRejectedEntry();

            if (loanSchemeParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(loanSchemeParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(LoanSchemeParameterViewModel _loanSchemeParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await loanSchemeParameterRepository.Amend(_loanSchemeParameterViewModel);

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
                    bool result = await loanSchemeParameterRepository.Delete(_loanSchemeParameterViewModel);

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

            return View(_loanSchemeParameterViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<LoanSchemeParameterViewModel> loanSchemeParameterViewModels = await loanSchemeParameterRepository.GetLoanSchemeParameterIndex();

            return View(loanSchemeParameterViewModels);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify()
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();

            if (await loanSchemeParameterRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            return View(loanSchemeParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(LoanSchemeParameterViewModel _loanSchemeParameterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await loanSchemeParameterRepository.Save(_loanSchemeParameterViewModel);

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

            return View(_loanSchemeParameterViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetUnVerifiedEntry();

            if (loanSchemeParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(loanSchemeParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(LoanSchemeParameterViewModel _loanSchemeParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _loanSchemeParameterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await loanSchemeParameterRepository.Verify(_loanSchemeParameterViewModel);

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
                    _loanSchemeParameterViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await loanSchemeParameterRepository.Reject(_loanSchemeParameterViewModel);

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

            return View(_loanSchemeParameterViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry()
        {
            LoanSchemeParameterViewModel loanSchemeParameterViewModel = await loanSchemeParameterRepository.GetActiveEntry();

            if (loanSchemeParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(loanSchemeParameterViewModel);
        }
    }
}