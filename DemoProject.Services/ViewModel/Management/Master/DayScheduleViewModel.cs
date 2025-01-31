using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Master.General.Notice
{
    public class DayScheduleViewModel
    {
        //NoticeScheduleOnDateTime

        public short PrmKey { get; set; }

        public Guid NoticeScheduleOnDateTimeId { get; set; }

        public short NoticeScheduleOnDatePrmKey { get; set; }

        public TimeSpan DateScheduleTime { get; set; }

        public byte ModificationNumber { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //NoticeScheduleOnDateTimeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short NoticeScheduleOnDateTimePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

    }
}
