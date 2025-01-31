using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeAccountNumberViewModel
    {
        // BusinessOfficeAccountNumber
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoAccountNumber { get; set; }

        [StringLength(25)]
        public string AccountNumberMask { get; set; }

        public string AccountNumberMaskText { get; set; }

        public long StartAccountNumber { get; set; }

        public long EndAccountNumber { get; set; }

        public int AccountNumberIncrementBy { get; set; }

        public bool EnableRandomAccountNumber { get; set; }

        public bool EnableCustomizeAccountNumber { get; set; }

        public bool EnableReGenerateUnusedAccountNumber { get; set; }

        public bool EnableDigitalCodeForAccountNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // BusinessOfficeAccountNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeAccountNumberPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
        
        // DropDown  
        public Guid GeneralLedgerId { get; set; }

        public string NameOfGL { get; set; }

        //Mask

        public string[] MaskTypeCharacterForAccountId { get; set; }

        public string[] MaskTypeCharacterForAccount { get; set; }
    }
}
