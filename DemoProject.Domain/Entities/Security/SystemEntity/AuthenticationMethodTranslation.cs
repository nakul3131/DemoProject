using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.SystemEntity
{
    [Table("AuthenticationMethodTranslation")]
    public partial class AuthenticationMethodTranslation
    {
        [Key]
        public short PrmKey { get; set; }

        public byte AuthenticationMethodPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfAuthenticationMethod { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(500)]
        public string Note { get; set; }

    }
}
