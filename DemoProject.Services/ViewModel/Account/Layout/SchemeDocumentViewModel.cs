using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeDocumentViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short DocumentPrmKey { get; set; }

        public bool IsRequired { get; set; }

        public bool EnableDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string DocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForDocumentUploadInDb { get; set; }

        public bool EnableDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string DocumentLocalStoragePath { get; set; }

        [StringLength(500)]
        public string DocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForDocumentUploadInLocalStorage { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // SchemeDocumentTypeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeDocumentPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other

        public Guid DocumentId { get; set; }

        [StringLength(100)]
        public string NameOfDocument { get; set; }

        [StringLength(500)]
        public string[] DocumentAllowedFileFormatsForDbId { get; set; }

        [StringLength(500)]
        public string[] DocumentAllowedFileFormatsForLocalStorageId { get; set; }

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
