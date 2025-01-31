using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountEmailService")]
    public partial class CustomerAccountEmailService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountEmailService()
        {
            CustomerAccountEmailServiceMakerCheckers = new HashSet<CustomerAccountEmailServiceMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }
        
        public long CustomerAccountPrmKey { get; set; }

        public bool EnableCreditTransaction { get; set; }

        public bool EnableDebitTransaction { get; set; }

        public bool EnableInsufficientBalance { get; set; }

        public bool EnableStatement { get; set; }

        [Required]
        [StringLength(3)]
        public string StatementFrequency { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountEmailServiceMakerChecker> CustomerAccountEmailServiceMakerCheckers { get; set; }
    }
}
