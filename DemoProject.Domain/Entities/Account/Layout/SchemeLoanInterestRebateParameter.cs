using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanInterestRebateParameter")]
    public partial class SchemeLoanInterestRebateParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanInterestRebateParameter()
        {
            SchemeLoanInterestRebateParameterMakerCheckers = new HashSet<SchemeLoanInterestRebateParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte InterestRebateCriteriaPrmKey { get; set; }

        public byte InterestRebateApplyFrequencyPrmKey { get; set; }

        public bool IsApplicablePrePartPaymentForInterestRebate { get; set; }

        public bool IsApplicableForeClosureForInterestRebate { get; set; }

        public byte MinimumDuesInstallmentGracePeriodInDays { get; set; }

        public byte MaximumDuesInstallmentGracePeriodInDays { get; set; }

        public byte MinimumApplicableNumberOfLateInstallmentForInterestRebate { get; set; }

        public byte MaximumApplicableNumberOfLateInstallmentForInterestRebate { get; set; }

        public decimal InterestRebatePercentage { get; set; }

        public bool EnableManualOptionToSelectCustomerAccountForInterestRebate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanInterestRebateParameterMakerChecker> SchemeLoanInterestRebateParameterMakerCheckers { get; set; }
    }
}
