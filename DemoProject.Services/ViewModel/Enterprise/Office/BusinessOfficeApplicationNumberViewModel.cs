using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeApplicationNumberViewModel
    {
        // BusinessOfficeApplicationNumber

        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoApplicationNumber { get; set; }

        [StringLength(25)]
        public string ApplicationNumberMask { get; set; }

        public int StartApplicationNumber { get; set; }

        public int EndApplicationNumber { get; set; }

        public int ApplicationNumberIncrementBy { get; set; }

        public bool EnableReGenerateUnusedApplicationNumber { get; set; }

        public bool EnableRandomApplicationNumber { get; set; }

        public bool EnableCustomizeApplicationNumber { get; set; }

        public bool EnableDigitalCodeForApplicationNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }


        // BusinessOfficeApplicationNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeApplicationNumberPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        // DropDown  
        public Guid GeneralLedgerId { get; set; }

        public string NameOfGL{ get; set; }
        
        //Mask
        public string[] MaskTypeCharacterForApplicationId { get; set; }

        public string[] MaskTypeCharacterForApplication { get; set; }
    }
}
