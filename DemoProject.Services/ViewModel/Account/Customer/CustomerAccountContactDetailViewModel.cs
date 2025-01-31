using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountContactDetailViewModel
    {
        public long PrmKey { get; set; }

        public Guid CustomerAccountContactDetailId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public long PersonContactDetailPrmKey { get; set; }

        [StringLength(50)]
        public string VerificationCode { get; set; }

        public bool IsVerified { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //Maker Checker

        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountContactDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        // For SelectListItem

        [StringLength(50)]
        public string FieldValue { get; set; }

        public Guid ContactTypeId { get; set; }

        [StringLength(100)]
        public string NameOfContactType { get; set; }

    }
}
