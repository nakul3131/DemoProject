using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("OutwardTransactionDetail")]
    public partial class OutwardTransactionDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OutwardTransactionDetail()
        {
            OutwardTransactionDetailMakerCheckers = new HashSet<OutwardTransactionDetailMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public Guid OutwardTransactionDetailId { get; set; }

        public int OutwardTransactionPrmKey { get; set; }

        public short InwardOutwardTypePrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public int DocumentWriterEmployeePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual OutwardTransaction OutwardTransaction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutwardTransactionDetailMakerChecker> OutwardTransactionDetailMakerCheckers { get; set; }
    }
}
