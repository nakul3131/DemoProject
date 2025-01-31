using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class CreditTransactionViewModel
    {
        public short NumberOfTransactionLimit { get; set; }

        public decimal MinimumTransactionAmountLimit { get; set; }

        public decimal MaximumTransactionAmountLimit { get; set; }

        [StringLength(500)]
        public string Narration { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
    }
}
