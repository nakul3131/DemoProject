using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.UserRoles
{
    public class RoleProfileMenuViewModel
    {
        public int PrmKey { get; set; }

        public Guid RoleProfileMenuId { get; set; }

        public short RoleProfilePrmKey { get; set; }

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

        //RoleProfileMenuMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int RoleProfileMenuPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // For DropDown

        [StringLength(100)]
        public string NameOfModelMenu { get; set; }

        [StringLength(100)]
        public string NameOfMainMenu { get; set; }

        [StringLength(100)]
        public string NameOfMenu { get; set; }

        [StringLength(100)]
        public string NameOfSubMenu { get; set; }

        public Guid ModelMenuId { get; set; }

        public Guid MainMenuId { get; set; }

        public Guid SubMenuId { get; set; }

        public Guid[] MultiMainMenuId { get; set; }

        public Guid[] MultiSubMenuId { get; set; }
    }
}
