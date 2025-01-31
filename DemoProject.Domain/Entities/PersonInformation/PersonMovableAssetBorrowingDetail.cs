using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonMovableAssetBorrowingDetail")]
    public partial class PersonMovableAssetBorrowingDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonMovableAssetBorrowingDetail()
        {
            PersonMovableAssetBorrowingDetailMakerCheckers = new HashSet<PersonMovableAssetBorrowingDetailMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonMovableAssetBorrowingDetailId { get; set; }

        public long PersonMovableAssetPrmKey { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonMovableAssetBorrowingDetailMakerChecker> PersonMovableAssetBorrowingDetailMakerCheckers { get; set; }
    }
}
