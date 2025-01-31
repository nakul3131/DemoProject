using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountChequeDetailViewModel
    {
        //  CustomerAccountChequeDetail
        public int PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public short ChequeBookMasterPrmKey { get; set; }

        public int ChequeNumber { get; set; }

        [StringLength(3)]
        public string Status { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // CustomerAccountChequeDetailMakerChecker
        public DateTime EntryDateTime { get; set; }

        public int CustomerAccountChequeDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // other
        public Guid ChequeBookMasterId { get; set; }

        [StringLength(100)]
        public string NameOfCustomerAccount { get; set; }

        [StringLength(100)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
