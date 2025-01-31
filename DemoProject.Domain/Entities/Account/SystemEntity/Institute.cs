using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("Institute")]
    public partial class Institute
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Institute()
        {
            InstituteTranslations = new HashSet<InstituteTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public Guid InstituteId { get; set; }

        [Required]
        [StringLength(150)]
        public string NameOfInstitute { get; set; }

        [Required]
        [StringLength(200)]
        public string ContactDetails { get; set; }
        
        [Required]
        [StringLength(500)]
        public string AddressDetails { get; set; }
        
        public short CityPrmKey { get; set; }

        [Required]
        [StringLength(200)]
        public string WebsiteUrl { get; set; }

        public short UniversityPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InstituteTranslation> InstituteTranslations { get; set; }
    }
}
