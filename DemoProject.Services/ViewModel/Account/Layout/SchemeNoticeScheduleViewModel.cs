using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeNoticeScheduleViewModel
    {
        // SchemeNoticeSchedule

        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short NoticeTypePrmKey { get; set; }

        public byte CommunicationMediaPrmKey { get; set; }

        public short SchedulePrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeNoticeScheduleMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeNoticeSchedulePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Scheme

        public Guid SchemeId { get; set; }

        [StringLength(100)]
        public string NameOfScheme { get; set; }

        //Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        // Dropdown

        public Guid NoticeTypeId { get; set; }

        public string NameOfNoticeType { get; set; }

        public Guid CommunicationMediaId { get; set; }

        public string NameOfCommunicationMedia { get; set; }

        public Guid ScheduleId { get; set; }

        public string NameOfSchedule { get; set; }
    }
}
