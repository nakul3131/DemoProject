using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.PersonInformation.SystemEntity
{
    [Table("WorldWideTimeZone")]
    public partial class WorldWideTimeZone
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid WorldWideTimeZoneId { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfTimeZone { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(20)]
        public string UTCOffset { get; set; }

        [Required]
        [StringLength(20)]
        public string GMTOffset { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
