using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("BoardOfDirectorPowerAndFunctionTranslation")]
    public partial class BoardOfDirectorPowerAndFunctionTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BoardOfDirectorPowerAndFunctionTranslation()
        {
            BoardOfDirectorPowerAndFunctionTranslationMakerCheckers = new HashSet<BoardOfDirectorPowerAndFunctionTranslationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int BoardOfDirectorPowerAndFunctionPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string TransSequenceText { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual BoardOfDirectorPowerAndFunction BoardOfDirectorPowerAndFunction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BoardOfDirectorPowerAndFunctionTranslationMakerChecker> BoardOfDirectorPowerAndFunctionTranslationMakerCheckers { get; set; }
    }
}
