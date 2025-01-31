using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountChequeDetail")]
    public partial class CustomerAccountChequeDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountChequeDetail()
        {
            CustomerAccountChequeDetailMakerCheckers = new HashSet<CustomerAccountChequeDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public short ChequeBookMasterPrmKey { get; set; }

        public int ChequeNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string Status { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountChequeDetailMakerChecker> CustomerAccountChequeDetailMakerCheckers { get; set; }
    }
}
