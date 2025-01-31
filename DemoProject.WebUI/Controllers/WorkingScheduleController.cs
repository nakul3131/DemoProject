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
    [RoutePrefix("Employee/DataEntry/Maintenance/Enterprise/WorkingSchedule")]
    public class WorkingScheduleController : Controller
    {
        private readonly IWorkingScheduleRepository WorkingScheduleRepository;

        public WorkingScheduleController(IWorkingScheduleRepository _workingScheduleRepository)
        {
            WorkingScheduleRepository = _workingScheduleRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid WorkingScheduleId)
        {
            WorkingScheduleViewModel WorkingScheduleViewModel = await WorkingScheduleRepository.GetRejectedEntry(WorkingScheduleId);

            if (WorkingScheduleViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(WorkingScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(WorkingScheduleViewModel _workingScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await WorkingScheduleRepository.Amend(_workingScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "WorkingSchedule");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await WorkingScheduleRepository.Delete(_workingScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new
                        {
                            result = true,
                            redirectTo = Url.Action("RejectedIndex", "WorkingSchedule"),
                        }, JsonRequestBehavior.AllowGet);
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

            return View(_workingScheduleViewModel.WorkingScheduleId);
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
        public async Task<ActionResult> Create(WorkingScheduleViewModel _workingScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                bool result = await WorkingScheduleRepository.Save(_workingScheduleViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "VehicleModelVariant");
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

            return View(_workingScheduleViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueWorkingScheduleName(string NameOfSchedule)
        {
            bool data = WorkingScheduleRepository.GetUniqueWorkingScheduleName(NameOfSchedule);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid WorkingScheduleId)
        {
            WorkingScheduleViewModel WorkingScheduleViewModel = await WorkingScheduleRepository.GetVerifiedEntry(WorkingScheduleId);

            if (WorkingScheduleViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(WorkingScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(WorkingScheduleViewModel _workingScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                bool result = await WorkingScheduleRepository.Modify(_workingScheduleViewModel);

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

            return View(_workingScheduleViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<WorkingScheduleViewModel> WorkingScheduleViewModel = await WorkingScheduleRepository.GetIndexOfRejectedEntries();

            if (WorkingScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(WorkingScheduleViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<WorkingScheduleViewModel> WorkingScheduleViewModel = await WorkingScheduleRepository.GetIndexOfUnVerifiedEntries();

            if (WorkingScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(WorkingScheduleViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<WorkingScheduleViewModel> WorkingScheduleViewModel = await WorkingScheduleRepository.GetIndexOfVerifiedEntries();

            if (WorkingScheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(WorkingScheduleViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid WorkingScheduleId)
        {
            WorkingScheduleViewModel WorkingScheduleViewModel = await WorkingScheduleRepository.GetUnVerifiedEntry(WorkingScheduleId);

            if (WorkingScheduleViewModel == null)
            {
                throw new DatabaseException();
            }

            return View(WorkingScheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(WorkingScheduleViewModel _workingScheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _workingScheduleViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await WorkingScheduleRepository.Verify(_workingScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "WorkingSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _workingScheduleViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await WorkingScheduleRepository.Reject(_workingScheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "WorkingSchedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "WorkingSchedule");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_workingScheduleViewModel.WorkingScheduleId);
        }

    }
}