using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanAgainstDepositCollateralDetailMakerChecker")]
    public partial class CustomerLoanAgainstDepositCollateralDetailMakerChecker
    {
        [Key]
        public int PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public int CustomerLoanAgainstDepositCollateralDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual CustomerLoanAgainstDepositCollateralDetail CustomerLoanAgainstDepositCollateralDetail { get; set; }

    }
}
