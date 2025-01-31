using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Enterprise.Office;

namespace DemoProject.Services.Abstract.Enterprise.Office
{
    public interface IOfficeDetailRepository
    {

        Task<BusinessOfficeDetailViewModel> GetBusinessOfficeDetailEntry(short _businessOfficePrmKey, string _entryType);

        Task<BusinessOfficeCoopRegistrationViewModel> GetCoopRegistrationEntry(short _businessOfficePrmKey, string _entryType);

        Task<BusinessOfficeCustomerNumberViewModel> GetCustomerNumberEntry(short _businessOfficePrmKey, string _entryType);

        Task<BusinessOfficeMemberNumberViewModel> GetMemberNumberEntry(short _businessOfficePrmKey, string _entryType);

        Task<BusinessOfficePasswordPolicyViewModel> GetPasswordPolicyEntry(short _businessOfficePrmKey, string _entryType);

        Task<BusinessOfficeRBIRegistrationViewModel> GetRBIRegistrationEntry(short _businessOfficePrmKey, string _entryType);

        Task<BusinessOfficeTransactionParameterViewModel> GetTransactionParameterEntry(short _businessOfficePrmKey, string _entryType);

        Task<BusinessOfficeSharesCertificateNumberViewModel> GetSharesCertificateNumberEntry(short _businessOfficePrmKey, string _entryType);

        Task<BusinessOfficePersonInformationNumberViewModel> GetPersonInformationNumberEntry(short _businessOfficePrmKey, string _entryType);

        Task<IEnumerable<BusinessOfficeAccountNumberViewModel>> GetAccountNumberEntries(short _businessOfficePrmKey, string _entryType);
         Task<IEnumerable<BusinessOfficeAgreementNumberViewModel>> GetAgreementNumberEntries(short _businessOfficePrmKey, string _entryType);

        Task<IEnumerable<BusinessOfficeApplicationNumberViewModel>> GetApplicationNumberEntries(short _businessOfficePrmKey, string _entryType);

        Task<IEnumerable<BusinessOfficeCurrencyViewModel>> GetCurrencyEntries(short _businessOfficePrmKey, string _entryType);

        Task<IEnumerable<BusinessOfficeDepositCertificateNumberViewModel>> GetDepositCertificateNumberEntries(short _businessOfficePrmKey, string _entryType);

        Task<IEnumerable<BusinessOfficePassbookNumberViewModel>> GetPassbookNumberEntries(short _businessOfficePrmKey, string _entryType);

        Task<IEnumerable<BusinessOfficeMenuViewModel>> GetMenuEntries(short _businessOfficePrmKey, string _entryType);

        Task<IEnumerable<BusinessOfficePasswordPolicyViewModel>> GetPasswordPolicyEntries(short _businessOfficePrmKey, string _entryType);

        Task<IEnumerable<BusinessOfficeSpecialPermissionViewModel>> GetSpecialPermissionEntries(short _businessOfficePrmKey, string _entryType);

        Task<IEnumerable<BusinessOfficeTransactionLimitViewModel>> GetTransactionLimitEntries(short _businessOfficePrmKey, string _entryType);






        Task<IEnumerable<BusinessOfficeViewModel>> GetCooperativeEntriesForOperation(string _entryType);

        Task<IEnumerable<BusinessOfficeViewModel>> GetBusinessOfficeDetailEntriesForOperation(string _entryType);

        Task<IEnumerable<BusinessOfficeViewModel>> GetRBIRegistrationEntriesForOperation(string _entryType);

        Task<IEnumerable<BusinessOfficeViewModel>> GetPasswordPolicyEntriesForOperation(string _entryType);


        void GetBusinessOfficeAllDefaultValues(BusinessOfficeViewModel _businessOfficeViewModel, string _entryStatus);


        
    }
}
