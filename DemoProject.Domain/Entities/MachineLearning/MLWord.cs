using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.MachineLearning
{
    [Table("MLWord")]
    public partial class MLWord
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MLWord()
        {
            MLWordDefinations = new HashSet<MLWordDefination>();
            MLWordTranslations = new HashSet<MLWordTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }

        [Required]
        [StringLength(500)]
        public string Word { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MLWordDefination> MLWordDefinations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MLWordTranslation> MLWordTranslations { get; set; }
    }
}
