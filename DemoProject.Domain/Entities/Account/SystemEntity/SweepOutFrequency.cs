using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("SweepOutFrequency")]
    public partial class SweepOutFrequency
    {
        [Key]
        public byte PrmKey { get; set; }

        public Guid SweepOutFrequencyId { get; set; }

        [Required]
        [StringLength(10)]
        public string SysNameOfFrequency { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfFrequency { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

    }
}
