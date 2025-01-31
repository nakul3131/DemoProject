using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserProfileMenuViewModel
    {
        public int PrmKey { get; set; }

        public Guid UserProfileMenuId { get; set; }

        public short UserProfilePrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public int MenuPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(25)]
        public string UserShortcutMenuCode { get; set; }

        public bool IsFavourite { get; set; }

        public bool IsRecentlyUsed { get; set; }

        public bool EnableSamePageRedirection { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //UserProfileMenuMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int UserProfileMenuPrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public Guid ModelMenuId { get; set; }

        public Guid MainMenuId { get; set; }

        public Guid[] MultiMainMenuId { get; set; }

        public Guid SubMenuId { get; set; }

        public Guid[] MultiSubMenuId { get; set; }

        public Guid BusinessOfficeId { get; set; }

        [StringLength(100)]
        public string NameOfBusinessOffice { get; set; }

        [StringLength(100)]
        public string NameOfMenu { get; set; }
    }
}
