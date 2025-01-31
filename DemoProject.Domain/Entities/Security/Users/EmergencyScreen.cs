using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("EmergencyScreen")]
    public  class EmergencyScreen
    {
        [Key]
        public byte PrmKey { get; set; }

        [Required]
        [StringLength(1000)]
        public string HeaderText { get; set; }

        [Required]
        [StringLength(4000)]
        public string BodyText { get; set; }

        [Required]
        [StringLength(2000)]
        public string FooterText { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

    }
}

