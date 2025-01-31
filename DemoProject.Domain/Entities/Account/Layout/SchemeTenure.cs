using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeTenure")]
    public partial class SchemeTenure
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeTenure()
        {
            SchemeTenureMakerCheckers = new HashSet<SchemeTenureMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short MinimumTenure { get; set; }

        public short MultiplesOf { get; set; }

        public short MaximumTenure { get; set; }

        public short DefaultTenure { get; set; }

        public byte TimePeriodUnitPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTenureMakerChecker> SchemeTenureMakerCheckers { get; set; }
    }
}
