using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("InterestRateChargedDuration")]
    public partial class InterestRateChargedDuration
    {
        [Key]
        public byte PrmKey { get; set; }

        public Guid InterestRateChargedDurationId { get; set; }

        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
