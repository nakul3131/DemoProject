using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Schedule
{
    [Table("WorkingScheduleModification")]
    public partial class WorkingScheduleModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WorkingScheduleModification()
        {
            WorkingScheduleModificationMakerCheckers = new HashSet<WorkingScheduleModificationMakerChecker>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public byte WorkingSchedulePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfSchedule { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan WorkingDuration { get; set; }

        public TimeSpan MorningTeaTime { get; set; }

        public TimeSpan MorningTeaTimeDuration { get; set; }

        public TimeSpan LunchTime { get; set; }

        public TimeSpan LunchTimeDuration { get; set; }

        public TimeSpan EveningTeaTime { get; set; }

        public TimeSpan EveningTeaTimeDuration { get; set; }

        public TimeSpan DinnerTime { get; set; }

        public TimeSpan DinnerTimeDuration { get; set; }

        public byte WeeklyHoliday1 { get; set; }

        [Required]
        [StringLength(3)]
        public string WeeklyHoliday1Occurance { get; set; }

        public byte WeeklyHoliday2 { get; set; }

        [Required]
        [StringLength(3)]
        public string WeeklyHoliday2Occurance { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual WorkingSchedule WorkingSchedule { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WorkingScheduleModificationMakerChecker> WorkingScheduleModificationMakerCheckers { get; set; }
    }
}
