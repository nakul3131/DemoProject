using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("AccountOperationMode")]
    public partial class AccountOperationMode
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountOperationMode()
        {
            AccountOperationModeTranslations = new HashSet<AccountOperationModeTranslation>();
        }

        [Key]
        public byte PrmKey { get; set; }

        public Guid AccountOperationModeId { get; set; }

        [Required]
        [StringLength(100)]
        public string SysNameOfAccountOperationMode { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfAccountOperationMode { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountOperationModeTranslation> AccountOperationModeTranslations { get; set; }
    }
}
