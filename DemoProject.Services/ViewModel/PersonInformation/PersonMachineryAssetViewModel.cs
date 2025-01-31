using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonMachineryAssetViewModel
    {
        // PersonMachineryAsset

        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(500)]
        public string NameOfMachinery { get; set; }

        [StringLength(1500)]
        public string MachineryFullDetails { get; set; }

        public short ManufacturingYear { get; set; }

        public byte NumberOfOwners { get; set; }

        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public DateTime DateOfPurchase { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal CurrentMarketValue { get; set; }

        public decimal OwnershipPercentage { get; set; }

        public bool HasAnyMortgage { get; set; }

        public bool IsOwnershipDeceased { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // PersonMachineryAssetDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonMachineryAssetPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // PersonMachineryAssetDocument
        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }

        [StringLength(500)]
        public string FileCaption { get; set; }

        //public Guid PersonMachineryAssetDocumentId { get; set; }
        
        public byte[] PhotoCopy { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        public HttpPostedFileBase PhotoPathMachinery { get; set; }

        // PersonMachineryAssetDocumentMakerChecker

        public long PersonMachineryAssetDocumentPrmKey { get; set; }

        // Person
        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        // Other
        public long CustomerAccountPrmKey { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1)]
        public string MachineryAssetDocumentUpload { get; set; }

        public bool EnableMachineryAssetDocumentUploadInDb { get; set; }

        [StringLength(1500)]
        public string MachineryAssetDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForMachineryAssetDocumentUploadInDb { get; set; }

        public bool EnableMachineryAssetDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string MachineryAssetDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForMachineryAssetDocumentUploadInLocalStorage { get; set; }

        public bool IsUploadedDocument { get; set; }
 
    }
}