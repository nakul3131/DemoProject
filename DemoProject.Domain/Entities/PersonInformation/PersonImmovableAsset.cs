using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonImmovableAsset")]
    public partial class PersonImmovableAsset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonImmovableAsset()
        {
            PersonImmovableAssetDocuments = new HashSet<PersonImmovableAssetDocument>();
            PersonImmovableAssetMakerCheckers = new HashSet<PersonImmovableAssetMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonImmovableAssetId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(4500)]
        public string AssetFullDescription { get; set; }

        [Required]
        [StringLength(50)]
        public string SurveyNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string CitySurveyNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string OtherNumber { get; set; }

        public decimal AreaOfLand { get; set; }

        public bool IsConstructed { get; set; }

        public decimal ConstructionArea { get; set; }

        public decimal CarpetArea { get; set; }

        public decimal CurrentMarketValue { get; set; }

        public decimal AnnualRentIncome { get; set; }

        public byte ResidenceTypePrmKey { get; set; }

        public byte OwnershipTypePrmKey { get; set; }

        public decimal OwnershipPercentage { get; set; }

        public bool HasAnyMortgage { get; set; }

        public bool IsOwnershipDeceased { get; set; }

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
        public virtual ICollection<PersonImmovableAssetDocument> PersonImmovableAssetDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonImmovableAssetMakerChecker> PersonImmovableAssetMakerCheckers { get; set; }
    }
}
