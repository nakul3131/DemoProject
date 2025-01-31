using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("SharesCessationTransaction")]
    public partial class SharesCessationTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SharesCessationTransaction()
        {
            SharesCessationTransactionMakerCheckers = new HashSet<SharesCessationTransactionMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long TransactionCustomerAccountPrmKey { get; set; }

        public int MinuteOfMeetingAgendaPrmKey { get; set; }

        public decimal SharesFaceValue { get; set; }

        public short NumberOfSharesCessation { get; set; }

        [Required]
        [StringLength(500)]
        public string CeasedSharesCertificateNumbers { get; set; }

        [Required]
        [StringLength(3)]
        public string CessionReason { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual TransactionCustomerAccount TransactionCustomerAccount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SharesCessationTransactionMakerChecker> SharesCessationTransactionMakerCheckers { get; set; }
    }
}
