using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanFunderParameter")]
    public partial class SchemeLoanFunderParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanFunderParameter()
        {
            SchemeLoanFunderParameterMakerCheckers = new HashSet<SchemeLoanFunderParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public decimal FunderLoanFundingPercentage { get; set; }

        public decimal FunderInterestCommissions { get; set; }

        public bool EnableLockFundsOnFundingAccountAtApproval { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanFunderParameterMakerChecker> SchemeLoanFunderParameterMakerCheckers { get; set; }
    }
}
