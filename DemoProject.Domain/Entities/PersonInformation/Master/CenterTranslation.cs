using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.Master
{
    [Table("CenterTranslation")]
    public partial class CenterTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CenterTranslation()
        {
            CenterTranslationMakerCheckers = new HashSet<CenterTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid CenterTranslationId { get; set; }

        public short CenterPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfCenter { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(50)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(4000)]
        public string TransNote { get; set; }
        
        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Center Center { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CenterTranslationMakerChecker> CenterTranslationMakerCheckers { get; set; }
    }
}
