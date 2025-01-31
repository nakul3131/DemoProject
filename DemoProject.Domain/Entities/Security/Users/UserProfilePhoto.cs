using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfilePhoto")]
    public partial class UserProfilePhoto
    {
        [Key]
        public short PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [Required]
        public byte[] Photo { get; set; }

        [Required]
        [StringLength(50)]
        public string Extension { get; set; }
    }
}
