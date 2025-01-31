using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.MachineLearning
{
    [Table("MLWordDefination")]
    public partial class MLWordDefination
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MLWordDefination()
        {
            MLWordDefinationTranslations = new HashSet<MLWordDefinationTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int MLWordPrmKey { get; set; }

        [Required]
        [StringLength(4000)]
        public string Defination { get; set; }

        public virtual MLWord Dictionary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MLWordDefinationTranslation> MLWordDefinationTranslations { get; set; }
    }
}
