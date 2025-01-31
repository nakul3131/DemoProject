using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.ViewModel.Account.Parameter;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Transaction
{
    public class TransactionSettingViewModel
    {
        public short MinimumNumberOfShares { get; set; }
        public short MaximumNumberOfShares { get; set; }
        public bool EnableAutoCertificateNumber { get; set; }
        public bool IsFirstTransaction { get; set; }
        public bool EnableOtherFees1 { get; set; }
        public short AdmissionFeesGeneralLedgerPrmKey { get; set; }
        public short OtherFeesGeneralLedgerPrmKey1 { get; set; }
        public decimal AdmissionFeesForMembership { get; set; }
        public decimal FeesAmount1 { get; set; }
        public decimal SharesFaceValue { get; set; }
       
        [StringLength(50)]  
        public string TitleForFees1 { get; set; }
        public decimal MaximumSharesHolidingLimitAmount { get; set; }
        public decimal CustomerAccountBalance { get; set; }
        public decimal GSTRate { get; set; }
        public decimal SGSTRate { get; set; }
        public decimal CGSTRate { get; set; }
        public decimal IGSTRate { get; set; }
        public decimal CessRate { get; set; }
        public decimal SGSTAmount { get; set; }
        public decimal CGSTAmount { get; set; }
        public decimal IGSTAmount { get; set; }
        public decimal CessAmount { get; set; }
        public short SGSTGeneralLedgerPrmKey { get; set; }
        public short CGSTGeneralLedgerPrmKey { get; set; }
        public short IGSTGeneralLedgerPrmKey { get; set; }
        public short CessGeneralLedgerPrmKey { get; set; }
        public decimal IncomeTaxCashLimit { get; set; }

    }
}
