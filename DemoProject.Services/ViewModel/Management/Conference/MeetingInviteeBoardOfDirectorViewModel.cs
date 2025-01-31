using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Conference
{
    public class MeetingInviteeBoardOfDirectorViewModel
    {
        // MeetingInviteeBoardOfDirector 

        public int PrmKey { get; set; }
        
        public int MeetingPrmKey { get; set; }

        public short BoardOfDirectorPrmKey { get; set; }

        [StringLength(50)]
        public string NoticeReferenceNumber { get; set; }

        [StringLength(3)]
        public string NoticeStatus { get; set; }

        [StringLength(3)]
        public string AttendanceStatus { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // MeetingInviteeBoardOfDirectorMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int MeetingInviteeBoardOfDirectorPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        // DropDown 
        public Guid MeetingId { get; set; }

        public string NameOfMeeting { get; set; }

        public Guid BoardOfDirectorId { get; set; }

        public string NameOfBoardOfDirector { get; set; }
    }
}
