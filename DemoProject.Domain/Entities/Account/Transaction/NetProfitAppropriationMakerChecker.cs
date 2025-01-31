using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("NetProfitAppropriationMakerChecker")]
    public partial class NetProfitAppropriationMakerChecker
    {
        [Key]
        public byte PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public byte NetProfitAppropriationPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual NetProfitAppropriation NetProfitAppropriation { get; set; }
    }
}
