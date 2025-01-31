using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Configuration
{
    [Table("CBSAPIValue")]
    public partial class CBSAPIValue
    {
        [Key]
        public short PrmKey { get; set; }

        public Guid CBSAPIValueId { get; set; }

        [Required]
        [StringLength(6)]
        public string CBSAPIValueSysCode { get; set; }

        [Required]
        [StringLength(50)]
        public string APITitle { get; set; }

        [Required]
        [StringLength(1500)]
        public string HeaderValue { get; set; }

        [Required]
        [StringLength(1500)]
        public string CBSAPIChecksum { get; set; }

        [Required]
        [StringLength(1500)]
        public string CBSAPIRequestValue { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    }
}
