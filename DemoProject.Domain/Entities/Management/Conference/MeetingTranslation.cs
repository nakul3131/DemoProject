using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MeetingTranslation")]
    public partial class MeetingTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingTranslation()
        {
            MeetingTranslationMakerCheckers = new HashSet<MeetingTranslationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int MeetingPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string TransTitle { get; set; }

        [Required]
        [StringLength(3500)]
        public string TransObjective { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransFullAddress { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Meeting Meeting { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingTranslationMakerChecker> MeetingTranslationMakerCheckers { get; set; }
    }
}
