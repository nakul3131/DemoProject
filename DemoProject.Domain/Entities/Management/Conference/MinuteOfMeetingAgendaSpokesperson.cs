using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Management.Master;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MinuteOfMeetingSpokesperson")]
    public partial class MinuteOfMeetingAgendaSpokesperson
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MinuteOfMeetingAgendaSpokesperson()
        {
            MinuteOfMeetingAgendaSpokespersonMakerCheckers = new HashSet<MinuteOfMeetingAgendaSpokespersonMakerChecker>();
            MinuteOfMeetingAgendaSpokespersonTranslations = new HashSet<MinuteOfMeetingAgendaSpokespersonTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int MeetingAgendaPrmKey { get; set; } 

        public short BoardOfDirectorPrmKey { get; set; }

        public TimeSpan SpeakingStartTime { get; set; }

        public TimeSpan SpeakingEndTime { get; set; }

        [Required]
        [StringLength(3500)]
        public string Speech { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual MeetingAgenda MeetingAgenda { get; set; }

        public virtual BoardOfDirector BoardOfDirector { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")] 
        public virtual ICollection<MinuteOfMeetingAgendaSpokespersonMakerChecker> MinuteOfMeetingAgendaSpokespersonMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MinuteOfMeetingAgendaSpokespersonTranslation> MinuteOfMeetingAgendaSpokespersonTranslations { get; set; }
    }
}
