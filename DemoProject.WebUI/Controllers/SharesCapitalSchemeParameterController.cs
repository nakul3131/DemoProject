using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Application/Configuration/SharesCapital/Scheme")]
    public class SharesCapitalSchemeParameterController : Controller
    {
        private readonly ISharesCapitalSchemeParameterRepository sharesCapitalSchemeParameterRepository;

        public SharesCapitalSchemeParameterController(ISharesCapitalSchemeParameterRepository _sharesCapitalSchemeParameterRepository)
        {
            sharesCapitalSchemeParameterRepository = _sharesCapitalSchemeParameterRepository;
        }

        [HttpGet]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetRejectedEntry();

            if (sharesCapitalSchemeParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalSchemeParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await sharesCapitalSchemeParameterRepository.Amend(_sharesCapitalSchemeParameterViewModel);

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
                    bool result = await sharesCapitalSchemeParameterRepository.Delete(_sharesCapitalSchemeParameterViewModel);

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

            return View(_sharesCapitalSchemeParameterViewModel);
        }

        [HttpGet]
        [Route("AuthorizedRecords")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<SharesCapitalSchemeParameterViewModel> sharesCapitalSchemeParameterViewModels = await sharesCapitalSchemeParameterRepository.GetSharesCapitalSchemeParameterIndex();

            if (sharesCapitalSchemeParameterViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalSchemeParameterViewModels);
        }

        [HttpGet]
        [Route("Change")]
        public async Task<ActionResult> Modify()
        {
            // Check If Authorization Pending
            if (await sharesCapitalSchemeParameterRepository.IsAnyAuthorizationPending())
                return View("~/Views/Shared/_AuthorizationPending.cshtml");

            // Check If Any Shares Scheme Authorization Pending
            //if (await sharesCapitalSchemeParameterRepository.IsAnySharesCapitalSchemeAuthorizationPending())
            //    return View("~/Views/Shared/_WorkInProgress.cshtml");

            SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();

            return View(sharesCapitalSchemeParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Change")]
        public async Task<ActionResult> Modify(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await sharesCapitalSchemeParameterRepository.Save(_sharesCapitalSchemeParameterViewModel);

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

            return View(_sharesCapitalSchemeParameterViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetUnverifiedEntry();

            if (sharesCapitalSchemeParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalSchemeParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(SharesCapitalSchemeParameterViewModel _sharesCapitalSchemeParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandVerify)
                {
                    _sharesCapitalSchemeParameterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                    bool result = await sharesCapitalSchemeParameterRepository.Verify(_sharesCapitalSchemeParameterViewModel);

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
                    _sharesCapitalSchemeParameterViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await sharesCapitalSchemeParameterRepository.Reject(_sharesCapitalSchemeParameterViewModel);

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

            return View(_sharesCapitalSchemeParameterViewModel);
        }

        [HttpGet]
        [Route("View")]
        public async Task<ActionResult> ViewEntry()
        {
            SharesCapitalSchemeParameterViewModel sharesCapitalSchemeParameterViewModel = await sharesCapitalSchemeParameterRepository.GetActiveEntry();

            if (sharesCapitalSchemeParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalSchemeParameterViewModel);
        }
    }
}