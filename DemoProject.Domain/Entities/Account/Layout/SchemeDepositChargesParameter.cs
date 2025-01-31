using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Domain.Entities.Account.Layout
{
    public class SchemeDepositChargesParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeDepositChargesParameter()
        {
            SchemeDepositChargesParameterMakerChecker = new HashSet<SchemeDepositChargesParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public bool IsApplicableTax { get; set; }

        public byte ChargesApplyingTypePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public bool IsOptional { get; set; }

        public byte LendingChargesBasePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeDepositChargesParameterMakerChecker> SchemeDepositChargesParameterMakerChecker { get; set; }
    }
}
