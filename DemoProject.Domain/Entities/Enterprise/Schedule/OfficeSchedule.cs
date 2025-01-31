using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Schedule
{
    [Table("OfficeSchedule")]
    public partial class OfficeSchedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OfficeSchedule()
        {
            OfficeScheduleMakerCheckers = new HashSet<OfficeScheduleMakerChecker>();
            OfficeScheduleModifications = new HashSet<OfficeScheduleModification>();
            OfficeScheduleTranslations = new HashSet<OfficeScheduleTranslation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public Guid OfficeScheduleId { get; set; }

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

        public TimeSpan OfficeWorkingDuration { get; set; }

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

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfficeScheduleMakerChecker> OfficeScheduleMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfficeScheduleModification> OfficeScheduleModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OfficeScheduleTranslation> OfficeScheduleTranslations { get; set; }
    }
}
