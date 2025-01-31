using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerLoanFieldInvestigationViewModel
    {
        public int PrmKey { get; set; }

        public Guid CustomerLoanFieldInvestigationId { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime DateOfInvestigation { get; set; }

        public int EmployeePrmKey { get; set; }

        [StringLength(100)]
        public string NameOfContactedPerson { get; set; }

        [StringLength(3)]
        public string RelationWithApplicant { get; set; }

        [StringLength(100)]
        public string OtherRelationTitle { get; set; }

        public bool IsAnyPoliticalAffiliation { get; set; }

        [StringLength(1500)]
        public string LocalityRemark { get; set; }

        [StringLength(100)]
        public string FirstReferenceName { get; set; }

        [StringLength(500)]
        public string FirstReferenceAddress { get; set; }

        public bool IsPositiveFeedbackFromFirstReference { get; set; }

        [StringLength(100)]
        public string SecondReferenceName { get; set; }

        [StringLength(500)]
        public string SecondReferenceAddress { get; set; }

        public bool IsPositiveFeedbackFromSecondReference { get; set; }

        [StringLength(100)]
        public string ThirdReferenceName { get; set; }

        [StringLength(500)]
        public string ThirdReferenceAddress { get; set; }

        public bool IsPositiveFeedbackFromThirdReference { get; set; }

        [StringLength(2500)]
        public string PositiveObservations { get; set; }

        [StringLength(2500)]
        public string NegativeObservations { get; set; }

        public bool IsRecommendedForFinance { get; set; }

        [StringLength(2500)]
        public string NonRecommendationReason { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerLoanFieldInvestigationMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerLoanFieldInvestigationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // DropdownList 

        public Guid InvestigationOfficerId { get; set; }

    }
}
