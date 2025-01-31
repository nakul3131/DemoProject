using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("Colour")]
    public partial class Colour
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]

        public Colour()
        {
            ColourTranslations = new HashSet<ColourTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid ColourId { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfColour { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ColourTranslation> ColourTranslations { get; set; }
    }
}
