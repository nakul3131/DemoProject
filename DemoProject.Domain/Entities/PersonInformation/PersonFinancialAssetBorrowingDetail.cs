using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonFinancialAssetBorrowingDetail")]
    public partial class PersonFinancialAssetBorrowingDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonFinancialAssetBorrowingDetail()
        {
            PersonFinancialAssetBorrowingDetailMakerCheckers = new HashSet<PersonFinancialAssetBorrowingDetailMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonFinancialAssetBorrowingDetailId { get; set; }

        public long PersonFinancialAssetPrmKey { get; set; }

        public long PersonBorrowingDetailPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual PersonFinancialAsset PersonFinancialAsset { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonFinancialAssetBorrowingDetailMakerChecker> PersonFinancialAssetBorrowingDetailMakerCheckers { get; set; }
    }
}
