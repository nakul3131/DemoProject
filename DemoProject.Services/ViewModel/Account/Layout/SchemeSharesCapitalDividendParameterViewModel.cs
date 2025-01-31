using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeSharesCapitalDividendParameterViewModel
    {
        // SchemeSharesCapitalDividendParameter
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public byte FinancialYearForSharesBalance { get; set; }

        public short MembershipAgeForDividend { get; set; }

        public short ExMemberAgeForDividend { get; set; }

        public decimal MinimumDividendPercentage { get; set; }

        public decimal MaximumDividendPercentage { get; set; }

        public byte DividendCalculationMethodPrmKey { get; set; }

        [StringLength(3)]
        public string RoundMethod { get; set; }

        public byte RoundNearest { get; set; }

        public short TimePeriodToCeaseUnclaimedDividend { get; set; }

        [StringLength(3)]
        public string CeasedDefaulterDividendAction { get; set; }

        [StringLength(3)]
        public string CeasedDefaulterGuarantorDividendAction { get; set; }

        public bool EnableAccountCustomisation { get; set; }

        public bool EnableDividendAmountCustomisation { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }   

        //SchemeSharesCapitalDividendParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeSharesCapitalDividendParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Scheme

        public Guid SchemeId { get; set; }

        [StringLength(100)]
        public string NameOfScheme { get; set; }

        //Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        // Dropdown

        public Guid DividendCalculationMethodId { get; set; }
    }
}
