using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeTargetGroupOccupation")]
    public partial class SchemeTargetGroupOccupation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeTargetGroupOccupation()
        {
            SchemeTargetGroupOccupationMakerCheckers = new HashSet<SchemeTargetGroupOccupationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemeTargetGroupPrmKey { get; set; }

        public short OccupationPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual SchemeTargetGroup SchemeTargetGroup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTargetGroupOccupationMakerChecker> SchemeTargetGroupOccupationMakerCheckers { get; set; }
    }
}
