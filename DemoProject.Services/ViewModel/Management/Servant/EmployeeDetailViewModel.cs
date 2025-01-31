using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Management.Servant
{
    public class EmployeeDetailViewModel
    {
        // EmployeeDetail

        public int PrmKey { get; set; }

        public Guid EmployeeDetailId { get; set; }

        public int EmployeePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte EmployeeTypePrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public DateTime DateOfJoining { get; set; }

        public DateTime? DateOfLeaving { get; set; }

        public short PostingPlacePrmKey { get; set; }

        public DateTime DateOfProbation { get; set; }

        public DateTime DateOfConfirmation { get; set; }

        public DateTime DateOfTrainingStarted { get; set; }

        public DateTime DateOfTrainingEnded { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // EmployeeDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int EmployeeDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other

        public Guid EmployeeTypeId { get; set; }

        public Guid PersonId { get; set; }

        public Guid PostingPlaceId { get; set; }
    }
}
