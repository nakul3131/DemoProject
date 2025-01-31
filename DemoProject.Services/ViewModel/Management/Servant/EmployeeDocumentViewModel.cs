using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.Management.Servant
{
    public class EmployeeDocumentViewModel
    {
        // EmployeeDocument

        public int PrmKey { get; set; }

        public Guid EmployeeDocumentId { get; set; }

        public int EmployeePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short DocumentPrmKey { get; set; }

        public byte[] DocumentCopy { get; set; }

        [StringLength(50)]
        public string NameOfFile { get; set; }

        [StringLength(1500)]
        public string StoragePath { get; set; }

        public HttpPostedFileBase DocPath { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //MakerChecker
        public DateTime EntryDateTime { get; set; }

        public int EmployeeDocumentPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid DocumentId { get; set; }

        public string NameOfDocument { get; set; }
    }
}
