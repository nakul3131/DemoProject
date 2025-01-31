using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Establishment
{
    [Table("AuthorizedSharesCapitalMakerChecker")]
    public partial class AuthorizedSharesCapitalMakerChecker
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short PrmKey { get; set; }

        public DateTime EntryDateTime { get; set; }

        public byte AuthorizedSharesCapitalPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string UserAction { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual AuthorizedSharesCapital AuthorizedSharesCapital { get; set; }
    }
}
