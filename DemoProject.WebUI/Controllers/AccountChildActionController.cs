using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountChildActionController : Controller
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ISchemeDetailRepository schemeDetailRepository;
        private readonly ISharesCapitalCustomerAccountRepository sharesCapitalCustomerAccountRepository;
        private readonly IAccountParameterDetailRepository accountParameterDetailRepository;

        public AccountChildActionController(IAccountDetailRepository _accountDetailRepository, ISchemeDetailRepository _schemeDetailRepository, ISharesCapitalCustomerAccountRepository _sharesCapitalCustomerAccountRepository, IAccountParameterDetailRepository _accountParameterDetailRepository)
        {
            accountDetailRepository = _accountDetailRepository;
            schemeDetailRepository = _schemeDetailRepository;
            sharesCapitalCustomerAccountRepository = _sharesCapitalCustomerAccountRepository;
            accountParameterDetailRepository = _accountParameterDetailRepository;
        }

        public ActionResult GetGoldLoanRateOfMetal(string _metalPurity)
        {
            var data = accountDetailRepository.GetGoldLoanRateByPurity(_metalPurity);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPolicyNumber(string _inputedPolicyNumber)
        {
            bool result = accountDetailRepository.IsDuplicatePolicyNumber(_inputedPolicyNumber);

            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult GetByLawsLoanScheduleParameterEntry(Guid _loanTypeId)
        {
            ByLawsLoanScheduleParameterViewModel byLawsLoanScheduleParameterViewModel = accountParameterDetailRepository.GetByLawsLoanScheduleParameterEntry(accountDetailRepository.GetLoanTypePrmKeyById(_loanTypeId), StringLiteralValue.Verify);
            return Json(byLawsLoanScheduleParameterViewModel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFirstDateOfOpenFinancialYear()
        {
            var data = accountDetailRepository.GetCurrentFinancialYearStartDate();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetGoldLoanRate(DateTime _acDateTime, string _metalPurity)
        //{
        //    var data = accountDetailRepository.GetGoldLoanRate(_acDateTime, _metalPurity);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetLoanTypeSysNameByLoanTypeId(Guid _loanTypeId)
        {
            string data = accountDetailRepository.GetSysNameOfLoanTypeByLoanTypeId(_loanTypeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetPreOwnedVehiclePhotoUpload(Guid _schemeId)
        //{
        //    string data = accountDetailRepository.GetPreOwnedVehiclePhotoUploadBySchemeId(_schemeId);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetSchemeVehicleLoanParameterValidations(Guid _schemeId)
        //{
        //    var data = accountDetailRepository.GetSchemeVehicleLoanParameterValidations(_schemeId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetVehicleTypeByVehicleVariantId(Guid _vehicleVariantId)
        //{
        //    string data = accountDetailRepository.GetVehicleTypeByVehicleVariantId(_vehicleVariantId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetVehicleTypePrmKey(Guid _vehicleModelId)
        {
            byte data = accountDetailRepository.GetVehicleTypePrmKey(_vehicleModelId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSysNameOfVehicleType(byte _vehicleTypePrmKey)
        {
            string data = accountDetailRepository.GetSysNameOfVehicleType(_vehicleTypePrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSysNameOfInterestMethodTypeById(Guid _interestMethodId)
        {
            string data = accountDetailRepository.GetSysNameOfInterestMethodTypeById(_interestMethodId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRenewTypeSysNameById(Guid _renewTypeId)
        {
            string data = accountDetailRepository.GetRenewTypeSysNameById(_renewTypeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetDepositSchemeDetailBySchemeId(Guid _schemeId)
        {
            CustomerDepositAccountOpeningConfigViewModel data = await schemeDetailRepository.GetDepositSchemeConfigDetail(_schemeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetLoanSchemeDetailBySchemeId(Guid _schemeId)
        {
            CustomerLoanAccountOpeningConfigViewModel data = await schemeDetailRepository.GetCustomerLoanAccountConfigDetail(_schemeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetSharesCapitalSchemeDetailBySchemeId(Guid _schemeId)
        {
            CustomerSharesAccountOpeningConfigViewModel data = await schemeDetailRepository.GetSharesCapitalSchemeConfigDetail(_schemeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTimePeriodUnitSysNameByPrmKey(byte _timePeriodUnitPrmKey)
        {
            string data = accountDetailRepository.GetSysNameOfTimePeriodUnitByPrmKey(_timePeriodUnitPrmKey);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTimePeriodUnitSysNameById(Guid _timePeriodUnitId)
        {
            string data = accountDetailRepository.GetSysNameOfTimePeriodUnitById(_timePeriodUnitId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetConsumerLoanMargin(short _schemePrmKey, Guid _consumerDurableItemId)
        {
            var data = accountDetailRepository.GetConsumerLoanMarginBySchemePrmKey(_schemePrmKey, _consumerDurableItemId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // Unique Scheme Name
        public ActionResult IsUniqueDepositSchemeName(string _nameOfScheme)
        {
            bool data = accountDetailRepository.IsUniqueDepositSchemeName(_nameOfScheme);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsUniqueLoanSchemeName(string _nameOfScheme)
        {
            bool data = accountDetailRepository.IsUniqueLoanSchemeName(_nameOfScheme);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsUniqueSharesCapitalSchemeName(string _nameOfScheme)
        {
            bool data = accountDetailRepository.IsUniqueSharesCapitalSchemeName(_nameOfScheme);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsUniqueEmployeeCode(string _employeeCode)
        {
            bool data = accountDetailRepository.IsUniqueEmployeeCode(_employeeCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsUniqueUserProfileName(string _nameOfUserProfile)
        {
            bool data = accountDetailRepository.IsUniqueUserProfileName(_nameOfUserProfile);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsUniqueNameOfVehicleMake(string _nameOfVehicleMake)
        {
            bool data = accountDetailRepository.IsUniqueNameOfVehicleMake(_nameOfVehicleMake);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsVisibleSharesApplicationNumber(Guid _schemeId)
        {
            bool data = accountDetailRepository.IsVisibleSharesApplicationNumber(_schemeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public  ActionResult IsValidApplicationNumber(Guid _schemeId, int _applicationNumber)
        {
            bool data = accountDetailRepository.IsValidSharesApplicationNumber(_schemeId, _applicationNumber);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        
        // ****** Rename To IsValidAccountNumber
        //public ActionResult ValidateAccountNumber(Guid _schemeId, int _accountNumber)
        //{
        //    bool data = accountDetailRepository.IsValidAccountNumber(_schemeId, _accountNumber);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult ValidateMemberNumber(Guid _schemeId, int _memberNumber)
        //{
        //    bool data = accountDetailRepository.IsValidMemberNumber(_schemeId, _memberNumber);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
    }
}