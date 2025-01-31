using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficePersonInformationNumberViewModel
    {
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public bool EnableAutoPersonInformationNumber { get; set; }

        [StringLength(25)]
        public string PersonInformationNumberMask { get; set; }

        public int StartPersonInformationNumber { get; set; }

        public int EndPersonInformationNumber { get; set; }

        public int PersonInformationNumberIncrementBy { get; set; }

        public bool EnableRandomPersonInformationNumber { get; set; }

        public bool EnableCustomizePersonInformationNumber { get; set; }

        public bool EnableReGenerateUnusedPersonInformationNumber { get; set; }

        public bool EnableDigitalCodeForPersonInformationNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }


        // BusinessOfficePersonInformationNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficePersonInformationNumberPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Mask

        public string[] MaskTypeCharacterForPersonId { get; set; }

        public string[] MaskTypeCharacterForPerson { get; set; }
    }
}
