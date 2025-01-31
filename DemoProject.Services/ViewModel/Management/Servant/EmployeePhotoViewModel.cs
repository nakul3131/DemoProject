using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.Management.Servant
{
    public class EmployeePhotoViewModel
    {
        // EmployeePhoto

        public int PrmKey { get; set; }

        public int EmployeePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        //public byte[] Photo { get; set; }

        [StringLength(500)]
        public string FileCaption { get; set; }

        public byte[] PhotoCopy { get; set; }

        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        public HttpPostedFileBase PhotoPath { get; set; }

        [StringLength(1500)]
        public string StoragePath { get; set; }

        [StringLength(500)]
        public string NameOfFile { get; set; }

        [StringLength(50)]
        public string Extension { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // EmployeePhotoMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int EmployeePhotoPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
    }
}
