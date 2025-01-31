using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeTransactionAmountLimit")]
    public partial class SchemeTransactionAmountLimit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeTransactionAmountLimit()
        {
            SchemeTransactionAmountLimitMakerCheckers = new HashSet<SchemeTransactionAmountLimitMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte TransactionTypePrmKey { get; set; }

        public decimal MinimumAmountLimit { get; set; }

        public decimal MaximumAmountLimit { get; set; }

        public bool IsForInitialTransaction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTransactionAmountLimitMakerChecker> SchemeTransactionAmountLimitMakerCheckers { get; set; }
    }
}
