using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("AccountClassTranslation")]
    public partial class AccountClassTranslation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short PrmKey { get; set; }

        public short AccountClassPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfAccountClass { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual AccountClass AccountClass { get; set; }
    }
}
