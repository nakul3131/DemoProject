using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanChargesParameterViewModel
    {
        // SchemeLoanChargesParameter

        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte ChargesTypePrmKey { get; set; }

        public byte LendingChargesBasePrmKey { get; set; }

        public decimal ChargesPercentage { get; set; }

        public decimal MinimumCharges { get; set; }

        public decimal MaximumCharges { get; set; }

        public decimal DefaultCharges { get; set; }

        public bool IsApplicableTax { get; set; }

        public bool IsOptional { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeLoanChargesParameterMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanChargesParameterPrmKey { get; set; }

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

        public Guid ChargesTypeId { get; set; }
        //public string ChargesTypeId { get; set; }

        [StringLength(100)]
        public string NameOfChargesType { get; set; }

        public Guid GeneralLedgerId { get; set; }

        [StringLength(100)]
        public string NameOfGL { get; set; }

        public Guid LendingChargesBaseId { get; set; }
       // public string LendingChargesBaseId { get; set; }

        [StringLength(100)]
        public string NameChargesBase { get; set; }
    }
}
