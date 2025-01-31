using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Layout
{
    [Table("SchemeCustomerAccountNumber")]
    public partial class SchemeCustomerAccountNumber
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SchemeCustomerAccountNumber()
        {
            SchemeCustomerAccountNumberMakerCheckers = new HashSet<SchemeCustomerAccountNumberMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short SchemePrmKey { get; set; }

        public bool EnableAccountNumberBranchwise { get; set; }

        [Required]
        [StringLength(25)]
        public string AccountNumberMask { get; set; }

        public long StartAccountNumber { get; set; }

        public long EndAccountNumber { get; set; }

        public int AccountNumberIncrementBy { get; set; }

        public bool EnableRandomAccountNumber { get; set; }

        public bool EnableAutoAccountNumber { get; set; }

        public bool EnableCustomizeAccountNumber { get; set; }

        public bool EnableReGenerateUnusedAccountNumber { get; set; }

        public bool EnableDigitalCodeForAccountNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual Scheme Scheme { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeCustomerAccountNumberMakerChecker> SchemeCustomerAccountNumberMakerCheckers { get; set; }
    }
}
