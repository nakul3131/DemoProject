using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeAgreementNumberViewModel
    {
        // BusinessOfficeAgreementNumber
        public short PrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool EnableAutoAgreementNumber { get; set; }

        [StringLength(25)]
        public string AgreementNumberMask { get; set; }

        public string AgreementNumberMaskText { get; set; }

        public int StartAgreementNumber { get; set; }

        public int EndAgreementNumber { get; set; }

        public int AgreementNumberIncrementBy { get; set; }

        public bool EnableRandomAgreementNumber { get; set; }

        public bool EnableCustomizeAgreementNumber { get; set; }

        public bool EnableReGenerateUnusedAgreementNumber { get; set; }

        public bool EnableDigitalCodeForAgreementNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // BusinessOfficeAgreementNumberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeAgreementNumberPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropDown  
        public Guid GeneralLedgerId { get; set; }

        public string NameOfGL { get; set; }

        //Mask
        public string[] MaskTypeCharacterForAgreementId { get; set; }

        public string[] MaskTypeCharacterForAgreement { get; set; }

    }
}