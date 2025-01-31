using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class DepositSchemeViewModel
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

        //SchemeAccountParameter N
        public SchemeAccountParameterViewModel SchemeAccountParameterViewModel { get; set; }

        //SchemeDepositAccountParameter N
        public SchemeDepositAccountParameterViewModel SchemeDepositAccountParameterViewModel { get; set; }

        //SchemeApplicationParameter N
        public SchemeApplicationParameterViewModel SchemeApplicationParameterViewModel { get; set; }

        //SchemeAccountBankingChannelParameter N
        public SchemeAccountBankingChannelParameterViewModel SchemeAccountBankingChannelParameterViewModel { get; set; }

        //SchemeLimit N
        public SchemeLimitViewModel SchemeLimitViewModel { get; set; }

        //SchemeEstimateTarget N
        public SchemeEstimateTargetViewModel SchemeEstimateTargetViewModel { get; set; }

        //SchemeReportFormat N
        public SchemeReportFormatViewModel SchemeReportFormatViewModel { get; set; }

        //SchemeNoticeSchedule N
        public SchemeNoticeScheduleViewModel SchemeNoticeScheduleViewModel { get; set; }

        public SchemeClosingChargesViewModel SchemeClosingChargesViewModel { get; set; }

        //SchemeCustomerAccountNumber N
        public SchemeCustomerAccountNumberViewModel SchemeCustomerAccountNumberViewModel { get; set; }

        //SchemeDepositInstallmentParameter N
        public SchemeDepositInstallmentParameterViewModel SchemeDepositInstallmentParameterViewModel { get; set; }

        //SchemeDepositAgentParameter N
        public SchemeDepositAgentParameterViewModel SchemeDepositAgentParameterViewModel { get; set; }

        //SchemeDepositAgentIncentive D
        public SchemeDepositAgentIncentiveViewModel SchemeDepositAgentIncentiveViewModel { get; set; }

        //SchemeDepositInterestParameter D
        public SchemeDepositInterestParameterViewModel SchemeDepositInterestParameterViewModel { get; set; }

        public SchemeDepositInterestProvisionParameterViewModel SchemeDepositInterestProvisionParameterViewModel { get; set; }

        //SchemeNumberOfTransactionLimit D
        public SchemeNumberOfTransactionLimitViewModel SchemeNumberOfTransactionLimitViewModel { get; set; }

        //SchemeTransactionAmountLimit D
        public SchemeTransactionAmountLimitViewModel SchemeTransactionAmountLimitViewModel { get; set; }

        // SchemeDepositCertificateParameter N 
        public SchemeDepositCertificateParameterViewModel SchemeDepositCertificateParameterViewModel { get; set; }

        // SchemeDemandDepositDetail N 
        public SchemeDemandDepositDetailViewModel SchemeDemandDepositDetailViewModel { get; set; }

        // SchemeFixedDepositParameter N 
        public SchemeFixedDepositParameterViewModel SchemeFixedDepositParameterViewModel { get; set; }

        // SchemeFixedDepositParameter N 
        public SchemeDepositAccountRenewalParameterViewModel SchemeDepositAccountRenewalParameterViewModel { get; set; }

        //SchemePassbook N
        public SchemePassbookViewModel SchemePassbookViewModel { get; set; }

        //SchemeDepositAccountClosureParameter N
        public SchemeDepositAccountClosureParameterViewModel SchemeDepositAccountClosureParameterViewModel { get; set; }

        //SchemeTenureList N
        public SchemeTenureListViewModel SchemeTenureListViewModel { get; set; }

        // SchemeTenure
        public SchemeTenureViewModel SchemeTenureViewModel { get; set; }

        //SchemeGeneralLedgerViewModel
        public SchemeGeneralLedgerViewModel SchemeGeneralLedgerViewModel { get; set; }

        // SchemeBusinessOffice
        public SchemeBusinessOfficeViewModel SchemeBusinessOfficeViewModel { get; set; }

        public SchemeTargetGroupViewModel SchemeTargetGroupViewModel { get; set; }

        public bool EnableMaskForMemberNumber { get; set; }

        public bool EnableMaskForAccountNumber { get; set; }

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        // Dropdown

        //public Guid SchemeTypeId { get; set; }
    }
}
