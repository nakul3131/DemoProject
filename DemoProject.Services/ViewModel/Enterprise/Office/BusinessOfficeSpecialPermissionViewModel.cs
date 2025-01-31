using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeSpecialPermissionViewModel
    {
        // BusinessOfficeSpecialPermission

        public int PrmKey { get; set; }

        public Guid BusinessOfficeSpecialPermissionId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short SpecialPermissionPrmKey { get; set; }

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

        // BusinessOfficeSpecialPermissionMakerCheker

        public DateTime EntryDateTime { get; set; }

        public int BusinessOfficeSpecialPermissionPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        // DropDown 
        public Guid SpecialPermissionId { get; set; }

        public string NameOfSpecialPermission { get; set; }

    }
}
