using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("PersonTypeTranslation")]
    public partial class PersonTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid PersonTypeTranslationId { get; set; }

        public byte PersonTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNameOfPersonType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual PersonType PersonType { get; set; }
    }
}
