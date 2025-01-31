using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeTargetGroup")]
    public partial class SchemeTargetGroup
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeTargetGroup()
        {
            SchemeTargetGroupGenders = new HashSet<SchemeTargetGroupGender>();
            SchemeTargetGroupMakerCheckers = new HashSet<SchemeTargetGroupMakerChecker>();
            SchemeTargetGroupOccupations = new HashSet<SchemeTargetGroupOccupation>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public byte TargetGroupPrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string RequiredMembership { get; set; }
        
        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTargetGroupGender> SchemeTargetGroupGenders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTargetGroupMakerChecker> SchemeTargetGroupMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeTargetGroupOccupation> SchemeTargetGroupOccupations { get; set; }
    }
}
