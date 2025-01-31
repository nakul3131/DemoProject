using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfilePassword")]
    public partial class UserProfilePassword
    {
        [Key]
        public int PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public byte[] UserPassword { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime? ChangeDate { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
    }
}
