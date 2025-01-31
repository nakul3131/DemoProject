using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeApplicationParameter")]
    public partial class SchemeApplicationParameter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeApplicationParameter()
        {
            SchemeApplicationParameterMakerCheckers = new HashSet<SchemeApplicationParameterMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableApplicationNumberBranchwise { get; set; }

        //[Required]
        [StringLength(25)]
        public string ApplicationNumberMask { get; set; }

        public bool EnableAutoApplicationNumber { get; set; }

        public bool EnableRandomApplicationNumber { get; set; }

        public bool EnableRegenerateUnusedApplicationNumber { get; set; }

        public bool EnableCustomizeApplicationNumber { get; set; }

        public bool EnableDigitalCodeForApplicationNumber { get; set; }

        public int StartApplicationNumber { get; set; }

        public int ApplicationNumberIncrementBy { get; set; }

        public int EndApplicationNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeApplicationParameterMakerChecker> SchemeApplicationParameterMakerCheckers { get; set; }
    }
}
