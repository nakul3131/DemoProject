using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeVehicleTypeLoanParameter")]
    public partial class SchemeVehicleTypeLoanParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeVehicleTypeLoanParameter()
        {
            SchemeVehicleTypeLoanParameterMakerCheckers = new HashSet<SchemeVehicleTypeLoanParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public byte VehicleTypePrmKey { get; set; }

        public decimal DownPaymentPercentage { get; set; }

        public decimal MaximumLoanAmountWithoutExtraSecurity { get; set; }

        [StringLength(3)]
        public string PhotoUpload { get; set; }

        public bool EnablePhotoUploadInDb { get; set; }

        [StringLength(500)]
        public string AllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForDb { get; set; }

        public bool EnablePhotoUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string StoragePath { get; set; }

        [StringLength(500)]
        public string AllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForLocalStorage { get; set; }

        public byte MinimumNumberOfPhoto { get; set; }

        public byte MaximumNumberOfPhoto { get; set; }


        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeVehicleTypeLoanParameterMakerChecker> SchemeVehicleTypeLoanParameterMakerCheckers { get; set; }
    }
}
