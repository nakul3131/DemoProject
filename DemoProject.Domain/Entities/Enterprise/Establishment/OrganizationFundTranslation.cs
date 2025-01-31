using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Establishment
{
    [Table("OrganizationFundTranslation")]
    public partial class OrganizationFundTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrganizationFundTranslation()
        {
            OrganizationFundTranslationMakerCheckers = new HashSet<OrganizationFundTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short OrganizationFundPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(20)]
        public string TransSequenceNumberText { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual OrganizationFund OrganizationFund { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrganizationFundTranslationMakerChecker> OrganizationFundTranslationMakerCheckers { get; set; }
    }
}
