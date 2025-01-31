using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("PageTranslation")]
    public partial class PageTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short PagePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfPage { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(500)]
        public string ReasonForClose { get; set; }

        public virtual Page Page { get; set; }
    }
}
