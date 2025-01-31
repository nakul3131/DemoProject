using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
  public  class TransactionAllViewModel
    {
        //TransactionMasterViewModel

        public Guid TransactionMasterId { get; set; }

        public Guid TransactionTypeId { get; set; }

        public Guid PersonId { get; set; }

        public short PeriodCodePrmKey { get; set; }

        public DateTime TransactionDate { get; set; }

        public byte TransactionTypePrmKey { get; set; }

        [StringLength(25)]
        public string TransactionNumber { get; set; }

        [StringLength(25)]
        public string TokenNumber { get; set; }

        [StringLength(500)]
        public string Narration { get; set; }

        [StringLength(500)]
        public string ByHand { get; set; }

        public bool IsBeginDay { get; set; }

        // TransactionCustomerAccount

        public Guid BusinessOfficeId { get; set; }

        public Guid GeneralLedgerId { get; set; }

        public Guid TransactionCustomerAccountId { get; set; }

        public long TransactionMasterPrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public int GeneralLedgerPrmKey { get; set; }

        public decimal Amount { get; set; }

        public bool IsCredit { get; set; }

        public decimal Balance { get; set; }

        // TransactionGeneralLedger

        [StringLength(1500)]
        public string Particulars { get; set; }

        //SharesTransactionViewModel
        public decimal SharesFaceValue { get; set; }

        public short NumberOfShares { get; set; }

        [StringLength(25)]
        public int StartSharesCertificateNumber { get; set; }

        [StringLength(25)]
        public int EndSharesCertificateNumber { get; set; }

        public bool IsPrinted { get; set; }

        public bool IsSharesCertificateIssued { get; set; }

        public long SharesCapitalTransactionPrmKey { get; set; }

        //TransactionCustomerAccountOtherSubscription
        public Guid TransactionCustomerAccountOtherSubscriptionId { get; set; }

        public long TransactionCustomerAccountPrmKey { get; set; }

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

        public DateTime EntryDateTime { get; set; }

        public long TransactionCustomerAccountOtherSubscriptionPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }
        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
    }
}
