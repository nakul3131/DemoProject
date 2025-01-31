using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeLoanRecoveryActionViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short LoanRecoveryActionPrmKey { get; set; }

        public byte FromDuesInstallment { get; set; }

        public byte ToDuesInstallment { get; set; }


        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short SchemeLoanRecoveryActionPrmKey { get; set; }

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
