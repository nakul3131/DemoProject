using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountLimitStatus")]
    public partial class CustomerAccountLimitStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountLimitStatus()
        {
            CustomerAccountLimitStatusMakerCheckers = new HashSet<CustomerAccountLimitStatusMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public Guid CustomerAccountLimitStatusId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public bool IsReachedMaximumNumberOfTransactionLimit { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountLimitStatusMakerChecker> CustomerAccountLimitStatusMakerCheckers { get; set; }
    }

}
