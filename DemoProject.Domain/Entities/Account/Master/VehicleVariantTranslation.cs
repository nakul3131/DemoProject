using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("VehicleVariantTranslation")]
    public partial class VehicleVariantTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VehicleVariantTranslation()
        {
            VehicleVariantTranslationMakerCheckers = new HashSet<VehicleVariantTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short VehicleVariantPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfVariant { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(4000)]
        public string TransNote { get; set; }
        
        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual VehicleVariant VehicleVariant { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VehicleVariantTranslationMakerChecker> VehicleVariantTranslationMakerCheckers { get; set; }
    }
}
