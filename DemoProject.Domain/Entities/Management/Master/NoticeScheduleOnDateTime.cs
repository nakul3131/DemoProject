using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Master.General.Notice
{
    [Table("NoticeScheduleOnDateTime")]
    public partial class NoticeScheduleOnDateTime
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeScheduleOnDateTime()
        {
            NoticeScheduleOnDateTimeMakerCheckers = new HashSet<NoticeScheduleOnDateTimeMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid NoticeScheduleOnDateTimeId { get; set; }

        public short NoticeScheduleOnDatePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public TimeSpan DateScheduleTime { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual NoticeScheduleOnDate NoticeScheduleOnDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleOnDateTimeMakerChecker> NoticeScheduleOnDateTimeMakerCheckers { get; set; }
    }
}
