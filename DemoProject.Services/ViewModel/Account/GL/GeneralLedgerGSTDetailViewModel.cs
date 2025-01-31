using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.GL
{
    public class GeneralLedgerGSTDetailViewModel
    {
        public short PrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool IsService { get; set; }

        [StringLength(20)]
        public string HsnSacCode { get; set; }

        public bool IsAllowReverseChargeMechanism { get; set; }

        public bool IsEligibleForInputTaxCredit { get; set; }

        public decimal TaxRate { get; set; }

        public decimal CGSTRate { get; set; }

        public decimal SGSTRate { get; set; }

        public decimal IGSTRate { get; set; }

        public decimal CessRate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //GeneralLedgerGSTDetailMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short GeneralLedgerGSTDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

    }
}
