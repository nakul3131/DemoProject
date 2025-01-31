using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserRoleProfileViewModel
    {
        public short PrmKey { get; set; }

        public Guid UserRoleProfileId { get; set; }

        public short UserProfilePrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short RoleProfilePrmKey { get; set; }

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

        //UserRoleProfilePasswordPolicyMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short UserRoleProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public Guid BusinessOfficeId { get; set; }

        public Guid RoleProfileId { get; set; }

        public Guid[] MultiRoleProfileId { get; set; }

        [StringLength(100)]
        public string NameOfBusinessOffice { get; set; }

        [StringLength(100)]
        public string NameOfRoleProfile { get; set; }
    }
}
