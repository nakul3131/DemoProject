using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerAccountPhotoSignViewModel
    {
        public long PrmKey { get; set; }

        public Guid CustomerAccountPhotoSignId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(500)]
        public string PhotoNameOfFile { get; set; }

        [StringLength(500)]
        public string PhotoFileCaption { get; set; }

        public byte[] PhotoCopy { get; set; }

        [StringLength(1500)]
        public string PhotoLocalStoragePath { get; set; }

        [StringLength(500)]
        public string SignNameOfFile { get; set; }

        [StringLength(500)]
        public string SignFileCaption { get; set; }

        public byte[] SignPhotoCopy { get; set; }

        [StringLength(1500)]
        public string SignLocalStoragePath { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerAccountPhotoSignMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountPhotoSignPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //Other

        public HttpPostedFileBase PhotoPath { get; set; }

        public HttpPostedFileBase SignPath { get; set; }

    }
}
