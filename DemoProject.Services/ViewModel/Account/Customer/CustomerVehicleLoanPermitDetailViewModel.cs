using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Customer
{
   public class CustomerVehicleLoanPermitDetailViewModel
    {
        //CustomerVehicleLoanPermit
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(3)]
        public string PermitType { get; set; }

        [StringLength(500)]
        public string PermitDetails { get; set; }

        public DateTime PermitIssueDate { get; set; }

        public DateTime PermitExpiryDate { get; set; }

        [StringLength(150)]
        public string IssuingAuthority { get; set; }

        public decimal PermitAmountPerMonth { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerVehicleLoanPermitMakerChecker

        public DateTime EntryDateTime { get; set; }

        public int CustomerVehicleLoanPermitDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
       
        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

    }
}
