using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanSanctionAuthorityViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public decimal ManagerEmpoweredSanctionLoanAmountFrom { get; set; }

        public decimal ManagerEmpoweredSanctionLoanAmountTo { get; set; }

        public decimal CommitteeEmpoweredSanctionLoanAmountFrom { get; set; }

        public decimal CommitteeEmpoweredSanctionLoanAmountTo { get; set; }

        public decimal BoardOfDirectorEmpoweredSanctionLoanAmountFrom { get; set; }

        public decimal BoardOfDirectorEmpoweredSanctionLoanAmountTo { get; set; }

        public decimal CEOEmpoweredSanctionLoanAmountFrom { get; set; }

        public decimal CEOEmpoweredSanctionLoanAmountTo { get; set; }

        public decimal ChairmanEmpoweredSanctionLoanAmountFrom { get; set; }

        public decimal ChairmanEmpoweredSanctionLoanAmountTo { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanSanctionAuthorityPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }
    }
}
