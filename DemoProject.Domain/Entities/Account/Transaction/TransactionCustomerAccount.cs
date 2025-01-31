using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("TransactionCustomerAccount")]
    public partial class TransactionCustomerAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionCustomerAccount()
        {
            TransactionCustomerAccountInterests = new HashSet<TransactionCustomerAccountInterest>();
            SharesCapitalTransactions = new HashSet<SharesCapitalTransaction>();
            TransactionCustomerAccountMakerCheckers = new HashSet<TransactionCustomerAccountMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public int TransactionMasterPrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public decimal Amount { get; set; }

        public bool IsCredit { get; set; }

        public decimal Balance { get; set; }

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
        public virtual ICollection<TransactionCustomerAccountInterest> TransactionCustomerAccountInterests { get; set; }

       [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SharesCapitalTransaction> SharesCapitalTransactions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionCustomerAccountMakerChecker> TransactionCustomerAccountMakerCheckers { get; set; }

    }
}
