using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountContactDetail")]
    public partial class CustomerAccountContactDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountContactDetail()
        {
            CustomerAccountContactDetailMakerCheckers = new HashSet<CustomerAccountContactDetailMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public long PersonContactDetailPrmKey { get; set; }

        [Required]
        [StringLength(20)]
        public string VerificationCode { get; set; }

        public bool IsVerified { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        public virtual PersonContactDetail PersonContactDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountContactDetailMakerChecker> CustomerAccountContactDetailMakerCheckers { get; set; }
    }
}
