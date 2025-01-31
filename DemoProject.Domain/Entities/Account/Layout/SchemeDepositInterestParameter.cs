using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDepositInterestParameter")]
    public partial class SchemeDepositInterestParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDepositInterestParameter()
        {
            SchemeDepositInterestParameterMakerCheckers = new HashSet<SchemeDepositInterestParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public byte InterestMethodPrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public decimal MinimumInterestRate { get; set; }

        public decimal MaximumInterestRate { get; set; }

        public byte InterestRateChargedDurationPrmKey { get; set; }

        public byte PrematureVoidInterestPeriod { get; set; }

        public short InterestCalculationStartingPeriod { get; set; }

        public bool EnableInterestCalculationFromDepositDate { get; set; }

        public bool EnablePrematureInterestCalculation { get; set; }

        public bool TakePrematureInterestRateSameAsSaving { get; set; }

        public decimal LessInterestRateForPrematurity { get; set; }

        public bool EnablePostMatureInterestCalculation { get; set; }

        public short PostMatureVoidInterestPeriod { get; set; }

        public bool TakePostMatureInterestRateSameAsSaving { get; set; }

        public bool TakePostMatureInterestRateSameAsMaturityDate { get; set; }

        public bool TakePostMatureInterestRateSameAsCurrentDate { get; set; }

        public bool EnableInterestProvision { get; set; }

        public bool EnablePeriodicInterestPayout { get; set; }

        public byte MinimumMonthForPeriodicInterestPayout { get; set; }

        public bool EnableCustomisePayoutInterestDayInAccountOpening { get; set; }

        [Required]
        [StringLength(3)]
        public string InterestPayoutDay { get; set; }

        public decimal MinimumOverrideInterestAmount { get; set; }

        public decimal MaximumOverrideInterestAmount { get; set; }
   
        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositInterestParameterMakerChecker> SchemeDepositInterestParameterMakerCheckers { get; set; }
    }
}
