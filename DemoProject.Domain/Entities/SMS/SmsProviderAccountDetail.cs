using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Management.SystemEntity;

namespace DemoProject.Domain.Entities.SMS
{
    [Table("SmsProviderAccountDetail")]
    public partial class SmsProviderAccountDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SmsProviderAccountDetail()
        {
            NoticeTypeTemplates = new HashSet<NoticeTypeTemplate>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SmsProviderPrmKey { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] ClientCode { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] UserID { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] UserPassword { get; set; }

        [Required]
        [MaxLength(128)]
        public byte[] AuthenticationKey { get; set; }

        public short PriorityNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1)]
        public string RowStatus { get; set; }

        public virtual SmsProvider SmsProvider { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeTypeTemplate> NoticeTypeTemplates { get; set; }
    }
}
