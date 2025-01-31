using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerTermDepositAccountDetailViewModel
    {
        //CustomerTermDepositAccountDetail
        public int PrmKey { get; set; }

        public int CustomerDepositAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int CertificateNumber { get; set; }

        public decimal DepositAmount { get; set; }

        [StringLength(3)]
        public string MaturityInstruction { get; set; }

        [StringLength(3)]
        public string InterestPayoutFrequency { get; set; }

        public decimal InterestPayoutAmount { get; set; }

        public byte InterestPayoutDay { get; set; }

        public decimal TotalInterestAmount { get; set; }

        public decimal MaturityAmount { get; set; }

        public short GracePeriodForRenewal { get; set; }

        public bool EnableAutoRenewOnMaturity { get; set; }

        public short AutoRenewWaitingTimePeriod { get; set; }

        public byte RenewTypePrmKey { get; set; }

        public decimal CustomRenewAmount { get; set; }

        public short RenewTenure { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerTermDepositAccountDetailMakerChecker
        public DateTime EntryDateTime { get; set; }

        public int CustomerTermDepositAccountDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropdownList
        public Guid CustomerDepositAccountId { get; set; }

        [StringLength(150)]
        public string NameOfCustomerDepositAccount { get; set; }

        public Guid RenewTypeId { get; set; }

        [StringLength(50)]
        public string NameOfRenewType { get; set; }

        // Other
        [StringLength(150)]
        public string NameOfUser { get; set; }

        public byte NoOfDeposits { get; set; }

        public decimal[] NoOfDepositsAmount {get; set;}

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
