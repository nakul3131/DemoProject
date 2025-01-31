using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Master.General.Notice
{
    [Table("NoticeScheduleOnDate")]
    public partial class NoticeScheduleOnDate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NoticeScheduleOnDate()
        {
            NoticeScheduleOnDateMakerCheckers = new HashSet<NoticeScheduleOnDateMakerChecker>();
            NoticeScheduleOnDateTimes = new HashSet<NoticeScheduleOnDateTime>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid NoticeScheduleOnDateId { get; set; }

        public short NoticeSchedulePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ScheduleDate { get; set; }

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
        public virtual ICollection<NoticeScheduleOnDateMakerChecker> NoticeScheduleOnDateMakerCheckers { get; set; }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NoticeScheduleOnDateTime> NoticeScheduleOnDateTimes { get; set; }
    }
}
