using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonFamilyDetailTranslation")]
    public partial class PersonFamilyDetailTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonFamilyDetailTranslation()
        {
            PersonFamilyDetailTranslationMakerCheckers = new HashSet<PersonFamilyDetailTranslationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonFamilyDetailTranslationId { get; set; }

        public long PersonFamilyDetailPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransFullNameOfFamilyMember { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PersonFamilyDetail PersonFamilyDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonFamilyDetailTranslationMakerChecker> PersonFamilyDetailTranslationMakerCheckers { get; set; }
    }
}
