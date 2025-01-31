using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("EducationalCourse")]
    public partial class EducationalCourse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EducationalCourse()
        {
            EducationalCourseTranslations = new HashSet<EducationalCourseTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid EducationalCourseId { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOfCourse { get; set; }

        [Required]
        [StringLength(3)]
        public string CourseType { get; set; }

        public byte DurationInMonth { get; set; }

        public bool IsGranted { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EducationalCourseTranslation> EducationalCourseTranslations { get; set; }
    }
}
