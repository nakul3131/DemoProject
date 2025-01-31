using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("AgendaTranslation")]
    public partial class AgendaTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AgendaTranslation()
        {
            AgendaTranslationMakerCheckers = new HashSet<AgendaTranslationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int AgendaPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(4000)]
        public string TransNameOfAgenda { get; set; }

        [Required]
        [StringLength(100)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(4000)]
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

        public virtual Agenda Agenda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgendaTranslationMakerChecker> AgendaTranslationMakerCheckers { get; set; }
    }
}
