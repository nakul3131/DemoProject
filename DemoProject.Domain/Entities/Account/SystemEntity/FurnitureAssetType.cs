using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("FurnitureAssetType")]
    public partial class FurnitureAssetType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FurnitureAssetType()
        {
            FurnitureAssetTypeTranslations = new HashSet<FurnitureAssetTypeTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid FurnitureAssetTypeId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfFurnitureAssetType { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FurnitureAssetTypeTranslation> FurnitureAssetTypeTranslations { get; set; }
    }
}
