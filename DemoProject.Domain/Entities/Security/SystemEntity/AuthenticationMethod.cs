using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.SystemEntity
{
    [Table("AuthenticationMethod")]
    public partial class AuthenticationMethod
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string AuthenticationMethodID { get; set; }

        [Required]
        [StringLength(50)]
        public string NameForSystem { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfAuthenticationMethod { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOnReport { get; set; }

        public byte AuthenticationMethodCategory { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1)]
        public string RowStatus { get; set; }

        [StringLength(500)]
        public string Note { get; set; }
    }
}
