using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Constants;

namespace DemoProject.WebUI.Controllers
{
    [AllowAnonymous]
    public class DynamicDropdownListController : Controller
    {
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public DynamicDropdownListController(IAccountDetailRepository _accountDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, IPersonDetailRepository _personDetailRepository)
        {
            accountDetailRepository = _accountDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            personDetailRepository = _personDetailRepository;
        }

        //public ActionResult GetFixedDepositCustomerListByPersonPrmKey(Guid _personId)
        //{
        //    var data = accountDetailRepository.GetFixedDepositCustomerListByPersonPrmKey(_personId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetDepositCustomerDropdownListByScheme(Guid _schemeId, Guid _personId)
        {
            var data = accountDetailRepository.GetDepositAccountDropdownListByScheme(_schemeId, _personId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAuthorizedDepositGeneralLedgerDropdownList(Guid _businessOfficeId, string _depositType)
        {
            var data = accountDetailRepository.GetAuthorizedDepositGLDropdownListForAccountOpening(_businessOfficeId, _depositType);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GeneralLedgerDropdownList For Loan Scheme Update Code in Js To Pass AccountClassCode As Parameter

        public ActionResult GetGeneralLedgerDropdownListByAccountClassCode(string _accountClassCode)
        {
            var data = accountDetailRepository.GetGeneralLedgerDropdownListByAccountClassCode(_accountClassCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetEducationalLoanGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.EducationalLoanGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetGoldLoanGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.GoldLoanGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetConsumerDurableLoanGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.ConsumerDurableLoanGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetGuarantorLoanGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.GuarantorLoanGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetHomeLoanGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.HomeLoanGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetLoanAgainstFixedDepositGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.LoanAgainstFixedDepositGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetLoanAgainstPropertyGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.LoanAgainstPropertyGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetVehicleLoanGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.VehicleLoanGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetBusinessLoanGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.BusinessLoanGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        // Deposit Dropdownlist **********Change DepositScheme.js code to pass AccountClassCode as parameter use GetGeneralLedgerDropdownListByParentAccountClassPrmKey method*********
        //public ActionResult GetDemandDepositGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.DemandDepositGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetRecurringDepositGeneralLedgerDropdownList()
        //{
        //    var data = accountDetailRepository.RecurringDepositGeneralLedgerDropdownList;
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        // **********Change DepositScheme.js code to pass AccountClassCode as parameter*********
        public ActionResult GetGeneralLedgerDropdownListByParentAccountClassPrmKey(string _accountClassCode)
        {
            var data = accountDetailRepository.GetGeneralLedgerDropdownListByParentAccountClassPrmKey( _accountClassCode);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetAuthorizedGeneralLedgerDropdownListByBusinessOfficeId(Guid _businessOfficeId)
        //{
        //    var data = securityDetailRepository.AuthorizedGeneralLedgerDropdownList(_businessOfficeId);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetBankBranchDropdownListByBankId(Guid _bankId)
        {
            var data = enterpriseDetailRepository.GetBankBranchDropdownList(_bankId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetCashCreditLoanDocumentDropdownList()
        //{
        //    var data = accountDetailRepository.CashCreditLoanDocumentDropdownList;

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetDemandDepositAccountHolderDropdownList()
        {
            var data = accountDetailRepository.DemandDepositAccountHolderDropdownList;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDemandDepositAccountHolderDropdownListByPerson(Guid _personId)
        {
            var data = accountDetailRepository.GetCustomerSavingAccountDropdownList(personDetailRepository.GetPersonPrmKeyById(_personId));

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetDepositGeneralLedgerDropdownListByBusinessOfficeId(Guid _businessOfficeId, string _depositType)
        //{
        //    var data = accountDetailRepository.GetAuthorizedDepositGeneralLedgerDropdownList(_businessOfficeId, _depositType);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetDocumentDropdownListBySchemeId(Guid _schemeId)
        {
            var data = accountDetailRepository.GetDocumentDropdownListBySchemeId(_schemeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDocumentDropdownListByLoanType(string _loanTypeSysName)
        {
            var data = accountDetailRepository.GetDocumentDropdownListByLoanType(_loanTypeSysName);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetGuarantorDropdownList(Guid _schemeId)
        {
            var data = accountDetailRepository.GetGuarantorDropdownListBySchemeId(_schemeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetLoanGeneralLedgerDropdownListByBusinessOfficeId(Guid _businessOfficeId, Guid _loanTypeId)
        {
            var data = accountDetailRepository.GetAuthorizedLoanGLDropdownListForAccountOpening(_businessOfficeId, _loanTypeId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPersonDropdownListForSharesAccountOpening(Guid _schemeId)
        {
            var data = personDetailRepository.GetPersonDropdownListForSharesAccountOpening(_schemeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPersonDropdownListForDemandDepositAccountOpening(Guid _schemeId)
        {
            var data = personDetailRepository.GetPersonDropdownListForDemandDepositAccountOpening(_schemeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetNonMemberPersonDropdownList()
        {
            var data = personDetailRepository.NonMemberPersonDropdownList;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPersonDropdownList()
        {
            var data = personDetailRepository.PersonDropdownList;

            return Json(data, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult GetPersonDropdownListForNominee()
        {
            var data = personDetailRepository.PersonInfoNumbersDropdownList;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetPersonDropdownListForGuardian()
        {
            var data = personDetailRepository.PersonInfoNumbersAgeAbove18DropdownList;

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetPersonDropdownListForAccountOpening(Guid _schemeId)
        //{
        //    var data = personDetailRepository.NonCustomerPersonDropdownListBySchemeId(_schemeId);

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        
        //Business Loan
        public ActionResult GetPersonDropdownListForBusinessLoanAccountOpening(Guid _schemeId)
        {
            var data = personDetailRepository.GetPersonDropdownListForBusinessLoanAccountOpening(_schemeId); //personDetailRepository.NonCustomerPersonDropdownListBySchemeId(_schemeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

      public ActionResult GetPersonDropdownListForLoanAccountOpening(Guid _schemeId)
        {
            var data = personDetailRepository.GetPersonDropdownListForLoanAccountOpening(_schemeId); //personDetailRepository.NonCustomerPersonDropdownListBySchemeId(_schemeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetSchemeDropdownListByGeneralLedger(Guid _generalLedgerId)
        {
            var data = accountDetailRepository.GetSchemeDropdownListByGeneralLedgerId(_generalLedgerId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetSchemeDropdownListByGeneralLedgerId(Guid GeneralLedgerId)
        //{
        //    var data = accountDetailRepository.GetSchemeDropdownListByGeneralLedgerId(GeneralLedgerId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetSharesGeneralLedgerDropdownListByBusinessOfficeId(Guid _businessOfficeId)
        {
            var data = accountDetailRepository.GetAuthorizedSharesCapitalGLDropdownListForAccountOpening(_businessOfficeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetTenureListBySchemeId(Guid _schemeId)
        //{
        //    var data = accountDetailRepository.GetSchemeTenureDropdownListBySchemeId(_schemeId);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetEducationalCourseDropdownListBySchemePrmKey(short _schemePrmKey, bool _isApplicableAllUniversities)
        {
            var data = accountDetailRepository.GetEducationalCourseDropdownListBySchemePrmKey(_schemePrmKey, _isApplicableAllUniversities);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInstituteDropdownListBySchemePrmKey(short _schemePrmKey, bool _isApplicableAllCourse)
        {
            var data = accountDetailRepository.GetInstituteDropdownListBySchemePrmKey(_schemePrmKey, _isApplicableAllCourse);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVehicleCompanyDropdownListBySchemePrmKey(short _schemePrmKey)
        {
            var data = accountDetailRepository.GetVehicleCompanyDropdownListByVehicleTypeId(_schemePrmKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetConsumerDurableLoanItemDropdownListBySchemePrmKey(short _schemePrmKey)
        {
            var data = accountDetailRepository.GetConsumerDurableLoanItemDropdownListBySchemePrmKey(_schemePrmKey);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVehicleModelDropdownListByVehicleMakeId(Guid _vehicleMakeId)
        {
            var data = accountDetailRepository.GetVehicleModelDropdownListByVehicleMakeId(_vehicleMakeId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetVehicleVariantDropdownListByVehicleModelId(Guid _vehicleModelId)
        {
            var policyClause = accountDetailRepository.GetVehicleVariantDropdownListByVehicleModelId(_vehicleModelId);

            return Json(policyClause, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetCustomerSavingAccountDropdownList(Guid _personId)
        //{

        //}

    }
}