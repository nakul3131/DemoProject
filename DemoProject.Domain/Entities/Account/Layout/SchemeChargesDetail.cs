using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeChargesDetail")]
    public partial class SchemeChargesDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeChargesDetail()
        {
            SchemeChargesDetailMakerCheckers = new HashSet<SchemeChargesDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public decimal ChargesInPercentage { get; set; }

        public decimal ChargesInAmount { get; set; }

        public decimal MinimumChargesAmount { get; set; }

        public decimal MaximumChargesAmount { get; set; }

        public bool IsTaxable { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeChargesDetailMakerChecker> SchemeChargesDetailMakerCheckers { get; set; }
    }
}
