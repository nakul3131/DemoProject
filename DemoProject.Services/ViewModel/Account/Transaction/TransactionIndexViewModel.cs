using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
  public  class TransactionIndexViewModel
    {
        public int PrmKey { get; set; }

        public Guid TransactionMasterId { get; set; }

        [StringLength(100)]
        public string NameOfCustomerAccount { get; set; }

        public string TransactionNumber { get; set; }

        public DateTime EntryDateTime { get; set; }

        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
