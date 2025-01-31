using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerEducationalLoanDetailViewModel
    {
        //CustomerEducationalLoanDetail
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short EducationalCoursePrmKey { get; set; }

        [StringLength(3)]
        public string CourseApprovedBy { get; set; }

        public short InstitutePrmKey { get; set; }

        [StringLength(150)]
        public string OtherNameOfInstitute { get; set; }

        [StringLength(200)]
        public string OtherInstituteContactDetails { get; set; }

        [StringLength(500)]
        public string OtherInstituteAddressDetails { get; set; }

        public short CityPrmKey { get; set; }

        public decimal TotalCourseFees { get; set; }

        public decimal AccommodationFees { get; set; }

        public decimal BooksOrEquipmentsExpenses { get; set; }

        public decimal TravellingExpenses { get; set; }

        public decimal RefundableDeposit { get; set; }

        public decimal OtherFees { get; set; }

        [StringLength(1500)]
        public string OtherFeesDetails { get; set; }

        [StringLength(3)]
        public string AdmissionThrough { get; set; }

        [StringLength(150)]
        public string ContactPersonName { get; set; }

        [StringLength(100)]
        public string ContactPersonContactDetails { get; set; }

        public DateTime CourseStartDate { get; set; }

        public DateTime CourseEndDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }
        //CustomerEducationalLoanDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerEducationalLoanDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //CustomerEducationalLoanDetailTranslation 

        public byte TransModificationNumber { get; set; }

        public short LanguagePrmKey { get; set; }

        [StringLength(150)]
        public string TransOtherNameOfInstitute { get; set; }

        [StringLength(200)]
        public string TransOtherInstituteContactDetails { get; set; }

        [StringLength(500)]
        public string TransOtherInstituteAddressDetails { get; set; }

        [StringLength(1500)]
        public string TransOtherFeesDetails { get; set; }

        [StringLength(150)]
        public string TransContactPersonName { get; set; }

        [StringLength(100)]
        public string TransContactPersonContactDetails { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        //CustomerEducationalLoanDetailTranslationMakerChecker
        public int CustomerEducationalLoanDetailTranslationPrmKey { get; set; }

        //Other
        public Guid EducationalCourseId { get; set; }

        [StringLength(150)]
        public string NameOfCourse { get; set; }

        public Guid InstituteId { get; set; }

        [StringLength(150)]
        public string NameOfInstitute { get; set; }
        public Guid CenterId { get; set; }

        [StringLength(150)]
        public string NameOfCenter { get; set; }

        
    }
}
