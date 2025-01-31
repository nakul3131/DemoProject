using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountEmailServiceViewModel
    {
        // CustomerAccountEmailService
        public long PrmKey { get; set; }
        
        public long CustomerAccountPrmKey { get; set; }

        public bool EnableAllservices { get; set; }

        public bool EnableCreditTransaction { get; set; }

        public bool EnableDebitTransaction { get; set; }

        public bool EnableInsufficientBalance { get; set; }

        public bool EnableStatement { get; set; }

        [StringLength(3)]
        public string StatementFrequency { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(1500)]
        public string ActivationStatus { get; set; }

        // CustomerAccountEmailServiceMakerChecker        

        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountEmailServicePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid StatementFrequencyId { get; set; }

        [StringLength(100)]
        public string NameOfCustomerAccount { get; set; }

        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
