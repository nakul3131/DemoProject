using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("EducationQualificationTranslation")]
    public partial class EducationQualificationTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid EducationQualificationTranslationId { get; set; }

        public short EducationQualificationPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfQualification { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual EducationQualification EducationQualification { get; set; }
    }
}
