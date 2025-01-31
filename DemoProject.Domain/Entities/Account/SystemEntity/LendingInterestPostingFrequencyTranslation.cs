using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("LendingInterestPostingFrequencyTranslation")]
    public partial class LendingInterestPostingFrequencyTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public byte LendingInterestPostingFrequencyPrmKey { get; set; }

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

        public virtual LendingInterestPostingFrequency LendingInterestPostingFrequency { get; set; }
    }
}
