using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{

    [Table("PowerAndFunctionTranslation")]
    public partial class PowerAndFunctionTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PowerAndFunctionTranslation()
        {
            PowerAndFunctionTranslationMakerCheckers = new HashSet<PowerAndFunctionTranslationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int PowerAndFunctionPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(4000)]
        public string TransNameOfPowerAndFunction { get; set; }

        [Required]
        [StringLength(150)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(1000)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PowerAndFunction PowerAndFunction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PowerAndFunctionTranslationMakerChecker> PowerAndFunctionTranslationMakerCheckers { get; set; }
    }
}
