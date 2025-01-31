using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerLoanAccountViewModel
    {
        public int PrmKey { get; set; }

        public Guid CustomerLoanAccountId { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int MinuteOfMeetingAgendaPrmKey { get; set; }

        public DateTime MaturityDate { get; set; }

        public short LoanReasonPrmKey { get; set; }
        
        public short OccupationPrmKey { get; set; }
        public bool IsEmployee { get; set; }

        public decimal DemandAmount { get; set; }

        public short CIBILScore { get; set; }

        public decimal SanctionAmount { get; set; }

        public decimal DeductedSharesAmount { get; set; }

        [StringLength(1000)]
        public string DeductionRemark { get; set; }

        [StringLength(1500)]
        public string StrengthsFactors { get; set; }

        [StringLength(1500)]
        public string WeaknessesFactors { get; set; }

        [StringLength(1500)]
        public string OpportunitiesFactors { get; set; }

        [StringLength(1500)]
        public string ThreatsFactors { get; set; }

        [StringLength(1500)]
        public string PastCreditHistory { get; set; }

        [Required]
        [StringLength(1500)]
        public string LegalAndRegulatoryCompliance { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        //CustomerLoanAccountMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }

        public string Day { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //CustomerLoanAccountTranslation

        public short LanguagePrmKey { get; set; }

        [StringLength(1500)]
        public string TransStrengthsFactors { get; set; }

        [StringLength(1500)]
        public string TransWeaknessesFactors { get; set; }

        [StringLength(1500)]
        public string TransOpportunitiesFactors { get; set; }

        [StringLength(1500)]
        public string TransThreatsFactors { get; set; }

        [StringLength(1500)]
        public string TransPastCreditHistory { get; set; }

        [StringLength(1500)]
        public string TransLegalAndRegulatoryCompliance { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        //CustomerLoanAccountTranslationMakerChecker

        public int CustomerLoanAccountTranslationPrmKey { get; set; }

        // Other

        public Guid MinuteOfMeetingAgendaId { get; set; }

        public Guid LoanReasonId { get; set; }

        public Guid TenureListId { get; set; }
        public Guid OccupationId { get; set; }

        public bool EnableAllservices { get; set; }

    }
}
