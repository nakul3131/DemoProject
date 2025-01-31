using DemoProject.Services.ViewModel.Enterprise.Office;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Enterprise.Office
{
    public interface IBusinessOfficeDbContextRepository
    {
        bool AttachBusinessOfficeData(BusinessOfficeViewModel _businessOfficeViewModel, string _entryType);

        bool AttachBusinessOfficeModificationData(BusinessOfficeViewModel _businessOfficeViewModel, string _entryType);

        bool AttachBusinessOfficeDetailData(BusinessOfficeDetailViewModel _businessOfficeDetail, string _entryType);

        bool AttachCooprativeRegistrationData(BusinessOfficeCoopRegistrationViewModel _businessOfficeCoopRegistrationViewModel, string _entryType);

        bool AttachPasswordPolicyData(BusinessOfficePasswordPolicyViewModel _businessOfficePasswordPolicyViewModel, string _entryType);

        bool AttachBusinessOfficeMenuData(BusinessOfficeMenuViewModel _businessOfficeMenuViewModel, string _entryType);

        bool AttachSpecialPermissionData(BusinessOfficeSpecialPermissionViewModel _businessOfficeSpecialPermissionViewModel, string _entryType);

        bool AttachTransactionLimitData(BusinessOfficeTransactionLimitViewModel _businessOfficeTransactionLimitViewModel, string _entryType);

        bool AttachAccountNumberParameterData(BusinessOfficeAccountNumberViewModel _businessOfficeAccountNumberViewModel, string _entryType);
        bool AttachAgreementNumberParameterData(BusinessOfficeAgreementNumberViewModel _businessOfficeAgreementNumberViewModel, string _entryType);

        bool AttachApplicationNumberData(BusinessOfficeApplicationNumberViewModel _businessOfficeApplicationNumberViewModel, string _entryType);

        bool AttachCurrencyData(BusinessOfficeCurrencyViewModel _businessOfficeCurrencyViewModel, string _entryType);

        bool AttachDepositCertificateNumberData(BusinessOfficeDepositCertificateNumberViewModel _businessOfficeDepositCertificateNumberViewModel, string _entryType);

        bool AttachSharesCertificateNumberData(BusinessOfficeSharesCertificateNumberViewModel _businessOfficeSharesCertificateNumberViewModel, string _entryType);

        bool AttachPersonInformationNumberData(BusinessOfficePersonInformationNumberViewModel _businessOfficePersonInformationNumberViewModel, string _entryType);

        bool AttachPassbookNumberData(BusinessOfficePassbookNumberViewModel _businessOfficePassbookNumberViewModel, string _entryType);

        bool AttachCustomerNumberData(BusinessOfficeCustomerNumberViewModel _businessOfficeCustomerNumberViewModel, string _entryType);

        bool AttachMemberNumberData(BusinessOfficeMemberNumberViewModel _businessOfficeMemberNumberViewModel, string _entryType);

        bool AttachTransactionParameterData(BusinessOfficeTransactionParameterViewModel _businessOfficeTransactionParameterViewModel, string _entryType);

        bool AttachRBIRegistrationData(BusinessOfficeRBIRegistrationViewModel _businessOfficeRBIRegistrationViewModel, string _entryType);

        Task<bool> SaveData();

    }
}
