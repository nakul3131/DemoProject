using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Constants;
using DemoProject.WebUI.Infrastructure.CustomException;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Services.ViewModel.PersonInformation.Master;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/Country")]
    public class CountryController : Controller
    {
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ICountryRepository countryRepository;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;
        private readonly ICountryAdditionalDetailRepository countryAdditionalDetailRepository;

        public CountryController(IPersonDetailRepository _personDetailRepository, ICountryRepository _countryRepository, ICenterISOCodeRepository _centerISOCodeRepository, ICountryAdditionalDetailRepository _countryAdditionalDetailRepository)
        {
            personDetailRepository = _personDetailRepository;
            countryRepository = _countryRepository;
            centerISOCodeRepository = _centerISOCodeRepository;
            countryAdditionalDetailRepository = _countryAdditionalDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            CountryViewModel countryViewModel = await countryRepository.GetRejectedEntry(CenterId);

            countryViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetRejectedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            countryViewModel.CountryAdditionalDetailViewModel = await countryAdditionalDetailRepository.GetRejectedEntry(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (countryViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(countryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(CountryViewModel _countryViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await countryRepository.Amend(_countryViewModel);

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
                    bool result = await countryRepository.Delete(_countryViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;
                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "Country"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("RejectedIndex", "Country");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_countryViewModel.CenterId);
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
        public async Task<ActionResult> Create(CountryViewModel _countryViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await countryRepository.Save(_countryViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "Country");
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

            return View(_countryViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey)
        {
            bool data = countryRepository.GetUniqueCenterName(NameOfCenter, CenterCategoryPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            CountryViewModel countryViewModel = await countryRepository.GetVerifiedEntry(CenterId);

            countryViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetVerifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            countryViewModel.CountryAdditionalDetailViewModel = await countryAdditionalDetailRepository.GetVerifiedEntry(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (countryViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(countryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(CountryViewModel _countryViewModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await countryRepository.Modify(_countryViewModel);

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

            return View(_countryViewModel);

        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await countryRepository.GetIndexOfRejectedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await countryRepository.GetIndexOfUnVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await countryRepository.GetIndexOfVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            CountryViewModel countryViewModel = await countryRepository.GetUnVerifiedEntry(CenterId);

            countryViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetUnverifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            countryViewModel.CountryAdditionalDetailViewModel = await countryAdditionalDetailRepository.GetUnverifiedEntry(personDetailRepository.GetCenterPrmKeyById(CenterId));

            if (countryViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(countryViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(CountryViewModel _countryViewModel, string Command)
        {
            if (ModelState.IsValid)
            {
                _countryViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await countryRepository.Verify(_countryViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Country"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _countryViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await countryRepository.Reject(_countryViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "Country"), }, JsonRequestBehavior.AllowGet);
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

            return View(_countryViewModel.CenterId);
        }
    }
}