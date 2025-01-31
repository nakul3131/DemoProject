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
    [RoutePrefix("Employee/DataEntry/Maintenance/HumanResource/EvaluationSection")]
    public class EvaluationSectionController : Controller
    {
        private readonly IEvaluationSectionRepository evaluationSectionRepository;

        public EvaluationSectionController(IEvaluationSectionRepository _evaluationSectionRepository)
        {
            evaluationSectionRepository = _evaluationSectionRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid EvaluationSectionId)
        {
            EvaluationSectionViewModel evaluationSectionViewModel = await evaluationSectionRepository.GetRejectedEntry(EvaluationSectionId);

            if (evaluationSectionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(evaluationSectionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(EvaluationSectionViewModel _evaluationSectionViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await evaluationSectionRepository.Amend(_evaluationSectionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "EvaluationSection");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await evaluationSectionRepository.Delete(_evaluationSectionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "EvaluationSection"), }, JsonRequestBehavior.AllowGet);
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

            return View(_evaluationSectionViewModel.EvaluationSectionId);
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
        public async Task<ActionResult> Create(EvaluationSectionViewModel _evaluationSectionViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await evaluationSectionRepository.Save(_evaluationSectionViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "EvaluationSection");
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

            return View(_evaluationSectionViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueEvaluationSectionName(string NameOfEvaluationSection)
        {
            bool data = evaluationSectionRepository.GetUniqueEvaluationSectionName(NameOfEvaluationSection);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid EvaluationSectionId)
        {
            EvaluationSectionViewModel evaluationSectionViewModel = await evaluationSectionRepository.GetVerifiedEntry(EvaluationSectionId);

            if (evaluationSectionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(evaluationSectionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(EvaluationSectionViewModel _evaluationSectionViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await evaluationSectionRepository.Modify(_evaluationSectionViewModel);

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

            return View(_evaluationSectionViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<EvaluationSectionViewModel> evaluationSectionViewModel = await evaluationSectionRepository.GetIndexOfRejectedEntries();

            if (evaluationSectionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(evaluationSectionViewModel);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<EvaluationSectionViewModel> evaluationSectionViewModel = await evaluationSectionRepository.GetIndexOfUnVerifiedEntries();

            if (evaluationSectionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(evaluationSectionViewModel);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<EvaluationSectionViewModel> evaluationSectionViewModel = await evaluationSectionRepository.GetIndexOfVerifiedEntries();

            if (evaluationSectionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(evaluationSectionViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid EvaluationSectionId)
        {
            EvaluationSectionViewModel evaluationSectionViewModel = await evaluationSectionRepository.GetUnVerifiedEntry(EvaluationSectionId);

            if (evaluationSectionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(evaluationSectionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(EvaluationSectionViewModel _evaluationSectionViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _evaluationSectionViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await evaluationSectionRepository.Verify(_evaluationSectionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "EvaluationSection"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    bool result = await evaluationSectionRepository.Reject(_evaluationSectionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "EvaluationSection"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "EvaluationSection");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Provide Correct Or Required Information");
            }

            return View(_evaluationSectionViewModel.EvaluationSectionId);
        }

    }
}