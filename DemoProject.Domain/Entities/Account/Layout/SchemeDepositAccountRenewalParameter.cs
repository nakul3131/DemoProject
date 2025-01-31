using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeDepositAccountRenewalParameter")]
    public partial class SchemeDepositAccountRenewalParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDepositAccountRenewalParameter()
        {
            SchemeDepositAccountRenewalParameterMakerCheckers = new HashSet<SchemeDepositAccountRenewalParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte MaximumRenewalDurationAfterMaturityInDays { get; set; }

        public bool EnableRenewalOnHoldiay { get; set; }

        public bool EnableRenewalOnSameDayOfAnyMonth { get; set; }

        public bool EnableAutoRenewal { get; set; }

        public bool EnableAutoRenewalOnHoldiay { get; set; }

        public byte MinimumDurationForAutoRenewal { get; set; }

        public byte MaximumDurationForAutoRenewal { get; set; }

        public byte DefaultDurationForAutoRenewal { get; set; }

        [Required]
        [StringLength(3)]
        public string AccountNumberOnRenewal { get; set; }

        [Required]
        [StringLength(3)]
        public string CertificateNumberOnRenewal { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositAccountRenewalParameterMakerChecker> SchemeDepositAccountRenewalParameterMakerCheckers { get; set; }
    }
}
