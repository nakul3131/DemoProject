using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Master.General.Notice
{
    [Table("NoticeScheduleModification")]
    public partial class NoticeScheduleModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeScheduleModification()
        {
            NoticeScheduleModificationMakerCheckers = new HashSet<NoticeScheduleModificationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short NoticeSchedulePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfNoticeSchedule { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short StartAfterDays { get; set; }

        public bool IsEveryDay { get; set; }

        public short IntervalBetweenDay { get; set; }

        public TimeSpan ScheduleTime { get; set; }

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
        public virtual ICollection<NoticeScheduleModificationMakerChecker> NoticeScheduleModificationMakerCheckers { get; set; }
    }
}
