using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("NumberInWord")]
    public partial class NumberInWord
    {
        [Key]
        public byte PrmKey { get; set; }

        public Guid NumberInWordId { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(2500)]
        public string HundredDigitNumberWords { get; set; }

        [Required]
        [StringLength(500)]
        public string HigherDigitNumberWords { get; set; }

        [Required]
        [StringLength(500)]
        public string NumberSeparatorWords { get; set; }
    }
}
