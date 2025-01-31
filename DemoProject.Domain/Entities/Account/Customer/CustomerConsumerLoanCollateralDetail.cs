using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerConsumerLoanCollateralDetail")]
    public partial class CustomerConsumerLoanCollateralDetail
    {
        public CustomerConsumerLoanCollateralDetail()
        {
            CustomerConsumerLoanCollateralDetailMakerCheckers = new HashSet<CustomerConsumerLoanCollateralDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonPrmKey { get; set; }

        public short ConsumerDurableItemPrmKey { get; set; }

        public short ConsumerDurableItemBrandPrmKey { get; set; }

        [Required]
        [StringLength(150)]
        public string ItemOtherDetail { get; set; }

        public short ManufactureYear { get; set; }

        [Required]
        [StringLength(10)]
        public string SerialNumber { get; set; }

        public decimal ProductAmount { get; set; }

        public decimal DownPayment { get; set; }

        public byte WarrantyInMonth { get; set; }

        public byte GuaranteeInMonth { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerLoanAccount CustomerLoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerConsumerLoanCollateralDetailMakerChecker> CustomerConsumerLoanCollateralDetailMakerCheckers { get; set; }

    }
}
