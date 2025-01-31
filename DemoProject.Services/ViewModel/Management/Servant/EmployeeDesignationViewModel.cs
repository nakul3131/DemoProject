using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Servant
{
    public class EmployeeDesignationViewModel
    {
        // EmployeeDesignation

        public int PrmKey { get; set; }

        public Guid EmployeeDesignationId { get; set; }

        public int EmployeePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short DesignationPrmKey { get; set; }

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

        // EmployeeDesignationMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int EmployeeDesignationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid DesignationId { get; set; }
    }
}
