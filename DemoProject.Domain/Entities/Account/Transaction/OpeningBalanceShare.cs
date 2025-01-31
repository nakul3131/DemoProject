using System.ComponentModel.DataAnnotations;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    public partial class OpeningBalanceShare
    {
        [Key]
        public int PrmKey { get; set; }

        public int CustomerAccountPrmKey { get; set; }

        public decimal PreviousYearBalanceOfShares { get; set; }

        public decimal FaceValueOfShares { get; set; }

        public decimal TotalShares { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
