using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanInstallmentParameterViewModel
    {
        // SchemeLoanTransactionParameter

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnablePartialInstallment { get; set; }

        public bool EnablePrePayment { get; set; }

        public bool EnableInstallmentAlteration { get; set; }

        public byte NumberOfOverdueInstallmentRecoveryFromLinkedAccount { get; set; }

        public short MinimumOverDuesInstallment { get; set; }

        public short MaximumOverDuesInstallment { get; set; }

        public short DefaultOverDuesInstallment { get; set; }

        public bool EnableTDSDeductionOfCashTransaction { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeLoanTransactionParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanInstallmentParameterPrmKey { get; set; }

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
