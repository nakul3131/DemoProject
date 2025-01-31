using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.MachineLearning
{
    [Table("MLChapter")]
    public partial class MLChapter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MLChapter()
        {
            MLChapterPoints = new HashSet<MLChapterPoint>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short MLSubjcetPrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfChapter { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        public virtual MLSubject MLSubject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MLChapterPoint> MLChapterPoints { get; set; }
    }
}
