using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountDocumentViewModel
    {
        public long PrmKey { get; set; }

        public Guid CustomerAccountDocumentId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short DocumentPrmKey { get; set; }

        public byte[] DocumentPhotoCopy { get; set; }

        public HttpPostedFileBase Photo { get; set; }

        public DateTime ExpectedSubmitDate { get; set; }

        public DateTime ActualSubmitDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerAccountDocumentMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountDocumentPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropdownList
        public Guid DocumentId { get; set; }

        [StringLength(500)]
        public string NameOfDocument { get; set; }

        [StringLength(500)]
        public string FileCaption { get; set; }

        public byte SequenceNumber { get; set; }

        public HttpPostedFileBase FileUploader { get; set; }
        // Other
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        public Guid SchemeDocumentId { get; set; }

        public HttpPostedFileBase PhotoPath { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }
    }
}
