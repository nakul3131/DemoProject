using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanSettlementAccountParameterViewModel
    {
        // SchemeLoanSettlementAccountParameter

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableAutoSetSettlementAccountsOnCreation { get; set; }

        public bool EnableAutoCreateSettlementAccount { get; set; }

        [StringLength(3)]
        public string SettlementType { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeLoanSettlementAccountParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanSettlementAccountParameterPrmKey { get; set; }

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
    }
}
