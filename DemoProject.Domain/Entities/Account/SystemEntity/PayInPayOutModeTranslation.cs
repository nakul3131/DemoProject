using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("PayInPayOutModeTranslation")]
    public partial class PayInPayOutModeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public byte PayInPayOutModePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfPayInPayOutMode { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual PayInPayOutMode PayInPayOutMode { get; set; }
    }
}
