using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Enterprise.Schedule;
using DemoProject.Services.ViewModel.Enterprise.Schedule;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Enterprise/OfficeSchedule")]
    public class OfficeScheduleController : Controller
    {
        private readonly IOfficeScheduleRepository OfficeScheduleRepository;

        public OfficeScheduleController(IOfficeScheduleRepository _OfficeScheduleRepository)
        {
            OfficeScheduleRepository = _OfficeScheduleRepository;
        }


        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid OfficeScheduleId)
        {
            OfficeScheduleViewModel OfficeScheduleViewModel = await OfficeScheduleRepository.GetRejectedEntry(OfficeScheduleId);

            if (OfficeScheduleViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(OfficeScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(OfficeScheduleViewModel _officeScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await OfficeScheduleRepository.Amend(_officeScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "OfficeSchedule");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await OfficeScheduleRepository.Delete(_officeScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new  {
                            result = true,  redirectTo = Url.Action("RejectedIndex", "OfficeSchedule"), }, JsonRequestBehavior.AllowGet);
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

            return View(_officeScheduleViewModel.OfficeScheduleId);
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
        public async Task<ActionResult> Create(OfficeScheduleViewModel _OfficeScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                bool result = await OfficeScheduleRepository.Save(_OfficeScheduleViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "OfficeSchedule");
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

            return View(_OfficeScheduleViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueOfficeScheduleName(string NameOfSchedule)
        {
            bool data = OfficeScheduleRepository.GetUniqueOfficeScheduleName(NameOfSchedule);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid OfficeScheduleId)
        {
            OfficeScheduleViewModel OfficeScheduleViewModel = await OfficeScheduleRepository.GetVerifiedEntry(OfficeScheduleId);

            if (OfficeScheduleViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(OfficeScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(OfficeScheduleViewModel _OfficeScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                bool result = await OfficeScheduleRepository.Modify(_OfficeScheduleViewModel);

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

            return View(_OfficeScheduleViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<OfficeScheduleViewModel> OfficeScheduleViewModel = await OfficeScheduleRepository.GetIndexOfRejectedEntries();

            if (OfficeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(OfficeScheduleViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<OfficeScheduleViewModel> OfficeScheduleViewModel = await OfficeScheduleRepository.GetIndexOfUnVerifiedEntries();

            if (OfficeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(OfficeScheduleViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<OfficeScheduleViewModel> OfficeScheduleViewModel = await OfficeScheduleRepository.GetIndexOfVerifiedEntries();

            if (OfficeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(OfficeScheduleViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid OfficeScheduleId)
        {
            OfficeScheduleViewModel OfficeScheduleViewModel = await OfficeScheduleRepository.GetUnVerifiedEntry(OfficeScheduleId);

            if (OfficeScheduleViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(OfficeScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(OfficeScheduleViewModel _OfficeScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _OfficeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await OfficeScheduleRepository.Verify(_OfficeScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new {  result = true, redirectTo = Url.Action("UnverifiedIndex", "OfficeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _OfficeScheduleViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await OfficeScheduleRepository.Reject(_OfficeScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new {  result = true, redirectTo = Url.Action("UnverifiedIndex", "OfficeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "OfficeSchedule");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_OfficeScheduleViewModel.OfficeScheduleId);

        }

    }
}