using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Account.Master;
using DemoProject.Services.ViewModel.Account.Layout;

namespace DemoProject.Services.Abstract.Account.SystemEntity
{
    public interface IAccountDetailRepository 
    {
        bool HasAccessOfAllBusinessOffice(short _userProfilePrmKey);

        bool HasAccessOfAllGeneralLedger(short _userProfilePrmKey);

        bool HasAccessOfAllTransaction(short _userProfilePrmKey);

        bool IsAnySharesApplicationPending();

        //bool IsValidAccountNumber(Guid _schemeId, int _accountNumber);

        //bool IsValidMemberNumber(Guid _schemeId, int _memberNumber);

        bool IsUniqueDepositSchemeName(string _nameOfScheme);

        bool IsUniqueLoanSchemeName(string _nameOfScheme);

        bool IsUniqueSharesCapitalSchemeName(string _nameOfScheme);

        bool IsUniqueEmployeeCode(string _employeeCode);

        bool IsUniqueUserProfileName(string _nameOfUserProfile);

        bool IsUniqueNameOfVehicleMake(string _nameOfVehicleMake);


        bool IsVisibleSharesApplicationNumber(Guid _schemeId);

        bool IsValidSharesApplicationNumber(Guid _schemeId, int _applicationNumber);

        bool IsDuplicatePolicyNumber(string _inputedPolicyNumber);


        byte GetCashCreditDocumentTypePrmKey();

        byte GetAccountOperationModePrmKeyById(Guid _accountOperationModeId);

        byte GetAgricultureLandTypePrmKeyById(Guid _agricultureLandTypeId);

        byte GetBalanceTypePrmKeyById(Guid _balanceTypeId);

        byte GetChargesTypePrmKeyById(Guid _chargesApplyingTypeId);

        short GetConsumerDurableLoanItemPrmKeyById(Guid _consumerDurableItemId);

        decimal GetConsumerLoanMarginBySchemePrmKey(short _schemePrmKey, Guid _consumerDurableItemId);

        short GetEducationalCoursePrmKeyById(Guid _educationalCourseId);

        short GetInstitutePrmKeyById(Guid _instituteId);

        byte GetCreditBureauAgencyPrmKeyById(Guid _creditBureauAgencyId);

        byte GetCustomerTypePrmKeyById(Guid _CustomerTypeId);

        byte GetDaysInYearPrmKeyById(Guid _daysInYearId);

        byte GetDenominationPrmKey(Guid _denominationId);

        byte GetDocumentTypePrmKeyBySysName(string _sysName);

        byte GetDividendCalculationMethodPrmKeyById(Guid _dividendCalculationMethodId);

        byte GetFinancialAssetTypePrmKeyById(Guid _financialAssetTypeId);

        byte GetGSTReturnPeriodicityPrmKeyById(Guid _gSTReturnPeriodicitysId);

        byte GetInstallmentFrequencyPrmKeyById(Guid _installmentFrequencyId);

        byte GetInterestCalculationFrequencyPrmKeyById(Guid _interestCalculationFrequencyId);

        byte GetInterestCompoundingFrequencyPrmKeyById(Guid _interestCompoundingFrequencyId);

        byte GetInterestMethodPrmKeyById(Guid _interestMethodId);

        byte GetInterestRateChargedDurationPrmKeyById(Guid _interestRateChargedDurationId);

        byte GetInterestRebateApplyFrequencyPrmKeyById(Guid _interestRebateApplyFrequencyId);

        byte GetInterestRebateCriteriaPrmKeyById(Guid _interestRebateCriteriaId);

        byte GetJointAccountHolderTypePrmKeyById(Guid _jointAccountHolderTypeId);

        byte GetLendingChargesBasePrmKeyById(Guid _lendingChargesBaseId);

        byte GetLendingInterestPostingFrequencyPrmKeyById(Guid _lendingInterestPostingFrequencyId);

        byte GetLendingRepaymentsInterestCalculationPrmKeyById(Guid _lendingRepaymentsInterestCalculationId);

        byte GetLoanTypePrmKeyById(Guid _loanTypeId);

        byte GetMemberTypePrmKeyById(Guid _memberTypeId);

        byte GetMemberTypePrmKeyBySysName(string _memberTypeSysName);   
        
        byte GetRepaymentIntervalFrequencyPrmKeyById(Guid _repaymentIntervalFrequency);

        byte GetRenewTypePrmKeyById(Guid _renewTypeId);

        byte GetSchemeTypePrmKeyById(Guid _schemeTypeId);

        byte GetSweepOutFrequencyPrmKeyById(Guid _sweepOutFrequencyId);

        byte GetTargetGroupPrmKeyById(Guid _targetGroupId);

        byte GetTransactionTypePrmKeyById(Guid _transactionTypeId);

        byte GetVehicleBodyTypePrmKeyById(Guid _vehicleBodyTypeId);

        byte GetVehicleTypePrmKeyById(Guid _vehicleTypeId);

        short GetConsumerItemSupplierPersonCategoryPrmKey();

        short GetMemberAdmissionFeeAccountClassPrmKey();

        short MembershipAgeForResignMembership();

        short GetAccountClassPrmKeyById(Guid _accountClassId);

        short GetChequeBookMasterPrmKeyById(Guid _chequeBookMasterId);

        short GetCurrencyPrmKeyById(Guid _currencyId);

        short GetCustomerAccountTypePrmKeyById(Guid _customerAccountTypeId);

        short GetDocumentPrmKeyId(Guid _documentId);

        short GetFrequencyPrmKeyById(Guid _frequencyId);

        short GetFundPrmKeyById(Guid _FundcategoryId);

        short GetGeneralLedgerPrmKeyById(Guid _GeneralLedgerId);

        short GetGSTRegistrationTypePrmKeyById(Guid _gSTRegistrationTypesId);

        short GetLoanReasonPrmKeyById(Guid _loanReasonId);

        short GetLoanRecoveryActionPrmKeyById(Guid _loanRecoveryActionId);

        short GetSchemePrmKeyById(Guid _SchemeId);

        short GetVehicleModelPrmKeyById(Guid _vehicleModelId);

        short GetVehicleMakePrmKeyById(Guid _vehicleMakeId);

        short GetVehicleVariantPrmKeyById(Guid _vehicleVariantId);

        short GetVehicleColourPrmKeyById(Guid _colourId);

        short GetGoldOrnamentPrmKeyById(Guid _goldOrnamentId);

        short GetConsumerDurableItemPrmKeyById(Guid _consumerDurableItemId);

        short GetConsumerDurableItemBrandPrmKeyById(Guid _consumerDurableItemBrandId);

        int GetCustomerDepositAccountPrmKeyByCustomerAccountId(Guid _depositAccount);


        short GetTimePeriodForNewCustomerAccountFlag(short _schemePrmKey);

        string GetSysNameOfInterestMethodTypeById(Guid _interestMethodId);

        string GetSysNameOfSchemeTypeByGeneralLedgerId(Guid _generalLedgerId);

        short GetUserHomeBranchPrmKey(short _userProfilePrmKey);

        short GetUserHomeBranchRoleProfilePrmKey(short _userProfilePrmKey);

        string GetSysNameOfTimePeriodUnitById(Guid _timePeriodUnitId);

        string GetSysNameOfTimePeriodUnitByPrmKey(byte _timePeriodUnitPrmKey);

        int GetVehicleSupplierPrmKeyById(Guid _vehicleSupplierId);

        int GetCurrentGoldLoanRatePrmKey(string _purity);

        int GetMinuteOfMeetingAgendaPrmKeyById(Guid _minuteOfMeetingAgendaId);

        long GetPersonPrmKeyByPersonId(Guid _personId);

        long GetCustomerAccountNumberById(Guid _customerAccountId);

        long GetCustomerAccountPrmKeyById(Guid _customerAccountId);
        //long GetCustomerAccountNomineePrmKeyById(Guid _customerAccountNomineeId);

        decimal GetGoldLoanRateByPurity(string _purity);

        decimal GetMaximumSharesHolidingLimitAmount();

        decimal GetMaximumSharesHoldingLimitPercentage();

        decimal AggregateSharesWithdrawalLimit();

        decimal GetAccountOpeningAmount(short schemePrmKey);

        short GetMinimumNumberOfShares(short _schemePrmKey);

        short GetMaximumNumberOfShares(short _schemePrmKey);

        bool GetEnableAutoCertificateNumber(short _schemePrmKey);

        decimal GetDemandDepositMinimumBalanceAmount(short _schemePrmKey);

        string GetAuditClass(byte _Year);

        //string GetPreOwnedVehiclePhotoUploadBySchemeId(Guid _schemeId);

        string GetEligibilityForGuarantor(Guid _schemeId);

        string GetSysNameOfLoanTypeByLoanTypeId(Guid _loanTypeId);

        string GetSysNameOfLoanTypeBySchemePrmKey(short _schemePrmKey);

        string GetNameOfColourByColourId(Guid _colourId);

        string GetNameOfInstituteByInstituteId(Guid _instituteId);

        string GetSysNameOfOccupationById(Guid _OccupationId);

        //string GetVehicleTypeByVehicleVariantId(Guid _vehicleTypeId);

        string GetRenewTypeSysNameById(Guid _renewTypeId);

        byte GetVehicleTypePrmKey(Guid _vehicleModelId);

        string GetSysNameOfVehicleType(byte _vehicleTypePrmKey);

        bool GetAuthorizedUserStatusByPrmKey(short _userProfilePrmKey);

        string GetUserNameByUserProfilePrmKey(short _userProfilePrmKey);

        DateTime GetCurrentFinancialYearStartDate();

        DateTime GetPreviousClosingFinancialYearEndDate();
        List<SelectListItem> AuthorizedBusinessOfficeDropdownList { get; }

        List<SelectListItem> AccountClassDropdownList { get; }

        List<SelectListItem> AccountClosingChargesGeneralLedgerDropdownList { get; }

        List<SelectListItem> AccountTransferChargesGeneralLedgerDropdownList { get; }

        List<SelectListItem> AccountOperationModeDropdownList { get; }

        List<SelectListItem> AgricultureLandTypeDropdownList { get; }

        List<SelectListItem> AuthorizedTransactionTypeDropdownList { get; }

        List<SelectListItem> BalanceTypeDropdownList { get; }

        List<SelectListItem> BusinessOfficeGeneralLedgerDropdownList { get; }

        List<SelectListItem> CashCreditLoanDocumentDropdownList { get; }

        List<SelectListItem> ChargesTypeDropdownList { get; }

        List<SelectListItem> ChequeBookDropdownList { get; }

        List<SelectListItem> ChequeReturnReasonDropdownList { get; }

        List<SelectListItem> ConsumerDurableItemDropdownList { get; }

        List<SelectListItem> ConsumerDurableItemBrandDropdownList { get; }

        List<SelectListItem> ConsumerDurableSupplierDropdownList { get; }

        List<SelectListItem> CreditBureauAgencyDropdownList { get; }

        List<SelectListItem> CurrencyDropdownList { get; }

        List<SelectListItem> CustomerTypeDropdownList { get; }

        List<SelectListItem> CustomerAccountDropdownList { get; }

        List<SelectListItem> DaysInYearDropdownList { get; }

        // List<SelectListItem> DemandDepositGeneralLedgerDropdownList { get; }
        // List<SelectListItem> TermDepositGeneralLedgerDropdownList { get; }
        //List<SelectListItem> RecurringDepositGeneralLedgerDropdownList { get; }
        List<SelectListItem> GetGeneralLedgerDropdownListByParentAccountClassPrmKey(string _accountClassCode);

        List<SelectListItem> GetGeneralLedgerDropdownListByAccountClassCode(string _accountClassCode);

        List<SelectListItem> EducationalLoanGeneralLedgerDropdownList { get; }

        //List<SelectListItem> GoldLoanGeneralLedgerDropdownList { get; }

        //List<SelectListItem> ConsumerDurableLoanGeneralLedgerDropdownList { get; }

        //List<SelectListItem> GuarantorLoanGeneralLedgerDropdownList { get; }

        //List<SelectListItem> HomeLoanGeneralLedgerDropdownList { get; }

        //List<SelectListItem> LoanAgainstFixedDepositGeneralLedgerDropdownList { get; }

        //List<SelectListItem> LoanAgainstPropertyGeneralLedgerDropdownList { get; }

        //List<SelectListItem> VehicleLoanGeneralLedgerDropdownList { get; }

        //List<SelectListItem> BusinessLoanGeneralLedgerDropdownList { get; }

        List<SelectListItem> DemandDepositAccountHolderDropdownList { get; }

        List<SelectListItem> DepositGeneralLedgerAvailableForPledgeDropdownList { get; }

        List<SelectListItem> DepositInterestProvisionGeneralLedgerDropdownList { get; }

        List<SelectListItem> DepositSchemeDropdownList { get; }

        List<SelectListItem> CustomerAccountTypeDropdownList { get; }

        List<SelectListItem> DividendCalculationMethodDropdownList { get; }

        List<SelectListItem> FinancialAssetTypeDropdownList { get; }

        List<SelectListItem> FineInterestReceivedOnLoanGeneralLedgerDropdownList { get; }

        List<SelectListItem> FrequencyDropdownList { get; }

        List<SelectListItem> FundDropdownList { get; }

        List<SelectListItem> FundTransferFrequencyDropdownList { get; }

        List<SelectListItem> FurnitureAssetTypeDropdownList { get; }

        List<SelectListItem> GeneralLedgerDropdownList { get; }

        List<SelectListItem> GLParentDropdownList { get; }

        List<SelectListItem> InterestPaidOnDepositGeneralLedgerDropdownList { get; }

        List<SelectListItem> InterestReceivedOnLoanGeneralLedgerDropdownList { get; }

        List<SelectListItem> LoanInterestProvisonGeneralLedgerDropdownList { get; }

        List<SelectListItem> AgentCommissionGeneralLedgerDropdownList { get; }

        List<SelectListItem> GoldOrnamentDropdownList { get; }

        List<SelectListItem> InstallmentFrequencyDropdownList { get; }

        List<SelectListItem> GSTRegistrationTypeDropdownList { get; }

        List<SelectListItem> GSTReturnPeriodicityDropdownList { get; }

        List<SelectListItem> InterestCalculationFrequencyDropdownList { get; }

        List<SelectListItem> InterestCompoundingFrequencyDropdownList { get; }

        List<SelectListItem> InterestMethodDropdownList { get; }

        List<SelectListItem> InterestRateChargedDurationDropdownList { get; }

        List<SelectListItem> InterestRateTypeDropdownList { get; }

        List<SelectListItem> InterestRebateApplyFrequencyDropdownList { get; }

        List<SelectListItem> InterestRebateCriteriaDropdownList { get; }

        List<SelectListItem> InterestTypeDropdownList { get; }

        List<SelectListItem> JointAccountHolderTypeDropdownList { get; }

        List<SelectListItem> LendingChargesBaseDropdownList { get; }

        List<SelectListItem> LendingInterestMethodDropdownList { get; }

        List<SelectListItem> LendingInterestPostingFrequencyDropdownList { get; }

        List<SelectListItem> LendingRepaymentsInterestCalculationDropdownList { get; }

        List<SelectListItem> InterestRebateGeneralLedgerDropdownList { get; }

      //  List<SelectListItem> LoanGeneralLedgerDropdownList { get; }
        List<SelectListItem> LoanChargesGeneralLedgerDropdownList { get; }

        List<SelectListItem> LoanReasonDropdownList { get; }

        List<SelectListItem> LoanRecoveryActionDropdownList { get; }

        List<SelectListItem> LoanTypeDropdownList { get; }

        List<SelectListItem> OrganizationLoanTypeDropdownList { get; }

        List<SelectListItem> MemberTypeDropdownList { get; }

        List<SelectListItem> MinuteOfMeetingAgendaDropdownList { get; }

        List<SelectListItem> PayInPayOutModeDropdownList { get; }

        List<SelectListItem> PaymentCardDropdownList { get; }


        List<SelectListItem> RepaymentIntervalFrequencyDropdownList { get; }

        List<SelectListItem> RenewTypeDropdownList { get; }

        List<SelectListItem> SharesApplicationDropdownList { get; }

        List<SelectListItem> SchemeTypeDropdownList { get; }

        List<SelectListItem> SweepOutFrequencyDropdownList { get; }

        List<SelectListItem> TargetGroupDropdownList { get; }

        List<SelectListItem> TenureListDropdownList { get; }

       
        List<SelectListItem> TransactionTypeDropdownList { get; }

        List<SelectListItem> SharesCapitalSchemeDropdownList { get; }

        List<SelectListItem> SharesCapitalGeneralLedgerDropdownList { get; }

        List<SelectListItem> SharesTransferChargesGeneralLedgerDropdownList { get; }

        List<SelectListItem> VehicleBodyTypeDropdownList { get; }

        List<SelectListItem> VehicleTypeDropdownList { get; }

        List<SelectListItem> VehicleMakeDropdownList { get; }

        List<SelectListItem> VehicleModelDropdownList { get; }

        List<SelectListItem> VehicleVariantDropdownList { get; }

        List<SelectListItem> VehicleSupplierDropdownList { get; }

        List<SelectListItem> SchemeLoanAccountParameterDropdownList { get; }

        List<SelectListItem> ColourDropdownList { get; }
       
        List<SelectListItem> EducationalCourseDropdownList { get; }

        List<SelectListItem> InstituteDropdownList { get; }

        List<SelectListItem> GetAuthorizedLoanGLDropdownListForAccountOpening(Guid _businessOfficeId, Guid _loanTypeId);
        
        List<SelectListItem> GetAuthorizedSharesCapitalGLDropdownListForAccountOpening(Guid _businessOfficeId);
        
        List<SelectListItem> GetAuthorizedDepositGLDropdownListForAccountOpening(Guid _businessOfficeId, string _depositType);
       
        //List<SelectListItem> GetAuthorizedDepositGeneralLedgerDropdownList(Guid _businessOfficeId, string _depositType);
        
        List<SelectListItem> GetCustomerSavingAccountDropdownList(long _personPrmKey);
        
        List<SelectListItem> GetDocumentDropdownListByLoanType(string _sysNameLoanType);
        
        List<SelectListItem> GetDocumentDropdownListBySchemeId(Guid _schemeId);
        
        List<SelectListItem> GetGuarantorDropdownListBySchemeId(Guid _schemeId);
        
        List<SelectListItem> GetSchemeDropdownListByGeneralLedgerId(Guid _generalLedgerId);
        
       // List<SelectListItem> GetSchemeTenureDropdownListBySchemeId(Guid _schemeId);
        
        List<SelectListItem> GetLoanSchemeDropdownListByLoanTypeId(Guid _loanTypeId);
        
        List<SelectListItem> GetVehicleVariantDropdownListByVehicleModelId(Guid _vehicleModelId);
        
        List<SelectListItem> GetVehicleModelDropdownListByVehicleMakeId(Guid _vehicleMakeId);
        
        List<SelectListItem> GetVehicleCompanyDropdownListByVehicleTypeId(short _schemePrmKey);

        List<SelectListItem> GetEducationalCourseDropdownListBySchemePrmKey(short _schemePrmKey, bool _isApplicableAllUniversities);

        List<SelectListItem> GetInstituteDropdownListBySchemePrmKey(short _schemePrmKey, bool _isApplicableAllCourse);

        //List<SelectListItem> GetFixedDepositCustomerListByPersonPrmKey(Guid _personId);
        List<SelectListItem> GetDepositAccountDropdownListByScheme(Guid _schemeId, Guid _personId);
          
        List<SelectListItem> GetConsumerDurableLoanItemDropdownListBySchemePrmKey(short _schemePrmKey);

        
        //List<SelectListItem> GetPersonDropdownListForLoanAccountOpening(short _schemePrmkey);

        SchemeClosingChargesViewModel GetSchemeClosingCharges(short _schemePrmkey);

        SchemeSharesTransferChargesViewModel GetSchemeSharesTransferCharges(short _schemePrmKey);

        //GoldLoanRate GetGoldLoanRate(DateTime _dateTime, string _purity);

        //SchemeVehicleLoanParameterViewModel GetSchemeVehicleLoanParameterValidations(Guid _schemeId);

    }
}
