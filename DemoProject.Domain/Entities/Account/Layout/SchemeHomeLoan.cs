using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeHomeLoan")]
    public partial class SchemeHomeLoan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeHomeLoan()
        {
            SchemeHomeLoanMakerCheckers = new HashSet<SchemeHomeLoanMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableMultipleDisbursement { get; set; }

        public byte MaximumNumberOfTimeDisbursement { get; set; }

        public short MinimumMoratoriumPeriod { get; set; }

        public short MaximumMoratoriumPeriod { get; set; }

        public bool IsMoratoriumForBoth { get; set; }

        public byte MinimumLTVRatio { get; set; }

        public byte MaximumLTVRatio { get; set; }

        [Required]
        [StringLength(1)]
        public string CollateralInsurance { get; set; }

        [Required]
        [StringLength(1500)]
        public string LocatedAreaRemark { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeHomeLoanMakerChecker> SchemeHomeLoanMakerCheckers { get; set; }
    }
}
