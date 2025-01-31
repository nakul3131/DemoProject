using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileGeneralLedger")]
    public partial class UserProfileGeneralLedger
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfileGeneralLedger()
        {
           UserProfileGeneralLedgerMakerCheckers = new HashSet<UserProfileGeneralLedgerMakerChecker>();
        }

        [Key]
        public int PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public short GeneralLedgerPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

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

        [Required]
        [StringLength(3)]
        public string ActivationStatus { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileGeneralLedgerMakerChecker> UserProfileGeneralLedgerMakerCheckers { get; set; }
    }
}
