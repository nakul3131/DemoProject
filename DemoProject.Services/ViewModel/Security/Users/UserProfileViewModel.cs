using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserProfileViewModel
    {
        //UserProfile

        public short PrmKey { get; set; }

        public Guid UserProfileId { get; set; }

        public byte UserTypePrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        [StringLength(50)]
        public string NameOfUserProfile { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        [StringLength(320)]
        public string EmailId { get; set; }

        public bool IsEmailIdConfirmed { get; set; }

        [StringLength(320)]
        public string AlternateEmailId { get; set; }

        public bool IsAlternateEmailIdConfirmed { get; set; }

        [StringLength(10)]
        public string MobileNumber { get; set; }

        public bool IsMobileNumberConfirmed { get; set; }

        [StringLength(10)]
        public string AlternateMobileNumber { get; set; }

        public bool IsAlternateMobileNumberConfirmed { get; set; }

        public DateTime LastLoginDate { get; set; }

        public DateTime LastActivityDate { get; set; }

        public DateTime LastPasswordChangeDate { get; set; }

        public DateTime LastLockedDate { get; set; }

        public byte InvalidSuccessiveAttemptCount { get; set; }

        public short InvalidCumulativeAttemptCount { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //UserProfileMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }

        public short UserProfileCreatorPrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        [StringLength(3)]
        public string UserProfileStatus { get; set; }

        //UserProfileModification

        public Guid UserProfileModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        public short UserProfileModificationPrmKey { get; set; }

        //UserProfileIdentity
        public Guid UserProfileIdentityId { get; set; }

        [StringLength(50)]
        public string UserId { get; set; }

        public DateTime CreateDate { get; set; }

        //UserProfileLoginDevice
        public Guid UserProfileLoginDeviceId { get; set; }

        public short LoginDevicePrmKey { get; set; }

        //UserProfileLoginDeviceMakerChecker
        public short UserProfileLoginDevicePrmKey { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        public Guid PersonId { get; set; }

        public Guid UserTypeId { get; set; }

        public Guid RoleProfileId { get; set; }

        //UserProfileAccessibilityViewModel
        public UserProfileAccessibilityViewModel UserProfileAccessibilityViewModel { get; set; }

        // UserProfileGroup
        public UserProfileGroupViewModel UserProfileGroupViewModel { get; set; }

        // UserProfileMenu
        public UserProfileMenuViewModel UserProfileMenuViewModel { get; set; }

        // UserProfileGroup
        public UserProfilePasswordPolicyViewModel UserProfilePasswordPolicyViewModel { get; set; }

        // UserProfileGroup
        public UserProfileSpecialPermissionViewModel UserProfileSpecialPermissionViewModel { get; set; }

        // UserProfileGroup
        public UserProfileTransactionLimitViewModel UserProfileTransactionLimitViewModel { get; set; }

        // UserProfileGroup
        public UserProfileBusinessOfficeViewModel UserProfileBusinessOfficeViewModel { get; set; }

        // UserProfileGroup
        public UserProfileCurrencyViewModel UserProfileCurrencyViewModel { get; set; }

        // UserProfileGroup
        public UserProfileGeneralLedgerViewModel UserProfileGeneralLedgerViewModel { get; set; }

        public UserRoleProfileViewModel UserRoleProfileViewModel { get; set; }


        // UserProfileIdentity
        //public UserProfileIdentityViewModel UserProfileIdentityViewModel { get; set; }

        // UserProfileHomeBusinessOffice
        public UserProfileHomeBusinessOfficeViewModel UserProfileHomeBusinessOfficeViewModel { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        //public bool IsShowUserProfileBusinessOffice { get; set; }

        public bool IsShowUserProfileCurrency { get; set; }

        public bool IsShowUserProfileLoginDevice { get; set; }
    }
}