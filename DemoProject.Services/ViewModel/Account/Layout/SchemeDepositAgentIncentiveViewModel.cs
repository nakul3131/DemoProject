using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDepositAgentIncentiveViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public decimal MinimumCollectionAmount { get; set; }

        public decimal MaximumCollectionAmount { get; set; }

        [StringLength(3)]
        public string IncentiveUnit { get; set; }

        public decimal Incentive { get; set; }

        [StringLength(3)]
        public string RoundingMethod { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeDepositAgentIncentiveMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeDepositAgentIncentivePrmKey { get; set; }

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

        [StringLength(30)]
        public string IncentiveUnitText { get; set; }

        [StringLength(30)]
        public string RoundingMethodText { get; set; }

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
