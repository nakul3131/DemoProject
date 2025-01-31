using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonCourtCaseViewModel
    {
        // PersonCourtCase
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte CourtCaseTypePrmKey { get; set; }

        public DateTime FilingDate { get; set; }

        [StringLength(50)]
        public string FilingNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        [StringLength(50)]
        public string RegistrationNumber { get; set; }

        [StringLength(50)]
        public string CNRNumber { get; set; }

        public decimal AmountOfDecree { get; set; }

        public decimal CollateralAmount { get; set; }

        public byte CourtCaseStagePrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        // PersonCourtCaseMakerChecker

        public DateTime EntryDateTime { get; set; }

        public long PersonCourtCasePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Person
        public Guid PersonId { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        // Other
        public long CustomerAccountPrmKey { get; set; }

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

        public Guid CourtCaseStageId { get; set; }

        [StringLength(100)]
        public string NameOfCourtCaseStage { get; set; }

        public Guid CourtCaseTypeId { get; set; }

        [StringLength(100)]
        public string NameOfCourtCaseType { get; set; }
        public long CustomerLoanAccountCourtCaseDetailPrmKey { get; set; }

    }
}