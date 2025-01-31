using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Conference
{
    public class MeetingInviteeMemberViewModel
    {
        // MeetingInviteeMember

        public int PrmKey { get; set; }
        
        public int MeetingPrmKey { get; set; }

        public int CustomerSharesCapitalAccountPrmKey { get; set; }

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

        // MeetingInviteeMemberMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int MeetingInviteeMemberPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        // DropDown 
        public Guid MeetingId { get; set; }

        public string NameOfMeeting { get; set; }

        public Guid CustomerSharesCapitalAccountId { get; set; }

        public string NameOfCustomerSharesCapitalAccount { get; set; } 

    }
}
