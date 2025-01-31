using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanAgainstDepositGeneralLedgerViewModel
    {
        public short PrmKey { get; set; }

        public short SchemeLoanAgainstDepositParameterPrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanAgainstDepositGeneralLedgerPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Scheme

       // public Guid SchemeId { get; set; }

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


        // For DropDowns
        public string[] DepositeGeneralLedgerId { get; set; }

        public Guid[] MultiDepositeGeneralLedgerId { get; set; }

        public Guid GeneralLedgerId { get; set; }
    }
}
