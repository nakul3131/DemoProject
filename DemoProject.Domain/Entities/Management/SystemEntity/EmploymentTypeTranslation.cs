using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;

namespace DemoProject.Domain.Entities.Management.SystemEntity
{
    [Table("EmploymentTypeTranslation")]
    public partial class EmploymentTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short EmploymentTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfEmploymentType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual EmploymentType EmploymentType { get; set; }

        public virtual Language Language { get; set; }
    }
}
