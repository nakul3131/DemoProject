using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("VehicleVariant")]
    public partial class VehicleVariant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehicleVariant()
        {
            VehicleVariantMakerCheckers = new HashSet<VehicleVariantMakerChecker>();
            VehicleVariantModifications = new HashSet<VehicleVariantModification>();
            VehicleVariantTranslations = new HashSet<VehicleVariantTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid VehicleVariantId { get; set; }

        public short VehicleModelPrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfVariant { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }
        
        [Required]
        [StringLength(3)]
        public string EngineType { get; set; }

        public short EngineCapacity { get; set; }

        [Required]
        [StringLength(3)]
        public string Transmission { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(4000)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        public virtual VehicleModel VehicleModel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleVariantMakerChecker> VehicleVariantMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleVariantModification> VehicleVariantModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleVariantTranslation> VehicleVariantTranslations { get; set; }
    }
}
