using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerEducationalLoanDetail")]
    public partial class CustomerEducationalLoanDetail
    {
        public CustomerEducationalLoanDetail()
        {
            CustomerEducationalLoanDetailMakerCheckers = new HashSet<CustomerEducationalLoanDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short EducationalCoursePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string CourseApprovedBy { get; set; }

        public short InstitutePrmKey { get; set; }

        [Required]
        [StringLength(150)]
        public string OtherNameOfInstitute { get; set; }

        [Required]
        [StringLength(200)]
        public string OtherInstituteContactDetails { get; set; }

        [Required]
        [StringLength(500)]
        public string OtherInstituteAddressDetails { get; set; }

        public short CityPrmKey { get; set; }

        public decimal TotalCourseFees { get; set; }

        public decimal AccommodationFees { get; set; }

        public decimal BooksOrEquipmentsExpenses { get; set; }

        public decimal TravellingExpenses { get; set; }

        public decimal RefundableDeposit { get; set; }

        public decimal OtherFees { get; set; }

        [Required]
        [StringLength(1500)]
        public string OtherFeesDetails { get; set; }

        [Required]
        [StringLength(3)]
        public string AdmissionThrough { get; set; }

        [Required]
        [StringLength(150)]
        public string ContactPersonName { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactPersonContactDetails { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseEndDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public CustomerLoanAccount CustomerLoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerEducationalLoanDetailMakerChecker> CustomerEducationalLoanDetailMakerCheckers { get; set; }
    }
}
