using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDepositInstallmentParameter")]
    public partial class SchemeDepositInstallmentParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDepositInstallmentParameter()
        {
            SchemeDepositInstallmentParameterMakerCheckers = new HashSet<SchemeDepositInstallmentParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte MinimumInstallment { get; set; }

        public byte InstallmentMultipleOf { get; set; }

        public byte MaximumInstallment { get; set; }

        public bool IsAllowPartialInstallment { get; set; }

        public bool IsAllowAdvanceInstallment { get; set; }

        public bool EnableInstallmentAlteration { get; set; }

        public short DuesInstallmentForDefault { get; set; }

        public bool EnableGracePeriodForDuesInstallment { get; set; }

        public byte NumberOfOverdueInstallmentRecoveryFromLinkedAccount { get; set; }

        public bool EnableIPenaltyInterestOnOverdues { get; set; }

        public decimal FixedPenaltyAmount { get; set; }

        public decimal PenaltyAmountPerHunderd { get; set; }

        public short DuesInstallmentForInactivityOfAccount { get; set; }

        public short RevivePeriodForInactivityAccount { get; set; }

        public bool EnableAutoClosureOfInactivityOfAccount { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositInstallmentParameterMakerChecker> SchemeDepositInstallmentParameterMakerCheckers { get; set; }
    }
}
