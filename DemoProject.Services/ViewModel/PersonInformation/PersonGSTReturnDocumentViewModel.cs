using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonGSTReturnDocumentViewModel
    {
        public long PrmKey { get; set; }

        public long PersonGSTRegistrationDetailPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short AssessmentYear { get; set; }

        public decimal TaxAmount { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }

        [StringLength(500)]
        public string FileCaption { get; set; }

        public byte[] PhotoCopy { get; set; }

        public HttpPostedFileBase PhotoPathGst { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonGSTReturnDocumentMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonGSTReturnDocumentPrmKey { get; set; }

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

        // Other

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1)]
        public string GSTDocumentUpload { get; set; }

        public bool EnableGSTDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string GSTDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForGSTDocumentUploadInDb { get; set; }

        public bool EnableGSTDocumentUploadInLocalStorage { get; set; }

        [StringLength(500)]
        public string GSTDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForGSTDocumentUploadInLocalStorage { get; set; }

    }
}
