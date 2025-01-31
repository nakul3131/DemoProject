using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Application/Configuration/Deposit/Scheme")]
    public class DepositSchemeParameterController : Controller
    {
        private readonly IDepositSchemeParameterRepository depositSchemeParameterRepository;

        public DepositSchemeParameterController(IDepositSchemeParameterRepository _depositSchemeParameterRepository)
        {
            depositSchemeParameterRepository = _depositSchemeParameterRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetRejectedEntry();

            if (depositSchemeParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(depositSchemeParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(DepositSchemeParameterViewModel _depositSchemeParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await depositSchemeParameterRepository.Amend(_depositSchemeParameterViewModel);

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
                    bool result = await depositSchemeParameterRepository.Delete(_depositSchemeParameterViewModel);

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

            return View(_depositSchemeParameterViewModel);
        }

        [HttpGet]
        [Route("AuthorizedRecords")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<DepositSchemeParameterViewModel> depositSchemeParameterViewModels = await depositSchemeParameterRepository.GetDepositSchemeParameterIndex();

            if (depositSchemeParameterViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(depositSchemeParameterViewModels);
        }

        [HttpGet]
        [Route("Change")]
        public async Task<ActionResult> Modify()
        {
            // Check If Same Page (DepositSchemeParameter) Authorization Pending
            if (await depositSchemeParameterRepository.IsAnyAuthorizationPending())
                return View("~/Views/Shared/_AuthorizationPending.cshtml");

            // Check If Any Deposit Scheme Authorization Pending
            //if (await depositSchemeParameterRepository.IsAnyDepositSchemeAuthorizationPending())
            //    return View("~/Views/Shared/_WorkInProgress.cshtml");

            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();

            return View(depositSchemeParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Change")]
        public async Task<ActionResult> Modify(DepositSchemeParameterViewModel _depositSchemeParameterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await depositSchemeParameterRepository.Save(_depositSchemeParameterViewModel);

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

            return View(_depositSchemeParameterViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetUnverifiedEntry();

            if (depositSchemeParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(depositSchemeParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(DepositSchemeParameterViewModel _depositSchemeParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _depositSchemeParameterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await depositSchemeParameterRepository.Verify(_depositSchemeParameterViewModel);

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
                    _depositSchemeParameterViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await depositSchemeParameterRepository.Reject(_depositSchemeParameterViewModel);

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

            return View(_depositSchemeParameterViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry()
        {
            DepositSchemeParameterViewModel depositSchemeParameterViewModel = await depositSchemeParameterRepository.GetActiveEntry();

            if (depositSchemeParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(depositSchemeParameterViewModel);
        }
    }
}