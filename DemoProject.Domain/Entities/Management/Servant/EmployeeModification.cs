using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Servant
{
    [Table("EmployeeModification")]
    public partial class EmployeeModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeModification()
        {
            EmployeeModificationMakerCheckers = new HashSet<EmployeeModificationMakerChecker>();
        }
        [Key]
        public int PrmKey { get; set; }

        public int EmployeePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(30)]
        public string EmployeeCode { get; set; }

        [Required]
        [StringLength(50)]
        public string ExternalEmployeeId1 { get; set; }

        [Required]
        [StringLength(50)]
        public string ExternalEmployeeId2 { get; set; }

        [Required]
        [StringLength(3)]
        public string EmployeeCategory { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EmployeeModificationMakerChecker> EmployeeModificationMakerCheckers { get; set; }
    }
}
