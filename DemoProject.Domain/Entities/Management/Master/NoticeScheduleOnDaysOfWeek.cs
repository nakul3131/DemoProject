using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Master.General.Notice
{
    [Table("NoticeScheduleOnDaysOfWeek")]
    public partial class NoticeScheduleOnDaysOfWeek
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeScheduleOnDaysOfWeek()
        {
            NoticeScheduleOnDaysOfWeekMakerCheckers = new HashSet<NoticeScheduleOnDaysOfWeekMakerChecker>();
            NoticeScheduleOnDaysOfWeekTimes = new HashSet<NoticeScheduleOnDaysOfWeekTime>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid NoticeScheduleOnDaysOfWeekId { get; set; }

        public short NoticeSchedulePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int DayOfWeekPrmKey { get; set; }

        public bool IsEveryWeek { get; set; }

        public short WeekInterval { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual NoticeSchedule NoticeSchedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleOnDaysOfWeekMakerChecker> NoticeScheduleOnDaysOfWeekMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleOnDaysOfWeekTime> NoticeScheduleOnDaysOfWeekTimes { get; set; }
    }
}
