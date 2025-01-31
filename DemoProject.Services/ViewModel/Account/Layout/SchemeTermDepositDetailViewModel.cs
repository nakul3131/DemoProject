using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeTermDepositDetailViewModel
    {
        // SchemeTermDepositDetail

        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableAutoRolloverOnMaturity { get; set; }

        public bool EnableAutoCloseOnMaturity { get; set; }

        public bool EnableTransferInterestToUnclaimedGeneralLedger { get; set; }

        public bool EnableTransferPrincipalToUnclaimedGeneralLedger { get; set; }

        public bool EnableInterestCalculationFromDepositDate { get; set; }

        public short MinimumGracePeriodForRenewal { get; set; }

        public short MaximumGracePeriodForRenewal { get; set; }

        public short DefaultGracePeriodForRenewal { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeTermDepositDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeTermDepositDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
    }
}
