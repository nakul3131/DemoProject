using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/Division")]
    public class DivisionController : Controller
    {
        private readonly IDivisionRepository divisionRepository;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;

        public DivisionController(IDivisionRepository _divisionRepository, ICenterISOCodeRepository _centerISOCodeRepository)
        {
            divisionRepository = _divisionRepository;
            centerISOCodeRepository = _centerISOCodeRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            DivisionViewModel divisionViewModel = await divisionRepository.GetRejectedEntry(CenterId);

            divisionViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetRejectedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (divisionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(divisionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(DivisionViewModel _divisionViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await divisionRepository.Amend(_divisionViewModel);

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
                    bool result = await divisionRepository.Delete(_divisionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "Division"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("RejectedIndex", "Division");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_divisionViewModel.CenterId);
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
        public async Task<ActionResult> Create(DivisionViewModel _divisionViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await divisionRepository.Save(_divisionViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "Division");
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

            return View(_divisionViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey)
        {
            bool data = divisionRepository.GetUniqueCenterName(NameOfCenter, CenterCategoryPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            DivisionViewModel divisionViewModel = await divisionRepository.GetVerifiedEntry(CenterId);

            divisionViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetVerifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (divisionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(divisionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(DivisionViewModel _divisionViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await divisionRepository.Modify(_divisionViewModel);

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

            return View(_divisionViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await divisionRepository.GetIndexOfRejectedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpPost]
        [Route("SaveContact")]
        public ActionResult SaveContact(List<CenterTradingEntityDetailViewModel> _centerTradingEntityDetail)
        {
            HttpContext.Session.Add("CenterTradingEntityDetail", _centerTradingEntityDetail);
            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await divisionRepository.GetIndexOfUnVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await divisionRepository.GetIndexOfVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            DivisionViewModel divisionViewModel = await divisionRepository.GetUnverifiedEntry(CenterId);

            divisionViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetUnverifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (divisionViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(divisionViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(DivisionViewModel _divisionViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _divisionViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await divisionRepository.Verify(_divisionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Division"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _divisionViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await divisionRepository.Reject(_divisionViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Division"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "Division");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_divisionViewModel.CenterId);
        }
    }
}