using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("TransactionGeneralLedger")]
    public partial class TransactionGeneralLedger
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionGeneralLedger()
        {
            TransactionGeneralLedgerMakerCheckers = new HashSet<TransactionGeneralLedgerMakerChecker>();
            TransactionGSTDetails = new HashSet<TransactionGSTDetail>();
        }

        [Key]
        public long PrmKey { get; set; }

        public int TransactionMasterPrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }
        public long PersonPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Particulars { get; set; }

        public decimal Amount { get; set; }

        public bool IsCredit { get; set; }

        [Required]
        [StringLength(1500)]
        public string Narration { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual TransactionMaster TransactionMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionGeneralLedgerMakerChecker> TransactionGeneralLedgerMakerCheckers { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionGSTDetail> TransactionGSTDetails { get; set; }
    }
}
