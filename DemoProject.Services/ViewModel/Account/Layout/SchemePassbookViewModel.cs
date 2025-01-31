using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemePassbookViewModel
    {
        // SchemePassbook

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public int StartPassbookNumber { get; set; }

        public int EndPassbookNumber { get; set; }

        [StringLength(25)]
        public string PassbookNumberMask { get; set; }

        public int PassbookNumberIncrementBy { get; set; }

        public bool IsRandomlyGeneratedPassbookNumber { get; set; }

        public bool ReGenerateUnusedPassbookNumber { get; set; }

        public bool EnableAutoPassbookNumber { get; set; }

        public bool EnablePassbookNumberBranchwise { get; set; }

        public bool EnableCustomizePassbookNumber { get; set; }

        public bool EnableDigitalCodeForPassbookNumber { get; set; }

        public bool IsVisiblePassbookNumber { get; set; }

        public bool EnablePassbookVerification { get; set; }

        public bool DuplicatePassbookCharges { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemePassbookMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemePassbookPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        // Dropdown

        public string[] MaskTypeCharacterForPassbookId { get; set; }

        public string[] MaskTypeCharacterForPassbook  { get; set; }

        public string MaskCharacter { get; set; }
    }
}
