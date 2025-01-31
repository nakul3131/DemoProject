using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerBusinessLoanCollateralDetailViewModel
    {
        //CustomerBusinessLoanCollateralDetail
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte TotalBusinessExperience { get; set; }

        public decimal AnnualTurnOver { get; set; }

        public decimal PreviousYearProfit1 { get; set; }

        public decimal PreviousYearProfit2 { get; set; }

        public decimal PreviousYearProfit3 { get; set; }

        public decimal PreviousYearProfit4 { get; set; }

        public decimal PreviousYearProfit5 { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerBusinessLoanCollateralDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerBusinessLoanCollateralDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other

        public Guid PersonId { get; set; }

    }
}
