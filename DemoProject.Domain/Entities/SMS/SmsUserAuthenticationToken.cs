using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.SMS
{
    [Table("SmsUserAuthenticationToken")]
    public partial class SmsUserAuthenticationToken
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrmKey { get; set; }

        public int UserAuthenticationTokenPrmKey { get; set; }

        public DateTime SendingDate { get; set; }

        [Required]
        [StringLength(50)]
        public string SMSProviderMessageID { get; set; }

        [Required]
        [StringLength(50)]
        public string SMSProviderClientID { get; set; }

        [Required]
        [StringLength(50)]
        public string DeliveryStatus { get; set; }

        public DateTime? DeliveryDate { get; set; }

    }
}
