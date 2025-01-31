using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class TransactionCustomerAccountViewModel
    {
        public long PrmKey { get; set; }

        public int TransactionMasterPrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public decimal Amount { get; set; }

        public bool IsCredit { get; set; }

        public decimal Balance { get; set; }

        [StringLength(1500)]
        public string Narration { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //TransactionCustomerAccountMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long TransactionCustomerAccountPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Dropdown  
        public Guid PersonId { get; set; }
        public Guid TransactionCustomerAccountId { get; set; }
        public Guid BusinessOfficeId { get; set; }

        
    }
}
