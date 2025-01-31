using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("TransactionCustomerAccountInterest")]
    public partial class TransactionCustomerAccountInterest
    {
        [Key]
        public long PrmKey { get; set; }

        public long TransactionCustomerAccountPrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte InterestMethodPrmKey { get; set; }

        public decimal RateOfInterest { get; set; }

        public DateTime InterestCalculationFromDate { get; set; }

        public DateTime InterestCalculationToDate { get; set; }

        public decimal CalculatedInterestAmount { get; set; }

        public decimal InputedInterestAmount { get; set; }

        public decimal InterestBalanceAmount { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual TransactionCustomerAccount TransactionCustomerAccount { get; set; }
    }
}
