using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Management.Servant;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerGoldLoanReappraisal")]
    public partial class CustomerGoldLoanReappraisal
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerGoldLoanReappraisal()
        {
            CustomerGoldLoanReappraisalMakerCheckers = new HashSet<CustomerGoldLoanReappraisalMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int EmployeePrmKey { get; set; }

        public bool HasAnyDamage { get; set; }

        [Required]
        [StringLength(1500)]
        public string DamageDescription { get; set; }

        public bool IsSealedProperly { get; set; }

        public decimal MetalNetWeight { get; set; }

        public decimal MetalGrossWeight { get; set; }

        public bool IsWithDiamond { get; set; }

        public byte NumberOfDiamond { get; set; }

        public decimal DiamondCarat { get; set; }

        [Required]
        [StringLength(150)]
        public string ClarityColour { get; set; }

        public decimal DiamondWeight { get; set; }

        [Required]
        [StringLength(1500)]
        public string AutoIrregularityNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerLoanAccount CustomerLoanAccount { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerGoldLoanReappraisalMakerChecker> CustomerGoldLoanReappraisalMakerCheckers { get; set; }
    }
}
