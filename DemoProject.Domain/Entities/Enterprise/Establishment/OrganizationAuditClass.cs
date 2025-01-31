using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Establishment
{
    [Table("OrganizationAuditClass")]
    public partial class OrganizationAuditClass
    {
        [Key]
        public byte PrmKey { get; set; }

        public short FinancialYear { get; set; }

        [Required]
        [StringLength(1)]
        public string AuditClass { get; set; }
    }
}
