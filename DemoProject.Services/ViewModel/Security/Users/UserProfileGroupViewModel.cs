using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserProfileGroupViewModel
    {
        // UserProfileGroup

        public short PrmKey { get; set; }

        public Guid UserProfileGroupId { get; set; }

        public short UserProfilePrmKey { get; set; }

        public int GroupMasterPrmKey { get; set; }

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

        // UserProfileGroupMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short UserProfileGroupPrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public Guid GroupMasterId { get; set; }
    }
}
