using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/Capital/AuthorizedSharesCapital")]
    public class AuthorizedSharesCapitalController : Controller
    {
        private readonly IAuthorizedSharesCapitalRepository authorizedSharesCapitalRepository;
        private readonly IOrganizationDetailRepository organizationDetailRepository;

        public AuthorizedSharesCapitalController(IAuthorizedSharesCapitalRepository _authorizedSharesCapitalRepository, IOrganizationDetailRepository _organizationDetailRepository)
        {
            authorizedSharesCapitalRepository = _authorizedSharesCapitalRepository;
            organizationDetailRepository = _organizationDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend()
        {
            AuthorizedSharesCapitalViewModel authorizedSharesCapitalViewModel = await organizationDetailRepository.GetAuthorizedSharesCapitalEntry(StringLiteralValue.Reject);

            if (authorizedSharesCapitalViewModel == null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(authorizedSharesCapitalViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await authorizedSharesCapitalRepository.Amend(_authorizedSharesCapitalViewModel);

                    if (result)
                    {
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
                    bool result = await authorizedSharesCapitalRepository.Delete(_authorizedSharesCapitalViewModel);

                    if (result)
                    {
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

            return View(_authorizedSharesCapitalViewModel);
        }

        [HttpGet]
        [Route("AuthorizedRecords")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<AuthorizedSharesCapitalViewModel> authorizedSharesCapitalViewModel = await organizationDetailRepository.GetAuthorizedSharesCapitalIndex();

            if (authorizedSharesCapitalViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(authorizedSharesCapitalViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify()
        {
            if (await authorizedSharesCapitalRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            AuthorizedSharesCapitalViewModel authorizedSharesCapitalViewModel = await organizationDetailRepository.GetAuthorizedSharesCapitalEntry(StringLiteralValue.Verify);

            return View(authorizedSharesCapitalViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await authorizedSharesCapitalRepository.Save(_authorizedSharesCapitalViewModel);

                if (result)
                {
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

            return View(_authorizedSharesCapitalViewModel);

        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            AuthorizedSharesCapitalViewModel authorizedSharesCapitalViewModel = await organizationDetailRepository.GetAuthorizedSharesCapitalEntry(StringLiteralValue.Unverified);

            if (authorizedSharesCapitalViewModel == null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(authorizedSharesCapitalViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(AuthorizedSharesCapitalViewModel _authorizedSharesCapitalViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _authorizedSharesCapitalViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await authorizedSharesCapitalRepository.Verify(_authorizedSharesCapitalViewModel);

                    if (result)
                    {
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
                    _authorizedSharesCapitalViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await authorizedSharesCapitalRepository.Reject(_authorizedSharesCapitalViewModel);

                    if (result)
                    {
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

            return View(_authorizedSharesCapitalViewModel);
        }

    }

}