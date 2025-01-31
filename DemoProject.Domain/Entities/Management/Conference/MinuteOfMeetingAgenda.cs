using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MinuteOfMeetingAgenda")]
    public partial class MinuteOfMeetingAgenda
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MinuteOfMeetingAgenda()
        {
            MinuteOfMeetingAgendaMakerCheckers = new HashSet<MinuteOfMeetingAgendaMakerChecker>();
            //MinuteOfMeetingSpokespeople = new HashSet<MinuteOfMeetingAgendaSpokesperson>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid MinuteOfMeetingAgendaId { get; set; }

        public int MeetingAgendaPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public TimeSpan AgendaStartTime { get; set; }

        public TimeSpan AgendaEndTime { get; set; }

        [Required]
        [StringLength(3)]
        public string Vote { get; set; }

        [Required]
        [StringLength(3500)]
        public string Resolution { get; set; }

        [Required]
        [StringLength(50)]
        public string ResolutionNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual MeetingAgenda MeetingAgenda { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MinuteOfMeetingAgendaMakerChecker> MinuteOfMeetingAgendaMakerCheckers { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<MinuteOfMeetingAgendaSpokesperson> MinuteOfMeetingSpokespeople { get; set; }
    }
}
