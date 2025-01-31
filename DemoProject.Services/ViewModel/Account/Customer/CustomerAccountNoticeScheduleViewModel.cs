using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountNoticeScheduleViewModel
    {
        public long PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public short NoticeTypePrmKey { get; set; }

        public byte CommunicationMediaPrmKey { get; set; }

        public short SchedulePrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }


        // CustomerAccountNoticeScheduleMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountNoticeSchedulePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // CustomerAccountId

        public Guid CustomerAccountId { get; set; }

        [StringLength(100)]
        public string NameOfCustomerAccount { get; set; }

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
