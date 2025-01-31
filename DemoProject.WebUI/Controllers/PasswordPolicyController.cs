using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Security.Master;
using DemoProject.Services.ViewModel.Security.Password;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Security/PasswordPolicy")]
    public class PasswordPolicyController : Controller
    {
        private readonly IPasswordPolicyRepository passwordPolicyRepository;

        public PasswordPolicyController(IPasswordPolicyRepository _passwordPolicyRepository)
        {
            passwordPolicyRepository = _passwordPolicyRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid PasswordPolicyId)
        {
            PasswordPolicyViewModel passwordPolicyViewModel = await passwordPolicyRepository.GetRejectedEntry(PasswordPolicyId);

            if (passwordPolicyViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(passwordPolicyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(PasswordPolicyViewModel _passwordPolicyViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await passwordPolicyRepository.Amend(_passwordPolicyViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "PasswordPolicy");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await passwordPolicyRepository.Delete(_passwordPolicyViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PasswordPolicy"), }, JsonRequestBehavior.AllowGet);
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

            return View(_passwordPolicyViewModel.PasswordPolicyId);
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(PasswordPolicyViewModel _passwordPolicyViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await passwordPolicyRepository.Save(_passwordPolicyViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "PasswordPolicy");
                    }

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

            return View(_passwordPolicyViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PasswordPolicyViewModel> passwordPolicyViewModel = await passwordPolicyRepository.GetIndexOfRejectedEntries();

            if (passwordPolicyViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(passwordPolicyViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PasswordPolicyViewModel> passwordPolicyViewModel = await passwordPolicyRepository.GetIndexOfUnVerifiedEntries();

            if (passwordPolicyViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(passwordPolicyViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<PasswordPolicyViewModel> passwordPolicyViewModel = await passwordPolicyRepository.GetIndexOfVerifiedEntries();

            if (passwordPolicyViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(passwordPolicyViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid PasswordPolicyId)
        {
            PasswordPolicyViewModel passwordPolicyViewModel = await passwordPolicyRepository.GetUnVerifiedEntry(PasswordPolicyId);

            if (passwordPolicyViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(passwordPolicyViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PasswordPolicyViewModel _passwordPolicyViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _passwordPolicyViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await passwordPolicyRepository.Verify(_passwordPolicyViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PasswordPolicy"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _passwordPolicyViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await passwordPolicyRepository.Reject(_passwordPolicyViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PasswordPolicy"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "PasswordPolicy");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_passwordPolicyViewModel.PasswordPolicyId);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry(Guid PasswordPolicyId)
        {
            PasswordPolicyViewModel passwordPolicyViewModel = await passwordPolicyRepository.GetVerifiedEntry(PasswordPolicyId);

            if (passwordPolicyViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(passwordPolicyViewModel);
        }
    }
}