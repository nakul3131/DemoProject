using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Master;
using DemoProject.Services.Abstract.Master.General.Notice;
using DemoProject.Services.ViewModel.Master.General.Notice;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/NoticeSchedule")]
    public class NoticeScheduleController : Controller
    {
        private readonly INoticeScheduleRepository noticeScheduleRepository;
        private readonly IWeekMonthDayScheduleRepository weekMonthDayScheduleRepository;

        public NoticeScheduleController(INoticeScheduleRepository _noticeScheduleRepository, IWeekMonthDayScheduleRepository _weekMonthDayScheduleRepository)
        {
            noticeScheduleRepository = _noticeScheduleRepository;
            weekMonthDayScheduleRepository = _weekMonthDayScheduleRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid NoticeScheduleId)
        {
            short NoticeSchedulePrmKey = noticeScheduleRepository.GetPrmKeyById(NoticeScheduleId);

            HttpContext.Session["WeekSchedule"] = await weekMonthDayScheduleRepository.GetWeekRejectedEntries(NoticeSchedulePrmKey);
            HttpContext.Session["MonthSchedule"] = await weekMonthDayScheduleRepository.GetMonthRejectedEntries(NoticeSchedulePrmKey);
            HttpContext.Session["DaySchedule"] = await weekMonthDayScheduleRepository.GetDayRejectedEntries(NoticeSchedulePrmKey);

            NoticeScheduleViewModel noticeScheduleViewModel = await noticeScheduleRepository.GetRejectedEntry(NoticeScheduleId);

            if (noticeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(noticeScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(NoticeScheduleViewModel _noticeScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await noticeScheduleRepository.Amend(_noticeScheduleViewModel);

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
                    bool result = await noticeScheduleRepository.Delete(_noticeScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("RejectedIndex", "NoticeSchedule");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_noticeScheduleViewModel.NoticeScheduleId);
        }

        [HttpGet]
        [Route("EditModification")]
        public async Task<ActionResult> AmendModification(Guid NoticeScheduleModificationId)
        {
            short NoticeSchedulePrmKey = noticeScheduleRepository.GetPrmKeyById(NoticeScheduleModificationId);

            HttpContext.Session["WeekSchedule"] = await weekMonthDayScheduleRepository.GetWeekRejectedEntries(NoticeSchedulePrmKey);
            HttpContext.Session["MonthSchedule"] = await weekMonthDayScheduleRepository.GetMonthRejectedEntries(NoticeSchedulePrmKey);
            HttpContext.Session["DaySchedule"] = await weekMonthDayScheduleRepository.GetDayRejectedEntries(NoticeSchedulePrmKey);

            NoticeScheduleViewModel noticeScheduleViewModel = await noticeScheduleRepository.GetRejectedEntry(NoticeScheduleModificationId);

            if (noticeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(noticeScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("EditModification")]
        public async Task<ActionResult> AmendModification(NoticeScheduleViewModel _noticeScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await noticeScheduleRepository.AmendModification(_noticeScheduleViewModel);

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
                    bool result = await noticeScheduleRepository.DeleteModification(_noticeScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedModificationIndex", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("RejectedModificationIndex", "NoticeSchedule");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_noticeScheduleViewModel.NoticeScheduleModificationId);
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
        public async Task<ActionResult> Create(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await noticeScheduleRepository.Save(_noticeScheduleViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "NoticeSchedule");
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

            return View(_noticeScheduleViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid NoticeScheduleId)
        {
            short NoticeSchedulePrmKey = noticeScheduleRepository.GetPrmKeyById(NoticeScheduleId);
            HttpContext.Session["WeekSchedule"] = await weekMonthDayScheduleRepository.GetWeekVerifiedEntries(NoticeSchedulePrmKey);
            HttpContext.Session["MonthSchedule"] = await weekMonthDayScheduleRepository.GetMonthVerifiedEntries(NoticeSchedulePrmKey);
            HttpContext.Session["DaySchedule"] = await weekMonthDayScheduleRepository.GetMonthVerifiedEntries(NoticeSchedulePrmKey);

            NoticeScheduleViewModel noticeScheduleViewModel = await noticeScheduleRepository.GetVerifiedEntry(NoticeScheduleId);

            if (noticeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(noticeScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(NoticeScheduleViewModel _noticeScheduleViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await noticeScheduleRepository.SaveModification(_noticeScheduleViewModel);

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

            return View(_noticeScheduleViewModel);
        }

        [HttpPost]
        [Route("SaveContact")]
        public ActionResult SaveNoticeScheduleDataTable(List<WeekScheduleViewModel> _weekSchedule, List<MonthScheduleViewModel> _monthSchedule, List<DayScheduleViewModel> _daySchedule)
        {
            HttpContext.Session.Add("WeekSchedule", _weekSchedule);
            HttpContext.Session.Add("MonthSchedule", _monthSchedule);
            HttpContext.Session.Add("DaySchedule", _daySchedule);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<NoticeScheduleViewModel> noticeScheduleViewModel = await noticeScheduleRepository.GetIndexOfRejectedEntries();

            if (noticeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(noticeScheduleViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<NoticeScheduleViewModel> noticeScheduleViewModel = await noticeScheduleRepository.GetIndexOfUnVerifiedEntries();

            if (noticeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(noticeScheduleViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<NoticeScheduleViewModel> noticeScheduleViewModel = await noticeScheduleRepository.GetIndexOfVerifiedEntries();

            if (noticeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(noticeScheduleViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid NoticeScheduleId)
        {

            short NoticeSchedulePrmKey = noticeScheduleRepository.GetPrmKeyById(NoticeScheduleId);
            HttpContext.Session["WeekSchedule"] = await weekMonthDayScheduleRepository.GetWeekUnverifiedEntries(NoticeSchedulePrmKey);
            HttpContext.Session["MonthSchedule"] = await weekMonthDayScheduleRepository.GetMonthUnverifiedEntries(NoticeSchedulePrmKey);
            HttpContext.Session["DaySchedule"] = await weekMonthDayScheduleRepository.GetDayUnverifiedEntries(NoticeSchedulePrmKey);

            NoticeScheduleViewModel noticeScheduleViewModel = await noticeScheduleRepository.GetUnVerifiedEntry(NoticeScheduleId);

            if (noticeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(noticeScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(NoticeScheduleViewModel _noticeScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await noticeScheduleRepository.Verify(_noticeScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _noticeScheduleViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await noticeScheduleRepository.Reject(_noticeScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedIndex", "NoticeSchedule");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_noticeScheduleViewModel.NoticeScheduleId);
        }

        [HttpGet]
        [Route("AuthorizationOfModification")]
        public async Task<ActionResult> VerifyModification(Guid NoticeScheduleModificationId)
        {
            short NoticeSchedulePrmKey = noticeScheduleRepository.GetPrmKeyById(NoticeScheduleModificationId);

            HttpContext.Session["WeekSchedule"] = await weekMonthDayScheduleRepository.GetWeekUnverifiedEntries(NoticeSchedulePrmKey);
            HttpContext.Session["MonthSchedule"] = await weekMonthDayScheduleRepository.GetMonthUnverifiedEntries(NoticeSchedulePrmKey);
            HttpContext.Session["DaySchedule"] = await weekMonthDayScheduleRepository.GetDayUnverifiedEntries(NoticeSchedulePrmKey);

            NoticeScheduleViewModel noticeScheduleViewModel = await noticeScheduleRepository.GetUnVerifiedEntry(NoticeScheduleModificationId);

            if (noticeScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(noticeScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AuthorizationOfModification")]
        public async Task<ActionResult> VerifyModification(NoticeScheduleViewModel _noticeScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _noticeScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await noticeScheduleRepository.VerifyModification(_noticeScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedModificationIndex", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _noticeScheduleViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await noticeScheduleRepository.RejectModification(_noticeScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedModificationIndex", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "NoticeSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedModificationIndex", "NoticeSchedule");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_noticeScheduleViewModel.NoticeScheduleModificationId);
        }
    }
}