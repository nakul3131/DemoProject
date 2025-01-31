using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserProfilePasswordPolicyViewModel
    {
        public short PrmKey { get; set; }

        public Guid UserProfilePasswordPolicyId { get; set; }

        public short UserProfilePrmKey { get; set; }

        public short PasswordPolicyPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //UserProfilePasswordPolicyMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePasswordPolicyPrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public Guid PasswordPolicyId { get; set; }

        //public Guid MultiPasswordPolicyId { get; set; }

        [StringLength(100)]
        public string NameOfPasswordPolicy { get; set; }
    }
}
