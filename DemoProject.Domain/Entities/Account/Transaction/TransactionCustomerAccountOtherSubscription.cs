using DemoProject.Domain.Entities.Account.GL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("TransactionCustomerAccountOtherSubscription")]
    public partial class TransactionCustomerAccountOtherSubscription
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionCustomerAccountOtherSubscription()
        {
            TransactionCustomerAccountOtherSubscriptionMakerCheckers = new HashSet<TransactionCustomerAccountOtherSubscriptionMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public Guid TransactionCustomerAccountOtherSubscriptionId { get; set; }

        public long TransactionCustomerAccountPrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual TransactionCustomerAccount TransactionCustomerAccount { get; set; }

        public virtual GeneralLedger GeneralLedger { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionCustomerAccountOtherSubscriptionMakerChecker> TransactionCustomerAccountOtherSubscriptionMakerCheckers { get; set; }
    }
}
