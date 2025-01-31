using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanPrePartPaymentParameterViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        [StringLength(1)]
        public string PrePartPaymentBasedOn { get; set; }

        public byte MinimumRepaymentOfEMIForPrePartPayment { get; set; }

        public byte MinimumMonthForPrePartPayment { get; set; }

        public byte MaximumMonthForPrePartPayment { get; set; }

        public decimal MinimumPrePartPaymentAmount { get; set; }

        public decimal MaximumPrePartPaymentAmount { get; set; }

        public decimal InterestRate { get; set; }

        public byte PrePartPaymentRepetitionLimitInFinancialYear { get; set; }

        public byte PrePartPaymentPenaltyCalculationMethodPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemePrePartPaymentParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanPrePartPaymentParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        public Guid InterestMethodId { get; set; }

        [StringLength(100)]
        public string NameOfInterestMethod { get; set; }
    }
}
