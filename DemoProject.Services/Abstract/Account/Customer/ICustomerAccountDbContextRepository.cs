using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.PersonInformation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerAccountDbContextRepository
    {
        bool AttachPersonIncomeTaxDetailData(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _entryType);

        bool AttachPersonIncomeTaxDocumentData(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _localStoragePath, string _oldFileName, string _entryType);

        bool AttachIncomeTaxDetailDocumentInLocalStorage(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _incomeTaxDocumentLocalStoragePath, IEnumerable<PersonIncomeTaxDetailViewModel> _personIncomeTaxDetailViewModelList, string _entryType);

        bool AttachIncomeTaxDetailDocumentInDatabaseStorage(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, IEnumerable<PersonIncomeTaxDetailViewModel> _personIncomeTaxDetailViewModelList, string _entryType);

        bool AttachPersonBorrowingDetailData(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel, string _entryType);

        bool AttachPersonCourtCaseData(PersonCourtCaseViewModel _personCourtCaseViewModel, string _entryType);

        bool AttachPersonAdditionalIncomeDetailData(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel, string _entryType);

        bool AttachCustomerAccountDetailData(CustomerAccountDetailViewModel _customerAccountDetailViewModel, string _entryType);

        bool AttachCustomerAccountEmailServiceData(CustomerAccountEmailServiceViewModel _customerAccountEmailServiceViewModel, string _entryType);

        bool AttachCustomerAccountNomineeData(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel, string _entryType);

        bool AttachCustomerAccountNoticeScheduleData(CustomerAccountNoticeScheduleViewModel _customerAccountNoticeScheduleViewModel, string _entryType);

        bool AttachCustomerAccountSmsServiceData(CustomerAccountSmsServiceViewModel _customerAccountSmsServiceViewModel, string _entryType);

        bool AttachCustomerAccountTurnOverLimitData(CustomerAccountTurnOverLimitViewModel _customerAccountTurnOverLimitViewModel, string _entryType);

        bool AttachCustomerJointAccountHolderData(CustomerJointAccountHolderViewModel _customerJointAccountHolderViewModel, string _entryType);

        bool AttachCustomerSharesCapitalAccountData(CustomerSharesCapitalAccountViewModel _customerSharesCapitalAccountViewModel, string _entryType);

        bool AttachPersonAddressData(PersonAddressViewModel _personAddressViewModel, string _entryType);

        bool AttachPersonContactDetailData(PersonContactDetailViewModel _personContactDetailViewModel, string _entryType);

        bool AttachSharesCapitalCustomerAccountData(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel, string _entryType);

        //CustomerDepositAccount

        bool AttachCustomerAccountBeneficiaryDetailData(CustomerAccountBeneficiaryDetailViewModel _customerAccountBeneficiaryDetailViewModel, string _entryType);

        bool AttachCustomerAccountChequeDetailData(CustomerAccountChequeDetailViewModel _customerAccountChequeDetailViewModel, string _entryType);

        bool AttachCustomerAccountDocumentData(CustomerAccountDocumentViewModel _customerAccountDocumentViewModel, string _storagePath, string _oldFileName, string _entryType);

        bool AttachCustomerAccountDocumentInLocalStorage(CustomerAccountDocumentViewModel _customerAccountDocumentViewModel,string _storagePath, IEnumerable<CustomerAccountDocumentViewModel> _customerAccountDocumentViewModelList, string _entryType);

        bool AttachCustomerAccountDocumentInDatabaseStorage(CustomerAccountDocumentViewModel _customerAccountDocumentViewModel, IEnumerable<CustomerAccountDocumentViewModel> _customerAccountDocumentViewModelList, string _entryType);

        bool AttachCustomerAccountInterestRateData(CustomerAccountInterestRateViewModel _customerAccountInterestRateViewModel, string _entryType);

        bool AttachCustomerLoanAgainstDepositCollateralDetailData(CustomerLoanAgainstDepositCollateralDetailViewModel _customerLoanAgainstDepositCollateralDetailViewModel, string _entryType);


        bool AttachCustomerLoanAgainstPropertyCollateralDetailData(CustomerLoanAgainstPropertyCollateralDetailViewModel _customerLoanAgainstPropertyCollateralDetailViewModel, string _entryType);

        bool AttachCustomerBusinessLoanCollateralDetailData(CustomerBusinessLoanCollateralDetailViewModel _customerBusinessLoanCollateralDetailViewModel, string _entryType);

        bool AttachCustomerAccountPhotoSignData(CustomerAccountPhotoSignViewModel _customerAccountPhotoSignViewModel, string _entryType);

        bool AttachCustomerAccountReferencePersonDetailData(CustomerAccountReferencePersonDetailViewModel _customerAccountReferencePersonDetailViewModel, string _entryType);

        bool AttachCustomerAccountStandingInstructionData(CustomerAccountStandingInstructionViewModel _customerAccountStandingInstructionViewModel, string _entryType, string _instructionType);

        bool AttachCustomerAccountSweepDetailData(CustomerAccountSweepDetailViewModel _customerAccountSweepDetailViewModel, string _entryType);

        bool AttachCustomerDepositAccountAgentData(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel, string _entryType);

        bool AttachCustomerDepositAccountData(CustomerDepositAccountViewModel _customerDepositAccountViewModel, string _entryType);

        bool AttachCustomerTermDepositAccountDetailData(CustomerTermDepositAccountDetailViewModel _customerTermDepositAccountDetailViewModel, string _entryType);

        bool AttachDepositCustomerAccountData(DepositCustomerAccountViewModel _depositCustomerAccountViewModel, string _entryType);

        bool AttachPhotoDocumentInDatabaseStorage(CustomerAccountPhotoSignViewModel _customerAccountPhotoSignViewModel, CustomerAccountPhotoSignViewModel customerAccountPhotoSignViewModel, string _entryType);

        bool AttachPhotoDocumentInLocalStorage(CustomerAccountPhotoSignViewModel _customerAccountPhotoSignViewModel, string _localStoragePath, CustomerAccountPhotoSignViewModel customerAccountPhotoSignViewModel, string _entryType);

        bool AttachSignDocumentInDatabaseStorage(CustomerAccountPhotoSignViewModel _customerAccountPhotoSignViewModel, CustomerAccountPhotoSignViewModel customerAccountPhotoSignViewModel, string _entryType);

        bool AttachSignDocumentInLocalStorage(CustomerAccountPhotoSignViewModel _customerAccountPhotoSignViewModel, string _localStoragePath, CustomerAccountPhotoSignViewModel customerAccountPhotoSignViewModel, string _entryType);

        //Customer Loan Accont

        bool AttachCustomerLoanAccountGuarantorDetailData(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetailViewModel, string _entryType);

        bool AttachCustomerVehicleLoanCollateralDetailData(CustomerVehicleLoanCollateralDetailViewModel _customerVehicleLoanCollateralDetailViewModel, string _entryType);

        bool AttachCustomerPreOwnedVehicleLoanInspectionData(CustomerPreOwnedVehicleLoanInspectionViewModel _customerPreOwnedVehicleLoanInspectionViewModel, string _entryType);

        bool AttachCustomerLoanAccountVehicleInsuranceDetailData(CustomerVehicleLoanInsuranceDetailViewModel _customerVehicleLoanInsuranceDetailViewModelViewModel, string _entryType);
        
        bool AttachCustomerVehicleLoanPermitDetail(CustomerVehicleLoanPermitDetailViewModel _customerVehicleLoanPermitDetailViewModel, string _entryType);

        bool AttachCustomerVehicleLoanContractDetailData(CustomerVehicleLoanContractDetailViewModel _customerVehicleLoanContractDetailViewModel, string _entryType);

        bool AttachCustomerVehicleLoanPhotoData(CustomerVehicleLoanPhotoViewModel _customerVehicleLoanPhotoViewModel,string _storagePath,string _oldFileName, string _entryType);

        bool AttachCustomerVehicleLoanPhotoInLocalStorage(CustomerVehicleLoanPhotoViewModel _customerVehicleLoanPhotoViewModel,string _storagePath, IEnumerable<CustomerVehicleLoanPhotoViewModel> _customerVehicleLoanPhotoViewModelList, string _entryType);

        bool AttachCustomerVehicleLoanPhotoInDatabaseStorage(CustomerVehicleLoanPhotoViewModel _customerVehicleLoanPhotoViewModel, IEnumerable<CustomerVehicleLoanPhotoViewModel> _customerVehicleLoanPhotoViewModelList, string _entryType);

        bool AttachCustomerGoldLoanCollateralPhotoData(CustomerGoldLoanCollateralPhotoViewModel _customerGoldLoanCollateralPhotoViewModel, string _storagePath, string _oldFileName, string _entryType);

        bool AttachCustomerGoldLoanCollateralPhotoInLocalStorage(CustomerGoldLoanCollateralPhotoViewModel _customerGoldLoanCollateralPhotoViewModel, string _storagePath, IEnumerable<CustomerGoldLoanCollateralPhotoViewModel> _customerGoldLoanCollateralPhotoViewModelList, string _entryType);

        bool AttachCustomerGoldLoanCollateralPhotoInDatabaseStorage(CustomerGoldLoanCollateralPhotoViewModel _customerGoldLoanCollateralPhotoViewModel, IEnumerable<CustomerGoldLoanCollateralPhotoViewModel> _customerGoldLoanCollateralPhotoViewModelList, string _entryType);

        bool AttachCustomerLoanFieldInvestigationData(CustomerLoanFieldInvestigationViewModel _customerLoanFieldInvestigationViewModel, string _entryType);

        bool AttachCustomerLoanAccountDebtToIncomeRatioData(CustomerLoanAccountDebtToIncomeRatioViewModel _customerLoanAccountDebtToIncomeRatioViewModel, string _entryType);

        bool AttachCustomerCashCreditLoanAccountData(CustomerCashCreditLoanAccountViewModel _customerCashCreditLoanAccountViewModel, string _entryType);
        bool AttachCustomerEducationalLoanDetailData(CustomerEducationalLoanDetailViewModel _customerEducationalLoanDetailViewModel, string _entryType);

        bool AttachCustomerGoldLoanCollateralDetailData(CustomerGoldLoanCollateralDetailViewModel _customerGoldLoanCollateralDetailViewModel, string _entryType);
        bool AttachCustomerConsumerLoanCollateralDetailData(CustomerConsumerLoanCollateralDetailViewModel _customerConsumerLoanCollateralDetailViewModel, string _entryType);

        bool AttachCustomerLoanAccountData(CustomerLoanAccountViewModel _customerLoanAccountViewModel, string _entryType);

        bool AttachLoanCustomerAccountData(LoanCustomerAccountViewModel _loanCustomerAccountViewModel, string _entryType);

        bool AttachPersonEmploymentDetailData(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel, string _entryType);

        bool AttachCustomerLoanAcquaintanceDetail(CustomerLoanAcquaintanceDetailViewModel _customerLoanAcquaintanceDetailViewModel, string _entryType);

        bool DeletePhotoForDeletedRecord(string _photoPathToDelete);

        bool DeletePhotoOfDeletedRecord();
        //To check File Existence In Database
        bool FileExist(string _fullFilePath);

        //Get File Path 
        string GetFullFilePath(string _fullPath, string _nameOfFile);

        Task<bool> SaveData();
    }
}
