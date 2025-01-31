using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeEstimateTarget")]
    public partial class SchemeEstimateTarget
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeEstimateTarget()
        {
            SchemeEstimateTargetMakerCheckers = new HashSet<SchemeEstimateTargetMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public decimal NumberOfAccount { get; set; }

        [Required]
        [StringLength(1)]
        public string NumberOfAccountUnit { get; set; }

        public decimal TurnOverAmount { get; set; }

        [Required]
        [StringLength(1)]
        public string TurnOverUnit { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeEstimateTargetMakerChecker> SchemeEstimateTargetMakerCheckers { get; set; }
    }
}
