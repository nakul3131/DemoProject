using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerVehicleLoanCollateralDetailViewModel
    {

        public int PrmKey { get; set; }

        public Guid CustomerVehicleLoanCollateralDetailId { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(3)]
        public string LoanPurpose { get; set; }

        public int VehicleSupplierPrmKey { get; set; }

        public short VehicleVariantPrmKey { get; set; }

        public short ColourPrmKey { get; set; }

        [StringLength(30)]
        public string OtherColour { get; set; }

        public bool IsUsedForCommercial { get; set; }

        public bool HasContract { get; set; }

        public short ManufactureYear { get; set; }

        public DateTime RegistrationDate { get; set; }

        [StringLength(13)]
        public string RegistrationNumber { get; set; }

        public decimal ExShowroomPrice { get; set; }

        public decimal OnroadPrice { get; set; }

        public decimal AdditionalAccessoriesAmount { get; set; }

        [StringLength(50)]
        public string EngineNumber { get; set; }

        [StringLength(50)]
        public string ChasisNumber { get; set; }
        
        public byte NumberOfTyres { get; set; }

        public byte SeatingCapacity { get; set; }

        public int RegisteredLadenWeight { get; set; }

        public byte BusinessExperience { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerVehicleLoanCollateralDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerVehicleLoanCollateralDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropdownList 
        public Guid VehicleMakeId { get; set; }

        public Guid VehicleModelId { get; set; }

        public Guid VehicleVariantId { get; set; }

        public Guid VehicleSupplierId { get; set; }

        public Guid ColourId { get; set; }

        [StringLength(1500)]
        public string NameOfColour { get; set; }

    }
}
