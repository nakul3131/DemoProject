using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonCommoditiesAsset")]
    public partial class PersonCommoditiesAsset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonCommoditiesAsset()
        {
            PersonCommoditiesAssetBorrowingDetails = new HashSet<PersonCommoditiesAssetBorrowingDetail>();
            PersonCommoditiesAssetMakerCheckers = new HashSet<PersonCommoditiesAssetMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonCommoditiesAssetId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public decimal GoldOrnaments { get; set; }

        public decimal SilverOrnaments { get; set; }

        public decimal PlatinumOrnaments { get; set; }

        public short NumberOfDiamondsInGoldOrnaments { get; set; }

        public bool HasAnyMortgage { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Person Person { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonCommoditiesAssetBorrowingDetail> PersonCommoditiesAssetBorrowingDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonCommoditiesAssetMakerChecker> PersonCommoditiesAssetMakerCheckers { get; set; }

        }
}
