using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccountChequeBookRequestDetail")]
    public partial class CustomerAccountChequeBookRequestDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccountChequeBookRequestDetail()
        {
            CustomerAccountChequeBookRequestDetailMakerCheckers = new HashSet<CustomerAccountChequeBookRequestDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public DateTime RequestDate { get; set; }

        [Required]
        [StringLength(50)]
        public string RequestReferenceNumber { get; set; }

        public DateTime IssueDate { get; set; }

        [Required]
        [StringLength(50)]
        public string IssueReferenceNumber { get; set; }

        public DateTime DeliveryDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountChequeBookRequestDetailMakerChecker> CustomerAccountChequeBookRequestDetailMakerCheckers { get; set; }
    }
}
