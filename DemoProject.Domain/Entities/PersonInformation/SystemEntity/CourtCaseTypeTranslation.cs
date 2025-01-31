using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("CourtCaseTypeTranslation")]
    public partial class CourtCaseTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid CourtCaseTypeTranslationId { get; set; }

        public byte CourtCaseTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfCourtCaseType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAbbreviatedForm { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CourtCaseType CourtCaseType { get; set; }
    }
}
