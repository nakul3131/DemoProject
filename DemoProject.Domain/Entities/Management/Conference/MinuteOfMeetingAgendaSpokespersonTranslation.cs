using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MinuteOfMeetingAgendaSpokespersonTranslation")]
    public partial class MinuteOfMeetingAgendaSpokespersonTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MinuteOfMeetingAgendaSpokespersonTranslation()
        {
            MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers = new HashSet<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int MinuteOfMeetingAgendaSpokespersonPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(3500)]
        public string TransSpeech { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual MinuteOfMeetingAgendaSpokesperson  MinuteOfMeetingAgendaSpokesperson { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MinuteOfMeetingAgendaSpokespersonTranslationMakerChecker> MinuteOfMeetingAgendaSpokespersonTranslationMakerCheckers { get; set; }
    }
}
