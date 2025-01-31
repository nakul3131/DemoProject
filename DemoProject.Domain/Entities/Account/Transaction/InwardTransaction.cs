using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("InwardTransaction")]
    public partial class InwardTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InwardTransaction()
        {
          InwardTransactionDetails = new HashSet<InwardTransactionDetail>();
          InwardTransactionMakerCheckers = new HashSet<InwardTransactionMakerChecker>();
          InwardTransactionTranslations = new HashSet<InwardTransactionTranslation>();
        }
        [Key]
        public int PrmKey { get; set; }

        public Guid InwardTransactionId { get; set; }

        [Required]
        [StringLength(50)]
        public string InwardNumber { get; set; }

        public long InwardReferenceNumber { get; set; }

        public DateTime ArrivalDate { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public DateTime DateOnTheInwardDocument { get; set; }

        [Required]
        [StringLength(50)]
        public string NumberOnInwardDocument { get; set; }

        [Required]
        [StringLength(1500)]
        public string InwardSubject { get; set; }

        [Required]
        [StringLength(4500)]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(1500)]
        public string SenderName { get; set; }

        [Required]
        [StringLength(1500)]
        public string SenderAddress { get; set; }

        [Required]
        [StringLength(1500)]
        public string DeliveryAgencyDetails { get; set; }

        [Required]
        [StringLength(3)]
        public string InwardStatus { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InwardTransactionDetail> InwardTransactionDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InwardTransactionMakerChecker> InwardTransactionMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InwardTransactionTranslation> InwardTransactionTranslations { get; set; }
    }
}
