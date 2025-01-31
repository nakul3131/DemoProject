using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountNomineeTranslation")]
    public partial class CustomerAccountNomineeTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountNomineeTranslation()
        {
            CustomerAccountNomineeTranslationMakerCheckers = new HashSet<CustomerAccountNomineeTranslationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }
        
        public long CustomerAccountNomineePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransNameOfNominee { get; set; }

        [Required]
        [StringLength(500)]
        public string TransFullAddressDetails { get; set; }

        [Required]
        [StringLength(150)]
        public string TransContactDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccountNominee CustomerAccountNominee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountNomineeTranslationMakerChecker> CustomerAccountNomineeTranslationMakerCheckers { get; set; }
    }
}
