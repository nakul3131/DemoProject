using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("MaritalStatusTranslation")]
    public partial class MaritalStatusTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid MaritalStatusTranslationId { get; set; }

        public short MaritalStatusPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfMaritalStatus { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual MaritalStatus MaritalStatus { get; set; }

        public virtual Language Language { get; set; }
    }
}
