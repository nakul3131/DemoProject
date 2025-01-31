using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanDistributorParameterViewModel
    {
        // SchemeLoanDistributorParameter

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableAdvance { get; set; }

        public decimal MinimumAdvanceLimit { get; set; }

        public decimal MaximumAdvanceLimit { get; set; }

        public bool EnableAdvanceDeductionOnDisbursement { get; set; }

        public bool EnableDistributorInterestRate { get; set; }

        public decimal MinimumDistributorInterestRate { get; set; }

        public decimal MaximumDistributorInterestRate { get; set; }

        public decimal DefaultDistributorInterestRate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeLoanDistributorParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanDistributorParameterPrmKey { get; set; }

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
    }
}
