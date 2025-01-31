using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Management.Master;
using DemoProject.Services.ViewModel.Management.Master;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Master/Schedule")]
    public class ScheduleController : Controller
    {
        private readonly IScheduleRepository scheduleRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IScheduleFrequencyRepository scheduleFrequencyRepository;
        
        public ScheduleController(IScheduleRepository _scheduleRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IScheduleFrequencyRepository _scheduleFrequencyRepository)
        {
            scheduleRepository = _scheduleRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            scheduleFrequencyRepository = _scheduleFrequencyRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid ScheduleId)
        {
            HttpContext.Session["ScheduleFrequency"] = await scheduleFrequencyRepository.GetRejectedEntries(enterpriseDetailRepository.GetSchedulePrmKeyById(ScheduleId));

            ScheduleViewModel scheduleViewModel = await scheduleRepository.GetRejectedEntry(ScheduleId);

            if (scheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(scheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(ScheduleViewModel _scheduleViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_scheduleViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await scheduleRepository.Amend(_scheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "Schedule");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await scheduleRepository.Delete(_scheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "Schedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_scheduleViewModel.ScheduleId);
        }

        private void ClearModelStateOfDataTableFields(ScheduleViewModel _scheduleViewModel)
        {
            ModelState[nameof(_scheduleViewModel.DaysOfMonthId)]?.Errors?.Clear();
            ModelState[nameof(_scheduleViewModel.DaysOfWeekId)]?.Errors?.Clear();
            ModelState[nameof(_scheduleViewModel.ScheduleTypeId)]?.Errors?.Clear();
            ModelState[nameof(_scheduleViewModel.Recur)]?.Errors?.Clear();
            ModelState[nameof(_scheduleViewModel.ScheduleTime)]?.Errors?.Clear();
            ModelState[nameof(_scheduleViewModel.SpecifiedDate)]?.Errors?.Clear();
            ModelState[nameof(_scheduleViewModel.ActivationDate)]?.Errors?.Clear();
            ModelState[nameof(_scheduleViewModel.ReasonForModification)]?.Errors?.Clear();
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
        public async Task<ActionResult> Create(ScheduleViewModel _scheduleViewModel)
          {
            ClearModelStateOfDataTableFields(_scheduleViewModel);

            if (ModelState.IsValid)
            {
                bool result = await scheduleRepository.Save(_scheduleViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "Schedule");
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

            return View(_scheduleViewModel);
        }

        [HttpPost]
        public ActionResult GetScheduleTypeList(Guid scheduleTypeId)
        {
            ScheduleViewModel scheduleViewModel = new ScheduleViewModel();
            var scheduleTypeKey = scheduleRepository.GetlistofScheduleType(scheduleTypeId);
            return Json(scheduleTypeKey, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetUniqueScheduleName(string nameOfSchedule)
        {
            bool data = scheduleRepository.GetUniqueScheduleName(nameOfSchedule);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid ScheduleId)
        {
            HttpContext.Session["ScheduleFrequency"] = await scheduleFrequencyRepository.GetVerifiedEntries(enterpriseDetailRepository.GetSchedulePrmKeyById(ScheduleId));

            ScheduleViewModel scheduleViewModel = await scheduleRepository.GetVerifiedEntry(ScheduleId);

            if (scheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(scheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(ScheduleViewModel _scheduleViewModel)
        {
            ClearModelStateOfDataTableFields(_scheduleViewModel);

            if (ModelState.IsValid)
            {
                bool result = await scheduleRepository.Modify(_scheduleViewModel);

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

            return View(_scheduleViewModel);
        }

        [HttpPost]
        [Route("SaveDataTable")]
        public ActionResult SaveDataTables(List<ScheduleFrequencyViewModel> _ScheduleFrequency)
        {
            HttpContext.Session.Add("ScheduleFrequency", _ScheduleFrequency);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<ScheduleViewModel> scheduleViewModels = await scheduleRepository.GetIndexOfRejectedEntries();

            if (scheduleViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(scheduleViewModels);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<ScheduleViewModel> scheduleViewModel = await scheduleRepository.GetIndexOfUnVerifiedEntries();

            if (scheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(scheduleViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<ScheduleViewModel> scheduleViewModel = await scheduleRepository.GetIndexOfVerifiedEntries();

            if (scheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(scheduleViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid ScheduleId)
        {
            ScheduleViewModel scheduleViewModel = await scheduleRepository.GetUnVerifiedEntry(ScheduleId);
            
            HttpContext.Session["ScheduleFrequency"] = await scheduleFrequencyRepository.GetUnverifiedEntries(enterpriseDetailRepository.GetSchedulePrmKeyById(ScheduleId));
            
            if (scheduleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(scheduleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(ScheduleViewModel _scheduleViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _scheduleViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await scheduleRepository.Verify(_scheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Schedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _scheduleViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await scheduleRepository.Reject(_scheduleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Schedule"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "Schedule");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_scheduleViewModel.ScheduleId);
        }
    }
}