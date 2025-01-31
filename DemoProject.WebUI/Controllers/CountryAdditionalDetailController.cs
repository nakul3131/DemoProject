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
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/CountryAdditionalDetail")]
    public class CountryAdditionalDetailController : Controller
    {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ICountryAdditionalDetailRepository countryAdditionalDetailRepository;

        public CountryAdditionalDetailController(IPersonDetailRepository _personDetailRepository, ICountryAdditionalDetailRepository _countryAdditionalDetailRepository)
        {
            personDetailRepository = _personDetailRepository;
            countryAdditionalDetailRepository = _countryAdditionalDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            CountryAdditionalDetailViewModel countryAdditionalDetailViewModel = await countryAdditionalDetailRepository.GetViewModelForReject(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (countryAdditionalDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(countryAdditionalDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await countryAdditionalDetailRepository.Amend(_countryAdditionalDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandAmend;

                        return RedirectToAction("RejectedIndex", "CountryAdditionalDetail");
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandDelete)
                {
                    bool result = await countryAdditionalDetailRepository.Delete(_countryAdditionalDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "CountryAdditionalDetail") }, JsonRequestBehavior.AllowGet);
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

            return View(_countryAdditionalDetailViewModel.CountryAdditionalDetailId);
        }

        [HttpGet]
        [Route("Create")]
        public async Task<ActionResult> Create(Guid CenterId)
        {
            CountryAdditionalDetailViewModel countryAdditionalDetailViewModel = await countryAdditionalDetailRepository.GetViewModelForCreate(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (countryAdditionalDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(countryAdditionalDetailViewModel); ;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await countryAdditionalDetailRepository.Save(_countryAdditionalDetailViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "CountryAdditionalDetail");
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

            return View(_countryAdditionalDetailViewModel);
        }

        [HttpGet]
        [Route("CityList")]
        public async Task<ActionResult> Index()
        {
            IEnumerable<CountryAdditionalDetailViewModel> countryAdditionalDetailViewModels = await countryAdditionalDetailRepository.GetIndexWithCreateModifyOperationStatus();

            if (countryAdditionalDetailViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(countryAdditionalDetailViewModels);
        }


        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            CountryAdditionalDetailViewModel countryAdditionalDetailViewModel = await countryAdditionalDetailRepository.GetViewModelForVerified(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (countryAdditionalDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(countryAdditionalDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await countryAdditionalDetailRepository.Modify(_countryAdditionalDetailViewModel);

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

            return View(_countryAdditionalDetailViewModel);
        }


        [HttpGet]
        [Route("ListOfRejectedISOCodeRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CountryAdditionalDetailViewModel> countryAdditionalDetailViewModels = await countryAdditionalDetailRepository.GetIndexOfRejectedEntries();

            if (countryAdditionalDetailViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(countryAdditionalDetailViewModels);
        }

        [HttpGet]
        [Route("UnAuthorizedISORecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CountryAdditionalDetailViewModel> countryAdditionalDetailViewModels = await countryAdditionalDetailRepository.GetIndexOfUnVerifiedEntries();

            if (countryAdditionalDetailViewModels is null)
            {
                return View("~/Views/Shared/_RecordNotFound.cshtml");
            }

            return View(countryAdditionalDetailViewModels);
        }

        [HttpGet]
        [Route("List")]
        public async Task<ActionResult> VerifyIndex()
        {
            IEnumerable<CountryAdditionalDetailViewModel> countryAdditionalDetailViewModels = await countryAdditionalDetailRepository.GetIndexOfVerifiedEntries();

            if (countryAdditionalDetailViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(countryAdditionalDetailViewModels);
        }


        [HttpGet]
        [Route("AuthorizeISOCode")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            CountryAdditionalDetailViewModel countryAdditionalDetailViewModel = await countryAdditionalDetailRepository.GetViewModelForUnverified(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (countryAdditionalDetailViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(countryAdditionalDetailViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("AuthorizeISOCode")]
        public async Task<ActionResult> Verify(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _countryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await countryAdditionalDetailRepository.Verify(_countryAdditionalDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new {  result = true,  redirectTo = Url.Action("UnverifiedIndex", "CountryAdditionalDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new {  result = false, redirectTo = Url.Action("DatabaseErrorPage", "CountryAdditionalDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _countryAdditionalDetailViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await countryAdditionalDetailRepository.Reject(_countryAdditionalDetailViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new  {  result = true,  redirectTo = Url.Action("UnverifiedIndex", "CountryAdditionalDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new  { result = false, redirectTo = Url.Action("DatabaseErrorPage", "CountryAdditionalDetail"), }, JsonRequestBehavior.AllowGet);
                    }
                }

                return RedirectToAction("UnverifiedIndex", "CountryAdditionalDetail");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_countryAdditionalDetailViewModel.CountryAdditionalDetailId);
        }

    }
}