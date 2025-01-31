using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeGoldLoanParameterViewModel
    {
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte MinimumLTVRatio { get; set; }

        public byte MaximumLTVRatio { get; set; }

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

        [StringLength(1)]
        public string GoldPhotoUpload { get; set; }

        public bool EnableGoldPhotoUploadInDb { get; set; }

        [StringLength(500)]
        public string GoldPhotoAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForGoldPhotoUploadInDb { get; set; }

        public bool EnableGoldPhotoUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string GoldPhotoLocalStoragePath { get; set; }

        [StringLength(500)]
        public string GoldPhotoAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForGoldPhotoUploadInLocalStorage { get; set; }

        public byte MinimumGoldPhoto { get; set; }

        public byte MaximumGoldPhoto { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeGoldLoanParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeGoldLoanParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other
        public string[] GoldPhotoAllowedFileFormatIdForDb { get; set; }

        public string[] GoldPhotoAllowedFileFormatIdForLocalStorage { get; set; }

    }
}
