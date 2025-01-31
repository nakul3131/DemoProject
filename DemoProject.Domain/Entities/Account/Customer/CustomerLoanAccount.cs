using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DemoProject.Domain.Entities.PersonInformation.SystemEntity;

namespace DemoProject.Domain.Entities.Account.Customer
{
    [Table("CustomerLoanAccount")]
    public partial class CustomerLoanAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CustomerLoanAccount()
        {
            CustomerLoanAccountGuarantorDetails = new HashSet<CustomerLoanAccountGuarantorDetail>();
            CustomerLoanAccountMakerCheckers = new HashSet<CustomerLoanAccountMakerChecker>();
            CustomerVehicleLoanInsuranceDetails = new HashSet<CustomerVehicleLoanInsuranceDetail>();
            CustomerLoanFieldInvestigations = new HashSet<CustomerLoanFieldInvestigation>();
            CustomerVehicleLoanCollateralDetails = new HashSet<CustomerVehicleLoanCollateralDetail>();
            CustomerPreOwnedVehicleLoanInspections = new HashSet<CustomerPreOwnedVehicleLoanInspection>();
            CustomerGoldLoanCollateralDetails = new HashSet<CustomerGoldLoanCollateralDetail>();
            CustomerGoldLoanCollateralPhotos = new HashSet<CustomerGoldLoanCollateralPhoto>();
            CustomerLoanAgainstPropertyCollateralDetails = new HashSet<CustomerLoanAgainstPropertyCollateralDetail>();
            CustomerVehicleLoanPermitDetails = new HashSet<CustomerVehicleLoanPermitDetail>();
            CustomerVehicleLoanPhotos = new HashSet<CustomerVehicleLoanPhoto>();
            CustomerVehicleLoanContractDetails = new HashSet<CustomerVehicleLoanContractDetail>();
            CustomerConsumerLoanCollateralDetails = new HashSet<CustomerConsumerLoanCollateralDetail>();
            CustomerBusinessLoanCollateralDetails = new HashSet<CustomerBusinessLoanCollateralDetail>();
            CustomerLoanAccountTranslations = new HashSet<CustomerLoanAccountTranslation>();
            CustomerCashCreditLoanAccounts = new HashSet<CustomerCashCreditLoanAccount>();
            CustomerEducationalLoanDetails = new HashSet<CustomerEducationalLoanDetail>();
            CustomerLoanAgainstDepositCollateralDetails = new HashSet<CustomerLoanAgainstDepositCollateralDetail>();
            CustomerLoanAccountDebtToIncomeRatios = new HashSet<CustomerLoanAccountDebtToIncomeRatio>();
            CustomerLoanAcquaintanceDetails = new HashSet<CustomerLoanAcquaintanceDetail>();
        }

        [Key]
        public int PrmKey { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public byte ModificationNumber { get; set; }

        public int MinuteOfMeetingAgendaPrmKey { get; set; }

        public DateTime MaturityDate { get; set; }

        public short OccupationPrmKey { get; set; }

        public bool IsEmployee { get; set; }

        public short CIBILScore { get; set; }

        public short LoanReasonPrmKey { get; set; }

        public decimal DemandAmount { get; set; }

        public decimal SanctionAmount { get; set; }

        public decimal InstallmentAmount { get; set; }

        public decimal DeductedSharesAmount { get; set; }

        [Required]
        [StringLength(500)]
        public string DeductionRemark { get; set; }

        [Required]
        [StringLength(1500)]
        public string StrengthsFactors { get; set; }

         [Required]
        [StringLength(1500)]
        public string WeaknessesFactors { get; set; }

         [Required]
        [StringLength(1500)]
        public string OpportunitiesFactors { get; set; }

         [Required]
        [StringLength(1500)]
        public string ThreatsFactors { get; set; }

         [Required]
        [StringLength(1500)]
        public string PastCreditHistory { get; set; }

         [Required]
        [StringLength(1500)]
        public string LegalAndRegulatoryCompliance { get; set; }

        [Required]
        [StringLength(1500)]
        public string Note { get; set; }

        [Required]
        [StringLength(3)]
        public string EntryStatus { get; set; }

        public virtual CustomerAccount CustomerAccount { get; set; }

        public virtual Occupation Occupation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountGuarantorDetail> CustomerLoanAccountGuarantorDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountMakerChecker> CustomerLoanAccountMakerCheckers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerVehicleLoanInsuranceDetail> CustomerVehicleLoanInsuranceDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanFieldInvestigation> CustomerLoanFieldInvestigations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerVehicleLoanCollateralDetail> CustomerVehicleLoanCollateralDetails { get; set; }
       
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerPreOwnedVehicleLoanInspection> CustomerPreOwnedVehicleLoanInspections{ get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerVehicleLoanPermitDetail> CustomerVehicleLoanPermitDetails { get; set; }
        
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerVehicleLoanPhoto>  CustomerVehicleLoanPhotos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerVehicleLoanContractDetail> CustomerVehicleLoanContractDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerGoldLoanCollateralDetail> CustomerGoldLoanCollateralDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerGoldLoanCollateralPhoto> CustomerGoldLoanCollateralPhotos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAgainstPropertyCollateralDetail> CustomerLoanAgainstPropertyCollateralDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerConsumerLoanCollateralDetail> CustomerConsumerLoanCollateralDetails { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerBusinessLoanCollateralDetail> CustomerBusinessLoanCollateralDetails { get; set; }
     
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountTranslation> CustomerLoanAccountTranslations { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerCashCreditLoanAccount> CustomerCashCreditLoanAccounts { get; set; }
    
         [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerEducationalLoanDetail> CustomerEducationalLoanDetails { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAgainstDepositCollateralDetail> CustomerLoanAgainstDepositCollateralDetails { get; set; }
     
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAccountDebtToIncomeRatio> CustomerLoanAccountDebtToIncomeRatios { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerLoanAcquaintanceDetail> CustomerLoanAcquaintanceDetails { get; set; }
    }
}
