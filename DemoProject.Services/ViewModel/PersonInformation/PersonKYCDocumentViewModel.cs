using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonKYCDocumentViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public short DocumentDocumentTypePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(150)]
        public string NameAsPerDocument { get; set; }
        
        [StringLength(50)]
        public string DocumentNumber { get; set; }

        public short SequenceNumber { get; set; }

        public DateTime DateOfIssue { get; set; }

        public DateTime? DateOfExpiry { get; set; }
        
        [StringLength(100)]
        public string IssuingAuthority { get; set; }
        
        [StringLength(100)]
        public string PlaceOfIssue { get; set; }
        
        [StringLength(500)]
        public string NameOfFile { get; set; }
        
        [StringLength(500)]
        public string FileCaption { get; set; }
        
        public byte[] PhotoCopy { get; set; }
        
        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        public DateTime? DateOfRequest { get; set; }

        public DateTime? DateOfExpectingSubmit { get; set; }

        public DateTime? DateOfSubmit { get; set; }
        
        [StringLength(1)]
        public string DocumentUploadStatus { get; set; }

        [StringLength(50)]
        public string DocumentUploadStatusText { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonKYCDocumentMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        //Other

        [StringLength(1)]
        public string KYCDocumentUpload { get; set; }

        public bool EnableKYCDocumentUploadInDb { get; set; }
        
        [StringLength(500)]
        public string KYCDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForKYCDocumentUploadInDb { get; set; }

        public bool EnableKYCDocumentUploadInLocalStorage { get; set; }
        
        [StringLength(500)]
        public string KYCDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForKYCDocumentUploadInLocalStorage { get; set; }

        public HttpPostedFileBase PhotoPathKYC { get; set; }

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

        public Guid DocumentDocumentTypeId { get; set; }

        public Guid DocumentId { get; set; }

        [StringLength(500)]
        public string NameOfDocument { get; set; }

        public Guid DocumentTypeId { get; set; }

        [StringLength(100)]
        public string NameOfDocumentType { get; set; }

        public long PersonKYCDetailDocumentPrmKey { get; set; }

        public long PersonKYCDetailPrmKey { get; set; }

    }
}
