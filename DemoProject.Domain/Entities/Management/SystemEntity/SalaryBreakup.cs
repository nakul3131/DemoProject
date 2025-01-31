using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.SystemEntity
{
    [Table("SalaryBreakup")]
    public partial class SalaryBreakup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SalaryBreakup()
        {
            SalaryBreakupTranslations = new HashSet<SalaryBreakupTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid SalaryBreakupId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfSalaryBreakup { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public short SequenceNumber { get; set; }

        public bool IsDeductible { get; set; }

        public bool IsTaxable { get; set; }

        public short BasedOnParentBreakupPrmKey { get; set; }

        public decimal AmountInPercentage { get; set; }

        public decimal FixedAmount { get; set; }

        public bool EnableOverridden { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalaryBreakupTranslation> SalaryBreakupTranslations { get; set; }
    }
}
