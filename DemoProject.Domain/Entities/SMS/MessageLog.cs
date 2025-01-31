using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.SMS
{
    [Table("MessageLog")]
    public partial class MessageLog
    {
        [Key]
        public long PrmKey { get; set; }

        [Required]
        [StringLength(20)]
        public string SystemReferenceNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string GateWayReferenceNumber { get; set; }

        [Required]
        [StringLength(10)]
        public string MobileNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Message { get; set; }

        public DateTime SentDateTime { get; set; }

        public DateTime? DeliveryDateTime { get; set; }

        [Required]
        [StringLength(30)]
        public string DeliveryCode { get; set; }

        [Required]
        [StringLength(50)]
        public string DeliveryStatus { get; set; }

        [Required]
        [StringLength(20)]
        public string SenderId { get; set; }
    }
}
