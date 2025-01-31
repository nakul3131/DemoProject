using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonCommoditiesAssetBorrowingDetail")]
    public partial class PersonCommoditiesAssetBorrowingDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonCommoditiesAssetBorrowingDetail()
        {
            PersonCommoditiesAssetBorrowingDetailMakerCheckers = new HashSet<PersonCommoditiesAssetBorrowingDetailMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonCommoditiesAssetBorrowingDetailId { get; set; }

        public long PersonCommoditiesAssetPrmKey { get; set; }

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

        public virtual PersonCommoditiesAsset PersonCommoditiesAsset { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonCommoditiesAssetBorrowingDetailMakerChecker> PersonCommoditiesAssetBorrowingDetailMakerCheckers { get; set; }
    }
}
