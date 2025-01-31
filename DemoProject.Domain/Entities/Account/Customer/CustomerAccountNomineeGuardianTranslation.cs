using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountNomineeGuardianTranslation")]
    public partial class CustomerAccountNomineeGuardianTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountNomineeGuardianTranslation()
        {
            CustomerAccountNomineeGuardianTranslationMakerCheckers = new HashSet<CustomerAccountNomineeGuardianTranslationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }
        
        public int CustomerAccountNomineeGuardianPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransFullName { get; set; }

        [Required]
        [StringLength(500)]
        public string TransFullAddress { get; set; }

        [Required]
        [StringLength(150)]
        public string TransContactDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccountNomineeGuardian CustomerAccountNomineeGuardian { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountNomineeGuardianTranslationMakerChecker> CustomerAccountNomineeGuardianTranslationMakerCheckers { get; set; }
    }
}
