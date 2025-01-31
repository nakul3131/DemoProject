using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDepositAccountRenewalParameterViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte MaximumRenewalDurationAfterMaturityInDays { get; set; }

        public bool EnableRenewalOnHoldiay { get; set; }

        public bool EnableRenewalOnSameDayOfAnyMonth { get; set; }

        public bool EnableAutoRenewal { get; set; }

        public bool EnableAutoRenewalOnHoldiay { get; set; }

        public byte MinimumDurationForAutoRenewal { get; set; }

        public byte MaximumDurationForAutoRenewal { get; set; }

        public byte DefaultDurationForAutoRenewal { get; set; }

        [StringLength(3)]
        public string AccountNumberOnRenewal { get; set; }

        [StringLength(3)]
        public string CertificateNumberOnRenewal { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeDepositAccountRenewalParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeDepositAccountRenewalParameterPrmKey { get; set; }

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
    }
}
