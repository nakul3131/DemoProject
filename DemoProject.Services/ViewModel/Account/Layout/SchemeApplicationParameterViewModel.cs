using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeApplicationParameterViewModel
    {
        // SchemeApplicationParameter

        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableApplicationNumberBranchwise { get; set; }

        [StringLength(25)]
        public string ApplicationNumberMask { get; set; }

        public bool EnableAutoApplicationNumber { get; set; }

        public bool EnableRandomApplicationNumber { get; set; }

        public bool EnableRegenerateUnusedApplicationNumber { get; set; }

        public bool EnableCustomizeApplicationNumber { get; set; }

        public bool EnableDigitalCodeForApplicationNumber { get; set; }

        public int StartApplicationNumber { get; set; }

        public int ApplicationNumberIncrementBy { get; set; }

        public int EndApplicationNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeApplicationParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeApplicationParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other
        //public bool EnableApplication { get; set; } 

        public bool EnableNumberOfSharesBranchwise { get; set; }

        public string[] MaskTypeCharacterForApplicationId { get; set; }

        public string[] MaskTypeCharacterForApplication { get; set; }

        public string MaskCharacter { get; set; }

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
