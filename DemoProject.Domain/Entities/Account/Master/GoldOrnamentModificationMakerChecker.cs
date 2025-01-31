﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Master
{
    [Table("GoldOrnamentModificationMakerChecker")]
    public partial class GoldOrnamentModificationMakerChecker
    {
        [Key]
        public short PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public short GoldOrnamentModificationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual GoldOrnamentModification GoldOrnamentModification { get; set; }
    }
}
