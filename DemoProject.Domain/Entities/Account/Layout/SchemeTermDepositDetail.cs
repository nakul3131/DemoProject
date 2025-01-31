using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeTermDepositDetail")]
    public partial class SchemeTermDepositDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeTermDepositDetail()
        {
            SchemeTermDepositDetailMakerCheckers = new HashSet<SchemeTermDepositDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableAutoRolloverOnMaturity { get; set; }

        public bool EnableAutoCloseOnMaturity { get; set; }

        public bool EnableTransferInterestToUnclaimedGeneralLedger { get; set; }

        public bool EnableTransferPrincipalToUnclaimedGeneralLedger { get; set; }

        public bool EnableInterestCalculationFromDepositDate { get; set; }

        public short MinimumGracePeriodForRenewal { get; set; }

        public short MaximumGracePeriodForRenewal { get; set; }

        public short DefaultGracePeriodForRenewal { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTermDepositDetailMakerChecker> SchemeTermDepositDetailMakerCheckers { get; set; }
    }
}
