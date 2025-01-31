using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("CastCategoryTranslation")]
    public partial class CastCategoryTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid CastCategoryTranslationId { get; set; }

        public short CastCategoryPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfCastCategory { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual CastCategory CastCategory { get; set; }
    }
}
