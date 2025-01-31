using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerVehicleLoanInsuranceDetailViewModel
    {
        public int PrmKey { get; set; }

        public Guid CustomerVehicleLoanInsuranceDetailId { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short InsuranceCompanyPrmKey { get; set; }

        [StringLength(50)]
        public string PolicyNumber { get; set; }

        public DateTime CommencementDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        [StringLength(3)]
        public string TypeOfCoverage { get; set; }

        public bool HasAddedZeroDepreciation { get; set; }

        public bool HasAddedEngineProtection { get; set; }

        public bool HasAddedReturnToInvoice { get; set; }

        public bool HasAddedRoadsideAssistance { get; set; }

        public decimal PolicyPremium { get; set; }

        public decimal SumInsured { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerVehicleLoanInsuranceDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerVehicleLoanInsuranceDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropdownList 

        public Guid InsuranceCompanyId { get; set; }

    }
}
