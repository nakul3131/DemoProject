using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("TransactionGSTDetail")]
    public partial class TransactionGSTDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionGSTDetail()
        {
            TransactionGSTDetailMakerCheckers = new HashSet<TransactionGSTDetailMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long TransactionGeneralLedgerPrmKey { get; set; }

        [Required]
        [StringLength(20)]
        public string InvoiceNumber { get; set; }

        public decimal TaxableAmount { get; set; }

        public decimal GSTRate { get; set; }

        public decimal CGSTRate { get; set; }

        public decimal SGSTRate { get; set; }

        public decimal IGSTRate { get; set; }

        public decimal GSTAmount { get; set; }

        public decimal CessRate { get; set; }

        public decimal CessAmount { get; set; }

        public bool IsApplicableForReverseCharge { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual TransactionGeneralLedger TransactionGeneralLedger { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionGSTDetailMakerChecker> TransactionGSTDetailMakerCheckers { get; set; }
    }
}
