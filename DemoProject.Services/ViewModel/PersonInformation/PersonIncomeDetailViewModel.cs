using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.PersonInformation
{
    public class PersonIncomeDetailViewModel
    {
        public long PrmKey { get; set; }

        public Guid PersonId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public byte NumberOfDependent { get; set; }

        public decimal SalaryIncome { get; set; }

        public decimal BusinessIncome { get; set; }

        public decimal AgricultureIncome { get; set; }

        public decimal InvestmentIncome { get; set; }

        public decimal HouseHoldIncome { get; set; }

        public decimal OtherSourceOfIncome { get; set; }

        [Required]
        [StringLength(1500)]
        public string OtherSourceDetails { get; set; }

        public decimal GrossAnnualIncome { get; set; }

        public bool IsIncomeTaxPayer { get; set; }

        public bool IsSubmitedForm60 { get; set; }
        
        [StringLength(1500)]
        public string Note { get; set; }
        
        [StringLength(1500)]
        public string ReasonForModification { get; set; }
        
        [StringLength(3)]
        public string EntryStatus { get; set; }
        
        //PersonIncomeDetailMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long PersonIncomeDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }
        
        [StringLength(3)]
        public string UserAction { get; set; }
        
        [StringLength(1500)]
        public string Remark { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }
    }
}
