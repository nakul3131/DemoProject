﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Servant
{
    public class EmployeeDepartmentViewModel
    {
        // EmployeeDocument

        public int PrmKey { get; set; }

        public Guid EmployeeDepartmentId { get; set; }

        public int EmployeePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short DepartmentPrmKey { get; set; }

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

        // EmployeeDepartmentMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int EmployeeDepartmentPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid DepartmentId { get; set; }
    }
}
