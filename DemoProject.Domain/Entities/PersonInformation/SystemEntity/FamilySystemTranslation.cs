using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("FamilySystemTranslation")]
    public partial class FamilySystemTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid FamilySystemTranslationId { get; set; }

        public short FamilySystemPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfFamilySystem { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual FamilySystem FamilySystem { get; set; }

        public virtual Language Language { get; set; }
    }
}
