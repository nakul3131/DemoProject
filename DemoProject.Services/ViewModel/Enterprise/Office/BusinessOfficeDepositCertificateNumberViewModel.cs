using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeDepositCertificateNumberViewModel
    {
        // BusinessOfficeAccountNumber
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoCertificateNumber { get; set; }

        [StringLength(25)]
        public string CertificateNumberMask { get; set; }

        public int StartCertificateNumber { get; set; }

        public int EndCertificateNumber { get; set; }

        public int CertificateNumberIncrementBy { get; set; }

        public bool EnableReGenerateUnusedCertificateNumber { get; set; }

        public bool EnableRandomCertificateNumber { get; set; }

        public bool EnableCustomizeCertificateNumber { get; set; }

        public bool EnableDigitalCodeForCertificateNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // BusinessOfficeAccountNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeDepositCertificateNumberPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropDown  
        public Guid GeneralLedgerId { get; set; }

        public string NameOfGL { get; set; }

        //Mask

        public string[] MaskTypeCharacterForCertificateId { get; set; }

        public string[] MaskTypeCharacterForCertificate { get; set; }
    }
}
