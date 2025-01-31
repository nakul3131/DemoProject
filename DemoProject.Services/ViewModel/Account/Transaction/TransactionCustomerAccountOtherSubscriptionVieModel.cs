using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
  public  class TransactionCustomerAccountOtherSubscriptionViewModel
    {
        public long PrmKey { get; set; }

        public Guid TransactionCustomerAccountOtherSubscriptionId { get; set; }

        public long TransactionCustomerAccountPrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public decimal Amount { get; set; }

        public decimal[] Amountlist { get; set; }

        public short[] GeneralLedgerPrmKeyList { get; set; }

        public short Gl1PrmKey { get; set; }

        public short Gl2PrmKey { get; set; }

        public short Gl3PrmKey { get; set; }

        public short Gl4PrmKey { get; set; }

        public short Gl5PrmKey { get; set; }

        public decimal Gl1Amount { get; set; }

        public decimal Gl2Amount { get; set; }

        public decimal Gl3Amount { get; set; }

        public decimal Gl4Amount { get; set; }

        public decimal Gl5Amount { get; set; }


        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //TransactionCustomerAccountOtherSubscriptionMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long TransactionCustomerAccountOtherSubscriptionPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        
    }
}
