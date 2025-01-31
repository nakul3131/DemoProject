using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanChargesParameter")]
    public partial class SchemeLoanChargesParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanChargesParameter()
        {
            SchemeLoanChargesParameterMakerCheckers = new HashSet<SchemeLoanChargesParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte ChargesTypePrmKey { get; set; }

        public byte LendingChargesBasePrmKey { get; set; }

        public decimal ChargesPercentage { get; set; }

        public decimal MinimumCharges { get; set; }

        public decimal MaximumCharges { get; set; }

        public decimal DefaultCharges { get; set; }

        public bool IsApplicableTax { get; set; }

        public bool IsOptional { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanChargesParameterMakerChecker> SchemeLoanChargesParameterMakerCheckers { get; set; }
    }
}
