using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeSharesCertificateNumberViewModel
    {
        // BusinessOfficeAccountNumber
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public bool EnableAutoSharesCertificateNumber { get; set; }

        [StringLength(25)]
        public string SharesCertificateNumberMask { get; set; }

        public int StartSharesCertificateNumber { get; set; }

        public int EndSharesCertificateNumber { get; set; }

        public int SharesCertificateNumberIncrementBy { get; set; }

        public bool EnableReGenerateUnusedSharesCertificateNumber { get; set; }

        public bool EnableRandomSharesCertificateNumber { get; set; }

        public bool EnableCustomizeSharesCertificateNumber { get; set; }

        public bool EnableDigitalCodeForSharesCertificateNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
      
        [StringLength(3)]
        public string EntryStatus { get; set; }

        // BusinessOfficeAccountNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeSharesCertificateNumberPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Mask
        public string[] MaskTypeCharacterForSharesCertificateId { get; set; }

        public string[] MaskTypeCharacterForSharesCertificate { get; set; }

    }
}
