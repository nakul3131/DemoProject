using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class TransactionDividendIndexViewModel
    {
        public int PrmKey { get; set; }

        public string CustomerFullName { get; set; }

        public long AccountNumber { get; set; }

        public int CustomerSharesCapitalAccountPrmKey { get; set; }

        public decimal SharesBalance { get; set; }

        public decimal DividendAmount { get; set; }

        public bool IsPaid { get; set; }

        [StringLength(1500)]
        public string Narration { get; set; }

        [StringLength(50)]
        public string SharesBalanceWithDividendAmount { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
