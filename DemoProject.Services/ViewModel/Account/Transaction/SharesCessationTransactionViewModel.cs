using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Management.Conference;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
   public class SharesCessationTransactionViewModel
    {
        public long PrmKey { get; set; }

        public long TransactionCustomerAccountPrmKey { get; set; }

        public int MinuteOfMeetingAgendaPrmKey { get; set; }

        public decimal SharesFaceValue { get; set; }

        public short NumberOfSharesCessation { get; set; }

        [StringLength(500)]
        public string CeasedSharesCertificateNumbers { get; set; }

        [StringLength(3)]
        public string CessionReason { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SharesCessationTransactionMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long SharesCessationTransactionPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

    }
}
