using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonAdditionalDetailTranslation")]
    public partial class PersonAdditionalDetailTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonAdditionalDetailTranslation()
        {
            PersonAdditionalDetailTranslationMakerCheckers = new HashSet<PersonAdditionalDetailTranslationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonAdditionalDetailTranslationId { get; set; }

        public long PersonAdditionalDetailPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransLifePartnerName { get; set; }

        [Required]
        [StringLength(50)]
        public string TransLifePartnerMaidenName { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransPoliticialBackgroundDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransVIPBackgroundDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PersonAdditionalDetail PersonAdditionalDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonAdditionalDetailTranslationMakerChecker> PersonAdditionalDetailTranslationMakerCheckers { get; set; }
    }
}
