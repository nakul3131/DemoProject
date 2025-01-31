using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InstallmentFrequencyTranslation")]
    public partial class InstallmentFrequencyTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        public byte InstallmentFrequencyPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(50)]
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

        public virtual InstallmentFrequency InstallmentFrequency { get; set; }
    }
}
