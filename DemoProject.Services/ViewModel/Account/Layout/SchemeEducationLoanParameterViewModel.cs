using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeEducationLoanParameterViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool IsApplicableAllUniversities { get; set; }

        public bool IsApplicableAllCourse { get; set; }

        public decimal MinimumFees { get; set; }

        public decimal MaximumFees { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }


        //SchemeEducationLoanParameterMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeEducationLoanParameterPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }


    }
}
