using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Master.General.Notice
{
    [Table("NoticeScheduleOnDaysOfMonthTime")]
    public partial class NoticeScheduleOnDaysOfMonthTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeScheduleOnDaysOfMonthTime()
        {
            NoticeScheduleOnDaysOfMonthTimeMakerCheckers = new HashSet<NoticeScheduleOnDaysOfMonthTimeMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid NoticeScheduleOnDaysOfMonthTimeId { get; set; }

        public short NoticeScheduleOnDaysOfMonthPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public TimeSpan MonthScheduleTime { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual NoticeScheduleOnDaysOfMonth NoticeScheduleOnDaysOfMonth { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleOnDaysOfMonthTimeMakerChecker> NoticeScheduleOnDaysOfMonthTimeMakerCheckers { get; set; }
    }
}
