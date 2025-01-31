using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.SystemEntity
{
    [Table("CoopSocietyClass")]
    public partial class CoopSocietyClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CoopSocietyClass()
        {
            CoopSocietyClassTranslations = new HashSet<CoopSocietyClassTranslation>();
            CoopSocietySubClasses = new HashSet<CoopSocietySubClass>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid SocietyClassId { get; set; }

        [Required]
        [StringLength(10)]
        public string SequenceNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfClass { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoopSocietyClassTranslation> CoopSocietyClassTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CoopSocietySubClass> CoopSocietySubClasses { get; set; }
    }
}
