using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.MachineLearning
{
    [Table("MLChapterPoint")]
    public partial class MLChapterPoint
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MLChapterPoint()
        {
            MLParameterConfigs = new HashSet<MLParameterConfig>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short MLChapterPrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfPoint { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public virtual MLChapter MLChapter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MLParameterConfig> MLParameterConfigs { get; set; }
    }
}
