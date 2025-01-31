using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeSharesTransferChargesViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public short FromTimePeriodInDays { get; set; }

        public short ToTimePeriodInDays { get; set; }

        [StringLength(3)]
        public string ChargesBase { get; set; }

        public decimal MinimumChargesAmount { get; set; }

        public decimal MaximumChargesAmount { get; set; }

        public bool IsTaxable { get; set; }

        public bool IsApplicableOnDeath { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeChargesDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeSharesTransferChargesPrmKey { get; set; }

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

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        // Dropdown

        public Guid GeneralLedgerId { get; set; }

        public string NameOfGL { get; set; }
    }
}
