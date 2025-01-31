using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Servant
{
    public class EmployeeViewModel
    {
        // Employee

        public int PrmKey { get; set; }

        public Guid EmployeeId { get; set; }

        [StringLength(30)]
        public string EmployeeCode { get; set; }

        [StringLength(50)]
        public string ExternalEmployeeId1 { get; set; }

        [StringLength(50)]
        public string ExternalEmployeeId2 { get; set; }

        [StringLength(3)]
        public string EmployeeCategory { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // EmployeeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int EmployeePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //EmployeeModification

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // EmployeeModificationMakerChecker

        public int EmployeeModificationPrmKey { get; set; }
        
        // EmployeeDocumentViewModel
        public EmployeeDocumentViewModel EmployeeDocumentViewModel { get; set; }

        // EmployeeSalaryStructureViewModel
        public EmployeeSalaryStructureViewModel EmployeeSalaryStructureViewModel { get; set; }

        // EmployeeDepartmentViewModel
        public EmployeeDepartmentViewModel EmployeeDepartmentViewModel { get; set; }

        // EmployeeDesignationViewModel
        public EmployeeDesignationViewModel EmployeeDesignationViewModel { get; set; }

        // EmployeeDetailViewModel
        public EmployeeDetailViewModel EmployeeDetailViewModel { get; set; }

        // EmployeePerformanceRatingViewModel
        public EmployeePerformanceRatingViewModel EmployeePerformanceRatingViewModel { get; set; }

        // EmployeePhotoViewModel
        public EmployeePhotoViewModel EmployeePhotoViewModel { get; set; }

        // EmployeeWorkingScheduleViewModel
        public EmployeeWorkingScheduleViewModel EmployeeWorkingScheduleViewModel { get; set; }

        //Document

        [StringLength(1500)]
        public string AllowedFileFormats { get; set; }

        public long MaximumFileSize { get; set; }

        // Other

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; } 
    }
}

