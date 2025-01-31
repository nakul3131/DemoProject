using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.Password
{
    public class PasswordPolicyViewModel
    {
        // PasswordPolicy
        public short PrmKey { get; set; }

        public Guid PasswordPolicyId { get; set; }

        [StringLength(100)]
        public string NameOfPasswordPolicy { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(50)]
        public string NameOnReport { get; set; }

        public byte MinimumPasswordLength { get; set; }

        public byte MaximumPasswordLength { get; set; }

        public byte MinimumNumberOfUpperCaseCharacters { get; set; }

        public byte MinimumNumberOfLowerCaseCharacters { get; set; }

        public byte MinimumNumberOfSpecialCaseCharacters { get; set; }

        public byte MinimumNumberOfNumericCharacters { get; set; }

        public byte MinimumNumberOfRepetitiveCharacters { get; set; }

        public byte ForcePasswordChangeAfterDays { get; set; }

        public short ReusePreviousPassword { get; set; }

        public short MinimumDaysForReusePreviousPassword { get; set; }

        public byte PasswordExpiryAlertDays { get; set; }

        public bool IsModified { get; set; } = false;

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //PasswordPolicyMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short PasswordPolicyPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // PasswordPolicy Modification

        public Guid PasswordPolicyModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //PasswordPolicyModificationMakerChecker

        public short PasswordPolicyModificationPrmKey { get; set; }

        //Other
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
