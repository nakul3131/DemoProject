using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeTranslation")]
    public partial class BusinessOfficeTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeTranslation()
        {
            BusinessOfficeTranslationMakerCheckers = new HashSet<BusinessOfficeTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

      //  public Guid BusinessOfficeTranslationId { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfBusinessOffice { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(500)]
        public string TransContactDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransAddressDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual BusinessOffice BusinessOffice { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeTranslationMakerChecker> BusinessOfficeTranslationMakerCheckers { get; set; }
    }
}
