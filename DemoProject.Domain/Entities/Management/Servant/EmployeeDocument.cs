using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Servant
{
    [Table("EmployeeDocument")]
    public partial class EmployeeDocument
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeDocument()
        {
            EmployeeDocumentMakerCheckers = new HashSet<EmployeeDocumentMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int EmployeePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public short DocumentPrmKey { get; set; }

        [Required]
        public byte[] DocumentCopy { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfFile { get; set; }

        [Required]
        [StringLength(1500)]
        public string StoragePath { get; set; }

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
        public virtual ICollection<EmployeeDocumentMakerChecker> EmployeeDocumentMakerCheckers { get; set; }
    }
}
