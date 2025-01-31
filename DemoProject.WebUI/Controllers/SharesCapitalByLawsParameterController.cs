using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Parameter/SharesCapital/ByLaws")]
    public class SharesCapitalByLawsParameterController : Controller
    {
        private readonly ISharesCapitalByLawsParameterRepository sharesCapitalByLawsParameterRepository;

        public SharesCapitalByLawsParameterController(ISharesCapitalByLawsParameterRepository _sharesCapitalByLawsParameterRepository)
        {
            sharesCapitalByLawsParameterRepository = _sharesCapitalByLawsParameterRepository;
        }

        [HttpGet]
        [Route("Amend")]
        public async Task<ActionResult> Amend()
        {
            SharesCapitalByLawsParameterViewModel sharesCapitalByLawsParameterViewModel = await sharesCapitalByLawsParameterRepository.GetRejectedEntry();

            if (sharesCapitalByLawsParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalByLawsParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(SharesCapitalByLawsParameterViewModel _byLawsSharesCapitalParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await sharesCapitalByLawsParameterRepository.Amend(_byLawsSharesCapitalParameterViewModel);

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
                    bool result = await sharesCapitalByLawsParameterRepository.Delete(_byLawsSharesCapitalParameterViewModel);

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

            return View(_byLawsSharesCapitalParameterViewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IEnumerable<SharesCapitalByLawsParameterViewModel> byLawsSharesCapitalParameterViewModels = await sharesCapitalByLawsParameterRepository.GetSharesCapitalByLawsParameterIndex();

            return View(byLawsSharesCapitalParameterViewModels);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify()
        {
            if (await sharesCapitalByLawsParameterRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            SharesCapitalByLawsParameterViewModel sharesCapitalByLawsParameterViewModel = sharesCapitalByLawsParameterRepository.GetActiveEntry();

            return View(sharesCapitalByLawsParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(SharesCapitalByLawsParameterViewModel _byLawsSharesCapitalParameterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await sharesCapitalByLawsParameterRepository.Save(_byLawsSharesCapitalParameterViewModel);

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

            return View(_byLawsSharesCapitalParameterViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            SharesCapitalByLawsParameterViewModel sharesCapitalByLawsParameterViewModel = await sharesCapitalByLawsParameterRepository.GetUnVerifiedEntry();

            if (sharesCapitalByLawsParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalByLawsParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(SharesCapitalByLawsParameterViewModel _byLawsSharesCapitalParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _byLawsSharesCapitalParameterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await sharesCapitalByLawsParameterRepository.Verify(_byLawsSharesCapitalParameterViewModel);

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
                    _byLawsSharesCapitalParameterViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await sharesCapitalByLawsParameterRepository.Reject(_byLawsSharesCapitalParameterViewModel);

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

            return View(_byLawsSharesCapitalParameterViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public ActionResult ViewEntry()
        {
            SharesCapitalByLawsParameterViewModel sharesCapitalByLawsParameterViewModel = sharesCapitalByLawsParameterRepository.GetActiveEntry();

            if (sharesCapitalByLawsParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(sharesCapitalByLawsParameterViewModel);
        }
    }
}