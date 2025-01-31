using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserAuthenticationToken")]
    public partial class UserAuthenticationToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public byte TokenFor { get; set; }

        [Required]
        [MaxLength(168)]
        public byte[] MobileOTP { get; set; }

        [Required]
        [MaxLength(168)]
        public byte[] EmailVCode { get; set; }

        [Required]
        [StringLength(3)]
        public string TokenStatus { get; set; }

        public DateTime TokenGeneratedOn { get; set; }

        public DateTime TokenExpiredOn { get; set; }

        public bool IsSmsGenerated { get; set; }

        public bool IsEmailGenerated { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
