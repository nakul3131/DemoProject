using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("EventRepeatReminder")]
    public partial class EventRepeatReminder
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventRepeatReminder()
        {
            EventRepeatReminderMakerCheckers = new HashSet<EventRepeatReminderMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid EventRepeatReminderId { get; set; }

        public short EventMasterPrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string RepeatType { get; set; }

        public short Recur { get; set; }

        public bool IsEvery { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public byte MaximumReminderCount { get; set; }

        public byte ReminderInterval { get; set; }

        [Required]
        [StringLength(1)]
        public string ReminderIntervalUnit { get; set; }

        public bool IsActive { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual EventMaster EventMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventRepeatReminderMakerChecker> EventRepeatReminderMakerCheckers { get; set; }
    }
}
