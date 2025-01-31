using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonImmovableAssetViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }
        
        [StringLength(4500)]
        public string AssetFullDescription { get; set; }

        [StringLength(50)]
        public string SurveyNumber { get; set; }

        [StringLength(50)]
        public string CitySurveyNumber { get; set; }

        [StringLength(50)]
        public string OtherNumber { get; set; }

        public decimal AreaOfLand { get; set; }

        public bool IsConstructed { get; set; }

        public decimal ConstructionArea { get; set; }

        public decimal CarpetArea { get; set; }

        public decimal CurrentMarketValue { get; set; }

        public decimal AnnualRentIncome { get; set; }

        public byte ResidenceTypePrmKey { get; set; }

        public byte OwnershipTypePrmKey { get; set; }

        public decimal OwnershipPercentage { get; set; }

        public bool HasAnyMortgage { get; set; }

        public bool IsOwnershipDeceased { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonImmovableAssetMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public long PersonImmovableAssetPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //PersonImmovableAssetDocument

        //public Guid PersonImmovableAssetDocumentId { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }
        
        [StringLength(500)]
        public string FileCaption { get; set; }
        
        public byte[] PhotoCopy { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        public HttpPostedFileBase PhotoPathImmovable { get; set; }

        //PersonImmovableAssetDocumentMakerChecker

        public long PersonImmovableAssetDocumentPrmKey { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        //Other
        public long CustomerAccountPrmKey { get; set; }

        public Guid ResidenceTypeId { get; set; }

        [StringLength(50)]
        public string NameOfResidenceType { get; set; }

        public Guid OwnershipTypeId { get; set; }

        [StringLength(50)]
        public string NameOfOwnershipType { get; set; }

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
        public string ImmovableAssetDocumentUpload { get; set; }

        public bool EnableImmovableAssetDocumentUploadInDb { get; set; }

        [StringLength(1500)]
        public string ImmovableAssetDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForImmovableAssetDocumentUploadInDb { get; set; }

        public bool EnableImmovableAssetDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string ImmovableAssetDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForImmovableAssetDocumentUploadInLocalStorage { get; set; }

        public bool IsUploadedDocument { get; set; }

    }
}
