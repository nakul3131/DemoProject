using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.Account.SystemEntity;
using DevExpress.XtraPrinting.Native.Lines;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeInstitute")]
    public partial class SchemeInstitute
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeInstitute()
        {
            SchemeInstituteMakerCheckers = new HashSet<SchemeInstituteMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public short InstitutePrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
        public virtual Scheme Scheme { get; set; }
        public virtual Institute Institute { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeInstituteMakerChecker> SchemeInstituteMakerCheckers { get; set; }
    }
}
