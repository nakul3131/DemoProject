using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class SharesCapitalCustomerAccountIndexViewModel
    {
        public long  PrmKey { get; set; }

        public Guid CustomerAccountId { get; set; }

        [StringLength(100)]
        public string NameOfCustomerAccount { get; set; }

        public long AccountNumber { get; set; }

        public bool IsModified { get; set; }

        public DateTime EntryDateTime { get; set; }

        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
