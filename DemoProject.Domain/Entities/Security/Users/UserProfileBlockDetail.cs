using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileBlockDetail")]
    public partial class UserProfileBlockDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfileBlockDetail()
        {
            UserProfileBlockDetailMakerCheckers = new HashSet<UserProfileBlockDetailMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public DateTime? BlockDateFrom { get; set; }

        public DateTime? BlockDateTo { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }
    
        public virtual UserProfile UserProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileBlockDetailMakerChecker> UserProfileBlockDetailMakerCheckers { get; set; }
    }
}
