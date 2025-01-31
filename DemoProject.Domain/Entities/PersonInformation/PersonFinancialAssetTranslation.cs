using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonFinancialAssetTranslation")]
    public partial class PersonFinancialAssetTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonFinancialAssetTranslation()
        {
            PersonFinancialAssetTranslationMakerCheckers = new HashSet<PersonFinancialAssetTranslationMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonFinancialAssetTranslationId { get; set; }

        public long PersonFinancialAssetPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(150)]
        public string TransNameOfFinancialOrganization { get; set; }

        [Required]
        [StringLength(50)]
        public string TransNameOfBranch { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransAddressDetails { get; set; }

        [Required]
        [StringLength(500)]
        public string TransContactDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransFinancialAssetDescription { get; set; }

        [Required]
        [StringLength(500)]
        public string TransReferenceNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PersonFinancialAsset PersonFinancialAsset { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonFinancialAssetTranslationMakerChecker> PersonFinancialAssetTranslationMakerCheckers { get; set; }
    }
}
