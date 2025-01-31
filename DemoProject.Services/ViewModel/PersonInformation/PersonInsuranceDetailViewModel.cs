using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonInsuranceDetailViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte InsuranceTypePrmKey { get; set; }

        public short InsuranceCompanyPrmKey { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? MaturityDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(50)]
        public string PolicyNumber { get; set; }

        public decimal PolicyPremium { get; set; }

        public decimal PolicySumAssured { get; set; }

        public short OverduesPremium { get; set; }

        public bool HasAnyMortgage { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonInsuranceDetailMakerChecker
        
        public DateTime EntryDateTime { get; set; }

        public long PersonInsuranceDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        //Other

        [StringLength(50)]
        public string NameOfUser { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        // For SelectListItem

        public Guid InsuranceTypeId { get; set; }

        public Guid InsuranceCompanyId { get; set; }

        [StringLength(50)]
        public string NameOfInsuranceType { get; set; }

        [StringLength(50)]
        public string NameOfInsuranceCompany { get; set; }

        [StringLength(50)]
        public string NameOfFrequency { get; set; }

    }
}
