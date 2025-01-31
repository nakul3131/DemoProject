using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("OpeningBalanceDeposit")]
    public partial class OpeningBalanceDeposit
    {
        [Key]
        public int PrmKey { get; set; }

        public int CustomerAccountPrmKey { get; set; }

        public decimal ProductMinBalance { get; set; }

        public decimal ProvisionAmountOfDeposit { get; set; }

        public DateTime LastProvisionDateOfDeposit { get; set; }

        public decimal PreviousInterestAmountOfDeposit { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
