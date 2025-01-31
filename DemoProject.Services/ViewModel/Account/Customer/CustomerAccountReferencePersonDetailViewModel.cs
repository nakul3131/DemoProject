using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountReferencePersonDetailViewModel
    {
        public int PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public long CustomerAccountNumber { get; set; }

        public bool IsValidateSign { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        public Guid CustomerAccountId { get; set; }


        //CustomerAccountReferencePersonDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerAccountReferencePersonDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        [StringLength(150)]
        public string CustomerAccountNumberText { get; set; }
    }
}
