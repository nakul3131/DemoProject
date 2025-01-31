using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanArrearParameter")]
    public partial class SchemeLoanArrearParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanArrearParameter()
        {
            SchemeLoanArrearParameterMakerCheckers = new HashSet<SchemeLoanArrearParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte ArrearsToleranceMinimumPeriod { get; set; }

        public byte ArrearsToleranceMaximumPeriod { get; set; }

        public byte ArrearsToleranceDefaultPeriod { get; set; }

        [Required]
        [StringLength(3)]
        public string ArrearsDaysCalculatedFrom { get; set; }

        public bool IsExcludeNonWorkingDay { get; set; }

        public byte LockArrearsAccountAfterDays { get; set; }

        public decimal CapChargesLimit { get; set; }

        [Required]
        [StringLength(3)]
        public string CapChargesLimitOn { get; set; }

        [Required]
        [StringLength(3)]
        public string CapType { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanArrearParameterMakerChecker> SchemeLoanArrearParameterMakerCheckers { get; set; }
    }
}
