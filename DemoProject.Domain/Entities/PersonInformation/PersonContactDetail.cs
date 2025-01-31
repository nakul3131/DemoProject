using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.Customer;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonContactDetail")]
    public partial class PersonContactDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonContactDetail()
        {
            PersonContactDetailMakerCheckers = new HashSet<PersonContactDetailMakerChecker>();
            CustomerAccountContactDetails = new HashSet<CustomerAccountContactDetail>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonContactDetailId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte ContactTypePrmKey { get; set; }

        [Required]
        [StringLength(500)]
        public string FieldValue { get; set; }

        [Required]
        [StringLength(20)]
        public string VerificationCode { get; set; }

        public bool IsVerified { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonContactDetailMakerChecker> PersonContactDetailMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountContactDetail> CustomerAccountContactDetails { get; set; }
    }
}
