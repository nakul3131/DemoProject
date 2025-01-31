using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeNumberOfTransactionLimit")]
    public partial class SchemeNumberOfTransactionLimit
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeNumberOfTransactionLimit()
        {
            SchemeNumberOfTransactionLimitMakerCheckers = new HashSet<SchemeNumberOfTransactionLimitMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte TransactionTypePrmKey { get; set; }

        public byte MinimumNumberOfTransaction { get; set; }

        public byte MaximumNumberOfTransaction { get; set; }

        public byte TimePeriodUnitPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeNumberOfTransactionLimitMakerChecker> SchemeNumberOfTransactionLimitMakerCheckers { get; set; }
    }
}
