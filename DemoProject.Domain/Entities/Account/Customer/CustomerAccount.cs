using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerAccount")]
    public partial class CustomerAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerAccount()
        {
            CustomerAccountBeneficiaryDetails = new HashSet<CustomerAccountBeneficiaryDetail>();
            CustomerAccountChequeDetails = new HashSet<CustomerAccountChequeDetail>();
            CustomerAccountDetails = new HashSet<CustomerAccountDetail>();
            CustomerAccountDocuments = new HashSet<CustomerAccountDocument>();
            CustomerAccountEmailServices = new HashSet<CustomerAccountEmailService>();
            CustomerAccountInterestRates = new HashSet<CustomerAccountInterestRate>();
            CustomerAccountMakerCheckers = new HashSet<CustomerAccountMakerChecker>();
            CustomerAccountModifications = new HashSet<CustomerAccountModification>();
            CustomerAccountNominees = new HashSet<CustomerAccountNominee>();
            CustomerAccountNoticeSchedules = new HashSet<CustomerAccountNoticeSchedule>();
            CustomerAccountPhotoSigns = new HashSet<CustomerAccountPhotoSign>();
            CustomerAccountReferencePersonDetails = new HashSet<CustomerAccountReferencePersonDetail>();
            CustomerAccountSmsServices = new HashSet<CustomerAccountSmsService>();
            CustomerAccountStandingInstructions = new HashSet<CustomerAccountStandingInstruction>();
            CustomerAccountSweepDetails = new HashSet<CustomerAccountSweepDetail>();
            CustomerAccountTurnOverLimits = new HashSet<CustomerAccountTurnOverLimit>();
            CustomerDepositAccounts = new HashSet<CustomerDepositAccount>();
            CustomerJointAccountHolders = new HashSet<CustomerJointAccountHolder>();
            CustomerSharesCapitalAccounts = new HashSet<CustomerSharesCapitalAccount>();
            //CustomerLoanAccount
            CustomerLoanAccounts = new HashSet<CustomerLoanAccount>();
            
        }

        [Key]
        public long PrmKey { get; set; }

        public Guid CustomerAccountId { get; set; }

        public DateTime AccountOpeningDate { get; set; }

        public long AccountNumber { get; set; }

        [Required]
        [StringLength(50)]
        public string AlternateAccountNumber1 { get; set; }

        [Required]
        [StringLength(50)]
        public string AlternateAccountNumber2 { get; set; }

        [Required]
        [StringLength(50)]
        public string AlternateAccountNumber3 { get; set; }

        public int ApplicationNumber { get; set; }

        public int PassbookNumber { get; set; }

        public int AgreementNumber { get; set; }

        public bool IsPrivateCustomer { get; set; }

        public bool IsDeniedDebits { get; set; }

        public bool IsDeniedCredits { get; set; }

        public bool IsDeniedDebitsOverride { get; set; }

        public bool IsDeniedCreditsOverride { get; set; }

        public bool IsDeniedPayments { get; set; }

        public bool IsDormant { get; set; }

        public bool IsFrozen { get; set; }

        public bool EnableTurnOverLimit { get; set; }

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
        public virtual ICollection<CustomerAccountMakerChecker> CustomerAccountMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountModification> CustomerAccountModifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerSharesCapitalAccount> CustomerSharesCapitalAccounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountDetail> CustomerAccountDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDepositAccount> CustomerDepositAccounts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerJointAccountHolder> CustomerJointAccountHolders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountNominee> CustomerAccountNominees { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountReferencePersonDetail> CustomerAccountReferencePersonDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountBeneficiaryDetail> CustomerAccountBeneficiaryDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountTurnOverLimit> CustomerAccountTurnOverLimits { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountInterestRate> CustomerAccountInterestRates { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountDocument> CustomerAccountDocuments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountSweepDetail> CustomerAccountSweepDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountStandingInstruction>  CustomerAccountStandingInstructions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountPhotoSign> CustomerAccountPhotoSigns { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountEmailService> CustomerAccountEmailServices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountSmsService> CustomerAccountSmsServices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountChequeDetail> CustomerAccountChequeDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerAccountNoticeSchedule> CustomerAccountNoticeSchedules { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccount> CustomerLoanAccounts { get; set; }

    }
}
