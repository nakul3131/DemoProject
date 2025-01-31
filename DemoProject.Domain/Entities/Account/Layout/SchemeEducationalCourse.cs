using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.SystemEntity;
using DevExpress.XtraPrinting.Native.Lines;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeEducationalCourse")]
    public partial class SchemeEducationalCourse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeEducationalCourse()
        {
            SchemeEducationalCourseMakerCheckers = new HashSet<SchemeEducationalCourseMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short EducationalCoursePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        public virtual EducationalCourse EducationalCourse { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeEducationalCourseMakerChecker> SchemeEducationalCourseMakerCheckers { get; set; }
    }
}
