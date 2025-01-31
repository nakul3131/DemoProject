using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonTranslation")]
    public partial class PersonTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonTranslation()
        {
            PersonTranslationMakerCheckers = new HashSet<PersonTranslationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonTranslationId { get; set; }

        public long PersonPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransFirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string TransMiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string TransLastName { get; set; }

        [Required]
        [StringLength(150)]
        public string TransFullName { get; set; }

        [Required]
        [StringLength(50)]
        public string TransMotherName { get; set; }

        [Required]
        [StringLength(50)]
        public string TransMothersMaidenName { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonTranslationMakerChecker> PersonTranslationMakerCheckers { get; set; }
    }
}
