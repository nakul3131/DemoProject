using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Master.General.Notice
{
    [Table("NoticeSchedule")]
    public partial class NoticeSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeSchedule()
        {
            NoticeScheduleMakerCheckers = new HashSet<NoticeScheduleMakerChecker>();
            NoticeScheduleModifications = new HashSet<NoticeScheduleModification>();
            NoticeScheduleOnDates = new HashSet<NoticeScheduleOnDate>();
            NoticeScheduleOnDaysOfMonths = new HashSet<NoticeScheduleOnDaysOfMonth>();
            NoticeScheduleOnDaysOfWeeks = new HashSet<NoticeScheduleOnDaysOfWeek>();
            NoticeScheduleTranslations = new HashSet<NoticeScheduleTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid NoticeScheduleId { get; set; }

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

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleMakerChecker> NoticeScheduleMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleModification> NoticeScheduleModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleOnDate> NoticeScheduleOnDates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleOnDaysOfMonth> NoticeScheduleOnDaysOfMonths { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleOnDaysOfWeek> NoticeScheduleOnDaysOfWeeks { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleTranslation> NoticeScheduleTranslations { get; set; }
    }
}
