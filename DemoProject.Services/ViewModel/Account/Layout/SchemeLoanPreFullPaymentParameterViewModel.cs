using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanPreFullPaymentParameterViewModel
    {
        // SchemePreFullPaymentParameter

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte MinimumRepaymentOfEMIForPreFullPayment { get; set; }

        public byte MinimumMonth { get; set; }

        public byte MaximumMonth { get; set; }

        public decimal InterestRate { get; set; }

        public byte PreFullPaymentPenaltyCalculationMethodPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemePreFullPaymentParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanPreFullPaymentParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other
        public Guid PreFullPaymentPenaltyCalculationMethodId { get; set; }

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
