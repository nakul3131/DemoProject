using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DemoProject.Services.ViewModel.PersonInformation;

namespace DemoProject.Services.ViewModel.Account.Customer
{
    public class LoanCustomerAccountViewModel
    {
        // CustomerAccount

        public long PrmKey { get; set; }

        public Guid CustomerAccountId { get; set; }

        public DateTime AccountOpeningDate { get; set; }

        public long AccountNumber { get; set; }

        [StringLength(50)]
        public string AlternateAccountNumber1 { get; set; }

        [StringLength(50)]
        public string AlternateAccountNumber2 { get; set; }

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

        [StringLength(1500)]
        public string Note { get; set; }

        public bool IsModified { get; set; }

        [StringLength(3)]
        public string EntryStatus { get; set; }

        [StringLength(3)]
        public string ActivationStatus { get; set; }

        // CustomerAccountMakerChecker
        public DateTime EntryDateTime { get; set; }

        public long CustomerAccountPrmKey { get; set; }

        public short UserProfilePrmKey { get; set; }

        [StringLength(3)]
        public string UserAction { get; set; }

        [StringLength(1500)]
        public string Remark { get; set; }

        // CustomerAccountModification
        public Guid CustomerAccountModificationId { get; set; }

        public byte ModificationNumber { get; set; }

        [StringLength(1500)]
        public string ReasonForModification { get; set; }

        // CustomerAccountModificationMakerChecker
        public long CustomerAccountModificationPrmKey { get; set; }

        //CustomerLoanAcquaintanceDetailViewModel
        public CustomerLoanAcquaintanceDetailViewModel CustomerLoanAcquaintanceDetailViewModel { get; set; }

        //CustomerAccountStandingInstructionViewModel
        public CustomerAccountStandingInstructionViewModel CustomerAccountStandingInstructionViewModel { get; set; }

        //PersonBorrowingDetailViewModel
        public PersonBorrowingDetailViewModel PersonBorrowingDetailViewModel { get; set; }

        //CustomerLoanAgainstDepositCollateralDetailViewModel
        public CustomerLoanAgainstDepositCollateralDetailViewModel CustomerLoanAgainstDepositCollateralDetailViewModel { get; set; }

        //PersonAdditionalDetailViewModel
        public PersonAdditionalDetailViewModel PersonAdditionalDetailViewModel { get; set; }

        //PersonEmploymentDetailViewModel
        public PersonEmploymentDetailViewModel PersonEmploymentDetailViewModel { get; set; }

        //PersonIncomeTaxDetailViewModel
        public PersonIncomeTaxDetailViewModel PersonIncomeTaxDetailViewModel { get; set; }

        //PersonCourtCaseViewModel
        public PersonCourtCaseViewModel PersonCourtCaseViewModel { get; set; }

        //PersonAdditionalIncomeDetailViewModel
        public PersonAdditionalIncomeDetailViewModel PersonAdditionalIncomeDetailViewModel { get; set; }

        public CustomerCashCreditLoanAccountViewModel CustomerCashCreditLoanAccountViewModel { get; set; }
        public CustomerEducationalLoanDetailViewModel CustomerEducationalLoanDetailViewModel { get; set; }

        // CustomerSharesCapitalAccountViewModel
        public CustomerLoanAccountViewModel CustomerLoanAccountViewModel { get; set; }

        public IEnumerable<CustomerLoanAccountViewModel> CustomerLoanAccountViewModelList { get; set; }

        // CustomerAccountDetailViewModel
        public CustomerAccountDetailViewModel CustomerAccountDetailViewModel { get; set; }

        public IEnumerable<CustomerAccountDetailViewModel> CustomerAccountDetailViewModelList { get; set; }

        // CustomerJointAccountHolderViewModel
        public CustomerJointAccountHolderViewModel CustomerJointAccountHolderViewModel { get; set; }

        // CustomerAccountNomineeViewModel
        public CustomerAccountNomineeViewModel CustomerAccountNomineeViewModel { get; set; }

        public IEnumerable<CustomerAccountNomineeViewModel> CustomerAccountNomineeViewModelist { get; set; }

        // CustomerLoanAccountGuarantorDetailViewModel
        public CustomerLoanAccountGuarantorDetailViewModel CustomerLoanAccountGuarantorDetailViewModel { get; set; }

        public IEnumerable<CustomerLoanAccountGuarantorDetailViewModel> CustomerLoanAccountGuarantorDetailViewModellist { get; set; }

        // CustomerPreOwnedVehicleLoanInspectionViewModel
        public CustomerPreOwnedVehicleLoanInspectionViewModel CustomerPreOwnedVehicleLoanInspectionViewModel { get; set; }

        public IEnumerable<CustomerPreOwnedVehicleLoanInspectionViewModel> CustomerPreOwnedVehicleLoanInspectionViewModellist { get; set; }

        // CustomerPreOwnedVehicleLoanPhotoViewModel
        public CustomerVehicleLoanPhotoViewModel CustomerVehicleLoanPhotoViewModel { get; set; }

        public IEnumerable<CustomerVehicleLoanPhotoViewModel> CustomerVehicleLoanPhotoViewModellist { get; set; }

        // CustomerLoanAccountVehicleInsuranceDetailViewModel
        public CustomerVehicleLoanInsuranceDetailViewModel CustomerVehicleLoanInsuranceDetailViewModel { get; set; }

        public IEnumerable<CustomerVehicleLoanInsuranceDetailViewModel> CustomerLoanAccountVehicleInsuranceDetaillist { get; set; }

        //CustomerVehicleLoanPermitDetailViewModel
        public CustomerVehicleLoanPermitDetailViewModel CustomerVehicleLoanPermitDetailViewModel { get; set; }

        //CustomerVehicleLoanContractDetailViewModel
        public CustomerVehicleLoanContractDetailViewModel CustomerVehicleLoanContractDetailViewModel { get; set; }

        // CustomerLoanFieldInvestigationViewModel
        public CustomerLoanFieldInvestigationViewModel CustomerLoanFieldInvestigationViewModel { get; set; }

        // CustomerLoanAccountDebtToIncomeRatioViewModel
        public CustomerLoanAccountDebtToIncomeRatioViewModel CustomerLoanAccountDebtToIncomeRatioViewModel { get; set; }

        public IEnumerable<CustomerLoanFieldInvestigationViewModel> CustomerLoanFieldInvestigationViewModellist { get; set; }

        // CustomerLoanFieldInvestigationViewModel
        public CustomerVehicleLoanCollateralDetailViewModel CustomerVehicleLoanCollateralDetailViewModel { get; set; }

        public IEnumerable<CustomerVehicleLoanCollateralDetailViewModel> CustomerVehicleLoanCollateralDetailViewModellist { get; set; }

        // PersonContactDetailViewModel
        public PersonContactDetailViewModel PersonContactDetailViewModel { get; set; }

        public IEnumerable<PersonContactDetailViewModel> PersonContactDetailViewModelList { get; set; }

        // PersonAddressViewModel
        public PersonAddressViewModel PersonAddressViewModel { get; set; }

        public IEnumerable<PersonAddressViewModel> PersonAddressViewModelList { get; set; }

        // CustomerAccountInterestRateViewModel
        public CustomerAccountInterestRateViewModel CustomerAccountInterestRateViewModel { get; set; }

        public IEnumerable<CustomerAccountInterestRateViewModel> CustomerAccountInterestRateViewModelList { get; set; }

        // CustomerGoldLoanCollateralDetailViewModel
        public CustomerGoldLoanCollateralDetailViewModel CustomerGoldLoanCollateralDetailViewModel { get; set; }

        // CustomerAccountDocumentViewModel
        public CustomerAccountDocumentViewModel CustomerAccountDocumentViewModel { get; set; }

        // CustomerGoldLoanCollateralPhotoViewModel
        public CustomerGoldLoanCollateralPhotoViewModel CustomerGoldLoanCollateralPhotoViewModel { get; set; }

        // CustomerGoldLoanReappraisalViewModel
        public CustomerGoldLoanReappraisalViewModel CustomerGoldLoanReappraisalViewModel { get; set; }

        //CustomerAccountSmsServiceViewModel
        public CustomerAccountSmsServiceViewModel CustomerAccountSmsServiceViewModel { get; set; }

        //CustomerAccountEmailServiceViewModel
        public CustomerAccountEmailServiceViewModel CustomerAccountEmailServiceViewModel { get; set; }

        //CustomerAccountNoticeScheduleViewModel
        public CustomerAccountNoticeScheduleViewModel CustomerAccountNoticeScheduleViewModel { get; set; }

        //CustomerHomeLoanCollateralDetailViewModel
        public CustomerLoanAgainstPropertyCollateralDetailViewModel CustomerLoanAgainstPropertyCollateralDetailViewModel { get; set; }

        //CustomerConsumerLoanCollateralDetailViewModel
        public CustomerConsumerLoanCollateralDetailViewModel CustomerConsumerLoanCollateralDetailViewModel { get; set; }

        //CustomerBusinessLoanCollateralDetailViewModel
        public CustomerBusinessLoanCollateralDetailViewModel CustomerBusinessLoanCollateralDetailViewModel { get; set; }

        [StringLength(1500)]
        public string CheckerRemark { get; set; }

        [StringLength(1500)]
        public string MakerRemark { get; set; }

        [StringLength(100)]
        public string NameOfCustomerAccount { get; set; }

        [StringLength(50)]
        public string NameOfUser { get; set; }

        // DropdownList 

        public Guid JointAccountHolderTypeId { get; set; }

        public Guid RelationId { get; set; }

        public Guid GuardianTypeId { get; set; }

        public Guid FrequencyId { get; set; }

        public Guid TransactionTypeId { get; set; }

    }
}
