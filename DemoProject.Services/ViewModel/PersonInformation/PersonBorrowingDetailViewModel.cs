using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonBorrowingDetailViewModel
    {
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }
        
        [StringLength(150)]
        public string NameOfOrganization { get; set; }
        
        [StringLength(50)]
        public string Branch { get; set; }
        
        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime MatureDate { get; set; }

        public DateTime? CloseDate { get; set; }
        
        [StringLength(1500)]
        public string LoanDetails { get; set; }
        
        [StringLength(1500)]
        public string MortgageDetails { get; set; }

        public decimal MortgageAmount { get; set; }

        public decimal SanctionLoanAmount { get; set; }

        public decimal InstallmentAmount { get; set; }

        public decimal LoanBalanceAmount { get; set; }

        public short OverduesInstallment { get; set; }

        public decimal OverduesAmount { get; set; }

        public bool IsTakingAnyCourtAction { get; set; }

        public byte CourtCaseTypePrmKey { get; set; }

        public DateTime FilingDate { get; set; }
        
        [StringLength(50)]
        public string FilingNumber { get; set; }

        public DateTime RegistrationDate { get; set; }
        
        [StringLength(50)]
        public string RegistrationNumber { get; set; }
        
        [StringLength(50)]
        public string CNRNumber { get; set; }

        public byte CourtCaseStagePrmKey { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }

        //PersonBorrowingDetailMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonBorrowingDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        //PersonBorrowingDetailTranslation

        //public Guid PersonBorrowingDetailTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }
        
        [StringLength(150)]
        public string TransNameOfOrganization { get; set; }
        
        [StringLength(50)]
        public string TransBranch { get; set; }

        [StringLength(1500)]
        public string TransLoanDetails { get; set; }
        
        [StringLength(1500)]
        public string TransMortgageDetails { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }
        
        //PersonBorrowingDetailTranslationMakerChecker
        
        public long PersonBorrowingDetailTranslationPrmKey { get; set; }

        //Person

        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

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

        public Guid CourtCaseTypeId { get; set; }

        [StringLength(100)]
        public string NameOfCourtCaseType { get; set; }
        
        public Guid CourtCaseStageId { get; set; }

        [StringLength(100)]
        public string NameOfCourtCaseStage { get; set; }

        //other
        public long CustomerAccountPrmKey { get; set; }

        public long CustomerLoanAccountBorrowingDetailPrmKey { get; set; }


    }
}
