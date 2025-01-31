using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.SystemEntity
{
    [Table("AuthenticationFactorTranslation")]
    public partial class AuthenticationFactorTranslation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuthenticationFactorTranslation()
        {
            
        }

        [Key]
        public short PrmKey { get; set; }

        public byte AuthenticationFactorPrmKey { get; set; }

        public short LanguagePrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfAuthenticationFactor { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(500)]
        public string Note { get; set; }

        public virtual AuthenticationFactor AuthenticationFactor { get; set; }

        
    }
}
