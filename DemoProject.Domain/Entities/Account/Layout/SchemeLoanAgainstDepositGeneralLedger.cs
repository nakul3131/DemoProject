using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeLoanAgainstDepositGeneralLedger")]
    public partial class SchemeLoanAgainstDepositGeneralLedger
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeLoanAgainstDepositGeneralLedger()
        {
            SchemeLoanAgainstDepositGeneralLedgerMakerCheckers = new HashSet<SchemeLoanAgainstDepositGeneralLedgerMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemeLoanAgainstDepositParameterPrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual SchemeLoanAgainstDepositParameter SchemeLoanAgainstDepositParameter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeLoanAgainstDepositGeneralLedgerMakerChecker> SchemeLoanAgainstDepositGeneralLedgerMakerCheckers { get; set; }

    }
}
