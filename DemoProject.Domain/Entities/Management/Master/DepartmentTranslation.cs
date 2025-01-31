using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Management.Master
{
    [Table("DepartmentTranslation")]
    public partial class DepartmentTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DepartmentTranslation()
        {
            DepartmentTranslationMakerCheckers = new HashSet<DepartmentTranslationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short DepartmentPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public short TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfDepartment { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(4000)]
        public string TransObjective { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Department Department { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepartmentTranslationMakerChecker> DepartmentTranslationMakerCheckers { get; set; }
    }
}
