using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeCurrencyViewModel
    {
        // BusinessOfficeCurrency

        public int PrmKey { get; set; }

        public Guid BusinessOfficeCurrencyId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

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

        // BusinessOfficeCurrencyMenuMakerCheker

        public DateTime EntryDateTime { get; set; }

        public int BusinessOfficeCurrencyPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        public Guid CurrencyId { get; set; }

        public string NameOfCurrency { get; set; }

    }
}
