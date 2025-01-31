using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Servant
{
    public class EmployeeSalaryStructureViewModel
    {
        // EmployeeSalaryStructure

        public int PrmKey { get; set; }

        public Guid EmployeeSalaryStructureId { get; set; }

        public int EmployeePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short SalaryBreakupPrmKey { get; set; }

        public decimal BreakupValue { get; set; }

        public bool IsPercentage { get; set; }

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

        //MakerChecker
        public DateTime EntryDateTime { get; set; }

        public int EmployeeSalaryStructurePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other
        public Guid SalaryBreakupId { get; set; }

        public string NameOfSalaryBreakup { get; set; }

        public string NameOfUser { get; set; }
    }
}
