using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDepositAgentParameterViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short AgentCommissionGeneralLedgerPrmKey { get; set; }

        public bool EnableCommissionOnOverDuesInstallment { get; set; }

        public byte MinimumOverDuesInstallment { get; set; }

        public byte MaximumOverDuesInstallment { get; set; }

        public byte DefaultOverDuesInstallment { get; set; }

        public bool EnableCommissionOnAdditionalInvestment { get; set; }

        public bool EnableCommissionOnAdvancePayment { get; set; }

        public byte MinimumAdditionalInstallment { get; set; }

        public byte MaximumAdditionalInstallment { get; set; }

        public decimal AgentCommissionPercentage { get; set; }

        public bool IsRequiredSecurity { get; set; }

        public decimal MinimumSecurityAmount { get; set; }

        public decimal MaximumSecurityAmount { get; set; }

        public decimal DefaultSecurityAmount { get; set; }

        public short CollectionMarginOverSecurity { get; set; }

        [StringLength(1)]
        public string AgentCollectionSettlementThrough { get; set; }

        public bool EnableCommisionDeductableOnForeclosure { get; set; }

        public bool EnableOverrideCommisionDeductionOnForeclosure { get; set; }

        public bool EnableCommisionDeductableOnForeclosureInLockInPeriod { get; set; }

        public bool EnableOverrideCommisionDeductionOnForeclosureInLockInPeriod { get; set; }

        public bool EnableDeceasedAccountInterestRate { get; set; }

        public bool EnableAgentCommisionDeductableOfDeceasedAccount { get; set; }

        public bool EnableOverrideCommisionDeductionOfDeceasedAccount { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeDepositAgentParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeDepositAgentParameterPrmKey { get; set; }

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

        // Dropdown

        public Guid AgentCommissionGeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfAgentCommissionGeneralLedger { get; set; }
    }
}
