using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonBankDetailViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short BankBranchPrmKey { get; set; }

        public long  AccountNumber { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime? CloseDate { get; set; }

        public bool IsDefaultBankForTransaction { get; set; }
        
        [StringLength(500)]
        public string NameOfFile { get; set; }
        
        [StringLength(500)]
        public string FileCaption { get; set; }
        
        public byte[] PhotoCopy { get; set; }
        
        [StringLength(1500)]
        public string LocalStoragePath { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonBankDetailMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public long PersonBankDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //Person

        [StringLength(150)]
        public string FullName { get; set; }

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        //Other
        
        [StringLength(1)]
        public string BankStatementDocumentUpload { get; set; }

        public bool EnableBankStatementDocumentUploadInDb { get; set; }
        
        [StringLength(500)]
        public string BankStatementDocumentAllowedFileFormatsForDb { get; set; }

        public short MaximumFileSizeForBankStatementDocumentUploadInDb { get; set; }

        public bool EnableBankStatementDocumentUploadInLocalStorage { get; set; }
        
        [StringLength(500)]
        public string BankStatementDocumentLocalStoragePath { get; set; }

        public short MaximumFileSizeForBankStatementDocumentUploadInLocalStorage { get; set; }

        public HttpPostedFileBase PhotoPathBank { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        // For SelectListItem

        public Guid BankBranchId { get; set; }

        public Guid BankId { get; set; }

        [StringLength(50)]
        public string NameOfBank { get; set; }

        [StringLength(50)]
        public string NameOfBankBranch { get; set; }

        public long PersonBankDetailDocumentPrmKey { get; set; }
    }
}
