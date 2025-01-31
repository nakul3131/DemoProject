using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("EducationLevelTranslation")]
    public partial class EducationLevelTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid EducationLevelTranslationId { get; set; }

        public short EducationLevelPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfEducationLevel { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual EducationLevel EducationLevel { get; set; }

        public virtual Language Language { get; set; }
    }
}
