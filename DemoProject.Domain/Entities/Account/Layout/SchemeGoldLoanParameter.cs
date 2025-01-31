using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeGoldLoanParameter")]
    public partial class SchemeGoldLoanParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeGoldLoanParameter()
        {
            SchemeGoldLoanParameterMakerCheckers = new HashSet<SchemeGoldLoanParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public decimal MinimumLTVRatio { get; set; }

        public decimal MaximumLTVRatio { get; set; }

        [Required]
        [StringLength(1)]
        public string GoldInsurance { get; set; }

        public bool EnableSuperValuation { get; set; }

        public byte SuperValuationsInYear { get; set; }

        public byte TimePeriodBetweenTwoSuperValuations { get; set; }

        public bool EnableGoldPhoto { get; set; }

        public bool EnableOwnershipProof { get; set; }

        public bool EnableWeighingPhoto { get; set; }

        public bool EnableDamagePhoto { get; set; }

        public bool EnableWestagePhoto { get; set; }

        public bool EnableDimondPhoto { get; set; }

        [Required]
        [StringLength(1)]
        public string GoldPhotoUpload { get; set; }

        public bool EnableGoldPhotoUploadInDb { get; set; }

        [Required]
        [StringLength(500)]
        public string GoldPhotoAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForGoldPhotoUploadInDb { get; set; }

        public bool EnableGoldPhotoUploadInLocalStorage { get; set; }

        [Required]
        [StringLength(1500)]
        public string GoldPhotoLocalStoragePath { get; set; }

        [Required]
        [StringLength(500)]
        public string GoldPhotoAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForGoldPhotoUploadInLocalStorage { get; set; }

        public byte MinimumGoldPhoto { get; set; }

        public byte MaximumGoldPhoto { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeGoldLoanParameterMakerChecker> SchemeGoldLoanParameterMakerCheckers { get; set; }
    }
}
