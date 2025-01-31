using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonBorrowingDetailTranslation")]
    public partial class PersonBorrowingDetailTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonBorrowingDetailTranslation()
        {
            PersonBorrowingDetailTranslationMakerCheckers = new HashSet<PersonBorrowingDetailTranslationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonBorrowingDetailTranslationId { get; set; }

        public long PersonBorrowingDetailPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransNameOfOrganization { get; set; }

        [Required]
        [StringLength(50)]
        public string TransBranch { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransLoanDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransMortgageDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PersonBorrowingDetail PersonBorrowingDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonBorrowingDetailTranslationMakerChecker> PersonBorrowingDetailTranslationMakerCheckers { get; set; }
    }
}
