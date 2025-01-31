using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanDistributorParameter")]
    public partial class SchemeLoanDistributorParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanDistributorParameter()
        {
            SchemeLoanDistributorParameterMakerCheckers = new HashSet<SchemeLoanDistributorParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool EnableAdvance { get; set; }

        public decimal MinimumAdvanceLimit { get; set; }

        public decimal MaximumAdvanceLimit { get; set; }

        public bool EnableAdvanceDeductionOnDisbursement { get; set; }

        public bool EnableDistributorInterestRate { get; set; }

        public decimal MinimumDistributorInterestRate { get; set; }

        public decimal MaximumDistributorInterestRate { get; set; }

        public decimal DefaultDistributorInterestRate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanDistributorParameterMakerChecker> SchemeLoanDistributorParameterMakerCheckers { get; set; }
    }
}
