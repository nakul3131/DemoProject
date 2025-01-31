using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.SMS
{
    [Table("TeleVerificationToken")]
    public partial class TeleVerificationToken
    {
        [Key]
        public int PrmKey { get; set; }

        [Required]
        [StringLength(30)]
        public string TeleType { get; set; }

        [Required]
        [StringLength(50)]
        public string TeleNumber { get; set; }

        [Required]
        [MaxLength(168)]
        public byte[] Token { get; set; }

        public DateTime TokenGeneratedOn { get; set; }

        public DateTime TokenExpiredOn { get; set; }

        public bool IsGenerated { get; set; }

        [Required]
        [StringLength(3)]
        public string TokenStatus { get; set; }
    }
}
