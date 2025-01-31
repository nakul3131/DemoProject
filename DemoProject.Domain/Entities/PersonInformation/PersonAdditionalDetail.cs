using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation.SystemEntity;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonAdditionalDetail")]
    public partial class PersonAdditionalDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonAdditionalDetail()
        {
            PersonAdditionalDetailMakerCheckers = new HashSet<PersonAdditionalDetailMakerChecker>();
            PersonAdditionalDetailTranslations = new HashSet<PersonAdditionalDetailTranslation>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte PersonTypePrmKey { get; set; }

        public byte PersonCategoryPrmKey { get; set; }

        public short BirthCityPrmKey { get; set; }

        public bool IsResident { get; set; }

        public bool HasOtherCountryResidentStatus { get; set; }

        public byte GenderPrmKey { get; set; }

        public byte BloodGroupPrmKey { get; set; }

        public byte MaritalStatusPrmKey { get; set; }

        public DateTime? DateOfMarriage { get; set; }

        [Required]
        [StringLength(50)]
        public string LifePartnerName { get; set; }

        [Required]
        [StringLength(50)]
        public string LifePartnerMaidenName { get; set; }

        public short CastCategoryPrmKey { get; set; }

        public short EducationalQualificationPrmKey { get; set; }

        public short OccupationPrmKey { get; set; }

        public byte PhysicalStatusPrmKey { get; set; }

        public byte PovertyStatusPrmKey { get; set; }

        public bool IsEmployee { get; set; }

        public bool IsPolitician { get; set; }

        [Required]
        [StringLength(1500)]
        public string PoliticialBackgroundDetails { get; set; }

        public byte VIPRank { get; set; }

        [Required]
        [StringLength(1500)]
        public string VIPBackgroundDetails { get; set; }

        public bool IsSubmitedForm60 { get; set; }

        public bool IsIncomeTaxPayer { get; set; }

        public bool IsSubmitedForm15G { get; set; }

        public bool IsSubmitedForm15H { get; set; }

        public bool IsGSTTaxPayer { get; set; }

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

        public virtual PersonCategory PersonCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAdditionalDetailMakerChecker> PersonAdditionalDetailMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAdditionalDetailTranslation> PersonAdditionalDetailTranslations { get; set; }
    }
}
