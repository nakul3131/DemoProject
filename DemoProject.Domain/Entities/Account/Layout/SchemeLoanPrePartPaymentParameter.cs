using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanPrePartPaymentParameter")]
    public partial class SchemeLoanPrePartPaymentParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanPrePartPaymentParameter()
        {
            SchemePrePartPaymentParameterMakerCheckers = new HashSet<SchemeLoanPrePartPaymentParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        [StringLength(1)]
        public string PrePartPaymentBasedOn { get; set; }

        public byte MinimumRepaymentOfEMIForPrePartPayment { get; set; }

        public byte MinimumMonthForPrePartPayment { get; set; }

        public byte MaximumMonthForPrePartPayment { get; set; }

        public decimal MinimumPrePartPaymentAmount { get; set; }

        public decimal MaximumPrePartPaymentAmount { get; set; }

        public decimal InterestRate { get; set; }

        public byte PrePartPaymentRepetitionLimitInFinancialYear { get; set; }

        public byte PrePartPaymentPenaltyCalculationMethodPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanPrePartPaymentParameterMakerChecker> SchemePrePartPaymentParameterMakerCheckers { get; set; }
    }
}
