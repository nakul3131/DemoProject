using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerLoanAccountGuarantorDetailViewModel
    {
        [Key]
        public int PrmKey { get; set; }

        public Guid CustomerLoanAccountGuarantorDetailId { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonPrmKey { get; set; }

        public byte SequenceNumber { get; set; }

        public decimal GuaranteePercentage { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerLoanAccountGuarantorDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerLoanAccountGuarantorDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropdownList 

        public Guid PersonId { get; set; }

        [StringLength(1500)]
        public string NameOfPerson { get; set; }

    }
}
