using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("CBSProviderAccountDetail")]
    public partial class CBSProviderAccountDetail
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid CBSProviderAccountDetailId { get; set; }

        public short CBSProviderPrmKey { get; set; }

        public int CompanyNumericId { get; set; }

        [Required]
        [StringLength(50)]
        public string UniqueRegistrationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string AdvancedEncryptionStandardKey { get; set; }

        [Required]
        [StringLength(50)]
        public string SecretKeyForEncryption { get; set; }

        [Required]
        [StringLength(50)]
        public string APIKey { get; set; }

        [Required]
        [StringLength(10)]
        public string RegisteredMobileNumber { get; set; }

        public int BankNumericId { get; set; }

        [Required]
        [StringLength(30)]
        public string ECollectionCode { get; set; }

        [Required]
        [StringLength(50)]
        public string OtherId1 { get; set; }

        [Required]
        [StringLength(50)]
        public string OtherId2 { get; set; }

        [Required]
        [StringLength(50)]
        public string OtherId3 { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
