using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.FinancialStatement
{
    [Table("TrialBalanceSheet")]
    public partial class TrialBalanceSheet
    {
        [Key]
        public long PrmKey { get; set; }

        public Guid TrialBalanceSheetId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public DateTime EffectiveDate { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public decimal TotalCreditAmount { get; set; }

        public decimal TotalDebitAmount { get; set; }

        public decimal Balance { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
