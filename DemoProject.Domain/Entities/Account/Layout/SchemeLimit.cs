using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{

    [Table("SchemeLimit")]
    public partial class SchemeLimit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLimit()
        {
            SchemeLimitMakerCheckers = new HashSet<SchemeLimitMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public decimal CashDepositLimit { get; set; }

        public decimal CashWithdrawalLimit { get; set; }

        public decimal RetailAccountTurnOverLimit { get; set; }

        public decimal CorporateAccountTurnOverLimit { get; set; }

        public decimal RetailHoldingAmountProportionToTotalAmount { get; set; }

        public decimal CorporateHoldingAmountProportionToTotalAmount { get; set; }

        public decimal TurnOverLimit { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLimitMakerChecker> SchemeLimitMakerCheckers { get; set; }
    }
}
