using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanAccountTranslation")]
    public partial class CustomerLoanAccountTranslation
    {

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerLoanAccountTranslation()
        {
            CustomerLoanAccountTranslationMakerCheckers = new HashSet<CustomerLoanAccountTranslationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransStrengthsFactors { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransWeaknessesFactors { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransOpportunitiesFactors { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransThreatsFactors { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransPastCreditHistory { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransLegalAndRegulatoryCompliance { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerLoanAccount CustomerLoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountTranslationMakerChecker> CustomerLoanAccountTranslationMakerCheckers { get; set; }

    }
}
