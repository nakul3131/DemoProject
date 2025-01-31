using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDepositAgentParameter")]
    public partial class SchemeDepositAgentParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDepositAgentParameter()
        {
            SchemeDepositAgentParameterMakerCheckers = new HashSet<SchemeDepositAgentParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short AgentCommissionGeneralLedgerPrmKey { get; set; }

        public bool EnableCommissionOnOverDuesInstallment { get; set; }

        public byte MinimumOverDuesInstallment { get; set; }

        public byte MaximumOverDuesInstallment { get; set; }

        public byte DefaultOverDuesInstallment { get; set; }

        public bool EnableCommissionOnAdditionalInvestment { get; set; }

        public bool EnableCommissionOnAdvancePayment { get; set; }

        public byte MinimumAdditionalInstallment { get; set; }

        public byte MaximumAdditionalInstallment { get; set; }

        public decimal AgentCommissionPercentage { get; set; }

        public bool IsRequiredSecurity { get; set; }

        public decimal MinimumSecurityAmount { get; set; }

        public decimal MaximumSecurityAmount { get; set; }

        public decimal DefaultSecurityAmount { get; set; }

        public short CollectionMarginOverSecurity { get; set; }

        [Required]
        [StringLength(1)]
        public string AgentCollectionSettlementThrough { get; set; }

        public bool EnableCommisionDeductableOnForeclosure { get; set; }

        public bool EnableOverrideCommisionDeductionOnForeclosure { get; set; }

        public bool EnableCommisionDeductableOnForeclosureInLockInPeriod { get; set; }

        public bool EnableOverrideCommisionDeductionOnForeclosureInLockInPeriod { get; set; }

        public bool EnableDeceasedAccountInterestRate { get; set; }

        public bool EnableAgentCommisionDeductableOfDeceasedAccount { get; set; }

        public bool EnableOverrideCommisionDeductionOfDeceasedAccount { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositAgentParameterMakerChecker> SchemeDepositAgentParameterMakerCheckers { get; set; }
    }
}
