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
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/CenterOccupation")]
    public class CenterOccupationController : Controller
    {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ICenterOccupationRepository centerOccupationRepository;

        public CenterOccupationController(IPersonDetailRepository _personDetailRepository, ICenterOccupationRepository _centerOccupationRepository)
        {
            personDetailRepository = _personDetailRepository;
            centerOccupationRepository = _centerOccupationRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            CenterOccupationViewModel centerOccupationViewModel = await centerOccupationRepository.GetViewModelForReject(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerOccupationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerOccupationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(CenterOccupationViewModel _centerOccupationViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await centerOccupationRepository.Amend(_centerOccupationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "CenterOccupation");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await centerOccupationRepository.Delete(_centerOccupationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "CenterOccupation") }, JsonRequestBehavior.AllowGet);
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

            return View(_centerOccupationViewModel.CenterOccupationId);
        }
        
        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(Guid CenterId)
        {
            CenterOccupationViewModel centerOccupationViewModel = await centerOccupationRepository.GetViewModelForCreate(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerOccupationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerOccupationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(CenterOccupationViewModel _centerOccupationViewModel)
        {
            if (ModelState.IsValid)
            {
                _centerOccupationViewModel.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_centerOccupationViewModel.CenterId);

                bool result = await centerOccupationRepository.Save(_centerOccupationViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "CenterOccupation");
                    }

                    return RedirectToAction("Index", "CenterOccupation");
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

            return View(_centerOccupationViewModel);
        }

        [HttpGet]
        [Route("CityList")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<CenterOccupationViewModel> centerOccupationViewModels = await centerOccupationRepository.GetIndexWithCreateModifyOperationStatus();

            if (centerOccupationViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(centerOccupationViewModels);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            CenterOccupationViewModel centerOccupationViewModel = await centerOccupationRepository.GetViewModelForVerified (personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerOccupationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerOccupationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(CenterOccupationViewModel _centerOccupationViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await centerOccupationRepository.Modify(_centerOccupationViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("Index", "CenterOccupation");
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

            return View(_centerOccupationViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CenterOccupationViewModel> centerOccupationViewModels = await centerOccupationRepository.GetIndexOfRejectedEntries();

            if (centerOccupationViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(centerOccupationViewModels);
        }

        [HttpGet]
        [Route("ListOfUnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CenterOccupationViewModel> centerOccupationViewModels = await centerOccupationRepository.GetIndexOfUnverifiedEntries();

            if (centerOccupationViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(centerOccupationViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            CenterOccupationViewModel centerOccupationViewModel = await centerOccupationRepository.GetViewModelForUnverified(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerOccupationViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerOccupationViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(CenterOccupationViewModel _centerOccupationViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _centerOccupationViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await centerOccupationRepository.Verify(_centerOccupationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "CenterOccupation"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _centerOccupationViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await centerOccupationRepository.Reject(_centerOccupationViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "CenterOccupation"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "CenterOccupation");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_centerOccupationViewModel.CenterOccupationId);
        }

    }
}