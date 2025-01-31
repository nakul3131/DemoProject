using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class CustomerConsumerLoanCollateralDetailViewModel
    {
        //CustomerConsumerLoanCollateralDetail
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonPrmKey { get; set; }

        public short ConsumerDurableItemPrmKey { get; set; }

        public short ConsumerDurableItemBrandPrmKey { get; set; }
        
        [StringLength(150)]
        public string ItemOtherDetail { get; set; }

        public short ManufactureYear { get; set; }
        
        [StringLength(10)]
        public string SerialNumber { get; set; }

        public decimal ProductAmount { get; set; }

        public decimal DownPayment { get; set; }

        public byte WarrantyInMonth { get; set; }

        public byte GuaranteeInMonth { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }
       
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //CustomerConsumerLoanCollateralDetailMakerChecker

        public int CustomerConsumerLoanCollateralDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public string UserAction { get; set; }

        public string Remark { get; set; }

        //Other
        public Guid PersonId { get; set; }

        [StringLength(150)]
        public string FullName { get; set; }

        public Guid ConsumerDurableItemId { get; set; }

        [StringLength(150)]
        public string NameOfItem { get; set; }

        public Guid ConsumerDurableItemBrandId { get; set; }

        [StringLength(50)]
        public string NameOfBrand { get; set; }
    }
}
