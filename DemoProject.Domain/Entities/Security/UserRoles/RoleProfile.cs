using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.UserRoles
{
    [Table("RoleProfile")]
    public partial class RoleProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoleProfile()
        {
            RoleProfileBusinessOffices = new HashSet<RoleProfileBusinessOffice>();
            RoleProfileGeneralLedgers = new HashSet<RoleProfileGeneralLedger>();
            RoleProfileMakerCheckers = new HashSet<RoleProfileMakerChecker>();
            RoleProfileMenus = new HashSet<RoleProfileMenu>();
            RoleProfileModifications = new HashSet<RoleProfileModification>();
            RoleProfileSpecialPermissions = new HashSet<RoleProfileSpecialPermission>();
            RoleProfileTransactionLimits = new HashSet<RoleProfileTransactionLimit>();
            RoleProfileTranslations = new HashSet<RoleProfileTranslation>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid RoleProfileId { get; set; }

        [Required]
        [StringLength(20)]
        public string RoleProfileCode { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfRoleProfile { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        public bool IsFixedRole { get; set; }

        public bool IsCentralizeRole { get; set; }

        public bool IsAllowAllAccessForBusinessOffice { get; set; }

        public bool IsAllowAllAccessForGeneralLedger { get; set; }

        public bool IsAllowAllAccessForMenu { get; set; }

        public bool IsAllowAllAccessForSpecialPermission { get; set; }

        public bool IsAllowAllTransactions { get; set; }

        [Required]
        [StringLength(1)]
        public string GeneralLedgerAccessType { get; set; }

        [Required]
        [StringLength(1)]
        public string BusinessOfficeAccessType { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleProfileBusinessOffice> RoleProfileBusinessOffices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleProfileGeneralLedger> RoleProfileGeneralLedgers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleProfileMakerChecker> RoleProfileMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleProfileMenu> RoleProfileMenus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleProfileModification> RoleProfileModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleProfileSpecialPermission> RoleProfileSpecialPermissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleProfileTransactionLimit> RoleProfileTransactionLimits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleProfileTranslation> RoleProfileTranslations { get; set; }
    }
}
