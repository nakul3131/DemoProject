using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Transaction
{
    [Table("SharesApplication")]
    public partial class SharesApplication
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SharesApplication()
        {
            SharesApplicationDetails = new HashSet<SharesApplicationDetail>();
            SharesApplicationMakerCheckers = new HashSet<SharesApplicationMakerChecker>();
            SharesApplicationModifications = new HashSet<SharesApplicationModification>();
            SharesApplicationTranslations = new HashSet<SharesApplicationTranslation>();
        }

        [Key]
        public int PrmKey { get; set; }

        public DateTime ApplicationAllotmentDate { get; set; }

        public DateTime ApplicationSubmitDate { get; set; }

        public long ApplicationNumber { get; set; }

        public bool HasOtherSocietyMembership { get; set; }

        [Required]
        [StringLength(150)]
        public string WitnessName { get; set; }

        [Required]
        [StringLength(500)]
        public string BankDetails { get; set; }

        public decimal TransactionAmount { get; set; }

        [Required]
        [StringLength(50)]
        public string UniqueTransactionNumber { get; set; }

        [Required]
        [StringLength(3)]
        public string ApplicationStatus { get; set; }

        [Required]
        [StringLength(1500)]
        public string StatusReason { get; set; }

        public bool IsModified { get; set; }

        public bool IsAccountOpened { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SharesApplicationDetail> SharesApplicationDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SharesApplicationMakerChecker> SharesApplicationMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SharesApplicationModification> SharesApplicationModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SharesApplicationTranslation> SharesApplicationTranslations { get; set; }
    }
}
