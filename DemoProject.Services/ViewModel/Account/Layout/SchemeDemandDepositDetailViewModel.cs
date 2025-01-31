using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDemandDepositDetailViewModel
    {
        // SchemeDemandDepositDetail

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

        [StringLength(1)]
        public string PhotoDocumentUpload { get; set; } 

        public bool EnablePhotoDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string PhotoDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForPhotoDocumentUploadInDb { get; set; }

        public bool EnablePhotoDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string PhotoDocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string PhotoDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForPhotoDocumentUploadInLocalStorage { get; set; }

        [StringLength(1)]
        public string SignDocumentUpload { get; set; }

        public bool EnableSignDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string SignDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForSignDocumentUploadInDb { get; set; }

        public bool EnableSignDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string SignDocumentLocalStoragePath { get; set; }

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

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeDemandDepositDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeDemandDepositDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public string[] PhotoDocumentFormatTypeIdForDatabase { get; set; }

        public string[] PhotoDocumentFormatTypeIdForLocalStorage { get; set; }

        public string[] SignDocumentFormatTypeIdForDatabase { get; set; }

        public string[] SignDocumentFormatTypeIdForLocalStorage { get; set; }

        public Guid SweepOutFrequencyId { get; set; }

        public Guid BalanceTypeId { get; set; }

        public Guid TimePeriodUnitId { get; set; }
    }
}
