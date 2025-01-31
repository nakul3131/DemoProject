using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("OpeningBalanceLoan")]
    public partial class OpeningBalanceLoan
    {
        [Key]
        public int PrmKey { get; set; }

        public int CustomerAccountPrmKey { get; set; }

        public decimal ProvisionAmountOfLoan { get; set; }

        public DateTime LastProvisionDateOfLoan { get; set; }

        public decimal DuesInterestOfLoan { get; set; }

        public decimal PreviousInterestAmountOfLoan { get; set; }

        public DateTime PreviousInstallmentDateOfLoan { get; set; }

        public DateTime PreviousInterestDateOfLoan { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
