using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Configuration
{
    public class PostCallbackApprovalTransactionViewModel
    {
        [StringLength(20)]
        public string TransactionNumber { get; set; }

        [StringLength(20)]
        public string UniqueId { get; set; }

        public decimal TransactionAmount { get; set; }

        [StringLength(15)]
        public string Status { get; set; }

        public short ApprovalCode { get; set; }

        [StringLength(1500)]
        public string Filler1 { get; set; }

        [StringLength(1500)]
        public string Filler2 { get; set; }

        [StringLength(1500)]
        public string Filler3 { get; set; }

        [StringLength(1500)]
        public string Filler4 { get; set; }

        [StringLength(1500)]
        public string Filler5 { get; set; }

        [StringLength(1500)]
        public string Filler6 { get; set; }
    }
}
