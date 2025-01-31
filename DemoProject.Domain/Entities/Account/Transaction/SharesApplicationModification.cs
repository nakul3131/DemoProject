using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("SharesApplicationModification")]
    public partial class SharesApplicationModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SharesApplicationModification()
        {
            SharesApplicationModificationMakerCheckers = new HashSet<SharesApplicationModificationMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public int SharesApplicationPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime ApplicationAllotmentDate { get; set; }

        public DateTime ApplicationSubmitDate { get; set; }

        public bool HasOtherSocietyMembership { get; set; }

        [Required]
        [StringLength(500)]
        public string BankDetails { get; set; }

        public decimal TransactionAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string UniqueTransactionNumber { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual SharesApplication SharesApplication { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SharesApplicationModificationMakerChecker> SharesApplicationModificationMakerCheckers { get; set; }
    }
}
