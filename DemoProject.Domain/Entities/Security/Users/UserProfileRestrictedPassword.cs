using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileRestrictedPassword")]
    public partial class UserProfileRestrictedPassword
    {
        [Key]
        public int PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        [StringLength(150)]
        public string UserPassword { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }
    }
}
