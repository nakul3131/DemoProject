using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.WebUI.Infrastructure.CustomException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.WebUI.Controllers
{
    [RoutePrefix("Employee/Master/Enterprise/BusinessOffice")]
    public class BusinessOfficeController : Controller
    {
        private readonly IBusinessOfficeRepository businessOfficeRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;

        public BusinessOfficeController(IBusinessOfficeRepository _businessOfficeRepository, IEnterpriseDetailRepository _officeDetailRepository)
        {
            businessOfficeRepository = _businessOfficeRepository;
            enterpriseDetailRepository = _officeDetailRepository;
        }

        [HttpGet]
        [Route("Edit")]
        public async Task<ActionResult> Amend(Guid BusinessOfficeId)
        {
            bool data = await businessOfficeRepository.GetSessionValues(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Reject);

            // Get Value From Main Table
            BusinessOfficeViewModel businessOfficeViewModel = await businessOfficeRepository.GetBusinessOfficeEntry(BusinessOfficeId, StringLiteralValue.Reject);

            if (businessOfficeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Edit")]
        public async Task<ActionResult> Amend(BusinessOfficeViewModel _businessOfficeViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandAmend)
                ClearModelStateOfDataTableFields(_businessOfficeViewModel, StringLiteralValue.Amend);
            else
                ClearModelStateOfDataTableFields(_businessOfficeViewModel, StringLiteralValue.Delete);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();


            if (ModelState.IsValid)
            {
                if (Command == StringLiteralValue.CommandAmend)
                {
                    bool result = await businessOfficeRepository.Amend(_businessOfficeViewModel);

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
                    bool result = await businessOfficeRepository.VerifyRejectDelete(_businessOfficeViewModel, StringLiteralValue.Delete);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandDelete;

                        return Json(new { result = true, redirectTo = Url.Action("RejectedIndex", "BusinessOffice"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }
                return RedirectToAction("RejectedIndex", "BusinessOffice");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Enter Required Valid Information");
            }

            return View(_businessOfficeViewModel);
        }

        private void ClearModelStateOfDataTableFields(BusinessOfficeViewModel _businessOfficeViewModel, string _entryType)
        {
            // Assign All DataTable ViewModels
            string errorViewModelName = "BusinessOfficePasswordPolicyViewModel,BusinessOfficeMenuViewModel,BusinessOfficeCurrencyViewModel,BusinessOfficeSpecialPermissionViewModel,BusinessOfficeTransactionLimitViewModel,BusinessOfficeAccountNumberViewModel,BusinessOfficeAgreementNumberViewModel,BusinessOfficeApplicationNumberViewModel,BusinessOfficeDepositCertificateNumberViewModel,BusinessOfficePassbookNumberViewModel";

            // BusinessOfficeCoopRegistrationViewModel
            OfficeDetailViewModel officeDetailViewModel = new OfficeDetailViewModel();

            // On Page False IsRegisterUnderCooperative
            if (officeDetailViewModel.IsRegisterUnderCooperative == false)
            {
                errorViewModelName = errorViewModelName + ",BusinessOfficeCoopRegistrationViewModel";
            }
            
            // On page False IsRegisterUnderRBI
            if (officeDetailViewModel.IsRegisterUnderRBI == false)
            {
                errorViewModelName = errorViewModelName + ",BusinessOfficeRBIRegistrationViewModel";
            }

            if (_businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.EnableAutoMemberNumber == false)
            {
                ModelState["BusinessOfficeMemberNumberViewModel.StartMemberNumber"]?.Errors?.Clear();
                ModelState["BusinessOfficeMemberNumberViewModel.EndMemberNumber"]?.Errors?.Clear();
                ModelState["BusinessOfficeMemberNumberViewModel.MemberNumberIncrementBy"]?.Errors?.Clear();
            }

            if (_businessOfficeViewModel.BusinessOfficeSharesCertificateNumberViewModel.EnableDigitalCodeForSharesCertificateNumber == false)
            {
                ModelState["BusinessOfficeSharesCertificateNumberViewModel.StartSharesCertificateNumber"]?.Errors?.Clear();
                ModelState["BusinessOfficeSharesCertificateNumberViewModel.EndSharesCertificateNumber"]?.Errors?.Clear();
                ModelState["BusinessOfficeSharesCertificateNumberViewModel.SharesCertificateNumberIncrementBy"]?.Errors?.Clear();
            }

            if (_businessOfficeViewModel.BusinessOfficePersonInformationNumberViewModel.EnableDigitalCodeForPersonInformationNumber == false)
            {
                ModelState["BusinessOfficePersonInformationNumberViewModel.StartPersonInformationNumber"]?.Errors?.Clear();
                ModelState["BusinessOfficePersonInformationNumberViewModel.EndPersonInformationNumber"]?.Errors?.Clear();
                ModelState["BusinessOfficePersonInformationNumberViewModel.PersonInformationNumberIncrementBy"]?.Errors?.Clear();
            }

            if (_businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.EnableTransactionDigitalCode == false)
            {
                ModelState["BusinessOfficeTransactionParameterViewModel.StartTransactionNumber"]?.Errors?.Clear();
                ModelState["BusinessOfficeTransactionParameterViewModel.EndTransactionNumber"]?.Errors?.Clear();
                ModelState["BusinessOfficeTransactionParameterViewModel.TransactionNumberIncrementBy"]?.Errors?.Clear();
                ModelState["BusinessOfficeTransactionParameterViewModel.ChecksumAlgorithmId"]?.Errors?.Clear();
            }

            // On Create Following Inputs Are Disabled (ex. Dividend) And Enabled In Other Operation
            // Then Those PrmKeys Require To Clear Error
            if (_entryType != StringLiteralValue.Create)
            {

                ModelState["BusinessOfficeCoopRegistrationViewModel.BusinessOfficeCoopRegistrationPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeRBIRegistrationViewModel.BusinessOfficeRBIRegistrationPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficePasswordPolicyViewModel.BusinessOfficePasswordPolicyPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeMenuViewModel.BusinessOfficeMenuPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeSpecialPermissionViewModel.BusinessOfficeSpecialPermissionPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeTransactionLimitViewModel.BusinessOfficeTransactionLimitPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeAccountNumberViewModel.BusinessOfficeAccountNumberPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeAgreementNumberViewModel.BusinessOfficeAgreementNumberPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeApplicationNumberViewModel.BusinessOfficeApplicationNumberPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeCurrencyViewModel.BusinessOfficeCurrencyPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeTransactionParameterViewModel.BusinessOfficeTransactionParameterPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeMemberNumberViewModel.BusinessOfficeMemberNumberPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficeSharesCertificateNumberViewModel.BusinessOfficeSharesCertificateNumberPrmKey"]?.Errors?.Clear();
                ModelState["BusinessOfficePersonInformationNumberViewModel.BusinessOfficePersonInformationNumberPrmKey"]?.Errors?.Clear();
            }

            // Clear DataTable Error
            foreach (var key in ModelState.Keys)
            {
                var viewModelPropertyArray = key.Split('.');
                int arrayLength = viewModelPropertyArray.Length;

                if (arrayLength > 1)
                {
                    var viewModel = viewModelPropertyArray[arrayLength - 2];

                    if (errorViewModelName.Contains(viewModel) || key.Contains("Enable"))
                    {
                        ModelState[key]?.Errors?.Clear();
                    }
                }
            }
        }

        [HttpGet]
        [Route("Create")]
        public ActionResult Create()
        {
            //var countInBusinessOffice = businessOfficeRepository.GetCountOfBusinessOffice();

            //var countInAppConfig = businessOfficeRepository.GetCountOfAppConfig();

            //if (countInBusinessOffice >= countInAppConfig)
            //{
            //    throw new ExceedBoundaryLimitException();
            //}
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<ActionResult> Create(BusinessOfficeViewModel _businessOfficeViewModel)
        {
            OfficeDetailViewModel officeDetailViewModel = new OfficeDetailViewModel();

            ViewBag.OfficeDetail = officeDetailViewModel;

            ClearModelStateOfDataTableFields(_businessOfficeViewModel, StringLiteralValue.Create);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                bool result = await businessOfficeRepository.Save(_businessOfficeViewModel);

                if (result)
                {
                    TempData.Clear(); // Clears Previous TempData Value.
                    TempData["TransactionStatus"] = StringLiteralValue.Save;

                    if ((bool)HttpContext.Session["IsRedirectToSamePage"])
                    {
                        return RedirectToAction("Create", "BusinessOffice");
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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record, Please Provide Correct Or Required Information");
            }

            return View(_businessOfficeViewModel);
        }

        [HttpPost]
        public ActionResult GetUniqueBusinessOfficeName(string NameOfBusinessOffice)
        {
            bool data = businessOfficeRepository.GetUniqueBusinessOfficeName(NameOfBusinessOffice);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("Modify")]
        public async Task<ActionResult> Modify(Guid BusinessOfficeId)
        {

            bool data = await businessOfficeRepository.GetSessionValues(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Verify);

            // Get Value From Main Table
            BusinessOfficeViewModel businessOfficeViewModel = await businessOfficeRepository.GetBusinessOfficeEntry(BusinessOfficeId, StringLiteralValue.Verify);

            if (businessOfficeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Modify")]
        public async Task<ActionResult> Modify(BusinessOfficeViewModel _businessOfficeViewModel)
        {
            ClearModelStateOfDataTableFields(_businessOfficeViewModel, StringLiteralValue.CommandModify);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {

                bool result = await businessOfficeRepository.Modify(_businessOfficeViewModel);

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
                ModelState.AddModelError("SubmitError", "An Error Occurred While Saving Record,Please Enter Required Valid Information");
            }

            return View(_businessOfficeViewModel);
        }

        [HttpGet]
        [Route("ListOfRejectedRecords")]
        public async Task<ActionResult> RejectedIndex()
        {
            IEnumerable<BusinessOfficeIndexViewModel> businessOfficeIndexViewModels = await businessOfficeRepository.GetBusinessOfficeIndex(StringLiteralValue.Reject);

            if (businessOfficeIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeIndexViewModels);
        }

        [HttpPost]
        [Route("SaveDataTables")]
        public ActionResult SaveDataTables(List<BusinessOfficeAccountNumberViewModel> _businessOfficeAccountNumber,List<BusinessOfficeAgreementNumberViewModel> _businessOfficeAgreementNumber, List<BusinessOfficeApplicationNumberViewModel> _businessOfficeApplicationNumber, List<BusinessOfficeCurrencyViewModel> _businessOfficeCurrency, List<BusinessOfficeMenuViewModel> _businessOfficeMenu,
                                            List<BusinessOfficePasswordPolicyViewModel> _businessOfficePasswordPolicy, List<BusinessOfficeSpecialPermissionViewModel> _businessOfficeSpecialPermission, List<BusinessOfficeTransactionLimitViewModel> _businessOfficeTransactionLimit,
                                            List<BusinessOfficeDepositCertificateNumberViewModel> _businessOfficeCertificateNumber, List<BusinessOfficePassbookNumberViewModel> _businessOfficePassbookNumber)
        {
            HttpContext.Session.Add("BusinessOfficeAccountNumber", _businessOfficeAccountNumber);
            HttpContext.Session.Add("BusinessOfficeAgreementNumber", _businessOfficeAgreementNumber);
            HttpContext.Session.Add("BusinessOfficeApplicationNumber", _businessOfficeApplicationNumber);
            HttpContext.Session.Add("BusinessOfficeCurrency", _businessOfficeCurrency);
            HttpContext.Session.Add("BusinessOfficeMenu", _businessOfficeMenu);
            HttpContext.Session.Add("BusinessOfficePasswordPolicy", _businessOfficePasswordPolicy);
            HttpContext.Session.Add("BusinessOfficeSpecialPermission", _businessOfficeSpecialPermission);
            HttpContext.Session.Add("BusinessOfficeTransactionLimit", _businessOfficeTransactionLimit);
            HttpContext.Session.Add("BusinessOfficeDepositCertificateNumber", _businessOfficeCertificateNumber);
            HttpContext.Session.Add("BusinessOfficePassbookNumber", _businessOfficePassbookNumber);

            string result = "Success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [Route("UnAuthorizedRecords")]
        public async Task<ActionResult> UnverifiedIndex()
        {
            IEnumerable<BusinessOfficeIndexViewModel> businessOfficeIndexViewModels = await businessOfficeRepository.GetBusinessOfficeIndex(StringLiteralValue.Unverified);

            if (businessOfficeIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeIndexViewModels);
        }

        [HttpGet]
        [Route("ListForModification")]
        public async Task<ActionResult> VerifiedIndex()
        {
            IEnumerable<BusinessOfficeIndexViewModel> businessOfficeIndexViewModels = await businessOfficeRepository.GetBusinessOfficeIndex(StringLiteralValue.Verify);

            if (businessOfficeIndexViewModels is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeIndexViewModels);
        }

        [HttpGet]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(Guid BusinessOfficeId)
        {

            bool data = await businessOfficeRepository.GetSessionValues(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Unverified);

            // Get Value From Main Table
            BusinessOfficeViewModel businessOfficeViewModel = await businessOfficeRepository.GetBusinessOfficeEntry(BusinessOfficeId, StringLiteralValue.Unverified);

            if (businessOfficeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Authorize")]
        public async Task<ActionResult> Verify(BusinessOfficeViewModel _businessOfficeViewModel, string Command)
        {
            if (Command == StringLiteralValue.CommandVerify)
                ClearModelStateOfDataTableFields(_businessOfficeViewModel, StringLiteralValue.Verify);
            else
                ClearModelStateOfDataTableFields(_businessOfficeViewModel, StringLiteralValue.Reject);

            var errors = ModelState.Where(x => x.Value.Errors.Count > 0).Select(x => new { x.Key, x.Value.Errors }).ToArray();

            if (ModelState.IsValid)
            {
                _businessOfficeViewModel.UserProfilePrmKey = (short)HttpContext.Session["UserProfilePrmKey"];

                if (Command == StringLiteralValue.CommandVerify)
                {
                    bool result = await businessOfficeRepository.VerifyRejectDelete(_businessOfficeViewModel, StringLiteralValue.Verify);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandVerify;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BusinessOffice"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                if (Command == StringLiteralValue.CommandReject)
                {
                    _businessOfficeViewModel.UserAction = StringLiteralValue.CommandReject;

                    bool result = await businessOfficeRepository.VerifyRejectDelete(_businessOfficeViewModel, StringLiteralValue.Reject);

                    if (result)
                    {
                        TempData.Clear(); // Clears Previous TempData Value.
                        TempData["TransactionStatus"] = StringLiteralValue.CommandReject;

                        return Json(new { result = true, redirectTo = Url.Action("UnverifiedIndex", "BusinessOffice"), }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        throw new DatabaseException();
                    }
                }

                return RedirectToAction("UnverifiedIndex", "BusinessOffice");
            }
            else
            {
                ModelState.AddModelError("SubmitError", "An Error Occurred While Authorize Record, Please Enter Required Valid Information");
            }

            return View(_businessOfficeViewModel.BusinessOfficeId);
        }

        [HttpGet]
        [Route("ViewEntry")]
        public async Task<ActionResult> ViewEntry(Guid BusinessOfficeId)
        {

            bool data = await businessOfficeRepository.GetSessionValues(enterpriseDetailRepository.GetBusinessOfficePrmKeyById(BusinessOfficeId), StringLiteralValue.Verify);

            // Get Value From Main Table
            BusinessOfficeViewModel businessOfficeViewModel = await businessOfficeRepository.GetBusinessOfficeEntry(BusinessOfficeId, StringLiteralValue.Verify);

            if (businessOfficeViewModel is null)
            {
                throw new DatabaseException();
            }

            return View(businessOfficeViewModel);
        }
    }
}