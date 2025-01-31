using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonMovableAssetViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short VehicleVariantPrmKey { get; set; }

        public short ManufacturingYear { get; set; } // lessthan RegistrationDate year

        public byte NumberOfOwners { get; set; } 

        public DateTime RegistrationDate { get; set; }

        [StringLength(15)]
        public string RegistrationNumber { get; set; } 

        public DateTime PurchaseDate { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal CurrentMarketValue { get; set; } //PurchasePrice > CurrentMarketValue

        public decimal OwnershipPercentage { get; set; }

        public bool HasAnyMortgage { get; set; }

        public bool IsOwnershipDeceased { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonMovableAssetMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonMovableAssetPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //PersonMovableAssetDocument

        //public Guid PersonMovableAssetDocumentId { get; set; }
        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }

        [StringLength(500)]
        public string FileCaption { get; set; }

        public byte[] PhotoCopy { get; set; }

        public HttpPostedFileBase PhotoPathMovable { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        //PersonMovableAssetDocumentMakerChecker

        public long PersonMovableAssetDocumentPrmKey { get; set; }

        //Person
        public long CustomerAccountPrmKey { get; set; }

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        //Other

        [StringLength(1)]
        public string MovableAssetDocumentUpload { get; set; }

        public bool EnableMovableAssetDocumentUploadInDb { get; set; }

        [StringLength(1500)]
        public string MovableAssetDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForMovableAssetDocumentUploadInDb { get; set; }

        public bool EnableMovableAssetDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string MovableAssetDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForMovableAssetDocumentUploadInLocalStorage { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        // For SelectListItem

        public Guid VehicleModelId { get; set; }

        [StringLength(50)]
        public string NameOfVehicleModel { get; set; }

        public Guid VehicleVariantId { get; set; }


        [StringLength(50)]
        public string NameOfVehicleVariant { get; set; }

        public Guid VehicleMakeId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfVehicleMake { get; set; }
    }
}
