using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanRecoveryAction")]
    public partial class SchemeLoanRecoveryAction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanRecoveryAction()
        {
            SchemeLoanRecoveryActionMakerCheckers = new HashSet<SchemeLoanRecoveryActionMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short LoanRecoveryActionPrmKey { get; set; }

        public byte FromDuesInstallment { get; set; }

        public byte ToDuesInstallment { get; set; }


        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanRecoveryActionMakerChecker> SchemeLoanRecoveryActionMakerCheckers { get; set; }
    }
}
