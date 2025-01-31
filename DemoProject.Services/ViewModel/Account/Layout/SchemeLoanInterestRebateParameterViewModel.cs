using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanInterestRebateParameterViewModel
    {
        // SchemeLoanInterestRebateParameter

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte InterestRebateCriteriaPrmKey { get; set; }

        public byte InterestRebateApplyFrequencyPrmKey { get; set; }

        public bool IsApplicablePrePartPaymentForInterestRebate { get; set; }

        public bool IsApplicableForeClosureForInterestRebate { get; set; }

        public byte MinimumDuesInstallmentGracePeriodInDays { get; set; }

        public byte MaximumDuesInstallmentGracePeriodInDays { get; set; }

        public byte MinimumApplicableNumberOfLateInstallmentForInterestRebate { get; set; }

        public byte MaximumApplicableNumberOfLateInstallmentForInterestRebate { get; set; }

        public decimal InterestRebatePercentage { get; set; }

        public bool EnableManualOptionToSelectCustomerAccountForInterestRebate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeLoanInterestRebateParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanInterestRebateParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        public Guid GeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfGL { get; set; }

        public Guid InterestRebateCriteriaId { get; set; }

        [StringLength(100)]
        public string SysNameOfCriteria { get; set; }

        public Guid InterestRebateApplyFrequencyId { get; set; }

        public Guid FrequencyId { get; set; }

        //[StringLength(100)]
        //public string SysNameOfCriteria { get; set; }
    }
}
