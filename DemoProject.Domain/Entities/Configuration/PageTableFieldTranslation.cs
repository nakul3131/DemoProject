using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("PageTableFieldTranslation")]
    public partial class PageTableFieldTranslation
    {
        [Key]

        public short PrmKey { get; set; }

        public short PageTableFieldPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfPageTableField { get; set; }

        public virtual PageTableField PageTableField { get; set; }
    }
}
