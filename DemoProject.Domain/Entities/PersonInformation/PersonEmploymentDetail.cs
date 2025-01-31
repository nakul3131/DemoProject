using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.Customer;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonEmploymentDetail")]
    public partial class PersonEmploymentDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonEmploymentDetail()
        {
            PersonEmploymentDetailMakerCheckers = new HashSet<PersonEmploymentDetailMakerChecker>();
            PersonEmploymentDetailTranslations = new HashSet<PersonEmploymentDetailTranslation>();
            CustomerAccountEmploymentDetails = new HashSet<CustomerAccountEmploymentDetail>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOfEmployer { get; set; }

        public DateTime DateOfIncorporation { get; set; }

        public byte EmploymentTypePrmKey { get; set; }

        public byte EmployerNaturePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string EmployerNatureOtherDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string EmployerAddressDetails { get; set; }

        [Required]
        [StringLength(500)]
        public string EmployerContactDetails { get; set; }

        public short EmployerCityPrmKey { get; set; }

        public short DesignationPrmKey { get; set; }

        public decimal AnnualIncome { get; set; }

        [Required]
        [StringLength(50)]
        public string EPFNumber { get; set; }

        public short EmployedSince { get; set; }

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
        public virtual ICollection<PersonEmploymentDetailMakerChecker> PersonEmploymentDetailMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonEmploymentDetailTranslation> PersonEmploymentDetailTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountEmploymentDetail> CustomerAccountEmploymentDetails { get; set; }
    }
}
