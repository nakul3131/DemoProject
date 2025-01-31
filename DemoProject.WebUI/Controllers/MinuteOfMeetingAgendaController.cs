using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.ViewModel.Management.Conference;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/MinuteOfMeetingAgenda")]
    public class MinuteOfMeetingAgendaController : Controller
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IMinuteOfMeetingAgendaRepository minuteOfMeetingAgendaRepository;
        private readonly IMinuteOfMeetingAgendaSpokespersonRepository minuteOfMeetingSpokespersonRepository;

        public MinuteOfMeetingAgendaController(IAccountDetailRepository _accountDetailRepository, IMinuteOfMeetingAgendaRepository _minuteOfMeetingAgendaRepository, IMinuteOfMeetingAgendaSpokespersonRepository _minuteOfMeetingSpokespersonRepository)
        {
            accountDetailRepository = _accountDetailRepository;
            minuteOfMeetingAgendaRepository = _minuteOfMeetingAgendaRepository;
            minuteOfMeetingSpokespersonRepository = _minuteOfMeetingSpokespersonRepository; 
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid MeetingAgendaId) 
        {
            HttpContext.Session["MinuteOfMeetingAgendaSpokesperson"] = await minuteOfMeetingSpokespersonRepository.GetRejectedEntries(accountDetailRepository.GetMinuteOfMeetingAgendaPrmKeyById(MeetingAgendaId));

            MinuteOfMeetingAgendaViewModel minuteOfMeetingAgendaViewModel = await minuteOfMeetingAgendaRepository.GetRejectedEntry(MeetingAgendaId);

            if (minuteOfMeetingAgendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(minuteOfMeetingAgendaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_minuteOfMeetingAgendaViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await minuteOfMeetingAgendaRepository.Amend(_minuteOfMeetingAgendaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await minuteOfMeetingAgendaRepository.Delete(_minuteOfMeetingAgendaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "MinuteOfMeetingAgenda"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "MinuteOfMeetingAgenda");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_minuteOfMeetingAgendaViewModel);
        }

        private void ClearModelStateOfDataTableFields(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel) 
        {
            ModelState[nameof(_minuteOfMeetingAgendaViewModel.BoardOfDirectorId)]?.Errors?.Clear();
            ModelState[nameof(_minuteOfMeetingAgendaViewModel.SpeakingEndTime)]?.Errors?.Clear();
            ModelState[nameof(_minuteOfMeetingAgendaViewModel.SpeakingStartTime)]?.Errors?.Clear();
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create(Guid MeetingAgendaId)
        {

           MinuteOfMeetingAgendaViewModel minuteOfMeetingAgendaViewModel = new MinuteOfMeetingAgendaViewModel();

           var minuteOfMeetingAgendaViewModelList =  accountDetailRepository.GetMinuteOfMeetingAgendaPrmKeyById(MeetingAgendaId);
            
            minuteOfMeetingAgendaViewModel.MeetingAgendaPrmKey = minuteOfMeetingAgendaViewModelList;

            if (minuteOfMeetingAgendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(minuteOfMeetingAgendaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel)
        {
            ClearModelStateOfDataTableFields(_minuteOfMeetingAgendaViewModel);  

            if (ModelState.IsValid)
            {
                bool result = await minuteOfMeetingAgendaRepository.Save(_minuteOfMeetingAgendaViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "MinuteOfMeetingAgenda");
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

            return View(_minuteOfMeetingAgendaViewModel);
        }

        [HttpGet]
        [Route("ListOfCreateIndex")]
        public async Task<ActionResult> CreateIndex()
        {
            IEnumerable<MinuteOfMeetingAgendaViewModel> minuteOfMeetingAgendaViewModel = await minuteOfMeetingAgendaRepository.GetIndexOfCreate();

            if (minuteOfMeetingAgendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(minuteOfMeetingAgendaViewModel);
        }

        [HttpGet]
        [Route("ListOfCreateModifyIndex")]
        public async Task<ActionResult> CreateModifyIndex()
        {
            IEnumerable<MinuteOfMeetingAgendaViewModel> minuteOfMeetingAgendaViewModel = await minuteOfMeetingAgendaRepository.GetIndexOfUnVerifiedEntries();

            if (minuteOfMeetingAgendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(minuteOfMeetingAgendaViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid MeetingAgendaId) 
        {
            HttpContext.Session["MinuteOfMeetingAgendaSpokesperson"] = await minuteOfMeetingSpokespersonRepository.GetVerifiedEntries(accountDetailRepository.GetMinuteOfMeetingAgendaPrmKeyById(MeetingAgendaId));

            MinuteOfMeetingAgendaViewModel minuteOfMeetingAgendaViewModel = await minuteOfMeetingAgendaRepository.GetVerifiedEntry(MeetingAgendaId);

            if (minuteOfMeetingAgendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(minuteOfMeetingAgendaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel)
        {
            ClearModelStateOfDataTableFields(_minuteOfMeetingAgendaViewModel); 

            if (ModelState.IsValid)
            {
                bool result = await minuteOfMeetingAgendaRepository.Modify(_minuteOfMeetingAgendaViewModel);

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

            return View(_minuteOfMeetingAgendaViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<MinuteOfMeetingAgendaViewModel> minuteOfMeetingAgendaViewModel = await minuteOfMeetingAgendaRepository.GetIndexOfRejectedEntries();

            if (minuteOfMeetingAgendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(minuteOfMeetingAgendaViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<MinuteOfMeetingAgendaSpokespersonViewModel> _minuteOfMeetingAgendaSpokesperson)
        {
            HttpContext.Session.Add("MinuteOfMeetingAgendaSpokesperson", _minuteOfMeetingAgendaSpokesperson); 

            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<MinuteOfMeetingAgendaViewModel> minuteOfMeetingAgendaViewModel = await minuteOfMeetingAgendaRepository.GetIndexOfUnVerifiedEntries();

            if (minuteOfMeetingAgendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(minuteOfMeetingAgendaViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<MinuteOfMeetingAgendaViewModel> minuteOfMeetingAgendaViewModel = await minuteOfMeetingAgendaRepository.GetIndexOfVerifiedEntries();

            if (minuteOfMeetingAgendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(minuteOfMeetingAgendaViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid MeetingAgendaId)
        {
            HttpContext.Session["MinuteOfMeetingAgendaSpokesperson"] = await minuteOfMeetingSpokespersonRepository.GetUnverifiedEntries(accountDetailRepository.GetMinuteOfMeetingAgendaPrmKeyById(MeetingAgendaId));

            MinuteOfMeetingAgendaViewModel minuteOfMeetingAgendaViewModel = await minuteOfMeetingAgendaRepository.GetUnVerifiedEntry(MeetingAgendaId);

            if (minuteOfMeetingAgendaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(minuteOfMeetingAgendaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(MinuteOfMeetingAgendaViewModel _minuteOfMeetingAgendaViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_minuteOfMeetingAgendaViewModel);

            if (ModelState.IsValid)
            {
                _minuteOfMeetingAgendaViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await minuteOfMeetingAgendaRepository.Verify(_minuteOfMeetingAgendaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "MinuteOfMeetingAgenda"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _minuteOfMeetingAgendaViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await minuteOfMeetingAgendaRepository.Reject(_minuteOfMeetingAgendaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "MinuteOfMeetingAgenda"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "MinuteOfMeeting");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_minuteOfMeetingAgendaViewModel.MeetingAgendaId);
        }
    }
}