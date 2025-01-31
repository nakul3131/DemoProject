using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Master
{
    [Table("PasswordPolicy")]
    public partial class PasswordPolicy
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PasswordPolicy()
        {
            PasswordPolicyMakerCheckers = new HashSet<PasswordPolicyMakerChecker>();
            PasswordPolicyModifications = new HashSet<PasswordPolicyModification>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid PasswordPolicyId { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfPasswordPolicy { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(50)]
        public string NameOnReport { get; set; }

        public byte MinimumPasswordLength { get; set; }

        public byte MaximumPasswordLength { get; set; }

        public byte MinimumNumberOfUpperCaseCharacters { get; set; }

        public byte MinimumNumberOfLowerCaseCharacters { get; set; }

        public byte MinimumNumberOfSpecialCaseCharacters { get; set; }

        public byte MinimumNumberOfNumericCharacters { get; set; }

        public byte MinimumNumberOfRepetitiveCharacters { get; set; }

        public byte ForcePasswordChangeAfterDays { get; set; }

        public short ReusePreviousPassword { get; set; }

        public short MinimumDaysForReusePreviousPassword { get; set; }

        public byte PasswordExpiryAlertDays { get; set; }

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
        public virtual ICollection<PasswordPolicyMakerChecker> PasswordPolicyMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PasswordPolicyModification> PasswordPolicyModifications { get; set; }
    }
}
