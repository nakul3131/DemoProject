using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("LocalGovernmentTranslation")]
    public partial class LocalGovernmentTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid LocalGovernmentTranslationId { get; set; }

        public short LocalGovernmentPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfLocalGovernment { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual LocalGovernment LocalGovernment { get; set; }

        public virtual Language Language { get; set; }
    }
}
