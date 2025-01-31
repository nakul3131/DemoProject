using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("SharesCapitalTransaction")]
    public partial class SharesCapitalTransaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SharesCapitalTransaction()
        {
            SharesCapitalTransactionMakerCheckers = new HashSet<SharesCapitalTransactionMakerChecker>();
        }

        [Key]
        public long PrmKey { get; set; }

        public long TransactionCustomerAccountPrmKey { get; set; }

        public decimal SharesFaceValue { get; set; }

        public short NumberOfShares { get; set; }

        public int StartSharesCertificateNumber { get; set; }

        public int EndSharesCertificateNumber { get; set; }

        public bool IsPrinted { get; set; }

        public bool IsSharesCertificateIssued { get; set; }

        public bool IsReturned { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual TransactionCustomerAccount TransactionCustomerAccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SharesCapitalTransactionMakerChecker> SharesCapitalTransactionMakerCheckers { get; set; }
    }
}
