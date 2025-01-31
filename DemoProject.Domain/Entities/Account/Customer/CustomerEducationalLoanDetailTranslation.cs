using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerEducationalLoanDetailTranslation")]
    public partial class CustomerEducationalLoanDetailTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerEducationalLoanDetailTranslation()
        {
            CustomerEducationalLoanDetailTranslationMakerCheckers = new HashSet<CustomerEducationalLoanDetailTranslationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerEducationalLoanDetailPrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(150)]
        public string TransOtherNameOfInstitute { get; set; }

        [Required]
        [StringLength(200)]
        public string TransOtherInstituteContactDetails { get; set; }

        [Required]
        [StringLength(500)]
        public string TransOtherInstituteAddressDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransOtherFeesDetails { get; set; }

        [Required]
        [StringLength(150)]
        public string TransContactPersonName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransContactPersonContactDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerEducationalLoanDetail CustomerEducationalLoanDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerEducationalLoanDetailTranslationMakerChecker> CustomerEducationalLoanDetailTranslationMakerCheckers { get; set; }
    }
}
