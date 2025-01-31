using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.ViewModel.PersonInformation.Master;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/CenterDemographicDetail")]
    public class CenterDemographicDetailController : Controller
    {
        private readonly ICenterDemographicDetailRepository centerDemographicDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public CenterDemographicDetailController(IPersonDetailRepository _personDetailRepository, ICenterDemographicDetailRepository _centerDemographicDetailRepository)
        {
            personDetailRepository = _personDetailRepository;
            centerDemographicDetailRepository = _centerDemographicDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            CenterDemographicDetailViewModel centerDemographicDetailViewModel = await centerDemographicDetailRepository.GetViewModelForReject(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerDemographicDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerDemographicDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(CenterDemographicDetailViewModel _centerDemographicDetailViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await centerDemographicDetailRepository.Amend(_centerDemographicDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "CenterDemographicDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await centerDemographicDetailRepository.Delete(_centerDemographicDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "CenterDemographicDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_centerDemographicDetailViewModel.CenterDemographicDetailId);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(Guid CenterId)
        {

            CenterDemographicDetailViewModel centerDemographicDetailViewModel = await centerDemographicDetailRepository.GetViewModelForCreate(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerDemographicDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerDemographicDetailViewModel); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(CenterDemographicDetailViewModel _centerDemographicDetailViewModel)
        {
            if (ModelState.IsValid)
            {

                bool result = await centerDemographicDetailRepository.Save(_centerDemographicDetailViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "CenterDemographicDetail");
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

            return View(_centerDemographicDetailViewModel);
        }

        [HttpGet]
        [Route("CityList")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<CenterDemographicDetailViewModel> villageTownCityViewModels  = await centerDemographicDetailRepository.GetIndexWithCreateModifyOperationStatus();

            if (villageTownCityViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(villageTownCityViewModels);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            CenterDemographicDetailViewModel centerDemographicDetailViewModel = await centerDemographicDetailRepository.GetViewModelForVerified(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerDemographicDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerDemographicDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(CenterDemographicDetailViewModel _centerDemographicDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await centerDemographicDetailRepository.Modify(_centerDemographicDetailViewModel);

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

            return View(_centerDemographicDetailViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CenterDemographicDetailViewModel> villageTownCityViewModels = await centerDemographicDetailRepository.GetIndexOfRejectedEntries();

            if (villageTownCityViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(villageTownCityViewModels);
        }

        [HttpGet]
        [Route("UnAuthorizedISORecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CenterDemographicDetailViewModel> villageTownCityViewModels = await centerDemographicDetailRepository.GetIndexOfUnVerifiedEntries();

            if (villageTownCityViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(villageTownCityViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            CenterDemographicDetailViewModel centerDemographicDetailViewModel = await centerDemographicDetailRepository.GetViewModelForUnverified(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerDemographicDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerDemographicDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(CenterDemographicDetailViewModel _centerDemographicDetailViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _centerDemographicDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await centerDemographicDetailRepository.Verify(_centerDemographicDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new  { result = true,  redirectTo = Url.Action("UnverifiedIndex", "CenterDemographicDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "CenterDemographicDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _centerDemographicDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await centerDemographicDetailRepository.Reject(_centerDemographicDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new  { result = true, redirectTo = Url.Action("UnverifiedIndex", "CenterDemographicDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "CenterDemographicDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedIndex", "CenterDemographicDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_centerDemographicDetailViewModel.CenterDemographicDetailId);
        }
    }
}