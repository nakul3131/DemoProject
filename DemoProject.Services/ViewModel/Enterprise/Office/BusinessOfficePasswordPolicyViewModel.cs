using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.Abstract.Security.Master;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficePasswordPolicyViewModel
    {
        // BusinessOfficePasswordPolicy
 
        public int PrmKey { get; set; }

        public Guid BusinessOfficePasswordPolicyId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short PasswordPolicyPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // BusinessOfficePasswordPolicyMakerCheker

        public DateTime EntryDateTime { get; set; }

        public int BusinessOfficePasswordPolicyPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public string UserAction { get; set; }

        public string Remark { get; set; }

        // BusinessOffice

        public Guid BusinessOfficeId { get; set; }

        [StringLength(50)]
        public string NameOfBusinessOffice { get; set; }

        // PasswordPolicy
        public Guid PasswordPolicyId { get; set; }

        [StringLength(100)]
        public string NameOfPasswordPolicy { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
        
    }
}
