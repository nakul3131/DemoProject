using DemoProject.Domain.Entities.Account.Layout;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Enterprise.Office
{
    [Table("BusinessOffice")]
    public partial class BusinessOffice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BusinessOffice()
        {
            BusinessOfficeAccountNumbers = new HashSet<BusinessOfficeAccountNumber>();
            BusinessOfficeAgreementNumbers = new HashSet<BusinessOfficeAgreementNumber>(); BusinessOfficeApplicationNumbers = new HashSet<BusinessOfficeApplicationNumber>();
            BusinessOfficeCurrencies = new HashSet<BusinessOfficeCurrency>();
            BusinessOfficeCustomerNumbers = new HashSet<BusinessOfficeCustomerNumber>();
            BusinessOfficeDetails = new HashSet<BusinessOfficeDetail>();
            BusinessOfficeMakerCheckers = new HashSet<BusinessOfficeMakerChecker>();
            BusinessOfficeMemberNumbers = new HashSet<BusinessOfficeMemberNumber>();
            BusinessOfficeMenus = new HashSet<BusinessOfficeMenu>();
            BusinessOfficeModifications = new HashSet<BusinessOfficeModification>();
            BusinessOfficeCoopRegistrations = new HashSet<BusinessOfficeCoopRegistration>();
            BusinessOfficePasswordPolicies = new HashSet<BusinessOfficePasswordPolicy>();           
            BusinessOfficeRBIRegistrations = new HashSet<BusinessOfficeRBIRegistration>();
            BusinessOfficeSpecialPermissions = new HashSet<BusinessOfficeSpecialPermission>();
            BusinessOfficeTranslations = new HashSet<BusinessOfficeTranslation>();
            BusinessOfficeTransactionLimits = new HashSet<BusinessOfficeTransactionLimit>();
            BusinessOfficeTransactionParameters = new HashSet<BusinessOfficeTransactionParameter>();
            BusinessOfficeSharesCertificateNumbers = new HashSet<BusinessOfficeSharesCertificateNumber>();
            SchemeBusinessOffices = new HashSet<SchemeBusinessOffice>();
        }

        [Key]
        public short PrmKey { get; set; }

        public Guid BusinessOfficeId { get; set; }

        [Required]
        [StringLength(3)]
        public string BusinessOfficeCode { get; set; }

        [Required]
        [StringLength(10)]
        public string AlternateBusinessOfficeCode { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfBusinessOffice { get; set; }

        [Required]
        [StringLength(10)]
        public string AliasName { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOnReport { get; set; }

        [Required]
        [StringLength(100)]
        public string NameOfBusinessOfficeForThirdPartyInterface { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [Required]
        [StringLength(500)]
        public string ContactDetails { get; set; }

        [Required]
        [StringLength(1500)]
        public string AddressDetails { get; set; }

        public bool IsFundBranch { get; set; }

        public bool EnableCorporateAccess { get; set; }

        public byte LoanDirectDebitGenerationDays { get; set; }

        public short ParentBusinessOfficePrmKey { get; set; }

        public short ClearingBusinessOfficePrmKey { get; set; }

        [Required]
        [StringLength(15)]
        public string TransactionCodeForClearing { get; set; }

        public short RegionalOfficePrmKey { get; set; }

        [Required]
        [StringLength(3)]
        public string BusinessOfficeStatusForCoreOperation { get; set; }

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
        public virtual ICollection<BusinessOfficeAccountNumber> BusinessOfficeAccountNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeAgreementNumber> BusinessOfficeAgreementNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeApplicationNumber> BusinessOfficeApplicationNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeCurrency> BusinessOfficeCurrencies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeCustomerNumber> BusinessOfficeCustomerNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeDetail> BusinessOfficeDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeMakerChecker> BusinessOfficeMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeMemberNumber> BusinessOfficeMemberNumbers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeMenu> BusinessOfficeMenus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeModification> BusinessOfficeModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeCoopRegistration> BusinessOfficeCoopRegistrations { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficePasswordPolicy> BusinessOfficePasswordPolicies { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeRBIRegistration> BusinessOfficeRBIRegistrations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeSpecialPermission> BusinessOfficeSpecialPermissions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeTranslation> BusinessOfficeTranslations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeTransactionLimit> BusinessOfficeTransactionLimits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeTransactionParameter> BusinessOfficeTransactionParameters { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SchemeBusinessOffice> SchemeBusinessOffices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BusinessOfficeSharesCertificateNumber> BusinessOfficeSharesCertificateNumbers { get; set; }
    }
}
 