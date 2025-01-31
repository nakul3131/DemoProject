using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Establishment
{
    public class OrganizationLoanTypeViewModel
    {
        public short PrmKey { get; set; }

        public Guid OrganizationLoanTypeId { get; set; }

        public byte LoanTypePrmKey { get; set; }

        public byte MaximumLoanTenure { get; set; }

        public decimal MinimumDownPaymentPercentage { get; set; }

        public short SequenceNumber { get; set; }

        [StringLength(20)]
        public string SequenceNumberText { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //OrganizationLoanTypeMakerChecker
        public DateTime EntryDateTime { get; set; }

        public short OrganizationLoanTypePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        //OrganizationLoanTypeTranslation
        public Guid OrganizationLoanTypeTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        [StringLength(20)]
        public string TransSequenceNumberText { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        //OrganizationLoanTypeTranslationMakerChecker
        public short OrganizationLoanTypeTranslationPrmKey { get; set; }

        //other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public string NameOfMaker { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // For SelectListItem
        public Guid LoanTypeId { get; set; }

        [StringLength(100)]
        public string NameOfLoanType { get; set; }

    }
}
