using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Parameter.Security;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.Security.Parameter;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Security/Login/Parameter")]
    public class UserAuthenticationParameterController : Controller
    {
        private readonly IUserAuthenticationParameterRepository userAuthenticationParameterRepository;

        public UserAuthenticationParameterController(IUserAuthenticationParameterRepository _userAuthenticationParameterRepository)
        {
            userAuthenticationParameterRepository = _userAuthenticationParameterRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend()
        {
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = await userAuthenticationParameterRepository.GetRejectedEntry();

            if (userAuthenticationParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(userAuthenticationParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await userAuthenticationParameterRepository.Amend(_userAuthenticationParameterViewModel);

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
                    bool result = await userAuthenticationParameterRepository.Delete(_userAuthenticationParameterViewModel);

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

            return View(_userAuthenticationParameterViewModel);
        }

        [HttpGet]
        [Route("AuthorizedRecords")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<UserAuthenticationParameterViewModel> userAuthenticationParameterViewModels = await userAuthenticationParameterRepository.GetUserAuthenticationParameterIndex();

            if (userAuthenticationParameterViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(userAuthenticationParameterViewModels);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify()
        {
            if (await userAuthenticationParameterRepository.IsAnyAuthorizationPending())
            {
                return View("~/Views/Shared/_AuthorizationPending.cshtml");
            }

            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = await userAuthenticationParameterRepository.GetActiveEntry();

            if (userAuthenticationParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userAuthenticationParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await userAuthenticationParameterRepository.Save(_userAuthenticationParameterViewModel);

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

            return View(_userAuthenticationParameterViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify()
        {
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = await userAuthenticationParameterRepository.GetUnVerifiedEntry();

            if (userAuthenticationParameterViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(userAuthenticationParameterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _userAuthenticationParameterViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await userAuthenticationParameterRepository.Verify(_userAuthenticationParameterViewModel);

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
                    _userAuthenticationParameterViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await userAuthenticationParameterRepository.Reject(_userAuthenticationParameterViewModel);

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

            return View(_userAuthenticationParameterViewModel);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry()
        {
            UserAuthenticationParameterViewModel userAuthenticationParameterViewModel = await userAuthenticationParameterRepository.GetActiveEntry();

            if (userAuthenticationParameterViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(userAuthenticationParameterViewModel);
        }
    }
}