using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountDetail")]
    public partial class CustomerAccountDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountDetail()
        {
            CustomerAccountDetailMakerCheckers = new HashSet<CustomerAccountDetailMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public short CurrencyPrmKey { get; set; }

        public short SchemePrmKey { get; set; }

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

        //public virtual BusinessOffice BusinessOffice { get; set; }

        //public virtual GeneralLedger GeneralLedger { get; set; }

        //public virtual Person Person { get; set; }

        //public virtual Currency Currency { get; set; }

        //public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountDetailMakerChecker> CustomerAccountDetailMakerCheckers { get; set; }
    }
}
