using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeBusinessLoanViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public decimal MinimumTurnOverAmount { get; set; }

        public byte CurrentBusinessMinimumAge { get; set; }

        public byte MinimumBusinessExperience { get; set; }

        public byte CapturePreviousProfitMakingYears { get; set; }

        public decimal MinimumAnnualIncome { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeBusinessLoanMakerChecker
        public short SchemeBusinessLoanPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public string NameOfUser { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
