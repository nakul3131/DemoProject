using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public  class CustomerGoldLoanReappraisalViewModel
    {
        public int PrmKey { get; set; }

        public Guid CustomerGoldLoanReappraisalId { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int EmployeePrmKey { get; set; }

        public bool HasAnyDamage { get; set; }

        [StringLength(1500)]
        public string DamageDescription { get; set; }

        public bool IsSealedProperly { get; set; }

        public decimal MetalNetWeight { get; set; }

        public decimal MetalGrossWeight { get; set; }

        public bool IsWithDiamond { get; set; }

        public byte NumberOfDiamond { get; set; }

        public decimal DiamondCarat { get; set; }

        [StringLength(150)]
        public string ClarityColour { get; set; }

        public decimal DiamondWeight { get; set; }

        [StringLength(1500)]
        public string AutoIrregularityNote { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerGoldLoanReappraisalMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerGoldLoanReappraisalPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid EmployeeId { get; set; }

    }
}
