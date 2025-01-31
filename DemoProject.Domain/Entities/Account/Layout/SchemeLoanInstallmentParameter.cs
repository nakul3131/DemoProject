using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanTransactionParameter")]
    public partial class SchemeLoanInstallmentParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanInstallmentParameter()
        {
            SchemeLoanInstallmentParameterMakerCheckers = new HashSet<SchemeLoanInstallmentParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnablePartialInstallment { get; set; }

        public bool EnablePrePayment { get; set; }

        public bool EnableInstallmentAlteration { get; set; }

        public byte NumberOfOverdueInstallmentRecoveryFromLinkedAccount { get; set; }

        public short MinimumOverDuesInstallment { get; set; }

        public short MaximumOverDuesInstallment { get; set; }

        public short DefaultOverDuesInstallment { get; set; }

        public bool EnableTDSDeductionOfCashTransaction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanInstallmentParameterMakerChecker> SchemeLoanInstallmentParameterMakerCheckers { get; set; }
    }
}
