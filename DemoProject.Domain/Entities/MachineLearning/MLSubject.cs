using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.MachineLearning
{
    [Table("MLSubject")]
    public partial class MLSubject
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MLSubject()
        {
            MLChapters = new HashSet<MLChapter>();
        }

        [Key]
        public short PrmKey { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfSubject { get; set; }

        [Required]
        [StringLength(1500)]
        public string Remark { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MLChapter> MLChapters { get; set; }
    }
}
