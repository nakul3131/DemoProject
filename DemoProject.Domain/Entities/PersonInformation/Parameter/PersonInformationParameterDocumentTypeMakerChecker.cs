using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.Parameter
{
    [Table("PersonInformationParameterDocumentTypeMakerChecker")]
    public partial class PersonInformationParameterDocumentTypeMakerChecker
    {
        [Key]
        public long PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public byte PersonInformationParameterDocumentTypePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual PersonInformationParameterDocumentType PersonInformationParameterDocumentType { get; set; }
    }
}
