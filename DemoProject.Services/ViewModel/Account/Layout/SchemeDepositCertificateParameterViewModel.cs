using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDepositCertificateParameterViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableCertificateNumberBranchwise { get; set; }

        [StringLength(25)]
        public string CertificateNumberMask { get; set; }

        public bool EnableDigitalCodeForCertificate { get; set; }

        public bool EnableAutoCertificateNumber { get; set; }

        public bool EnableCustomizeCertificateNumber { get; set; }

        public int StartCertificateNumber { get; set; }

        public int EndCertificateNumber { get; set; }

        public int CertificateNumberIncrementBy { get; set; }

        public bool IsRandomlyGeneratedCertificateNumber { get; set; }

        public bool ReGenerateUnusedCertificateNumber { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeDepositCertificateParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeDepositCertificateParameterPrmKey { get; set; }

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

        public string[] MaskTypeCharacterForCertificateId { get; set; }

        public string[] MaskTypeCharacterForCertificate { get; set; }

        public string MaskCharacter { get; set; }
    }
}
