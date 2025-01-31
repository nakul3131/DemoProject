using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanAcquaintanceDetail")]
    public partial class CustomerLoanAcquaintanceDetail
    {
        public CustomerLoanAcquaintanceDetail()
        {
            CustomerLoanAcquaintanceDetailMakerCheckers = new HashSet<CustomerLoanAcquaintanceDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int CustomerLoanAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public long PersonInformationNumber { get; set; }

        public byte RelationPrmKey { get; set; }

        public byte SequenceNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerLoanAccount CustomerLoanAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAcquaintanceDetailMakerChecker> CustomerLoanAcquaintanceDetailMakerCheckers { get; set; }
    }
}
