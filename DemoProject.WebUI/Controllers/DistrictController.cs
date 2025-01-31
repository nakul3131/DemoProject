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
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/District")]
    public class DistrictController : Controller
    {
        private readonly IDistrictRepository districtRepository;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;

        public DistrictController(IDistrictRepository _districtRepository, ICenterISOCodeRepository _centerISOCodeRepository)
        {
            districtRepository = _districtRepository;
            centerISOCodeRepository = _centerISOCodeRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            DistrictViewModel districtViewModel = await districtRepository.GetRejectedEntry(CenterId);

            districtViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetRejectedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (districtViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(districtViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(DistrictViewModel _districtViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await districtRepository.Amend(_districtViewModel);

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
                    bool result = await districtRepository.Delete(_districtViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "District"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("RejectedIndex", "District");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_districtViewModel.CenterId);
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
        public async Task<ActionResult> Create(DistrictViewModel _districtViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await districtRepository.Save(_districtViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "District");
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

            return View(_districtViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey)
        {
            bool data = districtRepository.GetUniqueCenterName(NameOfCenter, CenterCategoryPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            DistrictViewModel districtViewModel = await districtRepository.GetVerifiedEntry(CenterId);

            districtViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetVerifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (districtViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(districtViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(DistrictViewModel _districtViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await districtRepository.Modify(_districtViewModel);

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

            return View(_districtViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await districtRepository.GetIndexOfRejectedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await districtRepository.GetIndexOfUnVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await districtRepository.GetIndexOfVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            DistrictViewModel districtViewModel = await districtRepository.GetUnVerifiedEntry(CenterId);

            districtViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetUnverifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (districtViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(districtViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(DistrictViewModel _districtViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _districtViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await districtRepository.Verify(_districtViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "District"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { result = false, redirectTo = Url.Action("DatabaseErrorPage", "District"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _districtViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await districtRepository.Reject(_districtViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "District"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "District");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }
            return View(_districtViewModel.CenterId);
        }
    }
}