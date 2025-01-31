using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerVehicleLoanCollateralDetail")]
    public partial class CustomerVehicleLoanCollateralDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerVehicleLoanCollateralDetail()
        {
            CustomerVehicleLoanCollateralDetailMakerCheckers = new HashSet<CustomerVehicleLoanCollateralDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string LoanPurpose { get; set; }

        public int VehicleSupplierPrmKey { get; set; }

        public short VehicleVariantPrmKey { get; set; }

        public short ColourPrmKey { get; set; }

        [Required]
        [StringLength(30)]
        public string OtherColour { get; set; }

        public bool IsUsedForCommercial { get; set; }

        public bool HasContract { get; set; }

        public short ManufactureYear { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        [StringLength(13)]
        public string RegistrationNumber { get; set; }

        public decimal ExShowroomPrice { get; set; }

        public decimal OnroadPrice { get; set; }

        public decimal AdditionalAccessoriesAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string EngineNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string ChasisNumber { get; set; }

        public byte NumberOfTyres { get; set; }

        public byte SeatingCapacity { get; set; }

        public int RegisteredLadenWeight { get; set; }

        public byte BusinessExperience { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerLoanAccount CustomerLoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerVehicleLoanCollateralDetailMakerChecker> CustomerVehicleLoanCollateralDetailMakerCheckers { get; set; }
    }
}
