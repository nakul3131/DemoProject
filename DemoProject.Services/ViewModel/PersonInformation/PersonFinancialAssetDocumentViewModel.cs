using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonFinancialAssetDocumentViewModel
    {

        public long PrmKey { get; set; }

        public long PersonFinancialAssetPrmKey { get; set; }

        public byte ModificationNumber { get; set; }
       
        public byte[] PhotoCopy { get; set; }

        public HttpPostedFileBase PhotoPath { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
      
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        //PersonFinancialAssetDocumentMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long PersonFinancialAssetDocumentPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

    }
}
