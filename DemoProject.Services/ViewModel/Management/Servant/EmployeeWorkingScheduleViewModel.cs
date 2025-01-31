using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Servant
{
    public class EmployeeWorkingScheduleViewModel
    {
        // EmployeeWorkingSchedule

        public int PrmKey { get; set; }

        public Guid EmployeeWorkingScheduleId { get; set; }

        public int EmployeePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte WorkingSchedulePrmKey { get; set; }

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

        // EmployeeWorkingScheduleMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int EmployeeWorkingSchedulePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid WorkingScheduleId { get; set; }
    }
}
