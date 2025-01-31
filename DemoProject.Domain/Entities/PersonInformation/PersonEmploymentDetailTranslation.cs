using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonEmploymentDetailTranslation")]
    public partial class PersonEmploymentDetailTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonEmploymentDetailTranslation()
        {
            PersonEmploymentDetailTranslationMakerCheckers = new HashSet<PersonEmploymentDetailTranslationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonEmploymentDetailPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransNameOfEmployer { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransEmployerNatureOtherDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransEmployerAddressDetails { get; set; }

        [Required]
        [StringLength(500)]
        public string TransEmployerContactDetails { get; set; }

        [Required]
        [StringLength(50)]
        public string TransEPFNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PersonEmploymentDetail PersonEmploymentDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonEmploymentDetailTranslationMakerChecker> PersonEmploymentDetailTranslationMakerCheckers { get; set; }
    }
}
