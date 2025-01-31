using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonPhotoSignViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }
        
        [StringLength(500)]
        public string PhotoNameOfFile { get; set; }

        [StringLength(500)]
        public string PhotoFileCaption { get; set; }
        
        public byte[] PhotoCopy { get; set; }

        public string PersonCopyS { get; set; }

        [StringLength(1500)]
        public string PhotoLocalStoragePath { get; set; }
        
        [StringLength(500)]
        public string SignNameOfFile { get; set; }

        [StringLength(500)]
        public string SignFileCaption { get; set; }
        
        public byte[] PersonSign { get; set; }

        public string PersonSignS { get; set; }

        [StringLength(1500)]
        public string SignLocalStoragePath { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonPhotoSignMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public long PersonPhotoSignPrmKey { get; set; }

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
        public string PhotoDocumentUpload { get; set; }

        public bool EnablePhotoDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string PhotoDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForPhotoDocumentUploadInDb { get; set; }

        public bool EnablePhotoDocumentUploadInLocalStorage { get; set; }

        [StringLength(500)]
        public string PhotoDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForPhotoDocumentUploadInLocalStorage { get; set; }

        public HttpPostedFileBase PhotoPath { get; set; }

        public HttpPostedFileBase SignPath { get; set; }

        [StringLength(1)]
        public string SignDocumentUpload { get; set; }

        public bool EnableSignDocumentUploadInDb { get; set; }

        [StringLength(500)]
        public string SignDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForSignDocumentUploadInDb { get; set; }

        public bool EnableSignDocumentUploadInLocalStorage { get; set; }

        [StringLength(500)]
        public string SignDocumentAllowedFileFormatsForLocalStorage { get; set; }

        public short MaximumFileSizeForSignDocumentUploadInLocalStorage { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }
    }
}
