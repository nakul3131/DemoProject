using DemoProject.Domain.Entities.Account.Customer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{

    [Table("PersonIncomeTaxDetail")]
    public partial class PersonIncomeTaxDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonIncomeTaxDetail()
        {
            PersonIncomeTaxDetailDocuments = new HashSet<PersonIncomeTaxDetailDocument>();
            PersonIncomeTaxDetailMakerCheckers = new HashSet<PersonIncomeTaxDetailMakerChecker>();
            CustomerLoanAccountIncomeTaxDetails = new HashSet<CustomerLoanAccountIncomeTaxDetail>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short AssessmentYear { get; set; }

        public decimal TaxAmount { get; set; }

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

        //public virtual PersonIncomeTaxDetail PersonIncomeTaxDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonIncomeTaxDetailMakerChecker> PersonIncomeTaxDetailMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountIncomeTaxDetail> CustomerLoanAccountIncomeTaxDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonIncomeTaxDetailDocument> PersonIncomeTaxDetailDocuments { get; set; }
    }
}
