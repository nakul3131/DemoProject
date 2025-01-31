﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("BankTranslation")]
    public partial class BankTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public short BankPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOfBank { get; set; }

        [Required]
        [StringLength(10)]
        public string TransAliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [Required]
        [StringLength(20)]
        public string TransRBILicenseNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransAddressDetail { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string TransReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    }
}
