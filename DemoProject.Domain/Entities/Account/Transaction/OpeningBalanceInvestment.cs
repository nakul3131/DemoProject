using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("OpeningBalanceInvestment")]
    public partial class OpeningBalanceInvestment
    {
        [Key]
        public int PrmKey { get; set; }

        public int CustomerAccountPrmKey { get; set; }

        public decimal ProvisionAmountOfInvestment { get; set; }

        public DateTime LastProvisionDateOfInvestment { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
