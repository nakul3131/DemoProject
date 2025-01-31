using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace DemoProject.Services.ViewModel.Enterprise.Establishment
{
    public class OrganizationFundViewModel
    {
        //OrganizationFund
        public short PrmKey { get; set; }

        public Guid OrganizationFundId { get; set; }

        public short FundPrmKey { get; set; }

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
        
        //MakerChecker

        public DateTime EntryDateTime { get; set; }

        public short OrganizationFundPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        //OrganizationFundTranslation

        public Guid OrganizationFundTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        [StringLength(20)]
        public string TransSequenceNumberText { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        //OrganizationFundTranslationMakerChecker
        public short OrganizationFundTranslationPrmKey { get; set; }

        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        // For SelectListItem
        public Guid FundId { get; set; }

        [StringLength(100)]
        public string NameOfFund { get; set; }

    }
}
