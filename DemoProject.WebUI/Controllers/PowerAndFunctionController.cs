using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Enterprise/PowerAndFunction")]
    public class PowerAndFunctionController : Controller
    {
        private readonly IPowerAndFunctionRepository powerAndFunctionRepository;

        public PowerAndFunctionController(IPowerAndFunctionRepository _powerAndFunctionRepository)
        {
            powerAndFunctionRepository = _powerAndFunctionRepository;
        }

        [HttpGet]
        [Route("Amend")]
        public async Task<ActionResult> Amend(Guid PowerAndFunctionId)
        {
            PowerAndFunctionViewModel powerAndFunctionViewModel = await powerAndFunctionRepository.GetRejectedEntry(PowerAndFunctionId);

            if (powerAndFunctionViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(powerAndFunctionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Amend")]
        public async Task<ActionResult> Amend(PowerAndFunctionViewModel _powerAndFunctionViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await powerAndFunctionRepository.Amend(_powerAndFunctionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await powerAndFunctionRepository.Delete(_powerAndFunctionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "PowerAndFunction"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "PowerAndFunction"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("RejectedIndex", "PowerAndFunction");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_powerAndFunctionViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Create(PowerAndFunctionViewModel _powerAndFunctionViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await powerAndFunctionRepository.Save(_powerAndFunctionViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "PowerAndFunction");
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

            return View(_powerAndFunctionViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<PowerAndFunctionViewModel> powerAndFunctionViewModel = await powerAndFunctionRepository.GetIndexOfRejectedEntries();

            if (powerAndFunctionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(powerAndFunctionViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<PowerAndFunctionViewModel> powerAndFunctionViewModel = await powerAndFunctionRepository.GetIndexOfUnVerifiedEntries();

            if (powerAndFunctionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(powerAndFunctionViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid PowerAndFunctionId)
        {
            PowerAndFunctionViewModel powerAndFunctionViewModel = await powerAndFunctionRepository.GetUnverifiedEntry(PowerAndFunctionId);

            if (powerAndFunctionViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(powerAndFunctionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(PowerAndFunctionViewModel _powerAndFunctionViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _powerAndFunctionViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await powerAndFunctionRepository.Verify(_powerAndFunctionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PowerAndFunction"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "AddressParameter"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _powerAndFunctionViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await powerAndFunctionRepository.Reject(_powerAndFunctionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "PowerAndFunction"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "AddressParameter"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("Default", "Home");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_powerAndFunctionViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<PowerAndFunctionViewModel> powerAndFunctionViewModel = await powerAndFunctionRepository.GetIndexOfVerifiedEntries();

            if (powerAndFunctionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(powerAndFunctionViewModel);
        }

    }
}