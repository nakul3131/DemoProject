using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("OccupationTranslation")]
    public partial class OccupationTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid OccupationTranslationId { get; set; }

        public short OccupationPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfOccupation { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(4000)]
        public string TransNote { get; set; }

        public virtual Occupation Occupation { get; set; }
    }
}
