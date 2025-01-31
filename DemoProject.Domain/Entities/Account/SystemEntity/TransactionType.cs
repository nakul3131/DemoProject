using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.Transaction;
using DemoProject.Domain.Entities.Enterprise.Office;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("TransactionType")]
    public partial class TransactionType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionType()
        {
            TransactionTypeTranslations = new HashSet<TransactionTypeTranslation>();
            TransactionMasters = new HashSet<TransactionMaster>();
            BusinessOfficeTransactionLimits = new HashSet<BusinessOfficeTransactionLimit>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid TransactionTypeId { get; set; }

        [Required]
        [StringLength(10)]
        public string SysNameOfTransactionType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransactionTypeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfTransactionType { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(3)]
        public string AvailableFor { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionTypeTranslation> TransactionTypeTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionMaster> TransactionMasters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeTransactionLimit> BusinessOfficeTransactionLimits { get; set; }
    }
}
