using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;

namespace DemoProject.Domain.Entities.Management.SystemEntity
{
    [Table("EmployerNatureTranslation")]
    public partial class EmployerNatureTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short EmployerNaturePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfEmployerNature { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual EmployerNature EmployerNature { get; set; }

        public virtual Language Language { get; set; }
    }
}
