using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("BusinessNatureTranslation")]
    public partial class BusinessNatureTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public byte BusinessNaturePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfBusinessNature { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual BusinessNature BusinessNature { get; set; }
    }
}
