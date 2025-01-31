using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanAccountParameter")]
    public partial class SchemeLoanAccountParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanAccountParameter()
        {
            SchemeLoanAccountParameterMakerCheckers = new HashSet<SchemeLoanAccountParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public byte LoanTypePrmKey { get; set; }

        public bool EnableAgreementNumber { get; set; }

        public bool IsRequiredOrdinaryMembership { get; set; }

        public bool IsRequiredNominalMembership { get; set; }

        public bool IsRequiredSavingAccount { get; set; }

        public bool EnableLoanDisbursementAutoEntryToCustomerSavingsAc { get; set; }

        public decimal SharesRatioWithLoan { get; set; }

        public bool EnableGuarantorDetail { get; set; }

        public byte MinimumNumberOfGuarantors { get; set; }

        public byte MaximumNumberOfGuarantors { get; set; }

        public byte DefaultNumberOfGuarantors { get; set; }

        [Required]
        [StringLength(3)]
        public string EligibilityForGuarantor { get; set; }

        public byte NumberOfLoansLimitForSameGuarantors { get; set; }

        public bool EnableAddingGuarantor { get; set; }

        public bool EnableRemovingGuarantor { get; set; }

        public bool EnableRemovingGuarantorOnlyIfNotRequired { get; set; }

        public byte MinimumAge { get; set; }

        public byte MaximumAge { get; set; }

        public bool EnableAcquaintanceDetails { get; set; }

        public byte MinimumAcquaintance { get; set; }

        public byte MaximumAcquaintance { get; set; }

        public bool EnableDepositAsCollateral { get; set; }

        public bool EnableSWOTAnalysis { get; set; }

        public short SWOTAnalysisMinimumLength { get; set; }

        public bool EnablePastCreditHistory { get; set; }

        public short PastCreditHistoryMinimumLength { get; set; }

        public bool EnableLegalAndRegulatoryCompliance { get; set; }

        public short LegalAndRegulatoryComplianceMinimumLength { get; set; }

        public bool EnableFunder { get; set; }

        public bool EnableFundingAccountsLockAfterDisbursment { get; set; }

        public bool EnableDistributor { get; set; }

        public bool EnableBlockingDebitsAfterDisbursement { get; set; }

        public bool EnableSettlementAccount { get; set; }

        public bool EnablePrePayment { get; set; }

        public bool EnableForeClosure { get; set; }

        public bool EnableRebateInterest { get; set; }

        public bool EnableLatePaymentInterest { get; set; }

        public bool EnableFieldInvestigation { get; set; }

        public bool EnableCaptureCIBILScore { get; set; }

        public short MinimumCIBILScore { get; set; }

        public short MaximumCIBILScore { get; set; }

        public bool EnableCaptureDebtToIncomeRatio { get; set; }

        public byte MinimumDebtToIncomeRatio { get; set; }

        public byte MaximumDebtToIncomeRatio { get; set; }

        public bool EnableLegalSolicitorDetail { get; set; }

        public bool EnablePostDatedCheques { get; set; }

        public bool EnablePaymentGetway { get; set; }

        public bool EnableECS { get; set; }

        public bool EnableLinkingMultipleCollateralForSingleLoan { get; set; }

        public bool EnableLinkingMultipleLoanForSingleCollateral { get; set; }

        public bool EnableLinkingSingleCollateralToMultipleCustomer { get; set; }

        public bool EnableAdditionalIncomeDetail { get; set; }

        public bool EnableBorrowingDetail { get; set; }

        public bool EnableCourtCaseDetail { get; set; }

        public bool EnableIncomeTaxDetail { get; set; }

        public decimal MinimumLoanAmountForIndividual { get; set; }

        public decimal MaximumLoanAmountForIndividual { get; set; }

        public decimal MinimumLoanAmountForGroup { get; set; }

        public decimal MaximumLoanAmountForGroup { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanAccountParameterMakerChecker> SchemeLoanAccountParameterMakerCheckers { get; set; }
    }
}
