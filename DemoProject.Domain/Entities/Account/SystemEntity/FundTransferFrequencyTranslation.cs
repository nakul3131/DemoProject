using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("FundTransferFrequencyTranslation")]
    public partial class FundTransferFrequencyTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short PrmKey { get; set; }

        public byte FundTransferFrequencyPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfFundTransferFrequency { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual FundTransferFrequency FundTransferFrequency { get; set; }
    }
}
