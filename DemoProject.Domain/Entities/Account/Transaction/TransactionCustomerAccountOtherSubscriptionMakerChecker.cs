using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("TransactionCustomerAccountOtherSubscriptionMakerChecker")]
    public partial class TransactionCustomerAccountOtherSubscriptionMakerChecker
    {
        [Key]
        public long PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public long TransactionCustomerAccountOtherSubscriptionPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual TransactionCustomerAccountOtherSubscription TransactionCustomerAccountOtherSubscription { get; set; }
    }
}
