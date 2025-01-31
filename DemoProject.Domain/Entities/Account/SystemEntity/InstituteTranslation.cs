using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InstituteTranslation")]
    public partial class InstituteTranslation
    {

        [Key]
        public short PrmKey { get; set; }
        public short InstitutePrmKey { get; set; }
        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(150)]
        public string TransNameOfInstitute { get; set; }

        [Required]
        [StringLength(200)]
        public string TransContactDetails { get; set; }

        [Required]
        [StringLength(500)]
        public string TransAddressDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }
        public virtual Institute Institute { get; set; }
    }
}
