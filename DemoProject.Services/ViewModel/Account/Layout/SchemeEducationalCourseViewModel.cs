using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeEducationalCourseViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short EducationalCoursePrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeEducationalCourseMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeEducationalCoursePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other Field For DropDoown

        [StringLength(150)]
        public string NameOfCourse { get; set; }

        public Guid EducationalCourseId { get; set; }

        public Guid MultiEducationalCourseId { get; set; }
    }
}
