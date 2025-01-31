using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.UserRoles
{
    [Table("RoleProfileModification")]
    public partial class RoleProfileModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RoleProfileModification()
        {
            RoleProfileModificationMakerCheckers = new HashSet<RoleProfileModificationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short RoleProfilePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

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
        
        [Required]
        [StringLength(1)]
        public string BusinessOfficeAccessType { get; set; }

        [Required]
        [StringLength(1)]
        public string GeneralLedgerAccessType { get; set; }
        
        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual RoleProfile RoleProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RoleProfileModificationMakerChecker> RoleProfileModificationMakerCheckers { get; set; }
    }
}
