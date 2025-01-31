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
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/State")]
    public class StateController : Controller
    {
        private readonly IStateRepository stateRepository;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;

        public StateController(IStateRepository _stateRepository, ICenterISOCodeRepository _centerISOCodeRepository)
        {
            stateRepository = _stateRepository;
            centerISOCodeRepository = _centerISOCodeRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            StateViewModel stateViewModel = await stateRepository.GetRejectedEntry(CenterId);

            stateViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetRejectedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (stateViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(stateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(StateViewModel _stateViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await stateRepository.Amend(_stateViewModel);

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
                    bool result = await stateRepository.Delete(_stateViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "State"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("RejectedIndex", "State");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_stateViewModel.CenterId);
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
        public async Task<ActionResult> Create(StateViewModel _stateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await stateRepository.Save(_stateViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "State");
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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_stateViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey)
        {
            bool data = stateRepository.GetUniqueCenterName(NameOfCenter, CenterCategoryPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            StateViewModel stateViewModel = await stateRepository.GetVerifiedEntry(CenterId);

            stateViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetVerifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (stateViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(stateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(StateViewModel _stateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await stateRepository.Modify(_stateViewModel);

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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_stateViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await stateRepository.GetIndexOfRejectedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await stateRepository.GetIndexOfUnVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await stateRepository.GetIndexOfVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            StateViewModel stateViewModel = await stateRepository.GetUnVerifiedEntry(CenterId);

            stateViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetUnverifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (stateViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(stateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(StateViewModel _stateViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _stateViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await stateRepository.Verify(_stateViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "State"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _stateViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await stateRepository.Reject(_stateViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "State"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "State");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_stateViewModel.CenterId);
        }
    }
}