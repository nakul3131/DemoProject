using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.SMS;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("CBSProvider")]
    public partial class CBSProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public CBSProvider()
        //{
        //    CBSProviderAccountDetails = new HashSet<CBSProviderAccountDetails>();
        //}

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
        public string CBSApi { get; set; }

        public bool HasOnlineTransactionFacility { get; set; }

        public bool HasBPPSFacility { get; set; }

        public bool HasEKYCFacility { get; set; }

        public bool HasCIBILFacility { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SmsProviderAccountDetail> SmsProviderAccountDetails { get; set; }
    }
}
