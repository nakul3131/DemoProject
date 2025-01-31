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
    [RoutePrefix("Employee/DataEntry/Maintenance/HumanResource/EvaluationSectorContentItem")]
    public class EvaluationSectorContentItemController : Controller
    {
        private readonly IEvaluationSectorContentItemRepository evaluationSectorContentItemRepository;
        private readonly IEvaluationSectionRepository evaluationSectionRepository;

        public EvaluationSectorContentItemController(IEvaluationSectorContentItemRepository _evaluationSectorContentItemRepository, IEvaluationSectionRepository _evaluationSectionRepository)
        {
            evaluationSectorContentItemRepository = _evaluationSectorContentItemRepository;
            evaluationSectionRepository = _evaluationSectionRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid EvaluationSectionId)
        {
            HttpContext.Session["EvaluationSectorContentItem"] = await evaluationSectorContentItemRepository.GetRejectedEntries(evaluationSectionRepository.GetPrmKeyById(EvaluationSectionId));

            EvaluationSectorContentItemViewModel evaluationSectorContentItemViewModel = await evaluationSectorContentItemRepository.GetViewModelForReject(evaluationSectionRepository.GetPrmKeyById(EvaluationSectionId));

            if (evaluationSectorContentItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(evaluationSectorContentItemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(EvaluationSectorContentItemViewModel _evaluationSectorContentItemViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_evaluationSectorContentItemViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await evaluationSectorContentItemRepository.Amend(_evaluationSectorContentItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "EvaluationSectorContentItem");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await evaluationSectorContentItemRepository.Delete(_evaluationSectorContentItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "EvaluationSectorContentItem"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter  Required Valid Information");
            }

            return View(_evaluationSectorContentItemViewModel);
        }

        private void ClearModelStateOfDataTableFields(EvaluationSectorContentItemViewModel _userProfileViewModel)
        {
            ModelState[nameof(_userProfileViewModel.ContentItemId)]?.Errors?.Clear();
            ModelState[nameof(_userProfileViewModel.SequenceNumber)]?.Errors?.Clear();
            ModelState[nameof(_userProfileViewModel.ActivationDate)]?.Errors?.Clear();
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(Guid EvaluationSectionId)
        {
            HttpContext.Session["EvaluationSectorContentItem"] = await evaluationSectorContentItemRepository.GetVerifiedEntries(evaluationSectionRepository.GetPrmKeyById(EvaluationSectionId));

            EvaluationSectorContentItemViewModel evaluationSectorContentItemViewModel = await evaluationSectorContentItemRepository.GetViewModelForCreate(evaluationSectionRepository.GetPrmKeyById(EvaluationSectionId));

            if (evaluationSectorContentItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(evaluationSectorContentItemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(EvaluationSectorContentItemViewModel _evaluationSectorContentItemViewModel)
        {
            ClearModelStateOfDataTableFields(_evaluationSectorContentItemViewModel);

            if (ModelState.IsValid)
            {
                bool result = await evaluationSectorContentItemRepository.Save(_evaluationSectorContentItemViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    return RedirectToAction("Default", "Home");
                }
                else
                {
                    throw new DatabaseException();
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter  Required Valid Information");
            }

            return View(_evaluationSectorContentItemViewModel);
        }

        [HttpPost]
        public ActionResult EvaluationDropdownListForCreate(Guid EvaluationSectionId)
        {
            var EvaluationSectorContentItem = evaluationSectorContentItemRepository.EvaluationDropdownListForCreate(EvaluationSectionId);

            return Json(EvaluationSectorContentItem, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EvaluationDropdownListForEdit(Guid EvaluationSectionId, Guid ContentItemId)
        {
            var EvaluationSectorContentItem = evaluationSectorContentItemRepository.EvaluationDropdownListForEdit(EvaluationSectionId, ContentItemId);

            return Json(EvaluationSectorContentItem, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("CityList")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<EvaluationSectorContentItemViewModel> evaluationSectorContentItemViewModels = await evaluationSectorContentItemRepository.GetIndexWithCreateModifyOperationStatus();

            if (evaluationSectorContentItemViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(evaluationSectorContentItemViewModels);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<EvaluationSectorContentItemViewModel> designationViewModel = await evaluationSectorContentItemRepository.GetIndexOfRejectedEntries();

            if (designationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(designationViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<EvaluationSectorContentItemViewModel> designationViewModel = await evaluationSectorContentItemRepository.GetIndexOfUnVerifiedEntries();

            if (designationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(designationViewModel);
        }

        [HttpPost]
        [Route("SaveEvaluationSectorContentItemDataTables")]
        public ActionResult SaveEvaluationSectorContentItemDataTables(List<EvaluationSectorContentItemViewModel> _EvaluationSectorContentItem)
        {
            HttpContext.Session.Add("EvaluationSectorContentItem", _EvaluationSectorContentItem);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid EvaluationSectionId)
        {

            HttpContext.Session["EvaluationSectorContentItem"] = await evaluationSectorContentItemRepository.GetUnverifiedEntries(evaluationSectionRepository.GetPrmKeyById(EvaluationSectionId));

            EvaluationSectorContentItemViewModel evaluationSectorContentItemViewModel = await evaluationSectorContentItemRepository.GetViewModelForUnverified(evaluationSectionRepository.GetPrmKeyById(EvaluationSectionId));

            if (evaluationSectorContentItemViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(evaluationSectorContentItemViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(EvaluationSectorContentItemViewModel _evaluationSectorContentItemViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _evaluationSectorContentItemViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await evaluationSectorContentItemRepository.Verify(_evaluationSectorContentItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "EvaluationSectorContentItem"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _evaluationSectorContentItemViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await evaluationSectorContentItemRepository.Reject(_evaluationSectorContentItemViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "EvaluationSectorContentItem"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "EvaluationSectorContentItem");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter  Required Valid Information");
            }

            return View(_evaluationSectorContentItemViewModel.EvaluationSectorContentItemId);
        }

    }
}