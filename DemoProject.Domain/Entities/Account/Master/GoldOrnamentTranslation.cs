using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("GoldOrnamentTranslation")]
    public partial class GoldOrnamentTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoldOrnamentTranslation()
        {
            GoldOrnamentTranslationMakerCheckers = new HashSet<GoldOrnamentTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short GoldOrnamentPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfGoldOrnament { get; set; }

        [Required]
        [StringLength(10)]
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

        public virtual GoldOrnament GoldOrnament { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoldOrnamentTranslationMakerChecker> GoldOrnamentTranslationMakerCheckers { get; set; }
    }
}
