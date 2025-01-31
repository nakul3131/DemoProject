using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("FixedAssetItem")]
    public partial class FixedAssetItem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FixedAssetItem()
        {
            FixedAssetItemMakerCheckers = new HashSet<FixedAssetItemMakerChecker>();
            FixedAssetItemModifications = new HashSet<FixedAssetItemModification>();
            FixedAssetItemTranslations = new HashSet<FixedAssetItemTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid FixedAssetItemId { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOfItem { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOnReport { get; set; }

        public bool IsTangibleAsset { get; set; }

        public bool IsTaxable { get; set; }

        public short HSN_SACCode { get; set; }

        public decimal IGST { get; set; }

        public decimal CGST { get; set; }

        public decimal SGST { get; set; }

        public decimal Cess { get; set; }

        public bool IsEligibleForITC { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FixedAssetItemMakerChecker> FixedAssetItemMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FixedAssetItemModification> FixedAssetItemModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FixedAssetItemTranslation> FixedAssetItemTranslations { get; set; }
    }
}
