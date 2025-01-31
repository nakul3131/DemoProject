using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InterestRateChargedDurationTranslation")]
    public partial class InterestRateChargedDurationTranslation
    {
        [Key]
        public short PrmKey { get; set; }
        
        public byte InterestRateChargedDurationPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(500)]
        public string TransTitle { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual InterestRateChargedDuration InterestRateChargedDuration { get; set; }
    }
}
