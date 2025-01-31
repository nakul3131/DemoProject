using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("OutwardTransaction")]
    public partial class OutwardTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OutwardTransaction()
        {
            OutwardTransactionDetails = new HashSet<OutwardTransactionDetail>();
            OutwardTransactionMakerCheckers = new HashSet<OutwardTransactionMakerChecker>();
            OutwardTransactionTranslations = new HashSet<OutwardTransactionTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid OutwardTransactionId { get; set; }

        [Required]
        [StringLength(50)]
        public string OutwardNumber { get; set; }

        public long OutwardReferenceNumber { get; set; }

        public DateTime DateOnTheOutwardDocument { get; set; }

        [Required]
        [StringLength(50)]
        public string NumberOnOutwardDocument { get; set; }

        public DateTime ForwardingDate { get; set; }

        public TimeSpan ForwardingTime { get; set; }

        [Required]
        [StringLength(1500)]
        public string OutwardSubject { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReceiverName { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReceiverAddress { get; set; }

        [Required]
        [StringLength(1500)]
        public string CourierServiceDetails { get; set; }

        [Required]
        [StringLength(50)]
        public string CourierReferenceNumber { get; set; }

        public short DocumentCreatorEmployeePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string OutwardStatus { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutwardTransactionDetail> OutwardTransactionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutwardTransactionMakerChecker> OutwardTransactionMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutwardTransactionTranslation> OutwardTransactionTranslations { get; set; }
    }
}
