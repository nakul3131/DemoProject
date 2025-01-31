using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.SystemEntity
{
    [Table("AuthenticationFactor")]
    public partial class AuthenticationFactor
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AuthenticationFactor()
        {            
            AuthenticationFactorTranslations = new HashSet<AuthenticationFactorTranslation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string AuthenticationFactorID { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfAuthenticationFactor { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public byte SecurityLevelPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1)]
        public string RowStatus { get; set; }

        [Required]
        [StringLength(500)]
        public string Note { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AuthenticationFactorTranslation> AuthenticationFactorTranslations { get; set; }
    }
}
