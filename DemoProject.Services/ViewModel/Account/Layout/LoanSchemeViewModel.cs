using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class LoanSchemeViewModel
    {
        public short PrmKey { get; set; }

        public Guid SchemeId { get; set; }

        [StringLength(100)]
        public string NameOfScheme { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        public byte SchemeTypePrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //SchemeMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //SchemeModification
        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //SchemeModificationMakerChecker
        public short SchemeModificationPrmKey { get; set; }

        //SchemeTranslation
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(100)]
        public string TransNameOfScheme { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        //SchemeTranslationMakerChecker
        public short SchemeTranslationPrmKey { get; set; }

        // SchemeAccountParameter 
        public SchemeAccountParameterViewModel SchemeAccountParameterViewModel { get; set; }

        // SchemeLoanAccountParameter
        public SchemeLoanAccountParameterViewModel SchemeLoanAccountParameterViewModel { get; set; }

        // SchemeAccountBankingChannelParameter
        public SchemeAccountBankingChannelParameterViewModel SchemeAccountBankingChannelParameterViewModel { get; set; }

        // SchemeCustomerAccountNumber 
        public SchemeCustomerAccountNumberViewModel SchemeCustomerAccountNumberViewModel { get; set; }

        // SchemeTenure
        public SchemeTenureViewModel SchemeTenureViewModel { get; set; }

        // SchemeTenureList
        public SchemeTenureListViewModel SchemeTenureListViewModel { get; set; }

        // SchemeApplicationParameter 
        public SchemeApplicationParameterViewModel SchemeApplicationParameterViewModel { get; set; }

        // SchemeDocumentType   DT
        public SchemeDocumentViewModel SchemeDocumentViewModel { get; set; }

        // SchemeNoticeSchedule   DT
        public SchemeNoticeScheduleViewModel SchemeNoticeScheduleViewModel { get; set; }

        // SchemeReportFormat MS/DT
        public SchemeReportFormatViewModel SchemeReportFormatViewModel { get; set; }

        // SchemeEstimateTarget
        public SchemeEstimateTargetViewModel SchemeEstimateTargetViewModel { get; set; }

        // SchemeBusinessOffice   DT
        public SchemeBusinessOfficeViewModel SchemeBusinessOfficeViewModel { get; set; }

        // SchemePassbook
        public SchemePassbookViewModel SchemePassbookViewModel { get; set; }

        // SchemeTargetGroup   *****   Function Not Created
        public SchemeTargetGroupViewModel SchemeTargetGroupViewModel { get; set; }

        // SchemeNumberOfTransactionLimit  DT
        public SchemeNumberOfTransactionLimitViewModel SchemeNumberOfTransactionLimitViewModel { get; set; }

        // SchemeTransactionAmountLimit  DT
        public SchemeTransactionAmountLimitViewModel SchemeTransactionAmountLimitViewModel { get; set; }

        // SchemeLoanRepaymentScheduleParameter
        public SchemeLoanRepaymentScheduleParameterViewModel SchemeLoanRepaymentScheduleParameterViewModel { get; set; }

        // SchemeLoanSettlementAccountParameter
        public SchemeLoanSettlementAccountParameterViewModel SchemeLoanSettlementAccountParameterViewModel { get; set; }

        // SchemeLoanInterestParameterId
        public SchemeLoanInterestParameterViewModel SchemeLoanInterestParameterViewModel { get; set; }

        // SchemeInterestRateViewModel 
        public SchemeInterestRateViewModel SchemeInterestRateViewModel { get; set; }

        // SchemeLoanFineInterestParameterViewModel
        public SchemeLoanFineInterestParameterViewModel SchemeLoanFineInterestParameterViewModel { get; set; }

        // SchemeLoanInterestProvisionParameterViewModel
        public SchemeLoanInterestProvisionParameterViewModel SchemeLoanInterestProvisionParameterViewModel { get; set; }

        // SchemeLoanDistributorParameter 
        public SchemeLoanDistributorParameterViewModel SchemeLoanDistributorParameterViewModel { get; set; }

        // SchemeLoanArrearParameter
        public SchemeLoanArrearParameterViewModel SchemeLoanArrearParameterViewModel { get; set; }

        // SchemeLoanChargesParameter  DT
        public SchemeLoanChargesParameterViewModel SchemeLoanChargesParameterViewModel { get; set; }

        // SchemeLoanInterestRebateParameter  *** Complete JS ***
        public SchemeLoanInterestRebateParameterViewModel SchemeLoanInterestRebateParameterViewModel { get; set; }

        // SchemeLoanTransactionParameter
        public SchemeLoanInstallmentParameterViewModel SchemeLoanInstallmentParameterViewModel { get; set; }

        // SchemeLoanFunderParameter
        public SchemeLoanFunderParameterViewModel SchemeLoanFunderParameterViewModel { get; set; }

        // SchemeLoanOverduesAction
        public SchemeLoanOverduesActionViewModel SchemeLoanOverduesActionViewModel { get; set; }

        // SchemePreFullPaymentParameter
        public SchemeLoanPreFullPaymentParameterViewModel SchemeLoanPreFullPaymentParameterViewModel { get; set; }

        // SchemePrePartPaymentParameter
        public SchemeLoanPrePartPaymentParameterViewModel SchemeLoanPrePartPaymentParameterViewModel  { get; set; }

        // SchemeLoanAgreementNumberViewModel
        public SchemeLoanAgreementNumberViewModel SchemeLoanAgreementNumberViewModel { get; set; }

        // SchemeLoanSanctionAuthorityViewModel
        public SchemeLoanSanctionAuthorityViewModel SchemeLoanSanctionAuthorityViewModel { get; set; }

        // SchemeCashCreditLoanParameterViewModel
        public SchemeCashCreditLoanParameterViewModel SchemeCashCreditLoanParameterViewModel { get; set; }

        // SchemeEducationLoanParameterViewModel
        public SchemeEducationLoanParameterViewModel SchemeEducationLoanParameterViewModel { get; set; }

        // SchemeEducationalCourseViewModel
        public SchemeEducationalCourseViewModel SchemeEducationalCourseViewModel { get; set; }

        // SchemeInstituteViewModel
        public SchemeInstituteViewModel SchemeInstituteViewModel { get; set; }

        //  SchemeGeneralLedgerViewModel
        public SchemeGeneralLedgerViewModel SchemeGeneralLedgerViewModel { get; set; }

        // SchemeLoanRecoveryActionViewModel DT
        public SchemeLoanRecoveryActionViewModel SchemeLoanRecoveryActionViewModel { get; set; }

        //LoanPaymentReminderParameter
        public SchemeLoanPaymentReminderParameterViewModel SchemeLoanPaymentReminderParameterViewModel { get; set; }

        //PreownedVehicleLoanParameter
        public SchemePreownedVehicleLoanParameterViewModel SchemePreownedVehicleLoanParameterViewModel { get; set; }

        //VehicleTypeLoanParameter DT
        public SchemeVehicleTypeLoanParameterViewModel SchemeVehicleTypeLoanParameterViewModel { get; set; }

        //SchemeConsumerDurableLoanItemViewModel DT
        public SchemeConsumerDurableLoanItemViewModel SchemeConsumerDurableLoanItemViewModel { get; set; }

        //SchemeLoanAgainstDepositGeneralLedgerViewModel
        public SchemeLoanAgainstDepositGeneralLedgerViewModel SchemeLoanAgainstDepositGeneralLedgerViewModel { get; set; }

        //SchemeLoanAgainstDepositParameterViewModel
        public SchemeLoanAgainstDepositParameterViewModel SchemeLoanAgainstDepositParameterViewModel { get; set; }

        // SchemeGoldLoanParameterViewModel
        public SchemeGoldLoanParameterViewModel SchemeGoldLoanParameterViewModel { get; set; }

        //SchemeHomeLoanViewModel
        public SchemeHomeLoanViewModel SchemeHomeLoanViewModel { get; set; }

        //SchemeLoanAgainstPropertyViewModel
        public SchemeLoanAgainstPropertyViewModel SchemeLoanAgainstPropertyViewModel { get; set; }

        //SchemeBusinessLoanViewModel
        public SchemeBusinessLoanViewModel SchemeBusinessLoanViewModel { get; set; }
        
        //Other
        public string[] MaskTypeCharacterForApplication { get; set; }

        public string[] MaskTypeCharacterForAccount { get; set; }

        public string[] MaskTypeCharacterForCertificate { get; set; }

        public string[] MaskTypeCharacterForMember { get; set; }

        public bool EnableMaskForMemberNumber { get; set; }

        public bool EnableMaskForAccountNumber { get; set; }

        public bool EnableLoanInstallment { get; set; }

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        // Dropdown

        public Guid AgentCommissionGeneralLedgerId { get; set; }

        public Guid BankingChannelId { get; set; }

        [StringLength(100)]
        public string NameOfBankingChannel { get; set; }

        public Guid ChequeReturnReasonId { get; set; }

        [StringLength(100)]
        public string NameOfChequeReturnReason { get; set; }

        public Guid FrequencyId { get; set; }

        [StringLength(100)]
        public string NameOfFrequency { get; set; }

        public Guid GeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfGL { get; set; }

        public Guid InterestTypeId { get; set; }

        [StringLength(100)]
        public string NameOfInterestType { get; set; }

        public Guid InterestRateTypeId { get; set; }

        [StringLength(100)]
        public string NameOfInterestRateType { get; set; }

        public Guid TransactionTypeId { get; set; }

        [StringLength(100)]
        public string NameOfTransactionType { get; set; }

        public Guid PaymentCardId { get; set; }

        [StringLength(100)]
        public string NameOfPaymentCard { get; set; }

    }
}
