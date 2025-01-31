using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Master.General.Notice
{
    [Table("NoticeScheduleOnDaysOfWeekTime")]
    public partial class NoticeScheduleOnDaysOfWeekTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeScheduleOnDaysOfWeekTime()
        {
            NoticeScheduleOnDaysOfWeekTimeMakerCheckers = new HashSet<NoticeScheduleOnDaysOfWeekTimeMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid NoticeScheduleOnDaysOfWeekTimeId { get; set; }

        public short NoticeScheduleOnDaysOfWeekPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public TimeSpan WeekScheduleTime { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual NoticeScheduleOnDaysOfWeek NoticeScheduleOnDaysOfWeek { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleOnDaysOfWeekTimeMakerChecker> NoticeScheduleOnDaysOfWeekTimeMakerCheckers { get; set; }
    }
}
