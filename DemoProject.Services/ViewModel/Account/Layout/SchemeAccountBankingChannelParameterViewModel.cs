using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeAccountBankingChannelParameterViewModel
    {
        // SchemeAccountBankingChannelParameter

        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableInternetBanking { get; set; }

        public bool EnableMobileBanking { get; set; }

        public bool EnableATM { get; set; }

        public bool EnableCDM { get; set; }

        public bool EnableDebitCard { get; set; }

        public bool EnableCreditCard { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeAccountBankingChannelParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeAccountBankingChannelParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public bool EnableBankingChannelParameter { get; set; }

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
