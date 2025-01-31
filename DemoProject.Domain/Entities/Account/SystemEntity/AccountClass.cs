using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.SystemEntity
{
    [Table("AccountClass")]
    public partial class AccountClass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AccountClass()
        {
            AccountClassTranslations = new HashSet<AccountClassTranslation>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public short PrmKey { get; set; }

        public Guid AccountClassId { get; set; }

        public byte AccountElementPrmKey { get; set; }

        [Required]
        [StringLength(15)]
        public string AccountClassCode { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfAccountClass { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(50)]
        public string GLMask { get; set; }

        public bool EnableBranchWiseGLParameterSetup { get; set; }

        public byte NumberOfGeneralLedgerLimit { get; set; }

        public short ParentPrmKey { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        //public virtual AccountElement AccountElement { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AccountClassTranslation> AccountClassTranslations { get; set; }
    }
}
