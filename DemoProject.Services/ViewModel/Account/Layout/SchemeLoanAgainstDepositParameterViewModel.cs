using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanAgainstDepositParameterViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public string DepositType { get; set; }

        public bool IsApplicableAllGeneralLedgers { get; set; }

        public bool IsOverDraftLoan { get; set; }

        public bool IsTakenAsCollateralSecurity { get; set; }

        public bool IsAllowAutoRenew { get; set; }

        public bool IsAllowAutoClosure { get; set; }

        public bool EnableMaximumTenureUptoMaturityDate { get; set; }

        public decimal Margin { get; set; }

        public short MinimumDepositAgeForPledge { get; set; }

        public short MinimumDepositMaturityAgeForPledge { get; set; }

        public decimal MinimumAdditionalInterestRate { get; set; }

        public decimal MaximumAdditionalInterestRate { get; set; }

        public bool EnableThirdPersonDepositAttachment { get; set; }

        public decimal MinimumAdditionalInterestRateForThirdPersonDeposit { get; set; }

        public decimal MaximumAdditionalInterestRateForThirdPersonDeposit { get; set; }

        public byte InterestCalculationFrequencyPrmKey { get; set; }
        public Guid InterestCalculationFrequencyId { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }


        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeLoanAgainstDepositParameterMakerCheker
        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanAgainstDepositParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }


        [StringLength(1500)]
        public string Remark { get; set; }


        // Scheme

        public Guid SchemeId { get; set; }

        [StringLength(100)]
        public string NameOfScheme { get; set; }

        public Guid[] MultiDepositeGeneralLedgerId { get; set; }
        //Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }



    }
}
