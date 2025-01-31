using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfileModification")]
    public partial class UserProfileModification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfileModification()
        {
            UserProfileModificationMakerCheckers = new HashSet<UserProfileModificationMakerChecker>();
        }

        [Key]
        public short PrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOfUserProfile { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(320)]
        public string EmailId { get; set; }

        public bool IsEmailIdConfirmed { get; set; }

        [Required]
        [StringLength(320)]
        public string AlternateEmailId { get; set; }

        public bool IsAlternateEmailIdConfirmed { get; set; }

        [Required]
        [StringLength(10)]
        public string MobileNumber { get; set; }

        public bool IsMobileNumberConfirmed { get; set; }

        [Required]
        [StringLength(10)]
        public string AlternateMobileNumber { get; set; }

        public bool IsAlternateMobileNumberConfirmed { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileModificationMakerChecker> UserProfileModificationMakerCheckers { get; set; }
    }
}
