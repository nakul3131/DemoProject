using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanAgainstDepositParameter")]
    public partial class SchemeLoanAgainstDepositParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanAgainstDepositParameter()
        {
            SchemeLoanAgainstDepositGeneralLedgers = new HashSet<SchemeLoanAgainstDepositGeneralLedger>();
            SchemeLoanAgainstDepositParameterMakerCheckers = new HashSet<SchemeLoanAgainstDepositParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string DepositType { get; set; }

        public bool IsApplicableAllGeneralLedgers { get; set; }

        public bool IsOverDraftLoan { get; set; }

        public bool IsTakenAsCollateralSecurity { get; set; }

        public bool IsAllowAutoRenew { get; set; }

        public bool IsAllowAutoClosure { get; set; }

        public bool EnableMaximumTenureUptoMaturityDate { get; set; }

        public decimal Margin { get; set; }

        public short MinimumDepositAgeForPledge { get; set; }

        public short MinimumDepositMaturityAgeForPledge { get; set; }

        public decimal MinimumAdditionalInterestRate { get; set; }

        public decimal MaximumAdditionalInterestRate { get; set; }

        public bool EnableThirdPersonDepositAttachment { get; set; }

        public decimal MinimumAdditionalInterestRateForThirdPersonDeposit { get; set; }

        public decimal MaximumAdditionalInterestRateForThirdPersonDeposit { get; set; }

        public byte InterestCalculationFrequencyPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanAgainstDepositGeneralLedger> SchemeLoanAgainstDepositGeneralLedgers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanAgainstDepositParameterMakerChecker> SchemeLoanAgainstDepositParameterMakerCheckers { get; set; }
    }
}
