using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanAccountAdditionalIncomeDetailMakerChecker")]
    public partial class CustomerLoanAccountAdditionalIncomeDetailMakerChecker
    {
        [Key]
        public long PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public long CustomerLoanAccountAdditionalIncomeDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual CustomerLoanAccountAdditionalIncomeDetail CustomerAccountAdditionalIncomeDetail { get; set; }

    }
}
