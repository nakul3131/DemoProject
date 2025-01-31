using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.SMS
{
    [Table("SmsProvider")]
    public partial class SmsProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SmsProvider()
        {
            SmsProviderAccountDetails = new HashSet<SmsProviderAccountDetail>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short PrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfProvider { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfOwner { get; set; }

        [Required]
        [StringLength(1500)]
        public string OwnerOtherDetails { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(250)]
        public string SMSApi { get; set; }

        public bool HasHighPrioritySmsRoute { get; set; }

        public bool HasTransactionSmsFacility { get; set; }

        public bool HasPromotionalSmsFacility { get; set; }

        public bool HasShortCodeSmsFacility { get; set; }

        public bool HasLongCodeSmsFacility { get; set; }

        public bool HasWhatsAppSmsFacility { get; set; }

        public bool HasVoiceCallFacility { get; set; }

        public bool HasMissCallNotificationFacility { get; set; }

        public bool HasSmsReceiveFacility { get; set; }

        public bool HasEmailMarketingFacility { get; set; }

        [Required]
        [StringLength(1)]
        public string RowStatus { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SmsProviderAccountDetail> SmsProviderAccountDetails { get; set; }
    }
}
