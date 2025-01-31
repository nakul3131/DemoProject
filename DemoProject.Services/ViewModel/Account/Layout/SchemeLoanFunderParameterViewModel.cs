using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanFunderParameterViewModel
    {
        // SchemeLoanFunderParameter

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public decimal FunderLoanFundingPercentage { get; set; }

        public decimal FunderInterestCommissions { get; set; }

        public bool EnableLockFundsOnFundingAccountAtApproval { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeLoanFunderParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanFunderParameterPrmKey { get; set; }

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
