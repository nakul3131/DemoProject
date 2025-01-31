using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Establishment
{
    [Table("Organization")]
    public partial class Organization
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Organization()
        {
            OrganizationMakerCheckers = new HashSet<OrganizationMakerChecker>();
            OrganizationTranslations = new HashSet<OrganizationTranslation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid OrganizationId { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte ModificationNumber { get; set; }

        public short OrganizationType { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfOrganization { get; set; }

        [Required]
        [StringLength(50)]
        public string ShortName { get; set; }

        [Required]
        [StringLength(1500)]
        public string Motto { get; set; }

        [Required]
        [StringLength(2500)]
        public string Vision { get; set; }

        [Required]
        [StringLength(2500)]
        public string Mission { get; set; }

        [Required]
        [StringLength(2500)]
        public string Standards { get; set; }

        [Required]
        [StringLength(50)]
        public string OrganizationCode { get; set; }

        public DateTime CoopRegistrationDate { get; set; }

        [Required]
        [StringLength(150)]
        public string CoopRegistrationNumber { get; set; }

        public int CoopClassification { get; set; }

        public int CoopSubClassification { get; set; }

        [Required]
        [StringLength(150)]
        public string CoopReferenceNumber { get; set; }

        public DateTime? CoopDateOfClassificationAdvice { get; set; }

        public DateTime? LastElectionDate { get; set; }

        public DateTime RBIRegistrationDate { get; set; }

        [Required]
        [StringLength(150)]
        public string RBIRegistrationNumber { get; set; }

        public int RBIClassification { get; set; }

        public int RBISubClassification { get; set; }

        [Required]
        [StringLength(150)]
        public string RBIReferenceNumber { get; set; }

        public DateTime? RBIDateOfClassificationAdvice { get; set; }

        [Required]
        [StringLength(500)]
        public string RegistrationAddressDetails { get; set; }

        public short CenterPrmKey { get; set; }

        public byte AreaOfOperationPrmKey { get; set; }

        public short LanguageOfBookPrmKey { get; set; }

        [Required]
        [StringLength(10)]
        public string PANNumber { get; set; }
        
        [Required]
        [StringLength(150)]
        public string OfficialWebSite { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationMakerChecker> OrganizationMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationTranslation> OrganizationTranslations { get; set; }
    }
}
