using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("InwardOutwardTypeTranslation")]
    public partial class InwardOutwardTypeTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InwardOutwardTypeTranslation()
        {
          InwardOutwardTypeTranslationMakerCheckers = new HashSet<InwardOutwardTypeTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short InwardOutwardTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfInwardOutwardType { get; set; }

        [Required]
        [StringLength(50)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    
        public virtual InwardOutwardType InwardOutwardType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InwardOutwardTypeTranslationMakerChecker> InwardOutwardTypeTranslationMakerCheckers { get; set; }
    }
}
