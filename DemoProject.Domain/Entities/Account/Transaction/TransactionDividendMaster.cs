using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("TransactionDividendMaster")]
    public partial class TransactionDividendMaster
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TransactionDividendMaster()
        {
            NetProfitAppropriations = new HashSet<NetProfitAppropriation>();
            TransactionMasters = new HashSet<TransactionMaster>();
            TransactionDividendMasters = new HashSet<TransactionDividendMaster>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int TransactionMasterPrmKey { get; set; }

        public byte NetProfitAppropriationPrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NetProfitAppropriation> NetProfitAppropriations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionMaster> TransactionMasters  { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionDividendMaster> TransactionDividendMasters { get; set; }
    }
}
