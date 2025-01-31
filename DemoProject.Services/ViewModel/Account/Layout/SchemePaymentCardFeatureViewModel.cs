using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemePaymentCardFeatureViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short PaymentCardPrmKey { get; set; }

        public byte ChargesDuration { get; set; }

        [StringLength(1)]
        public string ChargesDurationUnit { get; set; }

        public decimal ChargesAmount { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemePaymentCardFeatureMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemePaymentCardFeaturePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Scheme

        public Guid SchemeId { get; set; }

        [StringLength(100)]
        public string NameOfScheme { get; set; }

        //Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        // Dropdown

        public Guid PaymentCardId { get; set; }

        [StringLength(100)]
        public string NameOfPaymentCard { get; set; }       
    }
}
