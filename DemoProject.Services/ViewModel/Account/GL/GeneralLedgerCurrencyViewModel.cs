using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.GL
{
    public class GeneralLedgerCurrencyViewModel
    {
        public short PrmKey { get; set; }

        public Guid GeneralLedgerCurrencyId { get; set; }

        public byte ModificationNumber { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public short CurrencyPrmKey { get; set; }

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

        //GeneralLedgerCurrencyMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short GeneralLedgerCurrencyPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Currency

        public Guid CurrencyId { get; set; }

        public Guid[] MultiCurrencyId { get; set; }

        [StringLength(100)]
        public string NameOfCurrency { get; set; }
    }
}
