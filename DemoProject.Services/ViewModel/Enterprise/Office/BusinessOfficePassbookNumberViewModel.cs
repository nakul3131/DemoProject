using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficePassbookNumberViewModel
    {
        // BusinessOfficeAccountNumber
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoPassbookNumber { get; set; }

        [StringLength(25)]
        public string PassbookNumberMask { get; set; }

        public int StartPassbookNumber { get; set; }

        public int EndPassbookNumber { get; set; }

        public int PassbookNumberIncrementBy { get; set; }

        public bool EnableReGenerateUnusedPassbookNumber { get; set; }

        public bool EnableRandomPassbookNumber { get; set; }

        public bool EnableCustomizePassbookNumber { get; set; }

        public bool EnableDigitalCodeForPassbookNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }
        // BusinessOfficeAccountNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficePassbookNumberPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropDown  
        public Guid GeneralLedgerId { get; set; }

        public string NameOfGL { get; set; }

        //Mask
        public string[] MaskTypeCharacterForPassbookId { get; set; }

        public string[] MaskTypeCharacterForPassbook { get; set; }
    }
}
