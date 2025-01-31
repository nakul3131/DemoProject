using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanRepaymentScheduleParameterViewModel
    {
        // SchemeLoanRepaymentScheduleParameter

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        [StringLength(3)]
        public string RepaymentSchedulingMethod { get; set; }

        [StringLength(3)]
        public string PaymentIntervalMethod { get; set; }

        public byte RepaymentIntervalFrequencyPrmKey { get; set; }

        [StringLength(3)]
        public string ShortMonthHandlingMethod { get; set; }

        public byte MinimumNumberOfInstallments { get; set; }

        public byte MaximumNumberOfInstallments { get; set; }

        public byte DefaultNumberOfInstallments { get; set; }

        [StringLength(3)]
        public string GracePeriod { get; set; }

        [StringLength(3)]
        public string RoundingOfRepaymentSchedule { get; set; }

        [StringLength(3)]
        public string RoundingOfPrincipal { get; set; }

        public byte PrincipalRoundingBy { get; set; }

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

        [StringLength(3)]
        public string RepaymentFallOnNonWorkingDay { get; set; }

        [StringLength(3)]
        public string PrePaymentRecalculationMethod { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeLoanRepaymentScheduleParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanRepaymentScheduleParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other
        public Guid RepaymentIntervalFrequencyId { get; set; }

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}