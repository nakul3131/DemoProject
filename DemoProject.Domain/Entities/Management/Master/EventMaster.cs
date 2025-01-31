using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("EventMaster")]
    public partial class EventMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventMaster()
        {
            EventMasterMakerCheckers = new HashSet<EventMasterMakerChecker>();
            EventMasterModifications = new HashSet<EventMasterModification>();
            EventMasterTranslations = new HashSet<EventMasterTranslation>();
            EventRepeatReminders = new HashSet<EventRepeatReminder>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid EventMasterId { get; set; }

        public short EventTypePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfEvent { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string EventDescription { get; set; }

        public DateTime ScheduledFrom { get; set; }

        public DateTime ScheduledTo { get; set; }

        public DateTime TriggeredAt { get; set; }

        public bool IsFullDayEvent { get; set; }

        public bool IsActive { get; set; }

        public bool IsSystemGenerated { get; set; }

        [Required]
        [StringLength(2048)]
        public string RedirectUrl { get; set; }

        public bool EnableReminder { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public virtual EventType EventType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventMasterMakerChecker> EventMasterMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventMasterModification> EventMasterModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventMasterTranslation> EventMasterTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventRepeatReminder> EventRepeatReminders { get; set; }
    }
}
