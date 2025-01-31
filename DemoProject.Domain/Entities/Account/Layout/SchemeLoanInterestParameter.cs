using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanInterestParameter")]
    public partial class SchemeLoanInterestParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanInterestParameter()
        {
            SchemeLoanInterestParameterMakerCheckers = new HashSet<SchemeLoanInterestParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte InterestMethodPrmKey { get; set; }

        public decimal MinimumInterestRate { get; set; }

        public decimal MaximumInterestRate { get; set; }

        public short InterestCalculationHolidayPeriod { get; set; }

        public byte LendingInterestPostingFrequencyPrmKey { get; set; }

        public byte InterestRateChargedDurationPrmKey { get; set; }

        public byte DaysInYearPrmKey { get; set; }

        public byte LendingRepaymentsInterestCalculationPrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string NewInterestAppliedFrom { get; set; }

        public byte InterestCompoundingFrequencyPrmKey { get; set; }

        public bool EnableLoanInterestProvision { get; set; }

        public bool EnableLoanFineInterest { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanInterestParameterMakerChecker> SchemeLoanInterestParameterMakerCheckers { get; set; }
    }
}
