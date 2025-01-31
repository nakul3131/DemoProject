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
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/Tehsil")]
    public class TalukaController : Controller
    {
        private readonly ITalukaRepository talukaRepository;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;

        public TalukaController(ITalukaRepository _talukaRepository, ICenterISOCodeRepository _centerISOCodeRepository)
        {
            talukaRepository = _talukaRepository;
            centerISOCodeRepository = _centerISOCodeRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            TalukaViewModel talukaViewModel = await talukaRepository.GetRejectedEntry(CenterId);

            talukaViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetRejectedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (talukaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(talukaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(TalukaViewModel _talukaViewModel, string Command)
        {
            ClearModelStateOfDataTableFields();

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await talukaRepository.Amend(_talukaViewModel);

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
                    bool result = await talukaRepository.Delete(_talukaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "Taluka"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("RejectedIndex", "Taluka");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_talukaViewModel.CenterId);
        }

        private void ClearModelStateOfDataTableFields()
        {
            //if (_talukaViewModel.CenterCategoryPrmKey != 4)
            //{
            //    ModelState["_talukaViewModel.ParentCenterDistrictId"]?.Errors?.Clear();
            //}


            //if (_talukaViewModel.CenterCategoryPrmKey != 5)
            //{
            //    ModelState["ParentCenterDistrictId"]?.Errors?.Clear();
            //}

            //if (_talukaViewModel.CenterCategoryPrmKey != 5)
            //{
            //    ModelState["ParentCenterSubDivisionOfficeId"]?.Errors?.Clear();
            //}

            //if (_talukaViewModel.CenterCategoryPrmKey != 5)
            //{
            //    ModelState["ParentCenterSubDivisionOfficeId"]?.Errors?.Clear();
            //}

            //ModelState["ParentCenterId"]?.Errors?.Clear();
            //ModelState["ParentCenterTalukaId"]?.Errors?.Clear();
            ModelState["ParentCenterSubDivisionOfficeId"]?.Errors?.Clear();
            //ModelState["ParentCenterDistrictId"]?.Errors?.Clear();
            ModelState["ParentCenterDistrictId"]?.Errors?.Clear();
        }

        [HttpGet]
        [Route("Create")]
        public  ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(TalukaViewModel _talukaViewModel)
        {
            ClearModelStateOfDataTableFields();

            if (ModelState.IsValid)
            {
                bool result = await talukaRepository.Save(_talukaViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "Taluka");
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

            return View(_talukaViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey)
        {
            bool data = talukaRepository.GetUniqueCenterName(NameOfCenter, CenterCategoryPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            TalukaViewModel talukaViewModel = await talukaRepository.GetVerifiedEntry(CenterId);

            talukaViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetVerifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (talukaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(talukaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(TalukaViewModel _talukaViewModel)
        {
            ClearModelStateOfDataTableFields();

            if (ModelState.IsValid)
            {
                bool result = await talukaRepository.Modify(_talukaViewModel);

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

            return View(_talukaViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await talukaRepository.GetIndexOfRejectedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await talukaRepository.GetIndexOfUnVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await talukaRepository.GetIndexOfVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            TalukaViewModel talukaViewModel = await talukaRepository.GetUnVerifiedEntry(CenterId);

            talukaViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetUnverifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (talukaViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(talukaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(TalukaViewModel _talukaViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _talukaViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await talukaRepository.Verify(_talukaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Taluka"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _talukaViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await talukaRepository.Reject(_talukaViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Taluka"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_talukaViewModel.CenterId);
        }
    }
}