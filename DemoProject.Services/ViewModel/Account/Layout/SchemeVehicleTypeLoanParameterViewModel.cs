using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeVehicleTypeLoanParameterViewModel
    {
        public short PrmKey { get; set; }

        public Guid VehicleTypeId { get; set; }

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

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }


        public DateTime EntryDateTime { get; set; }

        public short SchemeVehicleTypeLoanParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        
        [StringLength(3)]
        public string UserAction { get; set; }

        
        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        public string NameOfVehicleType { get; set; }   

        public string[] PhotoDocumentFormatTypeIdForDatabase { get; set; }

        public string[] PhotoDocumentFormatTypeIdForLocalStorage { get; set; }

        public string[] AllowedFileFormatsForDbText { get; set; }

        public string[] AllowedFileFormatsForLocalStorageText { get; set; }

        public string PhotoUploadText { get; set; }

        [StringLength(1500)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
