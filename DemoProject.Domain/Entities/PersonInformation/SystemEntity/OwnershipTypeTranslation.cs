using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("OwnershipTypeTranslation")]
    public partial class OwnershipTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid OwnershipTypeTranslationId { get; set; }

        public byte OwnershipTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfOwnershipType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual OwnershipType OwnershipType { get; set; }
    }
}
