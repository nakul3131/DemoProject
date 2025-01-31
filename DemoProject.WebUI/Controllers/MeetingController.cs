using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.ViewModel.Management.Conference;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Management.Master;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/Meeting")]
    public class MeetingController : Controller
    {
        private readonly IMeetingRepository meetingRepository;
        private readonly IMeetingAgendaRepository meetingAgendaRepository;
        private readonly IMeetingInviteeBoardOfDirectorRepository meetingInviteeBoardOfDirectorRepository;
        private readonly IMeetingInviteeMemberRepository meetingInviteeMemberRepository;
        private readonly IMeetingNoticeRepository meetingNoticeRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IAgendaRepository agendaRepository;

        public MeetingController(IMeetingRepository _meetingRepository, IMeetingAgendaRepository _meetingAgendaRepository, IMeetingInviteeBoardOfDirectorRepository _meetingInviteeBoardOfDirectorRepository, IMeetingInviteeMemberRepository _meetingInviteeMemberRepository, IMeetingNoticeRepository _meetingNoticeRepository, IAccountDetailRepository _accountDetailRepository, IAgendaRepository _agendaRepository)
        {
            meetingRepository = _meetingRepository;
            meetingAgendaRepository = _meetingAgendaRepository;
            meetingInviteeBoardOfDirectorRepository = _meetingInviteeBoardOfDirectorRepository;
            meetingInviteeMemberRepository = _meetingInviteeMemberRepository;
            meetingNoticeRepository = _meetingNoticeRepository;
            accountDetailRepository = _accountDetailRepository;
            agendaRepository = _agendaRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid MeetingId)
        {
            HttpContext.Session["MeetingAgenda"] = await meetingAgendaRepository.GetRejectedEntries(meetingRepository.GetPrmKeyById(MeetingId));
            HttpContext.Session["MeetingInviteeBoardOfDirector"] = await meetingInviteeBoardOfDirectorRepository.GetRejectedEntries(meetingRepository.GetPrmKeyById(MeetingId));
            HttpContext.Session["MeetingInviteeMember"] = await meetingInviteeMemberRepository.GetRejectedEntries(meetingRepository.GetPrmKeyById(MeetingId));
            HttpContext.Session["MeetingNotice"] = await meetingNoticeRepository.GetRejectedEntries(meetingRepository.GetPrmKeyById(MeetingId));

            MeetingViewModel meetingViewModel = await meetingRepository.GetRejectedEntry(MeetingId);

            if (meetingViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(MeetingViewModel _meetingViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_meetingViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await meetingRepository.Amend(_meetingViewModel);

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
                    bool result = await meetingRepository.Delete(_meetingViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "Meeting"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "Meeting");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_meetingViewModel);
        }

        private void ClearModelStateOfDataTableFields(MeetingViewModel _meetingViewModel)
        {
            ModelState[nameof(_meetingViewModel.MeetingTypeId)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.AgendaId)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.BoardOfDirectorId)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.CustomerSharesCapitalAccountId)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.ScheduleId)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.MenuId)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.NoticeMediaId)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.NoticeReferenceNumber)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.NoticeStatus)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.AttendanceStatus)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.SequenceNumber)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.SuggestiveMemberNumber)]?.Errors?.Clear();
            ModelState[nameof(_meetingViewModel.PermissiveMemberNumber)]?.Errors?.Clear();
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
        public async Task<ActionResult> Create(MeetingViewModel _meetingViewModel)
        {
            ClearModelStateOfDataTableFields(_meetingViewModel);

            if (ModelState.IsValid)
            {
                bool result = await meetingRepository.Save(_meetingViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "Meeting");
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

            return View(_meetingViewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult GetSharesApplicationPending()
        {
            bool data = accountDetailRepository.IsAnySharesApplicationPending();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public  ActionResult GetAgendaList()
        {
            var agendaList = agendaRepository.AgendaDropdownList;
            return Json(agendaList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid MeetingId)
        {
            HttpContext.Session["MeetingAgenda"] = await meetingAgendaRepository.GetVerifiedEntries(meetingRepository.GetPrmKeyById(MeetingId));
            HttpContext.Session["MeetingInviteeBoardOfDirector"] = await meetingInviteeBoardOfDirectorRepository.GetVerifiedEntries(meetingRepository.GetPrmKeyById(MeetingId));
            HttpContext.Session["MeetingInviteeMember"] = await meetingInviteeMemberRepository.GetVerifiedEntries(meetingRepository.GetPrmKeyById(MeetingId));
            HttpContext.Session["MeetingNotice"] = await meetingNoticeRepository.GetVerifiedEntries(meetingRepository.GetPrmKeyById(MeetingId));

            MeetingViewModel meetingViewModel = await meetingRepository.GetVerifiedEntry(MeetingId);

            if (meetingViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(MeetingViewModel _meetingViewModel)
        {
            ClearModelStateOfDataTableFields(_meetingViewModel);

            if (ModelState.IsValid)
            {
                bool result = await meetingRepository.Modify(_meetingViewModel);

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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record,Please Enter Required Valid Information");
            }

            return View(_meetingViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<MeetingViewModel> meetingViewModel = await meetingRepository.GetIndexOfRejectedEntries();

            if (meetingViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<MeetingAgendaViewModel> _meetingAgenda, List<MeetingInviteeBoardOfDirectorViewModel> _meetingInviteeBoardOfDirector, List<MeetingInviteeMemberViewModel> _meetingInviteeMember, List<MeetingNoticeViewModel> _meetingNotice)
        {
            HttpContext.Session.Add("MeetingAgenda", _meetingAgenda);
            HttpContext.Session.Add("MeetingInviteeBoardOfDirector", _meetingInviteeBoardOfDirector);
            HttpContext.Session.Add("MeetingInviteeMember", _meetingInviteeMember);
            HttpContext.Session.Add("MeetingNotice", _meetingNotice);

            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<MeetingViewModel> meetingViewModel = await meetingRepository.GetIndexOfUnVerifiedEntries();

            if (meetingViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<MeetingViewModel> meetingViewModel = await meetingRepository.GetIndexOfVerifiedEntries();

            if (meetingViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid MeetingId)
        {
            HttpContext.Session["MeetingAgenda"] = await meetingAgendaRepository.GetUnverifiedEntries(meetingRepository.GetPrmKeyById(MeetingId));
            HttpContext.Session["MeetingInviteeBoardOfDirector"] = await meetingInviteeBoardOfDirectorRepository.GetUnverifiedEntries(meetingRepository.GetPrmKeyById(MeetingId));
            HttpContext.Session["MeetingInviteeMember"] = await meetingInviteeMemberRepository.GetUnverifiedEntries(meetingRepository.GetPrmKeyById(MeetingId));
            HttpContext.Session["MeetingNotice"] = await meetingNoticeRepository.GetUnverifiedEntries(meetingRepository.GetPrmKeyById(MeetingId));

            MeetingViewModel meetingViewModel = await meetingRepository.GetUnVerifiedEntry(MeetingId);

            if (meetingViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(MeetingViewModel _meetingViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _meetingViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await meetingRepository.Verify(_meetingViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Meeting"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _meetingViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await meetingRepository.Reject(_meetingViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Meeting"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "Meeting");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_meetingViewModel.MeetingId);
        }
    }
}