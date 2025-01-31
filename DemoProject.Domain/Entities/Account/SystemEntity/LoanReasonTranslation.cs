using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("LoanReasonTranslation")]
    public partial class LoanReasonTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short LoanReasonPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(700)]
        public string TransNameOfLoanReason { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual LoanReason LoanReason { get; set; }
    }
}
