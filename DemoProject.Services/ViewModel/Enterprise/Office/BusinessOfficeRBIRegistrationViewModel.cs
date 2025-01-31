using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeRBIRegistrationViewModel
    {
        //BusinessOfficeRBIRegistration

        public short PrmKey { get; set; }

        public Guid BusinessOfficeRBIRegistrationId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public DateTime ApprovalDate { get; set; }

        [StringLength(50)]
        public string LicenseNumber { get; set; }

        public short UniformBusinessOfficeCode1 { get; set; }

        public short UniformBusinessOfficeCode2 { get; set; }

        public short BusinessOfficeCodeByRBI { get; set; }

        public short MICRCode { get; set; }

        [StringLength(11)]
        public string IFSCCode { get; set; }

        [StringLength(50)]
        public string AlphaNumericSWIFTAddress { get; set; }

        [StringLength(50)]
        public string AlphaNumericTelexAddress { get; set; }

        [StringLength(50)]
        public string BusinessOfficeUniqueIdentityNumberForATM { get; set; }

        [StringLength(20)]
        public string RoutingNumberForClearingTransaction { get; set; }

        [StringLength(2)]
        public string IBANCountryCode { get; set; }

        public byte IBANCheckDigitAlgorithm { get; set; }

        [StringLength(30)]
        public string BBANFormatMask { get; set; }

        [StringLength(1)]
        public string BBANDataType { get; set; }

        public byte BBANCheckDigitAlgorithm { get; set; }

        public byte UserDefinedAlgorithm { get; set; }

        [StringLength(30)]
        public string BBANBankCode { get; set; }

        [StringLength(30)]
        public string BBANBranchCode { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //BusinessOfficeRBIRegistrationMakerCheker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeRBIRegistrationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //BusinessOfficeRBIRegistrationTranslation

        public Guid BusinessOfficeRBIRegistrationTranslationId { get; set; }
        
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(50)]
        public string TransReferenceNumber { get; set; }

        [StringLength(50)]
        public string TransLicenseNumber { get; set; }

        [StringLength(50)]
        public string TransAlphaNumericSWIFTAddress { get; set; }

        [StringLength(50)]
        public string TransAlphaNumericTelexAddress { get; set; }

        [StringLength(50)]
        public string TransBusinessOfficeUniqueIdentityNumberForATM { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }
        
        //BusinessOfficeRBIRegistrationTranslationMakerCheker

        public short BusinessOfficeRBIRegistrationTranslationPrmKey { get; set; }

        // BusinessOffice

        public Guid BusinessOfficeId { get; set; }

        [StringLength(50)]
        public string NameOfBusinessOffice { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        public Guid IBANCheckDigitAlgorithmId { get; set; } 

        public Guid BBANCheckDigitAlgorithmId { get; set; }

        public Guid UserDefinedAlgorithmId { get; set; } 

    }
}
