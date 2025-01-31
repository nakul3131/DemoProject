using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.Account.Master;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Account/FinancialCycleAndPeriod")]
    public class FinancialCycleController : Controller
    {
        private readonly IFinancialCycleRepository financialCycleRepository;
        private readonly IPeriodCodeRepository periodCodeRepository; 

        public FinancialCycleController(IFinancialCycleRepository _financialCycleRepository, IPeriodCodeRepository _periodCodeRepository) 
        {
            financialCycleRepository = _financialCycleRepository;
            periodCodeRepository = _periodCodeRepository;  
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid FinancialCycleId) 
        {
            HttpContext.Session["PeriodCode"] = await periodCodeRepository.GetRejectedEntries(periodCodeRepository.GetPrmKeyByFinancialCycleId(FinancialCycleId));

            FinancialCycleViewModel financialCycleViewModel = await financialCycleRepository.GetRejectedEntry(FinancialCycleId);

            if (financialCycleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(financialCycleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(FinancialCycleViewModel _financialCycleViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_financialCycleViewModel);
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await financialCycleRepository.Amend(_financialCycleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "FinancialCycle");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await financialCycleRepository.Delete(_financialCycleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "FinancialCycle"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("RejectedIndex", "FinancialCycle");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_financialCycleViewModel.FinancialCycleId);
        }

        private void ClearModelStateOfDataTableFields(FinancialCycleViewModel _financialCycleViewModel)
        {

            ModelState[nameof(_financialCycleViewModel.Code)]?.Errors?.Clear();

            ModelState[nameof(_financialCycleViewModel.StartDate)]?.Errors?.Clear();

            ModelState[nameof(_financialCycleViewModel.EndDate)]?.Errors?.Clear();

            ModelState[nameof(_financialCycleViewModel.PeriodCodeStatus)]?.Errors?.Clear();

            ModelState[nameof(_financialCycleViewModel.Note)]?.Errors?.Clear();
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
        public async Task<ActionResult> Create(FinancialCycleViewModel _financialCycleViewModel)
        {

            ClearModelStateOfDataTableFields(_financialCycleViewModel);

            if (ModelState.IsValid)
            {
                bool result = await financialCycleRepository.Save(_financialCycleViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "FinancialCycle");
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

            return View(_financialCycleViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<FinancialCycleViewModel> financialCycleViewModel = await financialCycleRepository.GetIndexOfRejectedEntries();

            if (financialCycleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(financialCycleViewModel);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<PeriodCodeViewModel> _periodCode) 
        {
            HttpContext.Session.Add("PeriodCode", _periodCode); 

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<FinancialCycleViewModel> financialCycleViewModel = await financialCycleRepository.GetIndexOfUnVerifiedEntries();

            if (financialCycleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(financialCycleViewModel);
        }


        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid FinancialCycleId)
        {
            HttpContext.Session["PeriodCode"] = await periodCodeRepository.GetUnverifiedEntries(periodCodeRepository.GetPrmKeyByFinancialCycleId(FinancialCycleId));

            FinancialCycleViewModel financialCycleViewModel = await financialCycleRepository.GetUnVerifiedEntry(FinancialCycleId);

            if (financialCycleViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(financialCycleViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(FinancialCycleViewModel _financialCycleViewModel, string Command)
        {

            //ClearModelStateOfDataTableFields(_financialCycleViewModel);

            if (ModelState.IsValid)
            {
                _financialCycleViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await financialCycleRepository.Verify(_financialCycleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "FinancialCycle"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await financialCycleRepository.Reject(_financialCycleViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "FinancialCycle"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "FinancialCycle");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_financialCycleViewModel.FinancialCycleId);
        }
    }
}