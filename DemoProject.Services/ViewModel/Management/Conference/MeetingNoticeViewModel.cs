using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Conference
{
    public class MeetingNoticeViewModel
    {
        // MeetingNotice 

        public int PrmKey { get; set; }
        
        public int MeetingPrmKey { get; set; }

        public short NoticeMediaPrmKey { get; set; }

        public short SchedulePrmKey { get; set; }

        public int MenuPrmKey { get; set; } 

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // MeetingNoticeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int MeetingNoticePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        // DropDown 
        public Guid MeetingId { get; set; }

        public string NameOfMeeting { get; set; } 

        public Guid NoticeMediaId { get; set; }

        public string NameOfNoticeMedia { get; set; }

        public Guid ScheduleId { get; set; }

        public string NameOfSchedule { get; set; }

        public Guid MenuId { get; set; }

        public string NameOfMenu { get; set; } 

    }
}
