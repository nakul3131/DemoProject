using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.PersonInformation;

namespace DemoProject.Services.Abstract.Account.Customer
{
    public interface ICustomerDetailRepository
    {
        //   Other
        bool IsMinimumBalanceViolation(long _customerAccountPrmKey, decimal _balanceAmount);
        bool IsNewCustomer(long _customerAccountPrmKey);
        long GetPersonPrmKeyByCustomerAccountPrmKey(long _customerAccountPrmKey);
        short GetCustomerAccountAge(long _customerAccountPrmKey);
        short GetSchemePrmKeyOfCustomerAccount(long _customerAccountPrmKey);
        int GetTotalNumberOfShares(long _customerAccountPrmKey);
        string GetAllSharesCertificateNumbers(long _customerAccountPrmKey);
        string GetCustomerRegisterdMobileNumber(long _customerAccountPrmKey);
        string IsAllowToCloseSharesCapitalAccount(long _customerAccountPrmKey);

        // Check Whether After Withdrawal Minimum Balance Is Maintained Or Not

        Task<IEnumerable<PersonAdditionalIncomeDetailViewModel>> GetPersonAdditionalIncomeDetailEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonAdditionalIncomeDetailViewModel>> GetCustomerAccountAdditionalIncomeDetailEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonIncomeTaxDetailViewModel>> GetPersonIncomeTaxDetailEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonIncomeTaxDetailViewModel>> GetCustomerAccountIncomeTaxDetailEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonCourtCaseViewModel>> GetPersonCourtCaseEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonCourtCaseViewModel>> GetCustomerAccountCourtCaseEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerAccountBeneficiaryDetailViewModel>> GetBeneficiaryDetailEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonBorrowingDetailViewModel>> GetCustomerAccountBorrowingDetailEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonBorrowingDetailViewModel>> GetPersonBorrowingDetailEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerAccountNomineeViewModel>> GetNomineeEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerAccountNoticeScheduleViewModel>> GetCustomerAccountNoticeScheduleEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerAccountStandingInstructionViewModel>> GetStandingInstructionEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerAccountNoticeScheduleViewModel>> GetNoticeScheduleEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerAccountTurnOverLimitViewModel>> GetTurnOverLimitEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetJointAccountHolderEntries(long _customerAccountPrmKey, string _entryType);

        // Delete After All pages are Complition 29112023
        CustomerAccountNomineeGuardianViewModel GetNomineeGuardianEntry(long _customerAccountNomineePrmKey, string _entryType);

        IEnumerable<CustomerAccountNomineeGuardianViewModel> GetNomineeGuardianEntries(long _customerAccountNomineePrmKey, string _entryType);

        Task<CustomerAccountChequeDetailViewModel> GetChequeDetailEntry(long _customerAccountPrmKey, string _entryType);

        Task<CustomerAccountDetailViewModel> GetAccountDetailEntry(long _customerAccountPrmKey, string _entryType);

        Task<CustomerAccountEmailServiceViewModel> GetEmailServiceEntry(long _customerAccountPrmKey, string _entryType);

        Task<CustomerAccountInterestRateViewModel> GetCustomerAccountInterestRateEntry(long _customerAccountPrmKey, string _entryType);

        Task<CustomerLoanAgainstPropertyCollateralDetailViewModel> GetCustomerLoanAgainstPropertyCollateralDetailEntry(int _customerLoanAccountPrmKey, string _entryType);

        Task<CustomerBusinessLoanCollateralDetailViewModel> GetCustomerBusinessLoanCollateralDetailEntry(long _customerLoanAccountPrmKey, string _entryType);

        Task<CustomerAccountPhotoSignViewModel> GetPhotoSignEntry(long _customerAccountPrmKey, string _entryType);

        Task<CustomerAccountSmsServiceViewModel> GetSmsServiceEntry(long _customerAccountPrmKey, string _entryType);

        Task<CustomerAccountStandingInstructionViewModel> GetStandingInstructionEntry(long _customerAccountPrmKey, string _entryType);

        Task<CustomerAccountSweepDetailViewModel> GetSweepDetailEntry(long _customerAccountPrmKey, string _entryType);

        Task<CustomerSharesCapitalAccountViewModel> GetSharesCapitalAccountEntry(long _customerAccountPrmKey, string _entryType);

        Task <PersonEmploymentDetailViewModel> GetCustomerAccountEmploymentDetailEntry(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonAddressViewModel>> GetCustomerAccountAddressDetailEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonAddressViewModel>> GetPersonAddressDetailEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonContactDetailViewModel>> GetCustomerAccountContactDetailEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<PersonContactDetailViewModel>> GetPersonContactDetailEntries(long _customerAccountPrmKey, string _entryType);

        //   Deposite Account
        Task<CustomerDepositAccountViewModel> GetDepositAccountEntry(long _customerAccountPrmKey, string _entryType);

        Task<CustomerTermDepositAccountDetailViewModel> GetTermDepositAccountDetailEntry(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerAccountDocumentViewModel>> GetDocumentEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerAccountFacilityViewModel>> GetFacilityEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerAccountInterestRateViewModel>> GetInterestRateEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerAccountReferencePersonDetailViewModel>> GetReferencePersonEntries(long _customerAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetDepositAccountAgentEntries(long _customerAccountPrmKey, string _entryType);

        //    LoanCustomer Account
        Task<CustomerAccountInterestRateViewModel> GetInterestRateEntry(long _CustomerAccountPrmKey, string _entryType);

        Task<CustomerVehicleLoanInsuranceDetailViewModel> GetLoanAccountVehicleInsuranceDetailEntry(int _CustomerLoanAccountPrmKey, string _entryType);

        Task<CustomerVehicleLoanPermitDetailViewModel> GetCustomerVehicleLoanPermitDetailEntry(int _customerLoanAccountPrmKey, string _entryType);

        Task<CustomerVehicleLoanContractDetailViewModel> GetCustomerVehicleLoanContractDetailEntry(int _customerLaonAccountPrmKey, string _entryType);

        Task<CustomerLoanAccountViewModel> GetLoanAccountEntry(long _CustomerAccountPrmKey, string _entryType);

        Task<CustomerLoanFieldInvestigationViewModel> GetLoanFieldInvestigationEntry(int _CustomerLoanAccountPrmKey, string _entryType);

        Task<CustomerLoanAccountDebtToIncomeRatioViewModel> GetCustomerLoanAccountDebtToIncomeRatioEntry(int _CustomerLoanAccountPrmKey, string _entryType);

        Task<CustomerCashCreditLoanAccountViewModel> GetCustomerCashCreditLoanAccountEntry(int _CustomerLoanAccountPrmKey, string _entryType);
        
        Task<CustomerEducationalLoanDetailViewModel> GetCustomerEducationalLoanDetailEntry(int _CustomerLoanAccountPrmKey, string _entryType);

        Task<CustomerPreOwnedVehicleLoanInspectionViewModel> GetPreOwnedVehicleLoanInspectionEntry(int _CustomerLoanAccountPrmKey, string _entryType);

        Task<CustomerVehicleLoanCollateralDetailViewModel> GetVehicleLoanCollateralDetailEntry(int _CustomerLoanAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerGoldLoanCollateralDetailViewModel>> GetGoldLoanCollateralDetailEntries(int _customerLoanAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerGoldLoanCollateralPhotoViewModel>> GetGoldLoanCollateralPhotoEntries(int _customerLoanAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerConsumerLoanCollateralDetailViewModel>> GetConsumerLoanCollateralDetailEntries(int _customerLoanAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerLoanAcquaintanceDetailViewModel>> GetAcquaintanceDetailEntries(int _customerLoanAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetLoanAccountGuarantorDetailEntries(int _customerLoanAccountPrmKey, string _entryType);
        Task<IEnumerable<CustomerLoanAgainstDepositCollateralDetailViewModel>> GetCustomerLoanAgainstDepositCollateralDetailEntries(int _customerLoanAccountPrmKey, string _entryType);

        Task<IEnumerable<CustomerVehicleLoanPhotoViewModel>> GetVehicleLoanPhotoEntries(long _customerLoanAccountPrmKey, string _entryType);

    }
}
