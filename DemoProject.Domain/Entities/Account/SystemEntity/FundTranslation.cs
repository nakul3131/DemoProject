using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("FundTranslation")]
    public partial class FundTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short FundPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(150)]
        public string TransNameOfFund { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(150)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(20)]
        public string TransSequenceNumberText { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual Fund Fund { get; set; }
    }
}
