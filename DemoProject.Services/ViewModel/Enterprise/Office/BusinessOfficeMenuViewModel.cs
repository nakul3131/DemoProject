using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeMenuViewModel
    {
        // BusinessOfficeMenu

        public int PrmKey { get; set; }

        public Guid BusinessOfficeMenuId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public int MenuPrmKey { get; set; }

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

        // BusinessOfficeMenuMakerCheker

        public DateTime EntryDateTime { get; set; }

        public int BusinessOfficeMenuPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid MenuId { get; set; }

        public string NameOfMenu { get; set; }

    }
}
