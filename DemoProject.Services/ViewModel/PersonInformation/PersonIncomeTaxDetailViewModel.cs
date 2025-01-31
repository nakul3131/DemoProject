using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonIncomeTaxDetailViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short AssessmentYear { get; set; }

        public decimal TaxAmount { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }

        [StringLength(500)]
        public string FileCaption { get; set; }

        public byte[] PhotoCopy { get; set; }

        public HttpPostedFileBase PhotoPathTax { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonIncomeTaxDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonIncomeTaxDetailPrmKey { get; set; }

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
        public string IncomeTaxDocumentUpload { get; set; }

        public bool EnableIncomeTaxDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string IncomeTaxDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForIncomeTaxDocumentUploadInDb { get; set; }

        public bool EnableIncomeTaxDocumentUploadInLocalStorage { get; set; }

        [StringLength(500)]
        public string IncomeTaxDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForIncomeTaxDocumentUploadInLocalStorage { get; set; }
       
        public long PersonIncomeTaxDetailDocumentPrmKey { get; set; }
        public long CustomerLoanAccountIncomeTaxDetailPrmKey { get; set; }
    }
}
