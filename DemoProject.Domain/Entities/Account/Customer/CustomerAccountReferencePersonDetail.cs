using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountReferencePersonDetail")]
    public partial class CustomerAccountReferencePersonDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountReferencePersonDetail()
        {
            CustomerAccountReferencePersonDetailMakerCheckers = new HashSet<CustomerAccountReferencePersonDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public long CustomerAccountNumber { get; set; }

        public bool IsValidateSign { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountReferencePersonDetailMakerChecker> CustomerAccountReferencePersonDetailMakerCheckers { get; set; }

    }
}
