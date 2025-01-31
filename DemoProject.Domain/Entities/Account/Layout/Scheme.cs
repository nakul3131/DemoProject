using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("Scheme")]
    public partial class Scheme
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Scheme()
        {
            SchemeAccountParameters = new HashSet<SchemeAccountParameter>();
            SchemeApplicationParameters = new HashSet<SchemeApplicationParameter>();
            SchemeBusinessOffices = new HashSet<SchemeBusinessOffice>();
            SchemeGeneralLedgers = new HashSet<SchemeGeneralLedger>();
            SchemeChargesDetails = new HashSet<SchemeChargesDetail>();
            SchemeCustomerAccountNumbers = new HashSet<SchemeCustomerAccountNumber>();
            SchemeDepositAccountParameters = new HashSet<SchemeDepositAccountParameter>();
            SchemeDepositAgentIncentives = new HashSet<SchemeDepositAgentIncentive>();
            SchemeDepositAgentParameters = new HashSet<SchemeDepositAgentParameter>();
            SchemeDepositCertificateParameters = new HashSet<SchemeDepositCertificateParameter>();
            SchemeDepositInstallmentParameters = new HashSet<SchemeDepositInstallmentParameter>();
            SchemeDepositInterestParameters = new HashSet<SchemeDepositInterestParameter>();
            SchemeDepositInterestProvisionParameters = new HashSet<SchemeDepositInterestProvisionParameter>();
            SchemeDepositInterestPayoutFrequencies = new HashSet<SchemeDepositInterestPayoutFrequency>();
            SchemeEstimateTargets = new HashSet<SchemeEstimateTarget>();
            SchemeLimits = new HashSet<SchemeLimit>();
            SchemeMakerCheckers = new HashSet<SchemeMakerChecker>();
            SchemeNumberOfTransactionLimits = new HashSet<SchemeNumberOfTransactionLimit>();
            SchemePaymentCardFeatures = new HashSet<SchemePaymentCardFeature>();
            SchemeSharesCapitalAccountParameters = new HashSet<SchemeSharesCapitalAccountParameter>();
            SchemeSharesCapitalDividendParameters = new HashSet<SchemeSharesCapitalDividendParameter>();
            SchemeSharesCertificateParameters = new HashSet<SchemeSharesCertificateParameter>();
            SchemeTransactionAmountLimits = new HashSet<SchemeTransactionAmountLimit>();
            SchemeTranslations = new HashSet<SchemeTranslation>();
            SchemeFixedDepositParameters = new HashSet<SchemeFixedDepositParameter>();
            SchemeDepositAccountRenewalParameters = new HashSet<SchemeDepositAccountRenewalParameter>();
            SchemeDepositPledgeLoanParameters = new HashSet<SchemeDepositPledgeLoanParameter>();
            SchemeDemandDepositDetails = new HashSet<SchemeDemandDepositDetail>();
            SchemeDepositClosingModes = new HashSet<SchemeDepositClosingMode>();
            SchemeTermDepositDetails = new HashSet<SchemeTermDepositDetail>();
            SchemeInterestPayoutFrequencies = new HashSet<SchemeInterestPayoutFrequency>();
            SchemeTenureLists = new HashSet<SchemeTenureList>();
            SchemeAccountBankingChannelParameters = new HashSet<SchemeAccountBankingChannelParameter>();
            SchemeLoanChargesParameters = new HashSet<SchemeLoanChargesParameter>();
            SchemeLoanDistributorParameters = new HashSet<SchemeLoanDistributorParameter>();
            SchemeLoanFunderParameters = new HashSet<SchemeLoanFunderParameter>();
            SchemeLoanOverduesActions = new HashSet<SchemeLoanOverduesAction>();
            SchemeLoanInstallmentParameters = new HashSet<SchemeLoanInstallmentParameter>();
            SchemeDocuments = new HashSet<SchemeDocument>();
            SchemeNoticeSchedules = new HashSet<SchemeNoticeSchedule>();
            SchemeReportFormats = new HashSet<SchemeReportFormat>();
            SchemeTargetGroups = new HashSet<SchemeTargetGroup>();
            SchemeLoanAccountParameters = new HashSet<SchemeLoanAccountParameter>();
            SchemeLoanAgreementNumbers = new HashSet<SchemeLoanAgreementNumber>();
            SchemeLoanArrearParameters = new HashSet<SchemeLoanArrearParameter>();
            SchemeLoanInterestParameters = new HashSet<SchemeLoanInterestParameter>();
            SchemeLoanSanctionAuthorities = new HashSet<SchemeLoanSanctionAuthority>();
            SchemeCashCreditLoanParameters = new HashSet<SchemeCashCreditLoanParameter>();
            SchemeEducationLoanParameters = new HashSet<SchemeEducationLoanParameter>();
            SchemeEducationalCourses = new HashSet<SchemeEducationalCourse>();
            SchemeInstitutes = new HashSet<SchemeInstitute>();
            SchemeHomeLoans = new HashSet<SchemeHomeLoan>();
            SchemeLoanAgainstProperties = new HashSet<SchemeLoanAgainstProperty>();
            SchemeBusinessLoans = new HashSet<SchemeBusinessLoan>();
            SchemeLoanRecoveryActions = new HashSet<SchemeLoanRecoveryAction>();
            SchemeLoanPaymentReminderParameters = new HashSet<SchemeLoanPaymentReminderParameter>();
            SchemeVehicleTypeLoanParameters = new HashSet<SchemeVehicleTypeLoanParameter>();
            SchemePreownedVehicleLoanParameters = new HashSet<SchemePreownedVehicleLoanParameter>();
            SchemeConsumerDurableLoanItems = new HashSet<SchemeConsumerDurableLoanItem>();
            SchemeLoanAgainstDepositParameters = new HashSet<SchemeLoanAgainstDepositParameter>();
            SchemeInterestRates = new HashSet<SchemeInterestRate>();
            SchemeLoanFineInterestParameters = new HashSet<SchemeLoanFineInterestParameter>();
            SchemeLoanInterestProvisionParameters = new HashSet<SchemeLoanInterestProvisionParameter>();
            SchemeLoanRepaymentScheduleParameters = new HashSet<SchemeLoanRepaymentScheduleParameter>();
            SchemeLoanSettlementAccountParameters = new HashSet<SchemeLoanSettlementAccountParameter>();
            SchemeLoanInterestRebateParameters = new HashSet<SchemeLoanInterestRebateParameter>();
            SchemePassbooks = new HashSet<SchemePassbook>();
            SchemePreFullPaymentParameters = new HashSet<SchemeLoanPreFullPaymentParameter>();
            SchemeTenures = new HashSet<SchemeTenure>();
            SchemeTypes = new HashSet<SchemeType>();
            SchemePrePartPaymentParameters = new HashSet<SchemeLoanPrePartPaymentParameter>();
            SchemeDepositAccountClosureParameters = new HashSet<SchemeDepositAccountClosureParameter>();
            SchemeGoldLoanParameters = new HashSet<SchemeGoldLoanParameter>();
            SchemeClosingCharges = new HashSet<SchemeClosingCharges>();
            SchemeSharesTransferCharges = new HashSet<SchemeSharesTransferCharges>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid SchemeId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfScheme { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public byte SchemeTypePrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeAccountParameter> SchemeAccountParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeApplicationParameter> SchemeApplicationParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeBusinessOffice> SchemeBusinessOffices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeGeneralLedger> SchemeGeneralLedgers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeChargesDetail> SchemeChargesDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeCustomerAccountNumber> SchemeCustomerAccountNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositAccountParameter> SchemeDepositAccountParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositAgentIncentive> SchemeDepositAgentIncentives { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositAgentParameter> SchemeDepositAgentParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositCertificateParameter> SchemeDepositCertificateParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositInstallmentParameter> SchemeDepositInstallmentParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositInterestParameter> SchemeDepositInterestParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositInterestProvisionParameter> SchemeDepositInterestProvisionParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositInterestPayoutFrequency> SchemeDepositInterestPayoutFrequencies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeEstimateTarget> SchemeEstimateTargets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLimit> SchemeLimits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeMakerChecker> SchemeMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeNumberOfTransactionLimit> SchemeNumberOfTransactionLimits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemePaymentCardFeature> SchemePaymentCardFeatures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeSharesCapitalAccountParameter> SchemeSharesCapitalAccountParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeSharesCapitalDividendParameter> SchemeSharesCapitalDividendParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeSharesCertificateParameter> SchemeSharesCertificateParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTransactionAmountLimit> SchemeTransactionAmountLimits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTranslation> SchemeTranslations { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDemandDepositDetail> SchemeDemandDepositDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositClosingMode> SchemeDepositClosingModes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTermDepositDetail> SchemeTermDepositDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeInterestPayoutFrequency> SchemeInterestPayoutFrequencies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTenureList> SchemeTenureLists { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeAccountBankingChannelParameter> SchemeAccountBankingChannelParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanChargesParameter> SchemeLoanChargesParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanDistributorParameter> SchemeLoanDistributorParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanFunderParameter> SchemeLoanFunderParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanOverduesAction> SchemeLoanOverduesActions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanInstallmentParameter> SchemeLoanInstallmentParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDocument> SchemeDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeNoticeSchedule> SchemeNoticeSchedules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeReportFormat> SchemeReportFormats { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTargetGroup> SchemeTargetGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanAccountParameter> SchemeLoanAccountParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanAgreementNumber> SchemeLoanAgreementNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanArrearParameter> SchemeLoanArrearParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanInterestParameter> SchemeLoanInterestParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanSanctionAuthority> SchemeLoanSanctionAuthorities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanPaymentReminderParameter> SchemeLoanPaymentReminderParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeCashCreditLoanParameter> SchemeCashCreditLoanParameters { get; set; }

       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeEducationLoanParameter> SchemeEducationLoanParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeEducationalCourse> SchemeEducationalCourses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeInstitute> SchemeInstitutes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeHomeLoan> SchemeHomeLoans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanAgainstProperty> SchemeLoanAgainstProperties { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeBusinessLoan> SchemeBusinessLoans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanRecoveryAction> SchemeLoanRecoveryActions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemePreownedVehicleLoanParameter> SchemePreownedVehicleLoanParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeVehicleTypeLoanParameter> SchemeVehicleTypeLoanParameters { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeConsumerDurableLoanItem> SchemeConsumerDurableLoanItems { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanAgainstDepositParameter> SchemeLoanAgainstDepositParameters { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeInterestRate> SchemeInterestRates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanFineInterestParameter> SchemeLoanFineInterestParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanInterestProvisionParameter> SchemeLoanInterestProvisionParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanRepaymentScheduleParameter> SchemeLoanRepaymentScheduleParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanSettlementAccountParameter> SchemeLoanSettlementAccountParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanInterestRebateParameter> SchemeLoanInterestRebateParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemePassbook> SchemePassbooks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanPreFullPaymentParameter> SchemePreFullPaymentParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeGoldLoanParameter> SchemeGoldLoanParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTenure> SchemeTenures { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeType> SchemeTypes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanPrePartPaymentParameter> SchemePrePartPaymentParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeFixedDepositParameter> SchemeFixedDepositParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositAccountRenewalParameter> SchemeDepositAccountRenewalParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositPledgeLoanParameter> SchemeDepositPledgeLoanParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositAccountClosureParameter> SchemeDepositAccountClosureParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeClosingCharges> SchemeClosingCharges { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeSharesTransferCharges> SchemeSharesTransferCharges { get; set; }
    }
}
