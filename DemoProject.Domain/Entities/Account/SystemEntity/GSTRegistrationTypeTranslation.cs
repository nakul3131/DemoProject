using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("GSTRegistrationTypeTranslation")]
    public partial class GSTRegistrationTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short GSTRegistrationTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfGSTRegistrationType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual GSTRegistrationType GSTRegistrationType { get; set; }
    }
}
