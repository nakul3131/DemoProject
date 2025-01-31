using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDepositAccountParameter")]
    public partial class SchemeDepositAccountParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDepositAccountParameter()
        {
            SchemeDepositAccountParameterMakerCheckers = new HashSet<SchemeDepositAccountParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string DepositType { get; set; }
      
        public bool IsRequiredOrdinaryMembership { get; set; }

        public bool IsRequiredNominalMembership { get; set; }

        public bool IsRequiredSavingAccount { get; set; }

        public bool EnableOverrideMaturedAmount { get; set; }

        public bool EnableLockinPeriod { get; set; }

        public bool EnableRenewal { get; set; }

        public bool IsAvailablePledgeLoan { get; set; }

        public bool EnableNumberOfTransactionLimit { get; set; }

        public bool EnableTransactionAmountLimit { get; set; }

        public bool EnableBankingChannel { get; set; }

        public bool EnableAgent { get; set; }

        public bool IsUnderCGAS { get; set; }

        public bool IsApplicableForTaxExempt { get; set; }

        public decimal MaximumTaxExemptAmount { get; set; }

        public bool EnableTDSDeductionOnInterest { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositAccountParameterMakerChecker> SchemeDepositAccountParameterMakerCheckers { get; set; }
    }
}
