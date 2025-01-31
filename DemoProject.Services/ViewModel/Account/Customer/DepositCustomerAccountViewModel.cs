using System;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.ViewModel.PersonInformation;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class DepositCustomerAccountViewModel
    {
        //CustomerAccount
        public long PrmKey { get; set; }

        public Guid CustomerAccountId { get; set; }

        public DateTime AccountOpeningDate { get; set; }

        public long AccountNumber { get; set; }

        [StringLength(50)]
        public string AlternateAccountNumber1 { get; set; }

        [StringLength(50)]
        public string AlternateAccountNumber2 { get; set; }

        [StringLength(50)]
        public string AlternateAccountNumber3 { get; set; }

        public int ApplicationNumber { get; set; }

        public int PassbookNumber { get; set; }

        public int AgreementNumber { get; set; }

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

        public long CustomerAccountPrmKey { get; set; }

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

        public string Year { get; set; }

        public string Month { get; set; }

        public string Day { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
        
        public byte NumberOfDepositAccount { get; set; }

        public CustomerAccountDetailViewModel CustomerAccountDetailViewModel { get; set; }

        public CustomerDepositAccountViewModel CustomerDepositAccountViewModel { get; set; }

        public CustomerJointAccountHolderViewModel CustomerJointAccountHolderViewModel { get; set; }

        public CustomerAccountNomineeViewModel CustomerAccountNomineeViewModel { get; set; }

        public CustomerAccountNomineeGuardianViewModel CustomerAccountNomineeGuardianViewModel { get; set; }

        public CustomerTermDepositAccountDetailViewModel CustomerTermDepositAccountDetailViewModel { get; set; }

        public CustomerAccountReferencePersonDetailViewModel CustomerAccountReferencePersonDetailViewModel { get; set; }

        public CustomerDepositAccountAgentViewModel CustomerDepositAccountAgentViewModel { get; set; }

        public CustomerAccountTurnOverLimitViewModel CustomerAccountTurnOverLimitViewModel { get; set; }

        public CustomerAccountInterestRateViewModel CustomerAccountInterestRateViewModel { get; set; }

        public CustomerAccountDocumentViewModel CustomerAccountDocumentViewModel { get; set; }

        // PersonContactDetailViewModel
        public PersonContactDetailViewModel PersonContactDetailViewModel { get; set; }

        // PersonAddressViewModel
        public PersonAddressViewModel PersonAddressViewModel { get; set; }

        // CustomerAccountSweepDetailViewModel
        public CustomerAccountSweepDetailViewModel CustomerAccountSweepDetailViewModel { get; set; }

        // CustomerAccountPhotoSignViewModel
        public CustomerAccountPhotoSignViewModel CustomerAccountPhotoSignViewModel { get; set; }

        // CustomerAccountBeneficiaryDetailViewModel
        public CustomerAccountBeneficiaryDetailViewModel CustomerAccountBeneficiaryDetailViewModel { get; set; }

        // CustomerAccountChequeDetailViewModel
        public CustomerAccountChequeDetailViewModel CustomerAccountChequeDetailViewModel { get; set; }

        // CustomerAccountEmailServiceViewModel
        public CustomerAccountEmailServiceViewModel CustomerAccountEmailServiceViewModel { get; set; }

        // CustomerAccountSmsServiceViewModel
        public CustomerAccountSmsServiceViewModel CustomerAccountSmsServiceViewModel { get; set; }

        // CustomerAccountStandingInstructionViewModel
        public CustomerAccountStandingInstructionViewModel CustomerAccountStandingInstructionViewModel { get; set; }
        
        // CustomerAccountStandingInstructionViewModel
        public CustomerAccountNoticeScheduleViewModel CustomerAccountNoticeScheduleViewModel { get; set; }
    }

}
