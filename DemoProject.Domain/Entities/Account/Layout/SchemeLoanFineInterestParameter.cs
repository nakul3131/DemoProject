using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.GL;
using DemoProject.Domain.Entities.Account.SystemEntity;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanFineInterestParameter")]
    public partial class SchemeLoanFineInterestParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanFineInterestParameter()
        {
            SchemeLoanFineInterestParameterMakerCheckers = new HashSet<SchemeLoanFineInterestParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte InterestMethodPrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte NumberOfMissedInstallment { get; set; }

        public short FineDays { get; set; }

        public decimal RateOfFineInterest  { get; set; }

        [Required]
        [StringLength(1)]
        public string RateOfFineInterestUnit { get; set; }

        public byte InterestRateChargedDurationPrmKey { get; set; }

        public byte DaysInYearPrmKey { get; set; }

        public byte LendingRepaymentsInterestCalculationPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual InterestMethod InterestMethod { get; set; }

        public virtual GeneralLedger GeneralLedger { get; set; }

        public virtual InterestRateChargedDuration InterestRateChargedDuration { get; set; }

        public virtual DaysInYear DaysInYear { get; set; }

        public virtual LendingRepaymentsInterestCalculation LendingRepaymentsInterestCalculation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanFineInterestParameterMakerChecker> SchemeLoanFineInterestParameterMakerCheckers { get; set; }
    }
}
