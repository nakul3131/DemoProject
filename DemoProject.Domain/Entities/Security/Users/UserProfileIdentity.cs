using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileIdentity")]
    public partial class UserProfileIdentity
    {
        [Key]
        public int PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
    }
}
