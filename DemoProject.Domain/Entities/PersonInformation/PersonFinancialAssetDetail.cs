using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonFinancialAssetDetail")]
    public partial class PersonFinancialAssetDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonFinancialAssetDetail()
        {
            PersonFinancialAssetDetailMakerCheckers = new HashSet<PersonFinancialAssetDetailMakerChecker>();
            PersonFinancialAssetDetailTranslations = new HashSet<PersonFinancialAssetDetailTranslation>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonFinancialAssetDetailId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte FinancialOrganizationTypePrmKey { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOfFinancialOrganization { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfBranch { get; set; }

        [Required]
        [StringLength(1500)]
        public string AddressDetails { get; set; }

        [Required]
        [StringLength(500)]
        public string ContactDetails { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime MaturityDate { get; set; }

        public byte FinancialAssetTypePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string FinancialAssetDescription { get; set; }

        [Required]
        [StringLength(500)]
        public string ReferenceNumber { get; set; }

        public decimal InvestedAmount { get; set; }

        public decimal CurrentMarketValue { get; set; }

        public decimal MonthlyInterestIncomeAmount { get; set; }

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
        public virtual ICollection<PersonFinancialAssetDetailMakerChecker> PersonFinancialAssetDetailMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonFinancialAssetDetailTranslation> PersonFinancialAssetDetailTranslations { get; set; }
    }
}
