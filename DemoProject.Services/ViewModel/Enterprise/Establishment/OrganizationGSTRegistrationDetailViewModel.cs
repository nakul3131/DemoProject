using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Establishment
{
    public class OrganizationGSTRegistrationDetailViewModel
    {
        public byte PrmKey { get; set; }

        public Guid OrganizationGSTRegistrationDetailId { get; set; }

        public short StatePrmKey { get; set; }

        public short GSTRegistrationTypePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ApplicableFrom { get; set; }

        public byte GSTReturnPeriodicityPrmKey { get; set; }

        public bool IsApplicableEWayBill { get; set; }

        public decimal ThresholdLimit { get; set; }

        [StringLength(15)]
        public string GSTRegistrationNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //OrganizationGSTRegistrationDetailMakerChecker
        public DateTime EntryDateTime { get; set; }

        public byte OrganizationGSTRegistrationDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        public Guid StateId { get; set; }

        public Guid GSTRegistrationTypeId { get; set; }

        public Guid GSTReturnPeriodicityId { get; set; }
        
        [StringLength(100)]
        public string NameOfState { get; set; }
        
        [StringLength(100)]
        public string NameOfGSTRegistrationType { get; set; }
        
        [StringLength(100)]
        public string NameOfGSTReturnPeriodicity { get; set; }

    }
}
