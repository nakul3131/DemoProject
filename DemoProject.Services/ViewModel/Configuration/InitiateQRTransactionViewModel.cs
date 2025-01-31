using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class InitiateQRTransactionViewModel
    {
        [StringLength(50)]
        public string CustomerName { get; set; }

        [StringLength(50)]
        public string BeneficiaryName { get; set; }

        public decimal Amount { get; set; }

        public decimal ProcessingFee { get; set; }

        [StringLength(3)]
        public string PaymentMode { get; set; }

        [StringLength(500)]
        public string VirtualPaymentAddress { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        [StringLength(25)]
        public string TransactionNumber { get; set; }

        [StringLength(10)]
        public string MobileNumber { get; set; }
    }
}
