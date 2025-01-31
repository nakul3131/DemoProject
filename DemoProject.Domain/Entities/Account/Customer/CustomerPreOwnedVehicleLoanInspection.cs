using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerPreOwnedVehicleLoanInspection")]
    public partial class CustomerPreOwnedVehicleLoanInspection
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerPreOwnedVehicleLoanInspection()
        {
            CustomerPreOwnedVehicleLoanInspectionMakerCheckers = new HashSet<CustomerPreOwnedVehicleLoanInspectionMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte NumberOfOwner { get; set; }

        public int OdometerReading { get; set; }

        [Required]
        [StringLength(500)]
        public string MaintenanceRemark { get; set; }

        [Required]
        [StringLength(3)]
        public string EngineCondition { get; set; }

        [Required]
        [StringLength(3)]
        public string GearBoxCondition { get; set; }

        [Required]
        [StringLength(3)]
        public string BrakeCondition { get; set; }

        [Required]
        [StringLength(3)]
        public string SeatCondition { get; set; }

        [Required]
        [StringLength(3)]
        public string BodyCabinCondition { get; set; }

        [Required]
        [StringLength(3)]
        public string TyresCondition { get; set; }

        [Required]
        [StringLength(3)]
        public string BatteryCondition { get; set; }

        [Required]
        [StringLength(3)]
        public string InsuranceStatus { get; set; }

        public bool IsUnderAnyHypothecation { get; set; }

        [Required]
        [StringLength(100)]
        public string HypothecationInstitutionName { get; set; }

        [Required]
        [StringLength(500)]
        public string HypothecationInstitutionOtherDetails { get; set; }

        public bool RCAvailability { get; set; }

        [Required]
        [StringLength(500)]
        public string ReasonForUnavailability { get; set; }

        public decimal CurrentValuationAmount { get; set; }

        [Required]
        [StringLength(1500)]
        public string RemarkOfValuer { get; set; }

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
        public virtual ICollection<CustomerPreOwnedVehicleLoanInspectionMakerChecker> CustomerPreOwnedVehicleLoanInspectionMakerCheckers { get; set; }
    }
}
