using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonInsuranceDetail")]
    public partial class PersonInsuranceDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonInsuranceDetail()
        {
            //PersonInsuranceBorrowingDetails = new HashSet<PersonInsuranceBorrowingDetail>();
            PersonInsuranceDetailMakerCheckers = new HashSet<PersonInsuranceDetailMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonInsuranceDetailId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte InsuranceTypePrmKey { get; set; }

        public short InsuranceCompanyPrmKey { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? MaturityDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(50)]
        public string PolicyNumber { get; set; }

        public decimal PolicyPremium { get; set; }

        public decimal PolicySumAssured { get; set; }

        public short OverduesPremium { get; set; }

        public bool HasAnyMortgage { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person person { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<PersonInsuranceBorrowingDetail> PersonInsuranceBorrowingDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonInsuranceDetailMakerChecker> PersonInsuranceDetailMakerCheckers { get; set; }
    }
}
