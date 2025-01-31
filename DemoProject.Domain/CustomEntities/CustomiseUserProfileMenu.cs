using System;

namespace DemoProject.Domain.CustomEntities
{
    public class CustomiseUserProfileMenu
    {
        public short PrmKey { get; set; }

        public int MenuPrmKey { get; set; }

        public string NameOfMenu { get; set; }

        public string AlternateName { get; set; }

        public string AliasName { get; set; }

        public string NameOnReport { get; set; }

        public string ShortcutCode { get; set; }

        public short SequenceNumber { get; set; }

        public int ParentMenuPrmKey { get; set; }

        public string IconImageClass { get; set; }

        public string UserShortcutMenuCode { get; set; }

        public bool IsFavourite { get; set; }

        public bool IsRecentlyUsed { get; set; }
    }
}
