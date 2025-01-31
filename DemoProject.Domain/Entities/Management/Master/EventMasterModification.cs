using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("EventMasterModification")]
    public partial class EventMasterModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EventMasterModification()
        {
            EventMasterModificationMakerCheckers = new HashSet<EventMasterModificationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short EventMasterPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte EventTypePrmKey { get; set; }

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

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual EventMaster EventMaster { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EventMasterModificationMakerChecker> EventMasterModificationMakerCheckers { get; set; }
    }
}
