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
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/CenterTradingEntityDetail")]
    public class CenterTradingEntityDetailController : Controller
    {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ICenterTradingEntityDetailsRepository centerTradingDetailsRepository;

        public CenterTradingEntityDetailController(IPersonDetailRepository _personDetailRepository, ICenterTradingEntityDetailsRepository _centerTradingDetailsRepository)
        {
            personDetailRepository = _personDetailRepository;
            centerTradingDetailsRepository = _centerTradingDetailsRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            HttpContext.Session["CenterTradingEntityDetail"] = await centerTradingDetailsRepository.GetRejectedEntries(personDetailRepository.GetCenterPrmKeyById(CenterId));

            CenterTradingEntityDetailViewModel centerTradingDetailViewModel = await centerTradingDetailsRepository.GetViewModelForReject(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerTradingDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerTradingDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(CenterTradingEntityDetailViewModel _centerTradingDetailViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_centerTradingDetailViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await centerTradingDetailsRepository.Amend(_centerTradingDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "CenterTradingEntityDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await centerTradingDetailsRepository.Delete(_centerTradingDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "CenterTradingEntityDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_centerTradingDetailViewModel.CenterId);
        }

        private void ClearModelStateOfDataTableFields(CenterTradingEntityDetailViewModel _centerTradingDetailViewModel)
        {
            ModelState[nameof(_centerTradingDetailViewModel.TradingEntityId)]?.Errors?.Clear();
            ModelState[nameof(_centerTradingDetailViewModel.Volume)]?.Errors?.Clear();
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(Guid CenterId)
        {
            CenterTradingEntityDetailViewModel centerTradingDetailViewModel = await centerTradingDetailsRepository.GetViewModelForCreate(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerTradingDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerTradingDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(CenterTradingEntityDetailViewModel _centerTradingDetailViewModel)
        {
            ClearModelStateOfDataTableFields(_centerTradingDetailViewModel);

            if (ModelState.IsValid)
            {
                _centerTradingDetailViewModel.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_centerTradingDetailViewModel.CenterId);

                bool result = await centerTradingDetailsRepository.Save(_centerTradingDetailViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "CenterTradingEntityDetail");
                    }

                    return RedirectToAction("Index", "CenterTradingEntityDetail");
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

            return View(_centerTradingDetailViewModel);
        }

        [HttpGet]
        [Route("CityList")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModel = await centerTradingDetailsRepository.GetIndexWithCreateModifyOperationStatus();

            if (centerTradingEntityDetailViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(centerTradingEntityDetailViewModel);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            CenterTradingEntityDetailViewModel centerTradingDetailViewModel = await centerTradingDetailsRepository.GetViewModelForVerified (personDetailRepository.GetCenterPrmKeyById(CenterId));

            HttpContext.Session["CenterTradingEntityDetail"] = await centerTradingDetailsRepository.GetVerifiedEntries(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerTradingDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerTradingDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(CenterTradingEntityDetailViewModel _centerTradingDetailViewModel)
        {
            ClearModelStateOfDataTableFields(_centerTradingDetailViewModel);

            if (ModelState.IsValid)
            {
                _centerTradingDetailViewModel.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_centerTradingDetailViewModel.CenterId);

                bool result = await centerTradingDetailsRepository.Modify(_centerTradingDetailViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.CommandModify;

                    return RedirectToAction("Index", "CenterTradingEntityDetail");
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

            return View(_centerTradingDetailViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModel = await centerTradingDetailsRepository.GetIndexOfRejectedEntries();

            if (centerTradingEntityDetailViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(centerTradingEntityDetailViewModel);
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
        [Route("ListOfUnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CenterTradingEntityDetailViewModel> centerTradingEntityDetailViewModel = await centerTradingDetailsRepository.GetIndexOfUnVerifiedEntries();

            if (centerTradingEntityDetailViewModel is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(centerTradingEntityDetailViewModel);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            HttpContext.Session["CenterTradingEntityDetail"] = await centerTradingDetailsRepository.GetUnverifiedEntries(personDetailRepository.GetCenterPrmKeyById(CenterId));

            CenterTradingEntityDetailViewModel centerTradingDetailViewModel = await centerTradingDetailsRepository.GetViewModelForUnverified(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (centerTradingDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(centerTradingDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(CenterTradingEntityDetailViewModel _centerTradingDetailViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _centerTradingDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await centerTradingDetailsRepository.Verify(_centerTradingDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "CenterTradingEntityDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _centerTradingDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await centerTradingDetailsRepository.Reject(_centerTradingDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;
                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "CenterTradingEntityDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "CenterTradingEntityDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_centerTradingDetailViewModel.CenterTradingEntityDetailId);
        }

    }
}   