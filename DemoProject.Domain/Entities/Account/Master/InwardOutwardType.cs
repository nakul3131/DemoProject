using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.Transaction;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("InwardOutwardType")]
    public partial class InwardOutwardType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InwardOutwardType()
        {
            InwardOutwardTypeMakerCheckers = new HashSet<InwardOutwardTypeMakerChecker>();
            InwardOutwardTypeModifications = new HashSet<InwardOutwardTypeModification>();
            InwardOutwardTypeTranslations = new HashSet<InwardOutwardTypeTranslation>();
            InwardTransactionDetails = new HashSet<InwardTransactionDetail>();
            OutwardTransactionDetails = new HashSet<OutwardTransactionDetail>();
        }
        [Key]
        public short PrmKey { get; set; }

        public Guid InwardOutwardTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfInwardOutwardType { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short ReplyInDays { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InwardOutwardTypeMakerChecker> InwardOutwardTypeMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InwardOutwardTypeModification> InwardOutwardTypeModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InwardOutwardTypeTranslation> InwardOutwardTypeTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InwardTransactionDetail> InwardTransactionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutwardTransactionDetail> OutwardTransactionDetails { get; set; }
    }
}
