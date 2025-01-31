using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class TransactionViewModel
    {
        public int PrmKey { get; set; }

        public Guid TransactionMasterId { get; set; } 

        public short PeriodCodePrmKey { get; set; }  

        public DateTime TransactionDate { get; set; }

        public byte TransactionTypePrmKey { get; set; }

        public long TransactionNumber { get; set; }

        [StringLength(25)]
        public string TokenNumber { get; set; }

        [StringLength(500)]
        public string Narration { get; set; }

        [StringLength(150)]
        public string ByHand { get; set; }

        [StringLength(150)]
        public string Purpose { get; set; }
        
        [StringLength(25)]
        public string ReferenceNumber { get; set; }
        public decimal CGST { get; set; }
        public decimal GSTRate { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal SGST { get; set; }
        public decimal IGST { get; set; }
        public decimal CessRate { get; set; }
        public decimal CessAmount { get; set; }
        public bool IsApplicableForReverseCharge { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
      
        [StringLength(3)]
        public string EntryStatus { get; set; }

        // TransactionMasterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int TransactionMasterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Dropdown
        public Guid TransactionTypeId { get; set; }

        [StringLength(3)]
        public string PaymentMode { get; set; }

        public Guid PersonId { get; set; }

        //
        public TransactionCustomerAccountViewModel TransactionCustomerAccountViewModel { get; set; }
        
        public TransactionCashDenominationViewModel TransactionCashDenominationViewModel { get; set; }
        
        public TransactionGeneralLedgerViewModel TransactionGeneralLedgerViewModel { get; set; }
        
        public TransactionCustomerAccountOtherSubscriptionViewModel TransactionCustomerAccountOtherSubscriptionViewModel { get; set; }
        
        public SharesCessationTransactionViewModel SharesCessationTransactionViewModel { get; set; }
        
        public SharesCapitalTransactionViewModel SharesCapitalTransactionViewModel { get; set; }


    }
}
