using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeBusinessLoan")]
   public partial class SchemeBusinessLoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeBusinessLoan()
        {
            SchemeBusinessLoanMakerCheckers = new HashSet<SchemeBusinessLoanMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public decimal MinimumTurnOverAmount { get; set; }

        public byte CurrentBusinessMinimumAge { get; set; }

        public byte MinimumBusinessExperience { get; set; }

        public byte CapturePreviousProfitMakingYears { get; set; }

        public decimal MinimumAnnualIncome { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeBusinessLoanMakerChecker> SchemeBusinessLoanMakerCheckers { get; set; }

    }
}
