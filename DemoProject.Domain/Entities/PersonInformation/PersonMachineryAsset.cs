using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation
{
    [Table("PersonMachineryAsset")]
    public partial class PersonMachineryAsset
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonMachineryAsset()
        {
            PersonMachineryAssetDocuments = new HashSet<PersonMachineryAssetDocument>();
            PersonMachineryAssetMakerCheckers = new HashSet<PersonMachineryAssetMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long PersonPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string NameOfMachinery { get; set; }

        [Required]
        [StringLength(1500)]
        public string MachineryFullDetails { get; set; }

        public short ManufacturingYear { get; set; }

        public byte NumberOfOwners { get; set; }

        [Required]
        [StringLength(50)]
        public string ReferenceNumber { get; set; }

        public DateTime DateOfPurchase { get; set; }

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
        public virtual ICollection<PersonMachineryAssetDocument> PersonMachineryAssetDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonMachineryAssetMakerChecker> PersonMachineryAssetMakerCheckers { get; set; }
    }
}
