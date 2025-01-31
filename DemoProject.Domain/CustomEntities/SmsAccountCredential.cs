using System.ComponentModel.DataAnnotations;

namespace DemoProject.Domain.CustomEntities
{
    public class SmsAccountCredential
    {
        [StringLength(50)]
        public string NameOfProvider { get; set; }

        [StringLength(50)]
        public string ClientCode { get; set; }

        [StringLength(50)]
        public string UserID { get; set; }

        [StringLength(50)]
        public string UserPassword { get; set; }

        [StringLength(50)]
        public string AuthenticationKey { get; set; }

        [StringLength(6)]
        public string SenderId { get; set; }

        [StringLength(50)]
        public string PrincipalEntityId { get; set; }

        [Required]
        [StringLength(50)]
        public string TemplateId { get; set; }
    }
}
