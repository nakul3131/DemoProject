using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanRepaymentScheduleParameter")]
    public partial class SchemeLoanRepaymentScheduleParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanRepaymentScheduleParameter()
        {
            SchemeLoanRepaymentScheduleParameterMakerCheckers = new HashSet<SchemeLoanRepaymentScheduleParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string RepaymentSchedulingMethod { get; set; }

        [Required]
        [StringLength(3)]
        public string PaymentIntervalMethod { get; set; }

        public byte RepaymentIntervalFrequencyPrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string ShortMonthHandlingMethod { get; set; }

        public byte MinimumNumberOfInstallments { get; set; }

        public byte MaximumNumberOfInstallments { get; set; }

        public byte DefaultNumberOfInstallments { get; set; }

        [Required]
        [StringLength(3)]
        public string GracePeriod { get; set; }

        [Required]
        [StringLength(3)]
        public string RoundingOfRepaymentSchedule { get; set; }

        [Required]
        [StringLength(3)]
        public string RoundingOfPrincipal { get; set; }

        public byte PrincipalRoundingBy { get; set; }

        [Required]
        [StringLength(3)]
        public string RoundingOfInterest { get; set; }

        public byte InterestRoundingBy { get; set; }

        public short MinimumDaysConstraintsForFirstDueDate { get; set; }

        public short MaximumDaysConstraintsForFirstDueDate { get; set; }

        public short DefaultDaysConstraintsForFirstDueDate { get; set; }

        public bool EnableAdjustmentOfRepaymentSchedulePaymentDate { get; set; }

        public bool EnableAdjustmentOfRepaymentSchedulePrincipalPayment { get; set; }

        public bool EnableAdjustmentOfRepaymentScheduleInterestPayment { get; set; }

        public bool EnableAdjustmentOfRepaymentScheduleFeesPayment { get; set; }

        public bool EnableAdjustmentOfRepaymentSchedulePenaltyPayment { get; set; }

        public bool EnableAdjustmentOfPaymentHoliday { get; set; }

        public bool EnableToChangeMonthlyRepaymentDueDateToEarly { get; set; }

        public bool EnableToChangeMonthlyRepaymentDueDateToLate { get; set; }

        [Required]
        [StringLength(3)]
        public string RepaymentFallOnNonWorkingDay { get; set; }

        [Required]
        [StringLength(3)]
        public string PrePaymentRecalculationMethod { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanRepaymentScheduleParameterMakerChecker> SchemeLoanRepaymentScheduleParameterMakerCheckers { get; set; }
    }
}