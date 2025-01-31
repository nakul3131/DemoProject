using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("FinancialOrganizationTypeTranslation")]
    public partial class FinancialOrganizationTypeTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public byte FinancialOrganizationTypePrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfFinancialOrganization { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        public virtual FinancialOrganizationType FinancialOrganizationType { get; set; }
    }
}
