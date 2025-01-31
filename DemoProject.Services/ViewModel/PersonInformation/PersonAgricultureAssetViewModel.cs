using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonAgricultureAssetViewModel
    {
        //PersonAgricultureAsset
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte AgricultureLandTypePrmKey { get; set; }

        [StringLength(1500)]
        public string AgricultureLandDescription { get; set; }

        [StringLength(50)]
        public string SurveyNumber { get; set; }

        [StringLength(50)]
        public string GroupNumber { get; set; }

        public short AreaOfLand { get; set; }

        public decimal Volume { get; set; }

        public byte OwnershipTypePrmKey { get; set; }

        public decimal OwnershipPercentage { get; set; }

        public decimal CurrentMarketValue { get; set; }

        public bool IsOnlyRainFedTypeIrrigation { get; set; }

        public bool HasCanalRiverIrrigationSource { get; set; }

        public bool HasWellsIrrigationSource { get; set; }

        public bool HasFarmLakeSource { get; set; }

        public decimal AnnualIncomeFromLand { get; set; }

        public bool HasAnyCourtCase { get; set; }

        [StringLength(2500)]
        public string CourtCaseFullDetails { get; set; }

        public bool HasAnyMortgage { get; set; }

        public bool IsOwnershipDeceased { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonAgricultureAssetMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long PersonAgricultureAssetPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //PersonAgricultureAssetDocument

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }
        
        [StringLength(500)]
        public string FileCaption { get; set; }
        
        public byte[] PhotoCopy { get; set; }
        
        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        public HttpPostedFileBase PhotoPathAgree { get; set; }

        //public HttpPostedFileBase PhotoPathAgriculture { get; set; }

        //PersonAgricultureAssetDocumentMakerChecker
        public long PersonAgricultureAssetDocumentPrmKey { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        //Other
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

        [StringLength(3)]
        public string AgricultureAssetDocumentUpload { get; set; }

        public bool EnableAgricultureAssetDocumentUploadInDb { get; set; }
        
        [StringLength(1500)]
        public string AgricultureAssetDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForAgricultureAssetDocumentUploadInDb { get; set; }

        public bool EnableAgricultureAssetDocumentUploadInLocalStorage { get; set; }
        
        [StringLength(1500)]
        public string AgricultureAssetDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForAgricultureAssetDocumentUploadInLocalStorage { get; set; }

        // For SelectListItem

        public Guid AgricultureLandTypeId { get; set; }

        [StringLength(50)]
        public string NameOfAgricultureLandType { get; set; }

        public Guid OwnershipTypeId { get; set; }

        [StringLength(50)]
        public string NameOfOwnershipType { get; set; }
        

    }
}
