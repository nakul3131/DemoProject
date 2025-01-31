namespace DemoProject.Domain.Entities.Account.Transaction
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransactionGSTDetailMakerChecker")]
    public partial class TransactionGSTDetailMakerChecker
    {
        [Key]
        public long PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public long TransactionGSTDetailPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual TransactionGSTDetail TransactionGSTDetail { get; set; }
    }
}
