﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction 
{
    [Table("SharesCessationTransactionMakerChecker")]
    public partial class SharesCessationTransactionMakerChecker
    {
        [Key]
        public long PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public long SharesCessationTransactionPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual SharesCessationTransaction SharesCessationTransaction { get; set; }
    }
}
