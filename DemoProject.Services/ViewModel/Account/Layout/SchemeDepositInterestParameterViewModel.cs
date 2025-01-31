using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDepositInterestParameterViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public byte InterestMethodPrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public decimal MinimumInterestRate { get; set; }

        public decimal MaximumInterestRate { get; set; }

        public byte InterestRateChargedDurationPrmKey { get; set; }

        public byte PrematureVoidInterestPeriod { get; set; }

        public short InterestCalculationStartingPeriod { get; set; }

        public bool EnableInterestCalculationFromDepositDate { get; set; }

        public bool EnablePrematureInterestCalculation { get; set; }

        public bool TakePrematureInterestRateSameAsSaving { get; set; }

        public decimal LessInterestRateForPrematurity { get; set; }

        public bool EnablePostMatureInterestCalculation { get; set; }

        public short PostMatureVoidInterestPeriod { get; set; }

        public bool TakePostMatureInterestRateSameAsSaving { get; set; }

        public bool TakePostMatureInterestRateSameAsMaturityDate { get; set; }

        public bool TakePostMatureInterestRateSameAsCurrentDate { get; set; }

        public bool EnableInterestProvision { get; set; }

        public bool EnablePeriodicInterestPayout { get; set; }

        public byte MinimumMonthForPeriodicInterestPayout { get; set; }

        public bool EnableCustomisePayoutInterestDayInAccountOpening { get; set; }

        [StringLength(3)]
        public string InterestPayoutDay { get; set; }

        public byte InterestPayoutDayOther { get; set; }

        public bool EnablePayoutInterestAmountOverride { get; set; }

        public decimal MinimumOverrideInterestAmount { get; set; }

        public decimal MaximumOverrideInterestAmount { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeDepositInterestParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeDepositInterestParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(30)]
        public string RateOfInterestUnitText { get; set; }

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

        public Guid InterestMethodId { get; set; }

        [StringLength(100)]
        public string NameOfInterestMethod { get; set; }

        public Guid GeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfGeneralLedger { get; set; }

        public Guid InterestRateChargedDurationId { get; set; }

        [StringLength(100)]
        public string NameOfInterestRateChargedDuration { get; set; }
    }
}
