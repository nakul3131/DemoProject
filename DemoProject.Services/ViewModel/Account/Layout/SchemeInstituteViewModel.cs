using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Account.Layout
{
    public class SchemeInstituteViewModel
    {
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short InstitutePrmKey { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        //SchemeInstituteMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short SchemeInstitutePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // Other Field For DropDoown
        
        [StringLength(150)]
        public string NameOfInstitute { get; set; }

        public Guid InstituteId { get; set; }

        public Guid MultiInstituteId { get; set; }
    }
}
