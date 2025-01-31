using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.ViewModel.PersonInformation;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class SharesCapitalCustomerAccountViewModel
    {
        // CustomerAccount
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

        // CustomerAccountMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // CustomerAccountModification
        public Guid CustomerAccountModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // CustomerAccountModificationMakerChecker
        public int CustomerAccountModificationPrmKey { get; set; }

        // CustomerSharesCapitalAccountViewModel
        public CustomerSharesCapitalAccountViewModel CustomerSharesCapitalAccountViewModel { get; set; }

        public IEnumerable<CustomerSharesCapitalAccountViewModel> CustomerSharesCapitalAccountViewModelList { get; set; }

        // CustomerAccountDetailViewModel
        public CustomerAccountDetailViewModel CustomerAccountDetailViewModel { get; set; }

        public IEnumerable<CustomerAccountDetailViewModel> CustomerAccountDetailViewModelList { get; set; }

        // CustomerJointAccountHolderViewModel
        public CustomerJointAccountHolderViewModel CustomerJointAccountHolderViewModel { get; set; }

        // CustomerAccountNomineeViewModel
        public CustomerAccountNomineeViewModel CustomerAccountNomineeViewModel { get; set; }

        public IEnumerable<CustomerAccountNomineeViewModel> CustomerAccountNomineeViewModelIst { get; set; }

        // CustomerAccountTurnOverLimitViewModel
        public CustomerAccountTurnOverLimitViewModel CustomerAccountTurnOverLimitViewModel { get; set; }

        // SchemeAccountParameterViewModel
        public SchemeAccountParameterViewModel SchemeAccountParameterViewModel { get; set; }

        // SchemeCustomerAccountNumberViewModel
        public SchemeCustomerAccountNumberViewModel SchemeCustomerAccountNumberViewModel { get; set; }

        // PersonContactDetailViewModel
        public PersonContactDetailViewModel PersonContactDetailViewModel { get; set; }

        public IEnumerable<PersonContactDetailViewModel> PersonContactDetailViewModelList { get; set; }

        // PersonAddressViewModel
        public PersonAddressViewModel PersonAddressViewModel { get; set; }

        public IEnumerable<PersonAddressViewModel> PersonAddressViewModelList { get; set; }

        //Public CustomerAccountEmailService
        public CustomerAccountEmailServiceViewModel CustomerAccountEmailServiceViewModel { get; set; }

        //Public CustomerAccountSMSService
        public CustomerAccountSmsServiceViewModel CustomerAccountSmsServiceViewModel { get; set; }

        // CustomerAccountNoticeScheduleViewModel
        public CustomerAccountNoticeScheduleViewModel CustomerAccountNoticeScheduleViewModel { get; set; }

        //Other
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(100)]
        public string NameOfCustomerAccount { get; set; }

        // DropdownList 
        public Guid JointAccountHolderTypeId { get; set; }

        public Guid RelationId { get; set; }

        public Guid GuardianTypeId { get; set; }

        public Guid FrequencyId { get; set; }

        public Guid TransactionTypeId { get; set; }
    }
}
