using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.GL;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeClosingCharges")]
    public partial class SchemeClosingCharges
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeClosingCharges()
        {
            SchemeClosingChargesMakerCheckers = new HashSet<SchemeClosingChargesMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public short FromTimePeriodInDays { get; set; }

        public short ToTimePeriodInDays { get; set; }

        public bool IsTimePeriodForBeforeClosure { get; set; }

        public decimal MinimumChargesAmount { get; set; }

        public decimal MaximumChargesAmount { get; set; }

        public bool IsTaxable { get; set; }

        public bool IsApplicableOnDeath { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual GeneralLedger GeneralLedger { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeClosingChargesMakerChecker> SchemeClosingChargesMakerCheckers { get; set; }
    }
}
