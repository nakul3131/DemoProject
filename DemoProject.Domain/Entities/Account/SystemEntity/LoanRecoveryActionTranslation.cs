using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("LoanRecoveryActionTranslation")]
    public partial class LoanRecoveryActionTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short LoanRecoveryActionPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfLoanRecoveryAction { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual LoanRecoveryAction LoanRecoveryAction { get; set; }
    }
}
