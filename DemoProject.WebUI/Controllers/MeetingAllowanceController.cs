using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.ViewModel.Management.Conference;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/MeetingAllowance")]
    public class MeetingAllowanceController : Controller
    {
        private readonly IMeetingAllowanceRepository meetingAllowanceRepository; 

        public MeetingAllowanceController(IMeetingAllowanceRepository _meetingAllowanceRepository)
        {
            meetingAllowanceRepository = _meetingAllowanceRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid MeetingAllowanceId)
        {
            MeetingAllowanceViewModel meetingAllowanceViewModel = await meetingAllowanceRepository.GetRejectedEntry(MeetingAllowanceId);

            if (meetingAllowanceViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingAllowanceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(MeetingAllowanceViewModel _meetingAllowanceViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await meetingAllowanceRepository.Amend(_meetingAllowanceViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "MeetingAllowance");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await meetingAllowanceRepository.Delete(_meetingAllowanceViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "MeetingAllowance"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "MeetingAllowance");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_meetingAllowanceViewModel.MeetingAllowanceId);
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
        public async Task<ActionResult> Create(MeetingAllowanceViewModel _meetingAllowanceViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await meetingAllowanceRepository.Save(_meetingAllowanceViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "MeetingAllowance");
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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_meetingAllowanceViewModel);
        }
      
        [HttpGet]
        [Route("Modify")] 
        public async Task<ActionResult> Modify(Guid MeetingAllowanceId)
        {
            MeetingAllowanceViewModel meetingAllowanceViewModel = await meetingAllowanceRepository.GetVerifiedEntry(MeetingAllowanceId);

            if (meetingAllowanceViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingAllowanceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(MeetingAllowanceViewModel _meetingAllowanceViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await meetingAllowanceRepository.Modify(_meetingAllowanceViewModel);

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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_meetingAllowanceViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<MeetingAllowanceViewModel> meetingAllowanceViewModel = await meetingAllowanceRepository.GetIndexOfRejectedEntries();

            if (meetingAllowanceViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingAllowanceViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<MeetingAllowanceViewModel> meetingAllowanceViewModel = await meetingAllowanceRepository.GetIndexOfUnVerifiedEntries();

            if (meetingAllowanceViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingAllowanceViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<MeetingAllowanceViewModel> meetingAllowanceViewModel = await meetingAllowanceRepository.GetIndexOfVerifiedEntries();

            if (meetingAllowanceViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingAllowanceViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid MeetingAllowanceId)
        {
            MeetingAllowanceViewModel meetingAllowanceViewModel = await meetingAllowanceRepository.GetUnVerifiedEntry(MeetingAllowanceId);

            if (meetingAllowanceViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(meetingAllowanceViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(MeetingAllowanceViewModel _meetingAllowanceViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _meetingAllowanceViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await meetingAllowanceRepository.Verify(_meetingAllowanceViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "MeetingAllowance"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await meetingAllowanceRepository.Reject(_meetingAllowanceViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "MeetingAllowance"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "MeetingAllowance");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_meetingAllowanceViewModel.MeetingAllowanceId);
        }
    }
}