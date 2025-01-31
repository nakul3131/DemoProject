using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeFixedDepositParameter")]
    public partial class SchemeFixedDepositParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeFixedDepositParameter()
        {
            SchemeFixedDepositParameterMakerCheckers = new HashSet<SchemeFixedDepositParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public decimal MinimumDepositAmount { get; set; }

        public decimal DepositMultipleOfThereAfter { get; set; }

        public decimal MaximumDepositAmount { get; set; }

        public bool EnableAdditionalDeposit { get; set; }

        public bool EnablePartialWithdrawl { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeFixedDepositParameterMakerChecker> SchemeFixedDepositParameterMakerCheckers { get; set; }
    }
}
