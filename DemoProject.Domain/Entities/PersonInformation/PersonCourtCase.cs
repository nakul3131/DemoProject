using DemoProject.Domain.Entities.Account.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonCourtCase")]
    public partial class PersonCourtCase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonCourtCase()
        {
            PersonCourtCaseMakerCheckers = new HashSet<PersonCourtCaseMakerChecker>();
            CustomerLoanAccountCourtCaseDetails = new HashSet<CustomerLoanAccountCourtCaseDetail>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonCourtCaseId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte CourtCaseTypePrmKey { get; set; }

        public DateTime FilingDate { get; set; }

        [Required]
        [StringLength(50)]
        public string FilingNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string CNRNumber { get; set; }

        public decimal AmountOfDecree { get; set; }

        public decimal CollateralAmount { get; set; }

        public byte CourtCaseStagePrmKey { get; set; }

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
        public virtual ICollection<PersonCourtCaseMakerChecker> PersonCourtCaseMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountCourtCaseDetail> CustomerLoanAccountCourtCaseDetails { get; set; }
    }
}
