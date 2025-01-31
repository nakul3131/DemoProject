using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerVehicleLoanContractDetailViewModel
    {
        //CustomerVehicleLoanContract
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }
        
        [StringLength(3)]
        public string ContractNature { get; set; }
       
        [StringLength(100)]
        public string OtherContractNatureDetails { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        [StringLength(1500)]
        public string ContractObligations { get; set; }
        
        [StringLength(100)]
        public string CompanyName { get; set; }
        
        [StringLength(500)]
        public string CompanyDetails { get; set; }
        
        [StringLength(500)]
        public string ContactDetail { get; set; }
        
        [StringLength(500)]
        public string AddressDetails { get; set; }

        public decimal ContractMonthlyAmount { get; set; }
        
        [StringLength(3)]
        public string PaymentFrequency { get; set; }
        
        [StringLength(3)]
        public string PaymentMode { get; set; }

        public byte PaymentDay { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
       
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        //CustomerVehicleLoanContractDetailMakerChecker
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public DateTime EntryDateTime { get; set; }

        public int CustomerVehicleLoanContractDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }
    }
}
