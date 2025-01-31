using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanSanctionAuthority")]
    public partial class SchemeLoanSanctionAuthority
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanSanctionAuthority()
        {
            SchemeLoanSanctionAuthorityMakerCheckers = new HashSet<SchemeLoanSanctionAuthorityMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public decimal ManagerEmpoweredSanctionLoanAmountFrom { get; set; }

        public decimal ManagerEmpoweredSanctionLoanAmountTo { get; set; }

        public decimal CommitteeEmpoweredSanctionLoanAmountFrom { get; set; }

        public decimal CommitteeEmpoweredSanctionLoanAmountTo { get; set; }

        public decimal BoardOfDirectorEmpoweredSanctionLoanAmountFrom { get; set; }

        public decimal BoardOfDirectorEmpoweredSanctionLoanAmountTo { get; set; }

        public decimal CEOEmpoweredSanctionLoanAmountFrom { get; set; }

        public decimal CEOEmpoweredSanctionLoanAmountTo { get; set; }

        public decimal ChairmanEmpoweredSanctionLoanAmountFrom { get; set; }

        public decimal ChairmanEmpoweredSanctionLoanAmountTo { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanSanctionAuthorityMakerChecker> SchemeLoanSanctionAuthorityMakerCheckers { get; set; }

    }
}
