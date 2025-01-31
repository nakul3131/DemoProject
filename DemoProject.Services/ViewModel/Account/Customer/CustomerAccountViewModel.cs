using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountViewModel
    {
        //CustomerAccount
        public int PrmKey { get; set; }

        public Guid CustomerAccountId { get; set; }

        public DateTime AccountOpeningDate { get; set; }

        public int AccountNumber { get; set; }

        [StringLength(50)]
        public string AlternateAccountNumber1 { get; set; }

        [StringLength(50)]
        public string AlternateAccountNumber2 { get; set; }

        [StringLength(50)]
        public string AlternateAccountNumber3 { get; set; }

        public int ApplicationNumber { get; set; }

        public int PassbookNumber { get; set; }

        public bool IsPrivateCustomer { get; set; }

        public bool IsDeniedDebits { get; set; }

        public bool IsDeniedCredits { get; set; }

        public bool IsDeniedDebitsOverride { get; set; }

        public bool IsDeniedCreditsOverride { get; set; }

        public bool IsDeniedPayments { get; set; }

        public bool IsDormant { get; set; }

        public bool IsFrozen { get; set; }

        public bool EnableTurnOverLimit { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //CustomerAccountMakerChecker
        public DateTime EntryDateTime { get; set; }

        public int CustomerAccountPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //CustomerAccountModification
        public Guid CustomerAccountModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //CustomerAccountModification
        public int CustomerAccountModificationPrmKey { get; set; }

        // Other
        [StringLength(100)]
        public string NameOfCustomerAccount { get; set; }

        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
