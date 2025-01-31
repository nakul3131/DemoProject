using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Master.General.Notice
{
    [Table("NoticeScheduleOnDaysOfMonth")]
    public partial class NoticeScheduleOnDaysOfMonth
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeScheduleOnDaysOfMonth()
        {
            NoticeScheduleOnDaysOfMonthMakerCheckers = new HashSet<NoticeScheduleOnDaysOfMonthMakerChecker>();
            NoticeScheduleOnDaysOfMonthTimes = new HashSet<NoticeScheduleOnDaysOfMonthTime>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid NoticeScheduleOnDaysOfMonthId { get; set; }

        public short NoticeSchedulePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int MonthPrmKey { get; set; }

        public int DayOfMonthPrmKey { get; set; }

        public bool IsEveryMonth { get; set; }

        public short MonthInterval { get; set; }

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
        public virtual ICollection<NoticeScheduleOnDaysOfMonthMakerChecker> NoticeScheduleOnDaysOfMonthMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleOnDaysOfMonthTime> NoticeScheduleOnDaysOfMonthTimes { get; set; }
    }
}
