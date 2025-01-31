using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("EducationalCourseTranslation")]
    public partial class EducationalCourseTranslation
    {
        [Key]
        public short PrmKey { get; set; }
        public short EducationalCoursePrmKey { get; set; }
        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(150)]
        public string TransNameOfCourse { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }
        public virtual EducationalCourse EducationalCourse { get; set; }
    }
}
