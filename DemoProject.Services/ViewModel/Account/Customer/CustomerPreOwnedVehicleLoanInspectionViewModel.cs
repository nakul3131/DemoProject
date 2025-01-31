using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerPreOwnedVehicleLoanInspectionViewModel
    {
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte NumberOfOwner { get; set; }

        public int OdometerReading { get; set; }

        [StringLength(500)]
        public string MaintenanceRemark { get; set; }

        [StringLength(3)]
        public string EngineCondition { get; set; }

        [StringLength(3)]
        public string GearBoxCondition { get; set; }

        [StringLength(3)]
        public string BrakeCondition { get; set; }

        [StringLength(3)]
        public string SeatCondition { get; set; }

        [StringLength(3)]
        public string BodyCabinCondition { get; set; }

        [StringLength(3)]
        public string TyresCondition { get; set; }

        [StringLength(3)]
        public string BatteryCondition { get; set; }

        [StringLength(3)]
        public string InsuranceStatus { get; set; }

        public bool IsUnderAnyHypothecation { get; set; }

        [StringLength(100)]
        public string HypothecationInstitutionName { get; set; }

        [StringLength(500)]
        public string HypothecationInstitutionOtherDetails { get; set; }

        public bool RCAvailability { get; set; }

        [StringLength(500)]
        public string ReasonForUnavailability { get; set; }

        public decimal CurrentValuationAmount { get; set; }

        [StringLength(1500)]
        public string RemarkOfValuer { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerPreOwnedVehicleLoanInspectionMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerPreOwnedVehicleLoanInspectionPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
    }
}
