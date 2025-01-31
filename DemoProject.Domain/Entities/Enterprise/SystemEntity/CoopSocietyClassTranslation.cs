using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("CoopSocietyClassTranslation")]
    public partial class CoopSocietyClassTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public byte CoopSocietyClassPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(10)]
        public string SequenceNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfClass { get; set; }

        public virtual CoopSocietyClass CoopSocietyClass { get; set; }
    }
}
