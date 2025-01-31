using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanFieldInvestigation")]
    public partial class CustomerLoanFieldInvestigation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerLoanFieldInvestigation()
        {
            CustomerLoanFieldInvestigationMakerCheckers = new HashSet<CustomerLoanFieldInvestigationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime DateOfInvestigation { get; set; }

        public int EmployeePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfContactedPerson { get; set; }

        [Required]
        [StringLength(3)]
        public string RelationWithApplicant { get; set; }

        [Required]
        [StringLength(100)]
        public string OtherRelationTitle { get; set; }

        public bool IsAnyPoliticalAffiliation { get; set; }

        [Required]
        [StringLength(1500)]
        public string LocalityRemark { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstReferenceName { get; set; }

        [Required]
        [StringLength(500)]
        public string FirstReferenceAddress { get; set; }

        public bool IsPositiveFeedbackFromFirstReference { get; set; }

        [Required]
        [StringLength(100)]
        public string SecondReferenceName { get; set; }

        [Required]
        [StringLength(500)]
        public string SecondReferenceAddress { get; set; }

        public bool IsPositiveFeedbackFromSecondReference { get; set; }

        [Required]
        [StringLength(100)]
        public string ThirdReferenceName { get; set; }

        [Required]
        [StringLength(500)]
        public string ThirdReferenceAddress { get; set; }

        public bool IsPositiveFeedbackFromThirdReference { get; set; }

        [Required]
        [StringLength(2500)]
        public string PositiveObservations { get; set; }

        [Required]
        [StringLength(2500)]
        public string NegativeObservations { get; set; }

        public bool IsRecommendedForFinance { get; set; }

        [Required]
        [StringLength(2500)]
        public string NonRecommendationReason { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerLoanAccount CustomerLoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanFieldInvestigationMakerChecker> CustomerLoanFieldInvestigationMakerCheckers { get; set; }
    }
}
