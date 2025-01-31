using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOfficeRBIRegistrationTranslation")]
    public partial class BusinessOfficeRBIRegistrationTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOfficeRBIRegistrationTranslation()
        {
            BusinessOfficeRBIRegistrationTranslationMakerCheckers = new HashSet<BusinessOfficeRBIRegistrationTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

     //   public Guid BusinessOfficeRBIRegistrationTranslationId { get; set; }

        public short BusinessOfficeRBIRegistrationPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransReferenceNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransLicenseNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string TransAlphaNumericSWIFTAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string TransAlphaNumericTelexAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string TransBusinessOfficeUniqueIdentityNumberForATM { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual BusinessOfficeRBIRegistration BusinessOfficeRBIRegistration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeRBIRegistrationTranslationMakerChecker> BusinessOfficeRBIRegistrationTranslationMakerCheckers { get; set; }
    }
}
