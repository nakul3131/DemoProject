using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerGoldLoanCollateralPhotoViewModel
    {
        public int PrmKey { get; set; }

        public Guid CustomerGoldLoanCollateralPhotoId { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte SequenceNumber { get; set; }

        [StringLength(3)]
        public string PhotoType { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }

        [StringLength(500)]
        public string PhotoCaption { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        public byte[] PhotoCopy { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerGoldLoanCollateralPhotoMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerGoldLoanCollateralPhotoPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        [StringLength(100)]
        public string NameOfGoldOrnament { get; set; }

        [StringLength(100)]
        public string NameOfPhotoType { get; set; }

        public HttpPostedFileBase PhotoPath { get; set; }

        public bool EnableGoldPhotoUploadInLocalStorage { get; set; }

        [StringLength(3)]
        public string PhotoTypeId { get; set; }

    }
}
