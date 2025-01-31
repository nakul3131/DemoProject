using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Security.Users
{
    public class UserProfileCurrencyViewModel
    {
        public int PrmKey { get; set; }

        public Guid UserProfileCurrencyId { get; set; }

        public short UserProfilePrmKey { get; set; }

        public short CurrencyPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

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

        //UserProfileCurrencyMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int UserProfileCurrencyPrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public Guid CurrencyId { get; set; }

        public Guid[] MultiCurrencyId { get; set; }

        [StringLength(100)]
        public string NameOfCurrency { get; set; }
    }
}
