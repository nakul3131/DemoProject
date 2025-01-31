using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeTypeTranslation")]
    public partial class SchemeTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }
        
        public byte SchemeTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransSystemName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfSchemeType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual SchemeType SchemeType { get; set; }
    }
}
