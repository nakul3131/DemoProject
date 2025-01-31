using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("CreditSocietyTranslation")]
    public partial class CreditSocietyTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CreditSocietyTranslation()
        {
            CreditSocietyTranslationMakerCheckers = new HashSet<CreditSocietyTranslationMakerChecker>();
        }
        [Key]
        public short PrmKey { get; set; }

        public short CreditSocietyPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfCreditSociety { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(20)]
        public string TransRegistrationNumber { get; set; }

        [Required]
        [StringLength(2500)]
        public string TransFullAddressDetail { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(500)]
        public string TransContactDetails { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual CreditSociety CreditSociety { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CreditSocietyTranslationMakerChecker> CreditSocietyTranslationMakerCheckers { get; set; }
    }
}
