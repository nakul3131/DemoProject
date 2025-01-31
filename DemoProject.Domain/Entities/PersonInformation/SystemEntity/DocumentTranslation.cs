using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("DocumentTranslation")]
    public partial class DocumentTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid DocumentTranslationId { get; set; }

        public short DocumentPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfDocument { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        public virtual Document Document { get; set; }
    }
}
