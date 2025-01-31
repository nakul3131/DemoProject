using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanSettlementAccountParameter")]
    public partial class SchemeLoanSettlementAccountParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanSettlementAccountParameter()
        {
            SchemeLoanSettlementAccountParameterMakerCheckers = new HashSet<SchemeLoanSettlementAccountParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableAutoSetSettlementAccountsOnCreation { get; set; }

        public bool EnableAutoCreateSettlementAccount { get; set; }

        [Required]
        [StringLength(3)]
        public string SettlementType { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanSettlementAccountParameterMakerChecker> SchemeLoanSettlementAccountParameterMakerCheckers { get; set; }
    }
}
