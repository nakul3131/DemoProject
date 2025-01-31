using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountModification")]
    public partial class CustomerAccountModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountModification()
        {
            CustomerAccountModificationMakerCheckers = new HashSet<CustomerAccountModificationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime AccountOpeningDate { get; set; }
        
        public long AccountNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string AlternateAccountNumber1 { get; set; }

        [Required]
        [StringLength(50)]
        public string AlternateAccountNumber2 { get; set; }

        [Required]
        [StringLength(50)]
        public string AlternateAccountNumber3 { get; set; }

        public int ApplicationNumber { get; set; }

        public int PassbookNumber { get; set; }

        public bool IsPrivateCustomer { get; set; }

        public bool IsDeniedDebits { get; set; }

        public bool IsDeniedCredits { get; set; }

        public bool IsDeniedDebitsOverride { get; set; }

        public bool IsDeniedCreditsOverride { get; set; }

        public bool IsDeniedPayments { get; set; }

        public bool IsDormant { get; set; }

        public bool IsFrozen { get; set; }

        public bool EnableTurnOverLimit { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountModificationMakerChecker> CustomerAccountModificationMakerCheckers { get; set; }
    }
}
