using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("PageTableFieldHistory")]
    public partial class PageTableFieldHistory
    {
        [Key]
        public short PrmKey { get; set; }

        public short PageTableFieldPrmKey { get; set; }

        public short PageTablePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfPageTableField { get; set; }

        public byte DataTypePrmKey { get; set; }

        public byte MinimumLength { get; set; }

        public short MaximumLength { get; set; }

        public short FixedLength { get; set; }

        [Required]
        [StringLength(10)]
        public string PrefixString { get; set; }

        [Required]
        [StringLength(10)]
        public string PostfixString { get; set; }

        [Required]
        [StringLength(100)]
        public string IncludedCharacters { get; set; }

        [Required]
        [StringLength(100)]
        public string ExcludedCharacters { get; set; }
    }
}
