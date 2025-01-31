﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InterestCalculationFrequencyTranslation")]
    public partial class InterestCalculationFrequencyTranslation
    {
        [Key]
        public byte PrmKey { get; set; }

        public byte InterestCalculationFrequencyPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string TransNameOfFrequency { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransNote { get; set; }

        public virtual InterestCompoundingFrequency InterestCompoundingFrequency { get; set; }
    }
}
