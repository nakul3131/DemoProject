using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Establishment
{
    [Table("OrganizationTranslation")]
    public partial class OrganizationTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrganizationTranslation()
        {
            OrganizationTranslationMakerCheckers = new HashSet<OrganizationTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public byte OrganizationPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfOrganization { get; set; }

        [Required]
        [StringLength(50)]
        public string TransShortName { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransMotto { get; set; }

        [Required]
        [StringLength(2500)]
        public string TransVision { get; set; }

        [Required]
        [StringLength(2500)]
        public string TransMission { get; set; }

        [Required]
        [StringLength(2500)]
        public string TransStandards { get; set; }

        [Required]
        [StringLength(150)]
        public string TransCoopRegistrationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransCoopReferenceNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransRBIRegistrationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransRBIReferenceNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string TransRegistrationAddressDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }
        
        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Organization Organization { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationTranslationMakerChecker> OrganizationTranslationMakerCheckers { get; set; }
    }
}
