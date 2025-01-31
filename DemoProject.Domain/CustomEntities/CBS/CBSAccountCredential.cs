using System.ComponentModel.DataAnnotations;

namespace DemoProject.Domain.CustomEntities.CBS
{
    public class CBSAccountCredential
    {
        [StringLength(50)]
        public string NameOfProvider { get; set; }

        public int CompanyNumericId { get; set; }

        [StringLength(50)]
        public string UniqueRegistrationNumber { get; set; }

        [StringLength(50)]
        public string AdvancedEncryptionStandardKey { get; set; }

        [StringLength(50)]
        public string SecretKeyForEncryption { get; set; }

        [StringLength(50)]
        public string APIKey { get; set; }

        [StringLength(10)]
        public string RegisteredMobileNumber { get; set; }

        public int BankNumericId { get; set; }

        [StringLength(30)]
        public string ECollectionCode { get; set; }

        [StringLength(50)]
        public string OtherId1 { get; set; }

        [StringLength(50)]
        public string OtherId2 { get; set; }

        [StringLength(50)]
        public string OtherId3 { get; set; }
    }
}
