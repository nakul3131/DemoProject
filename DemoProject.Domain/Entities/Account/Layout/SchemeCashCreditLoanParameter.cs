using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeCashCreditLoanParameter")]
    public partial class SchemeCashCreditLoanParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeCashCreditLoanParameter()
        {
            SchemeCashCreditLoanParameterMakerCheckers = new HashSet<SchemeCashCreditLoanParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool IsRequiredDemandDepositAccount { get; set; }

        public bool EnableFixedDepositAsCollateral { get; set; }

        public bool EnableRealEstateAsCollateral { get; set; }

        public bool EnableExtraCollateral { get; set; }
        
        public byte SanctionLoanAndTurnOverProportion { get; set; }

        public decimal MarginBetweenStockAndWithdrawal { get; set; }

        public bool EnableFineInterestAfterMaturity { get; set; }

        public bool IsRequiredStockList { get; set; }

        public byte BalanceConfirmationCertificateTimePeriod { get; set; }

        public byte PastFinancialYearStatements { get; set; }

        public byte PastIncomeTaxReturnStatements { get; set; }

        public byte PastAssesmentOrders { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousYearTurnOver { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousSecondYearTurnOver { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousThirdYearTurnOver { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousYearGrossProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousSecondYearGrossProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousThirdYearGrossProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousYearNetProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousSecondYearNetProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string CapturePreviousThirdYearNetProfitMargin { get; set; }

        [Required]
        [StringLength(1)]
        public string DebtEquityRatio { get; set; }

        [Required]
        [StringLength(1)]
        public string WorkingCapitalCycle { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeCashCreditLoanParameterMakerChecker> SchemeCashCreditLoanParameterMakerCheckers { get; set; }

    }
}
