using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanPreFullPaymentParameter")]
    public partial class SchemeLoanPreFullPaymentParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanPreFullPaymentParameter()
        {
            SchemePreFullPaymentParameterMakerCheckers = new HashSet<SchemeLoanPreFullPaymentParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte MinimumRepaymentOfEMIForPreFullPayment { get; set; }

        public byte MinimumMonth { get; set; }

        public byte MaximumMonth { get; set; }

        public decimal InterestRate { get; set; }

        public byte PreFullPaymentPenaltyCalculationMethodPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanPreFullPaymentParameterMakerChecker> SchemePreFullPaymentParameterMakerCheckers { get; set; }
    }
}
