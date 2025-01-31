using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonGroupAuthorizedSignatoryTranslation")]
    public partial class PersonGroupAuthorizedSignatoryTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonGroupAuthorizedSignatoryTranslation()
        {
            PersonGroupAuthorizedSignatoryTranslationMakerCheckers = new HashSet<PersonGroupAuthorizedSignatoryTranslationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonGroupAuthorizedSignatoryPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransFullNameOfAuthorizedPerson { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransAuthorizedPersonAddressDetail { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransAuthorizedPersonContactDetail { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PersonGroupAuthorizedSignatory PersonGroupAuthorizedSignatory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonGroupAuthorizedSignatoryTranslationMakerChecker> PersonGroupAuthorizedSignatoryTranslationMakerCheckers { get; set; }
    }
}
