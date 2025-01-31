using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountStandingInstructionViewModel
    {
        // CustomerAccountStandingInstruction
        public long PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public long CustomerAccountNumber { get; set; }

        [StringLength(3)]
        public string InstructionFor { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // CustomerAccountStandingInstructionMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountStandingInstructionPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public bool EnableAutoDebit { get; set; } //new added

        public Guid DebitCustomerAccountNumberId { get; set; }

        public Guid CreditCustomerAccountNumberId { get; set; }

        public Guid InterestCustomerAccountNumberId { get; set; }
    }
}
