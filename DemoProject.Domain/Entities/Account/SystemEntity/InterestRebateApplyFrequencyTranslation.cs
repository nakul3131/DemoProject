using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InterestRebateApplyFrequencyTranslation")]
    public partial class InterestRebateApplyFrequencyTranslation
    {
        [Key]
        public byte PrmKey { get; set; }

        public byte InterestRebateApplyFrequencyPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfFrequency { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual InterestRebateApplyFrequency InterestRebateApplyFrequency { get; set; }
    }
}
