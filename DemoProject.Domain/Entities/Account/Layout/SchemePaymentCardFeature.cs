using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemePaymentCardFeature")]
    public partial class SchemePaymentCardFeature
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemePaymentCardFeature()
        {
            SchemePaymentCardFeatureMakerCheckers = new HashSet<SchemePaymentCardFeatureMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short PaymentCardPrmKey { get; set; }

        public byte ChargesDuration { get; set; }

        [Required]
        [StringLength(1)]
        public string ChargesDurationUnit { get; set; }

        public decimal ChargesAmount { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemePaymentCardFeatureMakerChecker> SchemePaymentCardFeatureMakerCheckers { get; set; }
    }
}
