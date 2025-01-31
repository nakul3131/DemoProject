using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerLoanAgainstDepositCollateralDetailViewModel
    {
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int CustomerDepositAccountPrmKey { get; set; }

        public Guid DepositAccountId { get; set; }

        public decimal MortgageAmount { get; set; }

        public bool IsLoanClosed { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(50)]
        public string NameOfDepositAccount { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerLoanAgainstDepositCollateralDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerLoanAgainstDepositCollateralDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
    }
}
