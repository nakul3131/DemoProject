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
    [RoutePrefix("Employee/DataEntry/Maintenance/Address/Locality/VillageTownCity")]
    public class VillageTownCityController : Controller
    {
        private readonly ICenterDemographicDetailRepository centerDemographicDetailRepository;
        private readonly ICenterISOCodeRepository centerISOCodeRepository;
        private readonly ICenterTradingEntityDetailsRepository centerTradingDetailsRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IVillageTownCityRepository villageTownCityRepository;

        public VillageTownCityController(ICenterDemographicDetailRepository _centerDemographicDetailRepository, ICenterISOCodeRepository _centerISOCodeRepository, ICenterTradingEntityDetailsRepository _centerTradingDetailsRepository, 
                                          IPersonDetailRepository _personDetailRepository, IVillageTownCityRepository _villageTownCityRepository)
        {
            centerDemographicDetailRepository = _centerDemographicDetailRepository;
            centerISOCodeRepository = _centerISOCodeRepository;
            centerTradingDetailsRepository = _centerTradingDetailsRepository;
            personDetailRepository = _personDetailRepository;
            villageTownCityRepository = _villageTownCityRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid CenterId)
        {
            HttpContext.Session["CenterTradingEntityDetail"] = await centerTradingDetailsRepository.GetRejectedEntries(personDetailRepository.GetCenterPrmKeyById(CenterId));

            VillageTownCityViewModel villageTownCityViewModel = await villageTownCityRepository.GetRejectedEntry(CenterId);

            villageTownCityViewModel.CenterDemographicDetailViewModel = await centerDemographicDetailRepository.GetRejectedEntry(centerDemographicDetailRepository.GetPrmKeyById(CenterId));

            villageTownCityViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetRejectedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (villageTownCityViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(villageTownCityViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(VillageTownCityViewModel _villageTownCityViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_villageTownCityViewModel);

            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await villageTownCityRepository.Amend(_villageTownCityViewModel);

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
                    bool result = await villageTownCityRepository.Delete(_villageTownCityViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "VillageTownCity"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "VillageTownCity");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_villageTownCityViewModel.CenterId);
        }

        private void ClearModelStateOfDataTableFields(VillageTownCityViewModel _villageTownCityViewModel)
        {
            if (_villageTownCityViewModel.CenterCategoryPrmKey != 1)
            {
                ModelState["_villageTownCityViewModel.ParentCenterPostId"]?.Errors?.Clear();
            }

            ModelState["CenterTradingEntityDetailViewModel.TradingEntityId"]?.Errors?.Clear();
            ModelState["CenterTradingEntityDetailViewModel.Volume"]?.Errors?.Clear();
            ModelState["CenterTradingEntityDetailViewModel.CenterTradingEntityDetailPrmKey"]?.Errors?.Clear();

            ModelState["CenterIsoCodeViewModel.CenterISOCodePrmKey"]?.Errors?.Clear();
            ModelState["CenterOccupationViewModel.CenterOccupationPrmKey"]?.Errors?.Clear();

            ModelState["CenterIsoCodeViewModel.ISONumericCode"]?.Errors?.Clear();

            if (_villageTownCityViewModel.CenterCategoryPrmKey != 1)
            {
                ModelState["_villageTownCityViewModel.ParentCenterPostId"]?.Errors?.Clear();
            }



            ModelState["ParentCenterPostId"]?.Errors?.Clear();


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
        public async Task<ActionResult> Create(VillageTownCityViewModel _villageTownCityViewModel)
        {
            ClearModelStateOfDataTableFields(_villageTownCityViewModel);

            if (ModelState.IsValid)
            {
                bool result = await villageTownCityRepository.Save(_villageTownCityViewModel);

                if (result)
                {
                        TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "VillageTownCity");
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

            return View(_villageTownCityViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey)
        {
            bool data = villageTownCityRepository.GetUniqueCenterName(NameOfCenter, CenterCategoryPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DistrictDropdownListByDivisionId(Guid _divisionId)
        {
            var districtList = personDetailRepository.DistrictDropdownListByDivisionId(_divisionId);

            return Json(districtList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DivisionDropdownListByStateId(Guid _stateId)
        {
            var divisionList = personDetailRepository.DivisionDropdownListByStateId(_stateId);

            return Json(divisionList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid CenterId)
        {
            HttpContext.Session["CenterTradingEntityDetail"] = await centerTradingDetailsRepository.GetVerifiedEntries(personDetailRepository.GetCenterPrmKeyById(CenterId));

            VillageTownCityViewModel villageTownCityViewModel = await villageTownCityRepository.GetVerifiedEntry(CenterId);

            villageTownCityViewModel.CenterDemographicDetailViewModel = await centerDemographicDetailRepository.GetVerifiedEntry(centerDemographicDetailRepository.GetPrmKeyById(CenterId));

            villageTownCityViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetVerifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (villageTownCityViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(villageTownCityViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(VillageTownCityViewModel _villageTownCityViewModel)
        {
            ClearModelStateOfDataTableFields(_villageTownCityViewModel);

            if (ModelState.IsValid)
            {
                bool result = await villageTownCityRepository.Modify(_villageTownCityViewModel);

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

            return View(_villageTownCityViewModel);
        }

        [HttpPost]
        public ActionResult PostalOfficeDropdownListByTalukaId(Guid _talukaId)
        {
            var postList = personDetailRepository.PostalOfficeDropdownListByTalukaId(_talukaId);

            return Json(postList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await villageTownCityRepository.GetIndexOfRejectedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<CenterTradingEntityDetailViewModel> _centerTradingEntityDetail)
        {
            HttpContext.Session.Add("CenterTradingEntityDetail", _centerTradingEntityDetail);

            string result = "Success";

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult StateDropdownListByCountryId(Guid _countryId)
        {
            var statesList = personDetailRepository.StateDropdownListByCountryId(_countryId);

            return Json(statesList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubDivisionOfficeDropdownListByDistrictId(Guid _districtId)
        {
            var subDivisionOfficeList = personDetailRepository.SubDivisionOfficeDropdownListByDistrictId(_districtId);

            return Json(subDivisionOfficeList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TalukaDropdownListBySubDivisionOfficeId(Guid _subDivisionOfficeId)
        {
            var talukaList = personDetailRepository.TalukaDropdownListBySubDivisionOfficeId(_subDivisionOfficeId);

            return Json(talukaList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await villageTownCityRepository.GetIndexOfUnVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<CenterIndexViewModel> centerIndexViewModels = await villageTownCityRepository.GetIndexOfVerifiedEntries();

            return centerIndexViewModels is null ? throw new DatabaseException() : (ActionResult)View(centerIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid CenterId)
        {
            HttpContext.Session["CenterTradingEntityDetail"] = await centerTradingDetailsRepository.GetUnverifiedEntries(personDetailRepository.GetCenterPrmKeyById(CenterId));

            VillageTownCityViewModel villageTownCityViewModel = await villageTownCityRepository.GetUnVerifiedEntry(CenterId);

            villageTownCityViewModel.CenterDemographicDetailViewModel = await centerDemographicDetailRepository.GetUnverifiedEntry(centerDemographicDetailRepository.GetPrmKeyById(CenterId));

            villageTownCityViewModel.CenterIsoCodeViewModel = await centerISOCodeRepository.GetUnverifiedEntry(centerISOCodeRepository.GetPrmKeyById(CenterId));

            if (villageTownCityViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(villageTownCityViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(VillageTownCityViewModel _villageTownCityViewModel, string Command)
        {
            ClearModelStateOfDataTableFields(_villageTownCityViewModel);

            if (ModelState.IsValid)
            {
                _villageTownCityViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await villageTownCityRepository.Verify(_villageTownCityViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "VillageTownCity"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _villageTownCityViewModel.UserAction = StringLiteralValue.Reject;

                    bool result = await villageTownCityRepository.Reject(_villageTownCityViewModel);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "VillageTownCity"), }, JsonRequestBehavior.AllowGet);
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

            return View(_villageTownCityViewModel.CenterId);
        }
    }
}