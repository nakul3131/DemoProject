using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class MenuViewModel
    {
        public int PrmKey { get; set; }

        public Guid MenuId { get; set; }

        [StringLength(6)]
        public String MenuCode { get; set; }

        public int MenuPrmKey { get; set; }

        public string NameOfMenu { get; set; }

        public string AlternateName { get; set; }

        public string AliasName { get; set; }

        public string NameOnReport { get; set; }

        public string ShortcutCode { get; set; }

        public int SequenceNumber { get; set; }

        public string NameOfController { get; set; }

        public string NameOfActionMethod { get; set; }

        public int ParentMenuPrmKey { get; set; }

        public string IconImageClass { get; set; }

        public string UserShortcutMenuCode { get; set; }

        public bool IsFavourite { get; set; }

        public bool IsRecentlyUsed { get; set; }
    }
}
