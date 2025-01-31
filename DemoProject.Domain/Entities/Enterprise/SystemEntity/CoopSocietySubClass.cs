using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("CoopSocietySubClass")]
    public partial class CoopSocietySubClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CoopSocietySubClass()
        {
            CoopSocietySubClassTranslations = new HashSet<CoopSocietySubClassTranslation>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid SocietySubClassId { get; set; }

        public byte CoopSocietyClassPrmKey { get; set; }

        [Required]
        [StringLength(10)]
        public string SequenceNumber { get; set; }

        [Required]
        [StringLength(500)]
        public string NameOfSubClass { get; set; }

        [Required]
        [StringLength(4000)]
        public string Examples { get; set; }

        public virtual CoopSocietyClass CoopSocietyClass { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoopSocietySubClassTranslation> CoopSocietySubClassTranslations { get; set; }
    }
}
