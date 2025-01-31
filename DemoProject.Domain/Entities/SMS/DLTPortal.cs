using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.SMS
{
    [Table("DLTPortal")]
    public partial class DLTPortal
    {
        [Key]
        public short PrmKey { get; set; }

        [Required]
        public Guid DLTPortalId { get; set; }

        [Required]
        [StringLength(500)]
        public string NameOfPortal { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(500)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(1500)]
        public string PortalAddress { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
