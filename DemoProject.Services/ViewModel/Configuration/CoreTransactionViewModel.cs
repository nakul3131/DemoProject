using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class CoreTransactionViewModel
    {
        public int PrmKey { get; set; }

        public Guid CoreTransactionViewModelId { get; set; }

        [StringLength(50)]
        public string CustomerName { get; set; }

        public string BeneficiaryCode { get; set; }

        [StringLength(50)]
        public string BeneficiaryName { get; set; }

        [StringLength(20)]
        public string BeneficiaryAccountNumber { get; set; }

        [StringLength(10)]
        public string MobileNumber { get; set; }

        public decimal ProcessingFee { get; set; }

        public decimal Amount { get; set; }

        [StringLength(4)]
        public string PaymentMode { get; set; }

        [StringLength(25)]
        public string TransactionNumber { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
    }
}
