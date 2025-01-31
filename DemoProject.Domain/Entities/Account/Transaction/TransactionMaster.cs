using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("TransactionMaster")]
    public partial class TransactionMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionMaster()
        {
            TransactionCashDenominations = new HashSet<TransactionCashDenomination>();
            TransactionCustomerAccounts = new HashSet<TransactionCustomerAccount>();
            TransactionGeneralLedgers = new HashSet<TransactionGeneralLedger>();
            TransactionMasterMakerCheckers = new HashSet<TransactionMasterMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid TransactionMasterId { get; set; }

        public short PeriodCodePrmKey { get; set; }

        public DateTime TransactionDate { get; set; }

        public byte TransactionTypePrmKey { get; set; }

        public long TransactionNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string TokenNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string ReferenceNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string Narration { get; set; }

        [Required]
        [StringLength(150)]
        public string ByHand { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Purpose { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionCashDenomination> TransactionCashDenominations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionCustomerAccount> TransactionCustomerAccounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionGeneralLedger> TransactionGeneralLedgers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionMasterMakerChecker> TransactionMasterMakerCheckers { get; set; }
    }
}
