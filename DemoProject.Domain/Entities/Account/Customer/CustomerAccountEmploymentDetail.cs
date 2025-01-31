using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountEmploymentDetail")]
    public partial class CustomerAccountEmploymentDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountEmploymentDetail()
        {
            CustomerAccountEmploymentDetailMakerCheckers = new HashSet<CustomerAccountEmploymentDetailMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonEmploymentDetailPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount  { get; set; }

        public virtual PersonEmploymentDetail PersonEmploymentDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountEmploymentDetailMakerChecker> CustomerAccountEmploymentDetailMakerCheckers { get; set; }
    }
}
