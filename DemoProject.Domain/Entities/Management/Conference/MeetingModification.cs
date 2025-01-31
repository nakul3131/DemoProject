using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Conference
{
    [Table("MeetingModification")]
    public partial class MeetingModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MeetingModification()
        {
            MeetingModificationMakerCheckers = new HashSet<MeetingModificationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int MeetingPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(3500)]
        public string Objective { get; set; }

        [Required]
        [StringLength(1500)]
        public string FullAddress { get; set; }

        public DateTime MeetingDate { get; set; }

        public TimeSpan ArrivalTime { get; set; }

        public bool IsAllowancePayble { get; set; }

        public bool IsPayByCash { get; set; }

        public TimeSpan CommencementTime { get; set; }

        public TimeSpan AdjournmentTime { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public DateTime NextMeetingDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Meeting Meeting { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MeetingModificationMakerChecker> MeetingModificationMakerCheckers { get; set; }
    }
}
