using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanOverduesActionViewModel
    {
        // SchemeLoanOverduesAction

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short MinimumOverduesInstallment { get; set; }

        public short MaximumOverduesInstallment { get; set; }

        public short LoanRecoveryActionPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeLoanOverduesActionMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanOverduesActionPrmKey { get; set; }

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

        public Guid LoanRecoveryActionId { get; set; }

        [StringLength(100)]
        public string NameOfLoanRecoveryAction { get; set; }
    }
}
