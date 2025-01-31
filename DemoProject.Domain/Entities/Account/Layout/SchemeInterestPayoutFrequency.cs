using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeInterestPayoutFrequency")]
    public partial class SchemeInterestPayoutFrequency
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeInterestPayoutFrequency()
        {
            SchemeInterestPayoutFrequencyMakerCheckers = new HashSet<SchemeInterestPayoutFrequencyMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }
        
        public short SchemePrmKey { get; set; }

        public short FrequencyPrmKey { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeInterestPayoutFrequencyMakerChecker> SchemeInterestPayoutFrequencyMakerCheckers { get; set; }
    }
}
