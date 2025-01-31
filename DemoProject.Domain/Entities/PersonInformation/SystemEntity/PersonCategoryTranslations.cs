using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("PersonCategoryTranslation")]
    public partial class PersonCategoryTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid PersonCategoryTranslationId { get; set; }

        public byte PersonCategoryPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNameOfPersonCategory { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual PersonCategory PersonCategory { get; set; }
}
}
