using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonGSTRegistrationDetailViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public short StatePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short GSTRegistrationTypePrmKey { get; set; }

        public DateTime ApplicableFrom { get; set; }

        public byte GSTReturnPeriodicityPrmKey { get; set; }

        public bool IsApplicableEWayBill { get; set; }

        public decimal ThresholdLimit { get; set; }

        [StringLength(15)]
        public string RegistrationNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonGSTRegistrationDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonGSTRegistrationDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // PersonGSTReturnDocumentViewModel
        public PersonGSTReturnDocumentViewModel PersonGSTReturnDocumentViewModel { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        // Other

        public bool IsPanCard { get; set; }

        public bool UploadGSTReturnDocument { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        // For DropdownList
        
        public Guid StateId { get; set; }

        public string NameOfState { get; set; }

        public Guid GSTRegistrationTypeId { get; set; }

        public string NameOfGSTRegistrationType { get; set; }

        public Guid GSTReturnPeriodicityId { get; set; }

        public string NameOfGSTReturnPeriodicity { get; set; }

    }
}
