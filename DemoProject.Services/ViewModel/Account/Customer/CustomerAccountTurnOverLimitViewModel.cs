using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountTurnOverLimitViewModel
    {
        public long PrmKey { get; set; }
        
        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short FrequencyPrmKey { get; set; }

        public byte TransactionTypePrmKey { get; set; }

        public decimal Amount { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //CustomerAccountTurnOverLimitMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountTurnOverLimitPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropdownList 

        public Guid FrequencyId { get; set; }

        public string NameOfFrequency { get; set; }

        public Guid TransactionTypeId { get; set; }

        public string NameOfTransactionType { get; set; }

    }
}
