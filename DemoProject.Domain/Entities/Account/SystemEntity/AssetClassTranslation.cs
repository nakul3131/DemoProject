using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Configuration;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("AssetClassTranslation")]
    public partial class AssetClassTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short AssetClassPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfAssetClass { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual AssetClass AssetClass { get; set; }

        public virtual Language Language { get; set; }
    }
}
