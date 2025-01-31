using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.SystemEntity
{
    [Table("ScheduleFrequency")]
    public partial class ScheduleFrequency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ScheduleFrequency()
        {
            ScheduleFrequencyMakerCheckers = new HashSet<ScheduleFrequencyMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid ScheduleFrequencyId { get; set; }

        public short SchedulePrmKey { get; set; }

        public byte ScheduleTypePrmKey { get; set; }

        public short NumberOfDays { get; set; }

        public byte DaysOfWeekPrmKey { get; set; }

        public byte DaysOfMonthPrmKey { get; set; }

        public DateTime SpecifiedDate { get; set; }

        public TimeSpan ScheduleTime { get; set; }

        public short Recur { get; set; }

        public bool IsEvery { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual DaysOfMonth DaysOfMonth { get; set; }

        public virtual DaysOfWeek DaysOfWeek { get; set; }

        public virtual Schedule Schedule { get; set; }

        public virtual ScheduleType ScheduleType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ScheduleFrequencyMakerChecker> ScheduleFrequencyMakerCheckers { get; set; }
        
    }
}
