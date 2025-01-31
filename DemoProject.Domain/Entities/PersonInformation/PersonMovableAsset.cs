using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonMovableAsset")]
    public partial class PersonMovableAsset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonMovableAsset()
        {
            PersonMovableAssetDocuments = new HashSet<PersonMovableAssetDocument>();
            PersonMovableAssetMakerCheckers = new HashSet<PersonMovableAssetMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        //public Guid PersonMovableAssetId { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short VehicleVariantPrmKey { get; set; }

        public short ManufacturingYear { get; set; }

        public byte NumberOfOwners { get; set; }

        public DateTime RegistrationDate { get; set; }

        [Required]
        [StringLength(15)]
        public string RegistrationNumber { get; set; }

        public DateTime PurchaseDate { get; set; }

        public decimal PurchasePrice { get; set; }

        public decimal CurrentMarketValue { get; set; }

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
        public virtual ICollection<PersonMovableAssetDocument> PersonMovableAssetDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonMovableAssetMakerChecker> PersonMovableAssetMakerCheckers { get; set; }
    }
}
