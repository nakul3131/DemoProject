using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SharesCapitalSchemeViewModel
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

        //SchemeTranslation

        public short LanguagePrmKey { get; set; }

        [StringLength(100)]
        public string TransNameOfScheme { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        //SchemeTranslationMakerChecker
        public short SchemeTranslationPrmKey { get; set; }

        // SchemeAccountParameterViewModel
        public SchemeAccountParameterViewModel SchemeAccountParameterViewModel { get; set; }

        // SchemeSharesCapitalAccountParameterViewModel
        public SchemeSharesCapitalAccountParameterViewModel SchemeSharesCapitalAccountParameterViewModel { get; set; }

        // SchemeCustomerAccountNumberViewModel
        public SchemeCustomerAccountNumberViewModel SchemeCustomerAccountNumberViewModel { get; set; }

        // SchemeApplicationParameterViewModel
        public SchemeApplicationParameterViewModel SchemeApplicationParameterViewModel { get; set; }

        // SchemeSharesCertificateParameterViewModel
        public SchemeSharesCertificateParameterViewModel SchemeSharesCertificateParameterViewModel { get; set; }

        // SchemeSharesCapitalDividendParameterViewModel
        public SchemeSharesCapitalDividendParameterViewModel SchemeSharesCapitalDividendParameterViewModel { get; set; }

        // SchemeEstimateTargetViewModel
        public SchemeEstimateTargetViewModel SchemeEstimateTargetViewModel { get; set; }

        // SchemeLimitViewModel
        public SchemeLimitViewModel SchemeLimitViewModel { get; set; }

        // SchemeAccountBankingChannelParameterViewModel
        public SchemeAccountBankingChannelParameterViewModel SchemeAccountBankingChannelParameterViewModel { get; set; }

        // SchemeBusinessOfficeViewModel
        public SchemeBusinessOfficeViewModel SchemeBusinessOfficeViewModel { get; set; }

        //SchemeGeneralLedgerViewModel
        public SchemeGeneralLedgerViewModel SchemeGeneralLedgerViewModel { get; set; }

        // SchemeChargesDetailViewModel
        public SchemeClosingChargesViewModel SchemeClosingChargesViewModel { get; set; }

        // SchemeNoticeScheduleViewModel
        public SchemeNoticeScheduleViewModel SchemeNoticeScheduleViewModel { get; set; }

        // SchemeReportFormatViewModel
        public SchemeReportFormatViewModel SchemeReportFormatViewModel { get; set; }

        // SchemeSharesTransferChargesViewModel
        public SchemeSharesTransferChargesViewModel SchemeSharesTransferChargesViewModel { get; set; }

        // Other
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
