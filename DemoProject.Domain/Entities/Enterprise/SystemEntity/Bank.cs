using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("Bank")]
    public partial class Bank
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bank()
        {
            BankTranslations = new HashSet<BankTranslation>();
            BankBranches = new HashSet<BankBranch>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid BankId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfBank { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(100)]
        public string RBILicenseNumber { get; set; }

        [Required]
        [StringLength(2500)]
        public string FullAddressDetail { get; set; }

        [Required]
        [StringLength(500)]
        public string ContactDetails { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? MergeDate { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankTranslation> BankTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BankBranch> BankBranches { get; set; }
    }
}
