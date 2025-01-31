using DemoProject.Services.Abstract.MachineLearning;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeCoopRegistrationViewModel
    {
        //BusinessOfficeCoopRegistration

        public short PrmKey { get; set; }

        public Guid BusinessOfficeCoopRegistrationId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ApprovalDate { get; set; }

        public DateTime RegistrationDate { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public short NumericCode { get; set; }

        [StringLength(20)]
        public string AlphaNumericCode { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //BusinessOfficeCoopRegistrationMakerCheker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficeCoopRegistrationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //BusinessOfficeCoopRegistrationTranslation

        public Guid BusinessOfficeCoopRegistrationTranslationId { get; set; }
        
        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(50)]
        public string TransRegistrationNumber { get; set; }

        [StringLength(50)]
        public string TransReferenceNumber { get; set; }

        [StringLength(50)]
        public string TransAlphaNumericCode { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }
        
        // BusinessOfficeCoopRegistrationTranslationMakerChecker

        public short BusinessOfficeCoopRegistrationTranslationPrmKey { get; set; }

        // Translation In Regional

        // BusinessOffice

        public Guid BusinessOfficeId { get; set; }

        [StringLength(50)]
        public string NameOfBusinessOffice { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

    }
}
