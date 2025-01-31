using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerVehicleLoanPhotoViewModel
    {
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(150)]
        public string NameOfFile { get; set; }

        [StringLength(100)]
        public string PhotoCaption { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        public HttpPostedFileBase DocPath { get; set; }

        public byte[] PhotoCopy { get; set; }

        public HttpPostedFileBase PhotoPath { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerVehicleLoanPhotoMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerVehicleLoanPhotoPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public Guid DocumentId { get; set; }
    }
}
