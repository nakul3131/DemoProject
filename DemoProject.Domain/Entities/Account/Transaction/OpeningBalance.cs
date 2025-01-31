using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("OpeningBalance")]
    public partial class OpeningBalance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OpeningBalance()
        {
            OpeningBalanceMakerCheckers = new HashSet<OpeningBalanceMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerAccountPrmKey { get; set; }

        public decimal Amount { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpeningBalanceMakerChecker> OpeningBalanceMakerCheckers { get; set; }
    }
}
