using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeVehicleLoanParameter")]
    public partial class SchemeVehicleLoanParameter
    {
        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public decimal DownPaymentPercentage { get; set; }

        public decimal PreOwnedVehicleDownPaymentPercentage { get; set; }

        public bool EnablePreOwnedVehicleInspection { get; set; }

        [Required]
        [StringLength(1)]
        public string PreOwnedVehiclePhotoUpload { get; set; }

        public bool EnablePreOwnedVehiclePhotoUploadInDb { get; set; }

        [Required]
        [StringLength(500)]
        public string PreOwnedVehiclePhotoAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForPreOwnedVehiclePhotoUploadInDb { get; set; }

        public bool EnablePreOwnedVehiclePhotoUploadInLocalStorage { get; set; }

        [Required]
        [StringLength(1500)]
        public string PreOwnedVehiclePhotoLocalStoragePath { get; set; }

        [Required]
        [StringLength(500)]
        public string PreOwnedVehiclePhotoAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForPreOwnedVehiclePhotoUploadInLocalStorage { get; set; }

        public byte MinmumPreOwnedVehiclePhoto { get; set; }

        public byte MaximumPreOwnedVehiclePhoto { get; set; }

        public bool EnableVehicleInsuranceDetail { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
