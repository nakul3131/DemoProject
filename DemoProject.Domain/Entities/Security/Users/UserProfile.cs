using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Security.Users
{
    [Table("UserProfile")]
    public partial class UserProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfile()
        {
             UserProfileMakerCheckers = new HashSet<UserProfileMakerChecker>();
             UserProfileModifications = new HashSet<UserProfileModification>();
             UserProfileAccessibilities = new HashSet<UserProfileAccessibility>();
             UserProfileAuthenticationMethods = new HashSet<UserProfileAuthenticationMethod>();
             UserProfileBlockDetails = new HashSet<UserProfileBlockDetail>();
             UserProfileBusinessOffices = new HashSet<UserProfileBusinessOffice>();
             UserProfileCurrencies = new HashSet<UserProfileCurrency>();
             UserProfileGeneralLedgers = new HashSet<UserProfileGeneralLedger>();
             UserProfileGroups = new HashSet<UserProfileGroup>();
             UserProfileHomeBusinessOffices = new HashSet<UserProfileHomeBusinessOffice>();
             UserProfileIdentitys = new HashSet<UserProfileIdentity>();
             UserProfileLoginDevices = new HashSet<UserProfileLoginDevice>();
             UserProfileMenus = new HashSet<UserProfileMenu>();
             UserProfilePasswords = new HashSet<UserProfilePassword>();
             UserProfilePasswordPolicies = new HashSet<UserProfilePasswordPolicy>();
             UserProfileRestrictedPasswords = new HashSet<UserProfileRestrictedPassword>();
             UserProfileSpecialPermissions = new HashSet<UserProfileSpecialPermission>();
             UserProfileTransactionLimits = new HashSet<UserProfileTransactionLimit>();
             UserRoleProfiles = new HashSet<UserRoleProfile>();
        }
        [Key]
        public short PrmKey { get; set; }

        public Guid UserProfileId { get; set; }

        public byte UserTypePrmKey { get; set; }

        public long PersonPrmKey { get; set; }

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

        public DateTime LastLoginDate { get; set; }

        public DateTime LastActivityDate { get; set; }

        public DateTime LastPasswordChangeDate { get; set; }

        public DateTime LastLockedDate { get; set; }

        public byte InvalidSuccessiveAttemptCount { get; set; }

        public short InvalidCumulativeAttemptCount { get; set; }

        public DateTime ActivationDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string UserProfileStatus { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileMakerChecker> UserProfileMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileModification> UserProfileModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileAccessibility> UserProfileAccessibilities { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileAuthenticationMethod> UserProfileAuthenticationMethods { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileBlockDetail> UserProfileBlockDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileBusinessOffice> UserProfileBusinessOffices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileCurrency> UserProfileCurrencies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileGeneralLedger> UserProfileGeneralLedgers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileGroup> UserProfileGroups { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileHomeBusinessOffice> UserProfileHomeBusinessOffices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileIdentity> UserProfileIdentitys { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileLoginDevice> UserProfileLoginDevices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileMenu> UserProfileMenus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfilePassword> UserProfilePasswords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfilePasswordPolicy> UserProfilePasswordPolicies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileRestrictedPassword> UserProfileRestrictedPasswords { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileSpecialPermission> UserProfileSpecialPermissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserProfileTransactionLimit> UserProfileTransactionLimits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoleProfile> UserRoleProfiles { get; set; }
    }
}
