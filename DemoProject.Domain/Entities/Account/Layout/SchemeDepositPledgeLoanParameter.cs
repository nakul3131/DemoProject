using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDepositPledgeLoanParameter")]
    public partial class SchemeDepositPledgeLoanParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDepositPledgeLoanParameter()
        {
            SchemeDepositPledgeLoanParameterMakerCheckers = new HashSet<SchemeDepositPledgeLoanParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short PledgeLoanPeriodAfterOpening { get; set; }

        public short PledgeLoanPeriodBeforeMaturity { get; set; }

        public decimal PledgeLoanMarginPercentage { get; set; }

        public bool IsTakenAsCollateralSecurity { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositPledgeLoanParameterMakerChecker> SchemeDepositPledgeLoanParameterMakerCheckers { get; set; }
    }
}
