using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("FinancialAssetTypeTranslation")]
    public partial class FinancialAssetTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public byte FinancialAssetTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(250)]
        public string TransNameOfFinancialAsset { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(250)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual FinancialAssetType FinancialAssetType { get; set; }
    }
}
