using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeTransactionLimitViewModel
    {
        // BusinessOfficeTransactionLimit

        public short PrmKey { get; set; }

        public Guid BusinessOfficeTransactionLimitId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte TransactionTypePrmKey { get; set; } 

        public short CurrencyPrmKey { get; set; }

        public decimal MinimumAmountLimitForTransaction { get; set; }

        public decimal MaximumAmountLimitForTransaction { get; set; }

        public short MaximumNumberOfBackDaysForTransaction { get; set; }

        public decimal MinimumAmountLimitForVerification { get; set; }

        public decimal MaximumAmountLimitForVerification { get; set; }

        public short MaximumNumberOfBackDaysForVerification { get; set; }

        public decimal MinimumAmountLimitForAutoVerification { get; set; }

        public decimal MaximumAmountLimitForAutoVerification { get; set; }

        public short MaximumNumberOfBackDaysForAutoVerification { get; set; }

        public bool EnableCashDenomination { get; set; }

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

        // BusinessOfficeTransactionLimitMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeTransactionLimitPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        // DropDown  
        public Guid GeneralLedgerId { get; set; }

        public string NameOfGeneralLedger { get; set; } 

        public Guid TransactionTypeId { get; set; }

        public string NameOfTransactionType { get; set; }

        public Guid CurrencyId { get; set; }

        public string NameOfCurrency { get; set; }

    }
}
