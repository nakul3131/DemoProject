using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeHomeLoanViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableMultipleDisbursement { get; set; }

        public byte MaximumNumberOfTimeDisbursement { get; set; }

        public short MinimumMoratoriumPeriod { get; set; }

        public short MaximumMoratoriumPeriod { get; set; }

        public bool IsMoratoriumForBoth { get; set; }

        public byte MinimumLTVRatio { get; set; }

        public byte MaximumLTVRatio { get; set; }

        [StringLength(1)]
        public string CollateralInsurance { get; set; }

        [StringLength(1500)]
        public string LocatedAreaRemark { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeHomeLoanViewModelMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeHomeLoanPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }


    }
}
