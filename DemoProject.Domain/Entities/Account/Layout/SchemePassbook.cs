using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemePassbook")]
    public partial class SchemePassbook
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemePassbook()
        {
            SchemePassbookMakerCheckers = new HashSet<SchemePassbookMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableAutoPassbookNumber { get; set; }

        [Required]
        [StringLength(25)]
        public string PassbookNumberMask { get; set; }

        public bool EnablePassbookNumberBranchwise { get; set; }

        public bool EnableCustomizePassbookNumber { get; set; }

        public bool IsRandomlyGeneratedPassbookNumber { get; set; }

        public bool ReGenerateUnusedPassbookNumber { get; set; }

        public int StartPassbookNumber { get; set; }

        public int EndPassbookNumber { get; set; }

        public int PassbookNumberIncrementBy { get; set; }

        public bool EnableDigitalCodeForPassbookNumber { get; set; }

        public bool IsVisiblePassbookNumber { get; set; }

        public bool EnablePassbookVerification { get; set; }

        public bool DuplicatePassbookCharges { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemePassbookMakerChecker> SchemePassbookMakerCheckers { get; set; }
    }
}
