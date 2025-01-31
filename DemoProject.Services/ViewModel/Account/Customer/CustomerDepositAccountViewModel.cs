using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerDepositAccountViewModel
    {
        //CustomerDepositAccount
        public int PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short Tenure { get; set; }

        public DateTime? MaturityDate { get; set; }

        public bool EnableAutoCloseOnMaturity { get; set; }

        public byte AccountOperationModePrmKey { get; set; }

        public decimal DepositInstallmentAmount { get; set; }

        public byte InstallmentFrequencyPrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerDepositAccountMakerChecker
        public DateTime EntryDateTime { get; set; }

        public int CustomerDepositAccountPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid TenureListId { get; set; }

        public int AccountNumber { get; set; }

        [StringLength(100)]
        public string NameOfCustomerAccount { get; set; }

        public Guid AccountOperationModeId { get; set; }

        public Guid InstallmentFrequencyId { get; set; }//new Added

        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
