using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("CoopSocietySubClassTranslation")]
    public partial class CoopSocietySubClassTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public byte CoopSocietySubClassPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(10)]
        public string SequenceNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string NameOfSubClass { get; set; }

        [Required]
        [StringLength(4000)]
        public string Examples { get; set; }

        public virtual CoopSocietySubClass CoopSocietySubClass { get; set; }
    }
}
