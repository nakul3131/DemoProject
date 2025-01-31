using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanAccountBorrowingDetail")]
    public partial class CustomerLoanAccountBorrowingDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerLoanAccountBorrowingDetail()
        {
            CustomerLoanAccountBorrowingDetailMakerCheckers = new HashSet<CustomerLoanAccountBorrowingDetailMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public long PersonBorrowingDetailPrmKey { get; set; }

        public bool IsVerified { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        public virtual PersonBorrowingDetail PersonBorrowingDetail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountBorrowingDetailMakerChecker> CustomerLoanAccountBorrowingDetailMakerCheckers { get; set; }
    }
}
