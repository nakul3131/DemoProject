using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("TransactionCashDenomination")]
    public partial class TransactionCashDenomination
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionCashDenomination()
        {
            TransactionCashDenominationMakerCheckers = new HashSet<TransactionCashDenominationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public Guid TransactionCashDenominationId { get; set; }

        public int TransactionMasterPrmKey { get; set; }

        public byte DenominationPrmKey { get; set; }

        public short Pieces { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual TransactionMaster TransactionMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionCashDenominationMakerChecker> TransactionCashDenominationMakerCheckers { get; set; }
    }
}
