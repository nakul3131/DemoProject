using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("GuardianPersonTranslation")]
    public partial class GuardianPersonTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GuardianPersonTranslation()
        {
            GuardianPersonTranslationMakerCheckers = new HashSet<GuardianPersonTranslationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long GuardianPersonPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransGuardianFullName { get; set; }

        [Required]
        [StringLength(500)]
        public string TransFullAddress { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual GuardianPerson GuardianPerson { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GuardianPersonTranslationMakerChecker> GuardianPersonTranslationMakerCheckers { get; set; }
    }
}
