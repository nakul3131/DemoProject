using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileAccessibility")]
    public partial class UserProfileAccessibility
    {
        [Key]
        public short PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string LoginVia { get; set; }

        public byte SessionTimeOut { get; set; }

        public short ScreenSaverTheme { get; set; }

        public short ApplicationTheme { get; set; }

        [Required]
        [StringLength(3)]
        public string TokenDeliveryChannel { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public virtual UserProfile UserProfile { get; set; }
    }
}
