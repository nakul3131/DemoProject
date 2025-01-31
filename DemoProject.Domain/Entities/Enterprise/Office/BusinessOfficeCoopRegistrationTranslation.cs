using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeCoopRegistrationTranslation")]
    public partial class BusinessOfficeCoopRegistrationTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeCoopRegistrationTranslation()
        {
            BusinessOfficeCoopRegistrationTranslationMakerCheckers = new HashSet<BusinessOfficeCoopRegistrationTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

      
        public short BusinessOfficeCoopRegistrationPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransRegistrationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransReferenceNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransAlphaNumericCode { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual BusinessOfficeCoopRegistration BusinessOfficeCoopRegistration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeCoopRegistrationTranslationMakerChecker> BusinessOfficeCoopRegistrationTranslationMakerCheckers { get; set; }
    }
}
