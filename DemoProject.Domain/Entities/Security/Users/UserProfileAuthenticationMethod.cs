using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileAuthenticationMethod")]
    public partial class UserProfileAuthenticationMethod
    {
        [Key]
        public int PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public byte AuthenticationMethodPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1)]
        public string RowStatus { get; set; }

        [Required]
        [StringLength(1)]
        public string ActivationStatus { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
    }
}
