using System;
using System.ComponentModel.DataAnnotations;

namespace DemoProject.Services.ViewModel.Enterprise.Office
{
    public class BusinessOfficeViewModel
    {
        // BusinessOffice
        public short PrmKey { get; set; }

        public Guid BusinessOfficeId { get; set; }

        [StringLength(3)]
        public string BusinessOfficeCode { get; set; }

        [StringLength(10)]
        public string AlternateBusinessOfficeCode { get; set; }

        [StringLength(100)]
        public string NameOfBusinessOffice { get; set; }

        [StringLength(10)]
        public string AliasName { get; set; }

        [StringLength(100)]
        public string NameOnReport { get; set; }

        [StringLength(100)]
        public string NameOfBusinessOfficeForThirdPartyInterface { get; set; }

        public DateTime OpeningDate { get; set; }

        public DateTime? CloseDate { get; set; }

        [StringLength(500)]
        public string ContactDetails { get; set; }

        [StringLength(1500)]
        public string AddressDetails { get; set; }

        public bool IsFundBranch { get; set; }

        public bool EnableCorporateAccess { get; set; }

        public byte LoanDirectDebitGenerationDays { get; set; }

        public short ParentBusinessOfficePrmKey { get; set; }

        public short ClearingBusinessOfficePrmKey { get; set; }

        [StringLength(15)]
        public string TransactionCodeForClearing { get; set; }

        public short RegionalOfficePrmKey { get; set; }

        public string BusinessOfficeStatusForCoreOperation { get; set; }

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // BusinessOfficeMakerChecker

        public DateTime EntryDateTime { get; set; }

        public short BusinessOfficePrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // BusinessOfficeModification

        public Guid BusinessOfficeModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // BusinessOfficeModificationMakerChecker

        public short BusinessOfficeModificationPrmKey { get; set; }

        // BusinessOfficeTranslation

        public Guid BusinessOfficeTranslationId { get; set; }

        public short LanguagePrmKey { get; set; }

        public byte TransModificationNumber { get; set; }

        [StringLength(100)]
        public string TransNameOfBusinessOffice { get; set; }

        [StringLength(10)]
        public string TransAliasName { get; set; }

        [StringLength(100)]
        public string TransNameOnReport { get; set; }

        [StringLength(500)]
        public string TransContactDetails { get; set; }

        [StringLength(1500)]
        public string TransAddressDetails { get; set; }

        [StringLength(1500)]
        public string TransNote { get; set; }

        // BusinessOfficeTranslationMakerChecker

        public short BusinessOfficeTranslationPrmKey { get; set; }

        // BusinessOfficeCoopRegistrationViewModel
        public BusinessOfficeCoopRegistrationViewModel BusinessOfficeCoopRegistrationViewModel { get; set; }

        // BusinessOfficeCustomerNumberViewModel
        //public BusinessOfficeCustomerNumberViewModel BusinessOfficeCustomerNumberViewModel { get; set; }

        // BusinessOfficeDetailViewModel
        public BusinessOfficeDetailViewModel BusinessOfficeDetailViewModel { get; set; }

        // BusinessOfficeMemberNumberViewModel
        public BusinessOfficeMemberNumberViewModel BusinessOfficeMemberNumberViewModel { get; set; }

        // BusinessOfficeRBIRegistrationViewModel
        public BusinessOfficeRBIRegistrationViewModel BusinessOfficeRBIRegistrationViewModel { get; set; }

        // BusinessOfficeTransactionParameterViewModel
        public BusinessOfficeTransactionParameterViewModel BusinessOfficeTransactionParameterViewModel { get; set; }

        // BusinessOfficePasswordPolicyViewModel
        public BusinessOfficePasswordPolicyViewModel BusinessOfficePasswordPolicyViewModel { get; set; }

        // BusinessOfficeMenuViewModel
        public BusinessOfficeMenuViewModel BusinessOfficeMenuViewModel { get; set; }

        // BusinessOfficeSpecialPermissionViewModel
        public BusinessOfficeSpecialPermissionViewModel BusinessOfficeSpecialPermissionViewModel { get; set; }

        // BusinessOfficeTransactionLimitViewModel 
        public BusinessOfficeTransactionLimitViewModel BusinessOfficeTransactionLimitViewModel { get; set; }

        // BusinessOfficeAccountNumberViewModel
        public BusinessOfficeAccountNumberViewModel BusinessOfficeAccountNumberViewModel { get; set; }

        // BusinessOfficeApplicationNumberViewModel
        public BusinessOfficeApplicationNumberViewModel BusinessOfficeApplicationNumberViewModel { get; set; }

        // BusinessOfficeCurrencyViewModel
        public BusinessOfficeCurrencyViewModel BusinessOfficeCurrencyViewModel { get; set; }

        //new

        // BusinessOfficeDepositCertificateNumberViewModel
        public BusinessOfficeDepositCertificateNumberViewModel BusinessOfficeDepositCertificateNumberViewModel { get; set; }


        // BusinessOfficeSharesCertificateNumberViewModel
        public BusinessOfficeSharesCertificateNumberViewModel BusinessOfficeSharesCertificateNumberViewModel { get; set; }

        // BusinessOfficePersonInformationNumberViewModel
        public BusinessOfficePersonInformationNumberViewModel BusinessOfficePersonInformationNumberViewModel { get; set; }

        // BusinessOfficePassbookNumberViewModel
        public BusinessOfficePassbookNumberViewModel BusinessOfficePassbookNumberViewModel { get; set; }

        // BusinessOfficeAgreementNumberViewModel
        public BusinessOfficeAgreementNumberViewModel BusinessOfficeAgreementNumberViewModel { get; set; }


        // Other
        [StringLength(50)]
        public string NameOfUser { get; set; }

        public DateTime MakerEntryDateTime { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(3)]
        public string Operation { get; set; }

        // For SelectListItem

        public Guid ClearingBusinessOfficeId { get; set; }

        public Guid RegionalOfficeId { get; set; }

        public Guid ParentBusinessOfficeId { get; set; }

    }
}
