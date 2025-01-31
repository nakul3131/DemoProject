using DemoProject.Domain.Entities.Account.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("InwardTransactionDetail")]
    public partial class InwardTransactionDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InwardTransactionDetail()
        {
          InwardTransactionDetailMakerCheckers = new HashSet<InwardTransactionDetailMakerChecker>();
        }
        [Key]
        public int PrmKey { get; set; }

        public Guid InwardTransactionDetailId { get; set; }

        public int InwardTransactionPrmKey { get; set; }

        public short InwardOutwardTypePrmKey { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public int RecipientEmployeePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    
        public virtual InwardOutwardType InwardOutwardType { get; set; }

        public virtual InwardTransaction InwardTransaction { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InwardTransactionDetailMakerChecker> InwardTransactionDetailMakerCheckers { get; set; }
    }
}
