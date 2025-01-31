using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonFinancialAssetViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte FinancialOrganizationTypePrmKey { get; set; }

        [StringLength(150)]
        public string NameOfFinancialOrganization { get; set; }

        [StringLength(50)]
        public string NameOfBranch { get; set; }
        
        [StringLength(1500)]
        public string AddressDetails { get; set; }
        
        [StringLength(500)]
        public string ContactDetails { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime MaturityDate { get; set; }

        public DateTime? ClosingDate { get; set; }

        public byte FinancialAssetTypePrmKey { get; set; }
        
        [StringLength(1500)]
        public string FinancialAssetDescription { get; set; }

        [StringLength(500)]
        public string ReferenceNumber { get; set; }

        public decimal InvestedAmount { get; set; }

        public decimal CurrentMarketValue { get; set; }

        public decimal MonthlyInterestIncomeAmount { get; set; }

        public bool HasAnyMortgage { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonFinancialAssetDetailMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public long PersonFinancialAssetPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //PersonFinancialAssetDetailTranslation
        
        //public Guid PersonFinancialAssetTranslationId { get; set; }
        
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransNameOfFinancialOrganization { get; set; }

        [StringLength(50)]
        public string TransNameOfBranch { get; set; }
        
        [StringLength(1500)]
        public string TransAddressDetails { get; set; }
        
        [StringLength(500)]
        public string TransContactDetails { get; set; }
        
        [StringLength(1500)]
        public string TransFinancialAssetDescription { get; set; }

        [StringLength(500)]
        public string TransReferenceNumber { get; set; }
        
        [StringLength(1500)]
        public string TransNote { get; set; }
        
        //PersonFinancialAssetDetailTranslationMakerchecker
        
        public long PersonFinancialAssetTranslationPrmKey { get; set; }

        //PersonFinancialAssetDocument

        [StringLength(150)]
        public string FullName { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }

        [StringLength(500)]
        public string FileCaption { get; set; }

        public byte[] PhotoCopy { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        public HttpPostedFileBase PhotoPathFinance { get; set; }

        //PersonFinancialAssetDocumentMakerChecker

        public long PersonFinancialAssetDocumentPrmKey { get; set; }
        
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

        // PersonInformation
        [StringLength(1)]
        public string FinancialAssetDocumentUpload { get; set; }

        public bool EnableFinancialAssetDocumentUploadInDb { get; set; }

        [StringLength(1500)]
        public string FinancialAssetDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForFinancialAssetDocumentUploadInDb { get; set; }

        public bool EnableFinancialAssetDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string FinancialAssetDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForFinancialAssetDocumentUploadInLocalStorage { get; set; }

        // For SelectListItem

        public Guid FinancialAssetTypeId { get; set; }

        [StringLength(50)]
        public string NameOfFinancialAssetType { get; set; }

        public Guid FinancialOrganizationTypeId { get; set; }

        [StringLength(50)]
        public string NameOfFinancialOrganizationType { get; set; }

    }
}
