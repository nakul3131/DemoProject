using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDemandDepositDetail")]
    public partial class SchemeDemandDepositDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDemandDepositDetail()
        {
            SchemeDemandDepositDetailMakerCheckers = new HashSet<SchemeDemandDepositDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableReferencePersonDetail { get; set; }

        public byte MinimumNumberOfReferencePerson { get; set; }

        public byte MaximumNumberOfReferencePerson { get; set; }

        public decimal InitialAccountOpeningAmount { get; set; }

        public byte BalanceTypePrmKey { get; set; }

        public byte TimePeriodUnitPrmKey { get; set; }

        public decimal BalanceAmount { get; set; }

        public bool EnablePhotoSign { get; set; }

        [Required]
        [StringLength(1)]
        public string PhotoDocumentUpload { get; set; }

        public bool EnablePhotoDocumentUploadInDb { get; set; }

        [Required]
        [StringLength(500)]
        public string PhotoDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForPhotoDocumentUploadInDb { get; set; }

        public bool EnablePhotoDocumentUploadInLocalStorage { get; set; }

        [Required]
        [StringLength(1500)]
        public string PhotoDocumentLocalStoragePath { get; set; }

        [Required]
        [StringLength(500)]
        public string PhotoDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForPhotoDocumentUploadInLocalStorage { get; set; }

        [Required]
        [StringLength(1)]
        public string SignDocumentUpload { get; set; }

        public bool EnableSignDocumentUploadInDb { get; set; }

        [Required]
        [StringLength(500)]
        public string SignDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForSignDocumentUploadInDb { get; set; }

        public bool EnableSignDocumentUploadInLocalStorage { get; set; }

        [Required]
        [StringLength(1500)]
        public string SignDocumentLocalStoragePath { get; set; }

        [Required]
        [StringLength(500)]
        public string SignDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForSignDocumentUploadInLocalStorage { get; set; }

        public bool EnableBeneficiaryDetail { get; set; }

        public bool EnableSweepOut { get; set; }

        public bool EnableSweepIn { get; set; }

        public decimal MinimumBalanceToTriggerSweepIn { get; set; }

        public decimal MaximumAmountToTriggerSweep { get; set; }

        public decimal MinimumTermDepositAmount { get; set; }

        public decimal MaximumTermDepositAmount { get; set; }

        public byte MinimumTermDepositTenure { get; set; }

        public byte MaximumTermDepositTenure { get; set; }

        public byte DefaultTermDepositTenure { get; set; }

        public decimal MaximumNumberOfSweepOut { get; set; }

        public bool EnableAutoRenew { get; set; }

        public byte SweepOutFrequencyPrmKey { get; set; }

        public bool EnableOnBeginingOfDay { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDemandDepositDetailMakerChecker> SchemeDemandDepositDetailMakerCheckers { get; set; }
    }
}
