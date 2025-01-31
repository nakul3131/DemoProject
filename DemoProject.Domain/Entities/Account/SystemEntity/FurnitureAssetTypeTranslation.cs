using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("FurnitureAssetTypeTranslation")]
    public partial class FurnitureAssetTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short FurnitureAssetTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfFurnitureAssetType { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual FurnitureAssetType FurnitureAssetType { get; set; }
    }
}
