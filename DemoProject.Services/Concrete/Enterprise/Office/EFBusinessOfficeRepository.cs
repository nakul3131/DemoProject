using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Domain.Entities.Enterprise.Office;
using DemoProject.Services.ViewModel.Enterprise.Office;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Security;

namespace DemoProject.Services.Concrete.Enterprise.Office
{
    public class EFBusinessOfficeRepository : IBusinessOfficeRepository
    {
        private readonly EFDbContext context;


        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IOfficeDetailRepository officeDetailRepository;
        private readonly IBusinessOfficeDbContextRepository businessOfficeDbContextRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        //private readonly IBusinessOfficeDetailRepository businessOfficeDetailRepository;



        public EFBusinessOfficeRepository(RepositoryConnection _connection, IConfigurationDetailRepository _configurationDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository,
                IOfficeDetailRepository _officeDetailRepository, IBusinessOfficeDbContextRepository _businessOfficeDbContextRepository, ISecurityDetailRepository _securityDetailRepository, IAccountDetailRepository _accountDetailRepository)
        {
            context = _connection.EFDbContext;
            configurationDetailRepository = _configurationDetailRepository;
            officeDetailRepository = _officeDetailRepository;
            businessOfficeDbContextRepository = _businessOfficeDbContextRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            securityDetailRepository = _securityDetailRepository;

        }

        public async Task<bool> Amend(BusinessOfficeViewModel _businessOfficeViewModel)
        {
            try
            {
                bool result = true;

                // Check Entry Existance In Modification Table Or Main Table

                if (_businessOfficeViewModel.BusinessOfficeModificationPrmKey == 0)
                {
                    // BusinessOffice
                    result = businessOfficeDbContextRepository.AttachBusinessOfficeData(_businessOfficeViewModel, StringLiteralValue.Amend);
                }
                else
                {
                    // BusinessOfficeModification
                    result = businessOfficeDbContextRepository.AttachBusinessOfficeModificationData(_businessOfficeViewModel, StringLiteralValue.Amend);

                }

                // BusinessOfficeDetail
                if (result)
                    result = businessOfficeDbContextRepository.AttachBusinessOfficeDetailData(_businessOfficeViewModel.BusinessOfficeDetailViewModel, StringLiteralValue.Amend);

                // BusinessOfficeCoopRegistrationViewModel
                if (result)
                {
                    if (configurationDetailRepository.IsRegisteredUnderCooperative())
                    {
                        result = businessOfficeDbContextRepository.AttachCooprativeRegistrationData(_businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficeRBIRegistrationViewModel
                if (result)
                {
                    if (configurationDetailRepository.IsRegisteredUnderRBI())
                    {
                        result = businessOfficeDbContextRepository.AttachRBIRegistrationData(_businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel, StringLiteralValue.Amend);
                    }
                }
                // BusinessOfficePasswordPolicy - Amend Old Record
                if (result)
                {
                    IEnumerable<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelForAmend = await officeDetailRepository.GetPasswordPolicyEntries(_businessOfficeViewModel.BusinessOfficePrmKey, StringLiteralValue.Reject);
                    foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelForAmend)
                    {
                        result = businessOfficeDbContextRepository.AttachPasswordPolicyData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficePasswordPolicy - Add New Amended Entry, Get BusinessOfficePasswordPolicy Details From Session Object
                if (result)
                {
                    List<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelList = new List<BusinessOfficePasswordPolicyViewModel>();
                    businessOfficePasswordPolicyViewModelList = (List<BusinessOfficePasswordPolicyViewModel>)HttpContext.Current.Session["BusinessOfficePasswordPolicy"];
                    foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelList)
                    {
                        viewModel.PasswordPolicyPrmKey = securityDetailRepository.GetPasswordPolicyPrmKeyById(viewModel.PasswordPolicyId);

                        result = businessOfficeDbContextRepository.AttachPasswordPolicyData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeMenu - Amend Old Record
                if (result)
                {
                    IEnumerable<BusinessOfficeMenuViewModel> businessOfficeMenuViewModelForAmend = await officeDetailRepository.GetMenuEntries(_businessOfficeViewModel.BusinessOfficePrmKey, StringLiteralValue.Reject);
                    foreach (BusinessOfficeMenuViewModel viewModel in businessOfficeMenuViewModelForAmend)
                    {
                        result = businessOfficeDbContextRepository.AttachBusinessOfficeMenuData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficeMenu - Add New Amended Entry, Get BusinessOfficeMenu Details From Session Object
                if (result)
                {
                    List<BusinessOfficeMenuViewModel> businessOfficeMenuViewModelList = new List<BusinessOfficeMenuViewModel>();
                    businessOfficeMenuViewModelList = (List<BusinessOfficeMenuViewModel>)HttpContext.Current.Session["BusinessOfficeMenu"];
                    foreach (BusinessOfficeMenuViewModel viewModel in businessOfficeMenuViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachBusinessOfficeMenuData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeSpecialPermission - Amend Old Record
                if (result)
                {
                    IEnumerable<BusinessOfficeSpecialPermissionViewModel> businessOfficeSpecialPermissionViewModelForAmend = await officeDetailRepository.GetSpecialPermissionEntries(_businessOfficeViewModel.BusinessOfficePrmKey, StringLiteralValue.Reject);
                    foreach (BusinessOfficeSpecialPermissionViewModel viewModel in businessOfficeSpecialPermissionViewModelForAmend)
                    {
                        result = businessOfficeDbContextRepository.AttachSpecialPermissionData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficeSpecialPermission - Add New Amended Entry, Get BusinessOfficeSpecialPermission Details From Session Object
                if (result)
                {
                    List<BusinessOfficeSpecialPermissionViewModel> businessOfficeSpecialPermissionViewModelList = new List<BusinessOfficeSpecialPermissionViewModel>();
                    businessOfficeSpecialPermissionViewModelList = (List<BusinessOfficeSpecialPermissionViewModel>)HttpContext.Current.Session["BusinessOfficeSpecialPermission"];
                    foreach (BusinessOfficeSpecialPermissionViewModel viewModel in businessOfficeSpecialPermissionViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachSpecialPermissionData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeTransactionLimit - Amend Old Record
                if (result)
                {
                    IEnumerable<BusinessOfficeTransactionLimitViewModel> businessOfficeTransactionLimitViewModelForAmend = await officeDetailRepository.GetTransactionLimitEntries(_businessOfficeViewModel.BusinessOfficePrmKey, StringLiteralValue.Reject);
                    foreach (BusinessOfficeTransactionLimitViewModel viewModel in businessOfficeTransactionLimitViewModelForAmend)
                    {
                        result = businessOfficeDbContextRepository.AttachTransactionLimitData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficeTransactionLimit - Add New Amended Entry, Get BusinessOfficeTransactionLimit Details From Session Object
                if (result)
                {

                    List<BusinessOfficeTransactionLimitViewModel> businessOfficeTransactionLimitViewModelList = new List<BusinessOfficeTransactionLimitViewModel>();
                    businessOfficeTransactionLimitViewModelList = (List<BusinessOfficeTransactionLimitViewModel>)HttpContext.Current.Session["BusinessOfficeTransactionLimit"];
                    foreach (BusinessOfficeTransactionLimitViewModel viewModel in businessOfficeTransactionLimitViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachTransactionLimitData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeAccountNumber - Amend Old Record
                if (result)
                {
                    IEnumerable<BusinessOfficeAccountNumberViewModel> businessOfficeAccountNumberViewModelForAmend = await officeDetailRepository.GetAccountNumberEntries(_businessOfficeViewModel.BusinessOfficePrmKey, StringLiteralValue.Reject);
                    foreach (BusinessOfficeAccountNumberViewModel viewModel in businessOfficeAccountNumberViewModelForAmend)
                    {
                        result = businessOfficeDbContextRepository.AttachAccountNumberParameterData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficeAccountNumber - Add New Amended Entry, Get BusinessOfficeTransactionLimit Details From Session Object
                if (result)
                {
                    List<BusinessOfficeAccountNumberViewModel> businessOfficeAccountNumberViewModelList = new List<BusinessOfficeAccountNumberViewModel>();
                    businessOfficeAccountNumberViewModelList = (List<BusinessOfficeAccountNumberViewModel>)HttpContext.Current.Session["BusinessOfficeAccountNumber"];
                    foreach (BusinessOfficeAccountNumberViewModel viewModel in businessOfficeAccountNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachAccountNumberParameterData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeAgreementNumber - Amend Old Record
                if (result)
                {
                    IEnumerable<BusinessOfficeAgreementNumberViewModel> businessOfficeAgreementNumberViewModelForAmend = await officeDetailRepository.GetAgreementNumberEntries(_businessOfficeViewModel.BusinessOfficePrmKey, StringLiteralValue.Reject);
                    foreach (BusinessOfficeAgreementNumberViewModel viewModel in businessOfficeAgreementNumberViewModelForAmend)
                    {
                        result = businessOfficeDbContextRepository.AttachAgreementNumberParameterData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficeAgreementNumber - Add New Amended Entry, Get BusinessOfficeTransactionLimit Details From Session Object
                if (result)
                {
                    List<BusinessOfficeAgreementNumberViewModel> businessOfficeAgreementNumberViewModelList = new List<BusinessOfficeAgreementNumberViewModel>();
                    businessOfficeAgreementNumberViewModelList = (List<BusinessOfficeAgreementNumberViewModel>)HttpContext.Current.Session["BusinessOfficeAgreementNumber"];
                    foreach (BusinessOfficeAgreementNumberViewModel viewModel in businessOfficeAgreementNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachAgreementNumberParameterData(viewModel, StringLiteralValue.Create);
                    }
                }


                // BusinessOfficeApplicationNumber - Amend Old Record
                if (result)
                {
                    IEnumerable<BusinessOfficeApplicationNumberViewModel> businessOfficeApplicationNumberViewModelForAmend = await officeDetailRepository.GetApplicationNumberEntries(_businessOfficeViewModel.BusinessOfficePrmKey, StringLiteralValue.Reject);
                    foreach (BusinessOfficeApplicationNumberViewModel viewModel in businessOfficeApplicationNumberViewModelForAmend)
                    {
                        result = businessOfficeDbContextRepository.AttachApplicationNumberData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficeApplicationNumber - Add New Amended Entry, Get BusinessOfficeTransactionLimit Details From Session Object
                if (result)
                {
                    List<BusinessOfficeApplicationNumberViewModel> businessOfficeApplicationNumberViewModelList = new List<BusinessOfficeApplicationNumberViewModel>();
                    businessOfficeApplicationNumberViewModelList = (List<BusinessOfficeApplicationNumberViewModel>)HttpContext.Current.Session["BusinessOfficeApplicationNumber"];
                    foreach (BusinessOfficeApplicationNumberViewModel viewModel in businessOfficeApplicationNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachApplicationNumberData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeCurrency - Amend Old Record
                if (result)
                {
                    IEnumerable<BusinessOfficeCurrencyViewModel> businessOfficeCurrencyViewModelForAmend = await officeDetailRepository.GetCurrencyEntries(_businessOfficeViewModel.BusinessOfficePrmKey, StringLiteralValue.Reject);
                    foreach (BusinessOfficeCurrencyViewModel viewModel in businessOfficeCurrencyViewModelForAmend)
                    {
                        result = businessOfficeDbContextRepository.AttachCurrencyData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficeCurrency - Add New Amended Entry, Get BusinessOfficeCurrency Details From Session Object
                if (result)
                {
                    List<BusinessOfficeCurrencyViewModel> businessOfficeCurrencyViewModelList = new List<BusinessOfficeCurrencyViewModel>();

                    businessOfficeCurrencyViewModelList = (List<BusinessOfficeCurrencyViewModel>)HttpContext.Current.Session["BusinessOfficeCurrency"];

                    foreach (BusinessOfficeCurrencyViewModel viewModel in businessOfficeCurrencyViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachCurrencyData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeDepositCertificateNumber - Amend Old Record
                if (result)
                {
                    IEnumerable<BusinessOfficeDepositCertificateNumberViewModel> businessOfficeDepositCertificateNumberViewModelForAmend = await officeDetailRepository.GetDepositCertificateNumberEntries(_businessOfficeViewModel.BusinessOfficePrmKey, StringLiteralValue.Reject);
                    foreach (BusinessOfficeDepositCertificateNumberViewModel viewModel in businessOfficeDepositCertificateNumberViewModelForAmend)
                    {
                        result = businessOfficeDbContextRepository.AttachDepositCertificateNumberData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficeDepositCertificateNumber - Add New Amended Entry, Get BusinessOfficeDepositCertificateNumber Details From Session Object
                if (result)
                {
                    List<BusinessOfficeDepositCertificateNumberViewModel> businessOfficeDepositCertificateNumberViewModelList = new List<BusinessOfficeDepositCertificateNumberViewModel>();

                    businessOfficeDepositCertificateNumberViewModelList = (List<BusinessOfficeDepositCertificateNumberViewModel>)HttpContext.Current.Session["BusinessOfficeDepositCertificateNumber"];

                    foreach (BusinessOfficeDepositCertificateNumberViewModel viewModel in businessOfficeDepositCertificateNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachDepositCertificateNumberData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeSharesCertificateNumberViewModel
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeSharesCertificateNumberViewModel.EnableAutoSharesCertificateNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachSharesCertificateNumberData(_businessOfficeViewModel.BusinessOfficeSharesCertificateNumberViewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficePersonInformationNumberViewModel
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficePersonInformationNumberViewModel.EnableAutoPersonInformationNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachPersonInformationNumberData(_businessOfficeViewModel.BusinessOfficePersonInformationNumberViewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficePassbookNumber - Amend Old Record
                if (result)
                {
                    IEnumerable<BusinessOfficePassbookNumberViewModel> businessOfficePassbookNumberViewModelForAmend = await officeDetailRepository.GetPassbookNumberEntries(_businessOfficeViewModel.BusinessOfficePrmKey, StringLiteralValue.Reject);
                    foreach (BusinessOfficePassbookNumberViewModel viewModel in businessOfficePassbookNumberViewModelForAmend)
                    {
                        result = businessOfficeDbContextRepository.AttachPassbookNumberData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // BusinessOfficePassbookNumber - Add New Amended Entry, Get BusinessOfficePassbookNumber Details From Session Object
                if (result)
                {

                    List<BusinessOfficePassbookNumberViewModel> businessOfficePassbookNumberViewModelList = new List<BusinessOfficePassbookNumberViewModel>();

                    businessOfficePassbookNumberViewModelList = (List<BusinessOfficePassbookNumberViewModel>)HttpContext.Current.Session["BusinessOfficePassbookNumber"];

                    foreach (BusinessOfficePassbookNumberViewModel viewModel in businessOfficePassbookNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachPassbookNumberData(viewModel, StringLiteralValue.Create);
                    }
                }


                // BusinessOfficeMemberNumberViewModel
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.EnableAutoMemberNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachMemberNumberData(_businessOfficeViewModel.BusinessOfficeMemberNumberViewModel, StringLiteralValue.Amend);
                    }
                }
                // BusinessOfficeTransactionParameterViewModel   
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.EnableAutoGenerateTransactionNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachTransactionParameterData(_businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel, StringLiteralValue.Amend);
                    }
                }
                if (result)
                    result = await businessOfficeDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public bool GetUniqueBusinessOfficeName(string _nameOfBusinessOffice)
        {
            bool status;
            if (context.BusinessOffices.Where(p => p.NameOfBusinessOffice == _nameOfBusinessOffice).Select(p => p.PrmKey).FirstOrDefault() > 0)
            {
                //Already registered  
                status = false;
            }
            else
            {
                //Available to use  
                status = true;
            }

            return status;

        }

        public async Task<bool> GetSessionValues(short _businessOfficePrmKey, string _entryType)
        {
            try
            {
                HttpContext.Current.Session["BusinessOfficePasswordPolicy"] = await officeDetailRepository.GetPasswordPolicyEntries(_businessOfficePrmKey, _entryType);
                HttpContext.Current.Session["BusinessOfficeMenu"] = await officeDetailRepository.GetMenuEntries(_businessOfficePrmKey, _entryType);
                HttpContext.Current.Session["BusinessOfficeSpecialPermission"] = await officeDetailRepository.GetSpecialPermissionEntries(_businessOfficePrmKey, _entryType);
                HttpContext.Current.Session["BusinessOfficeTransactionLimit"] = await officeDetailRepository.GetTransactionLimitEntries(_businessOfficePrmKey, _entryType);
                HttpContext.Current.Session["BusinessOfficeAccountNumber"] = await officeDetailRepository.GetAccountNumberEntries(_businessOfficePrmKey, _entryType);
                HttpContext.Current.Session["BusinessOfficeAgreementNumber"] = await officeDetailRepository.GetAgreementNumberEntries(_businessOfficePrmKey, _entryType);
                HttpContext.Current.Session["BusinessOfficeApplicationNumber"] = await officeDetailRepository.GetApplicationNumberEntries(_businessOfficePrmKey, _entryType);
                HttpContext.Current.Session["BusinessOfficeCurrency"] = await officeDetailRepository.GetCurrencyEntries(_businessOfficePrmKey, _entryType);
                HttpContext.Current.Session["BusinessOfficeDepositCertificateNumber"] = await officeDetailRepository.GetDepositCertificateNumberEntries(_businessOfficePrmKey, _entryType);
                HttpContext.Current.Session["BusinessOfficePassbookNumber"] = await officeDetailRepository.GetPassbookNumberEntries(_businessOfficePrmKey, _entryType);
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public int GetCountOfBusinessOffice()
        {
            var a = context.BusinessOffices.Where(b => b.ActivationStatus == "ACT" && b.EntryStatus == "VRF").Count();
            return a;
        }

        public int GetCountOfAppConfig()
        {
            var a = context.AppConfigs.Select(n => n.NumberOfBranch).FirstOrDefault();
            return a;
        }

        public async Task<BusinessOfficeViewModel> GetBusinessOfficeEntry(Guid _businessOfficeID, string _entryType)
        {
            try
            {
                BusinessOfficeViewModel businessOfficeViewModel = await context.Database.SqlQuery<BusinessOfficeViewModel>("SELECT * FROM dbo.GetBusinessOfficeEntry (@BusinessOfficeId, @EntryType)", new SqlParameter("@BusinessOfficeId", _businessOfficeID), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                short businessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_businessOfficeID);
                //businessOfficeViewModel.BusinessOfficeCustomerNumberViewModel = await officeDetailRepository.GetCustomerNumberEntry(businessOfficePrmKey, _entryType);
                businessOfficeViewModel.BusinessOfficeDetailViewModel = await officeDetailRepository.GetBusinessOfficeDetailEntry(businessOfficePrmKey, _entryType);
                businessOfficeViewModel.BusinessOfficeMemberNumberViewModel = await officeDetailRepository.GetMemberNumberEntry(businessOfficePrmKey, _entryType);
                businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel = await officeDetailRepository.GetTransactionParameterEntry(businessOfficePrmKey, _entryType);
                businessOfficeViewModel.BusinessOfficeSharesCertificateNumberViewModel = await officeDetailRepository.GetSharesCertificateNumberEntry(businessOfficePrmKey, _entryType);
                businessOfficeViewModel.BusinessOfficePersonInformationNumberViewModel = await officeDetailRepository.GetPersonInformationNumberEntry(businessOfficePrmKey, _entryType);
                if (configurationDetailRepository.IsRegisteredUnderRBI())
                {
                    businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel = await officeDetailRepository.GetRBIRegistrationEntry(businessOfficePrmKey, _entryType);
                }
                if (configurationDetailRepository.IsRegisteredUnderCooperative())
                {
                    businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel = await officeDetailRepository.GetCoopRegistrationEntry(businessOfficePrmKey, _entryType);
                }

                return businessOfficeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<BusinessOfficeIndexViewModel>> GetBusinessOfficeIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<BusinessOfficeIndexViewModel>("SELECT * FROM dbo.GetBusinessOfficeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***     
        public async Task<bool> Modify(BusinessOfficeViewModel _businessOfficeViewModel)
        {
            try
            {
                // Set Default Value
                bool result;
                result = businessOfficeDbContextRepository.AttachBusinessOfficeData(_businessOfficeViewModel, StringLiteralValue.Modify);

                // BusinessOfficeModification
                if (result)
                    result = businessOfficeDbContextRepository.AttachBusinessOfficeModificationData(_businessOfficeViewModel, StringLiteralValue.Modify);

                // BusinessOfficeDetail
                if (result)
                    result = businessOfficeDbContextRepository.AttachBusinessOfficeDetailData(_businessOfficeViewModel.BusinessOfficeDetailViewModel, StringLiteralValue.Modify);

                // BusinessOfficeCoopRegistration
                if (result)
                {
                    if (configurationDetailRepository.IsRegisteredUnderCooperative())
                        result = businessOfficeDbContextRepository.AttachCooprativeRegistrationData(_businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel, StringLiteralValue.Modify);
                }

                // BusinessOfficeRBIRegistration
                if (result)
                {
                    if (configurationDetailRepository.IsRegisteredUnderRBI())
                        result = businessOfficeDbContextRepository.AttachRBIRegistrationData(_businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel, StringLiteralValue.Modify);
                }

                // BusinessOfficePasswordPolicy
                if (result)
                {
                    List<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelList = new List<BusinessOfficePasswordPolicyViewModel>();

                    businessOfficePasswordPolicyViewModelList = (List<BusinessOfficePasswordPolicyViewModel>)HttpContext.Current.Session["BusinessOfficePasswordPolicy"];

                    foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachPasswordPolicyData(_businessOfficeViewModel.BusinessOfficePasswordPolicyViewModel, StringLiteralValue.Modify);
                    }
                }

                // BusinessOfficeMenu
                if (result)
                {
                    List<BusinessOfficeMenuViewModel> businessOfficeMenuViewModelList = new List<BusinessOfficeMenuViewModel>();
                    businessOfficeMenuViewModelList = (List<BusinessOfficeMenuViewModel>)HttpContext.Current.Session["BusinessOfficeMenu"];
                    List<BusinessOfficeMenu> businessOfficeMenuList = new List<BusinessOfficeMenu>();

                    foreach (BusinessOfficeMenuViewModel viewModel in businessOfficeMenuViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachBusinessOfficeMenuData(_businessOfficeViewModel.BusinessOfficeMenuViewModel, StringLiteralValue.Modify);

                    }
                }

                // BusinessOfficeSpecialPermission
                if (result)
                {
                    List<BusinessOfficeSpecialPermissionViewModel> businessOfficeSpecialPermissionViewModelList = new List<BusinessOfficeSpecialPermissionViewModel>();

                    businessOfficeSpecialPermissionViewModelList = (List<BusinessOfficeSpecialPermissionViewModel>)HttpContext.Current.Session["BusinessOfficeSpecialPermission"];

                    List<BusinessOfficeSpecialPermission> businessOfficeSpecialPermissionList = new List<BusinessOfficeSpecialPermission>();

                    foreach (BusinessOfficeSpecialPermissionViewModel viewModel in businessOfficeSpecialPermissionViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachSpecialPermissionData(_businessOfficeViewModel.BusinessOfficeSpecialPermissionViewModel, StringLiteralValue.Modify);

                    }
                }

                // BusinessOfficeTransactionLimit
                if (result)
                {
                    List<BusinessOfficeTransactionLimitViewModel> businessOfficeTransactionLimitViewModelList = new List<BusinessOfficeTransactionLimitViewModel>();
                    businessOfficeTransactionLimitViewModelList = (List<BusinessOfficeTransactionLimitViewModel>)HttpContext.Current.Session["BusinessOfficeTransactionLimit"];
                    foreach (BusinessOfficeTransactionLimitViewModel viewModel in businessOfficeTransactionLimitViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachTransactionLimitData(_businessOfficeViewModel.BusinessOfficeTransactionLimitViewModel, StringLiteralValue.Modify);
                    }
                }

                // BusinessOfficeAccountNumber
                if (result)
                {
                    List<BusinessOfficeAccountNumberViewModel> businessOfficeAccountNumberViewModelList = new List<BusinessOfficeAccountNumberViewModel>();
                    businessOfficeAccountNumberViewModelList = (List<BusinessOfficeAccountNumberViewModel>)HttpContext.Current.Session["BusinessOfficeAccountNumber"];
                    foreach (BusinessOfficeAccountNumberViewModel viewModel in businessOfficeAccountNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachAccountNumberParameterData(_businessOfficeViewModel.BusinessOfficeAccountNumberViewModel, StringLiteralValue.Modify);
                    }
                }

                // BusinessOfficeAgreementNumber
                if (result)
                {
                    List<BusinessOfficeAgreementNumberViewModel> businessOfficeAgreementNumberViewModelList = new List<BusinessOfficeAgreementNumberViewModel>();
                    businessOfficeAgreementNumberViewModelList = (List<BusinessOfficeAgreementNumberViewModel>)HttpContext.Current.Session["BusinessOfficeAgreementNumber"];
                    foreach (BusinessOfficeAgreementNumberViewModel viewModel in businessOfficeAgreementNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachAgreementNumberParameterData(_businessOfficeViewModel.BusinessOfficeAgreementNumberViewModel, StringLiteralValue.Modify);
                    }
                }

                // BusinessOfficeApplicationNumber
                if (result)
                {
                    List<BusinessOfficeApplicationNumberViewModel> businessOfficeApplicationNumberViewModelList = new List<BusinessOfficeApplicationNumberViewModel>();

                    businessOfficeApplicationNumberViewModelList = (List<BusinessOfficeApplicationNumberViewModel>)HttpContext.Current.Session["BusinessOfficeApplicationNumber"];

                    List<BusinessOfficeApplicationNumber> businessOfficeApplicationNumberList = new List<BusinessOfficeApplicationNumber>();

                    foreach (BusinessOfficeApplicationNumberViewModel viewModel in businessOfficeApplicationNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachApplicationNumberData(_businessOfficeViewModel.BusinessOfficeApplicationNumberViewModel, StringLiteralValue.Modify);
                    }

                }

                // BusinessOfficeCurrency
                if (result)
                {
                    List<BusinessOfficeCurrencyViewModel> businessOfficeCurrencyViewModelList = new List<BusinessOfficeCurrencyViewModel>();
                    businessOfficeCurrencyViewModelList = (List<BusinessOfficeCurrencyViewModel>)HttpContext.Current.Session["BusinessOfficeCurrency"];
                    foreach (BusinessOfficeCurrencyViewModel viewModel in businessOfficeCurrencyViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachCurrencyData(_businessOfficeViewModel.BusinessOfficeCurrencyViewModel, StringLiteralValue.Modify);
                    }
                }

                // BusinessOfficeDepositCertificateNumber
                if (result)
                {
                    List<BusinessOfficeDepositCertificateNumberViewModel> businessOfficeDepositCertificateNumberViewModelList = new List<BusinessOfficeDepositCertificateNumberViewModel>();
                    businessOfficeDepositCertificateNumberViewModelList = (List<BusinessOfficeDepositCertificateNumberViewModel>)HttpContext.Current.Session["BusinessOfficeDepositCertificateNumber"];
                    foreach (BusinessOfficeDepositCertificateNumberViewModel viewModel in businessOfficeDepositCertificateNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachDepositCertificateNumberData(_businessOfficeViewModel.BusinessOfficeDepositCertificateNumberViewModel, StringLiteralValue.Modify);
                    }
                }
                // BusinessOfficeSharesCertificateNumberViewModel
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeSharesCertificateNumberViewModel.EnableAutoSharesCertificateNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachSharesCertificateNumberData(_businessOfficeViewModel.BusinessOfficeSharesCertificateNumberViewModel, StringLiteralValue.Modify);
                    }
                }

                // BusinessOfficePersonInformationNumberViewModel
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficePersonInformationNumberViewModel.EnableAutoPersonInformationNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachPersonInformationNumberData(_businessOfficeViewModel.BusinessOfficePersonInformationNumberViewModel, StringLiteralValue.Modify);
                    }
                }
                // BusinessOfficePassbookNumber
                if (result)
                {
                    List<BusinessOfficePassbookNumberViewModel> businessOfficePassbookNumberViewModelList = new List<BusinessOfficePassbookNumberViewModel>();
                    businessOfficePassbookNumberViewModelList = (List<BusinessOfficePassbookNumberViewModel>)HttpContext.Current.Session["BusinessOfficePassbookNumber"];
                    foreach (BusinessOfficePassbookNumberViewModel viewModel in businessOfficePassbookNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachPassbookNumberData(_businessOfficeViewModel.BusinessOfficePassbookNumberViewModel, StringLiteralValue.Modify);
                    }
                }


                // BusinessOfficeMemberNumber
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.EnableAutoMemberNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachMemberNumberData(_businessOfficeViewModel.BusinessOfficeMemberNumberViewModel, StringLiteralValue.Modify);
                    }
                }
                // BusinessOfficeTransactionParameterViewModel   
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.EnableAutoGenerateTransactionNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachTransactionParameterData(_businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel, StringLiteralValue.Modify);
                    }
                }

                if (result)
                    result = await businessOfficeDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;

            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }


        public async Task<bool> VerifyRejectDelete(BusinessOfficeViewModel _businessOfficeViewModel, string _entryType)
        {
            try
            {
                // Set Default Value  

                bool result = true;
                string entriesType;

                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                    entriesType = StringLiteralValue.Unverified;
                else
                    entriesType = StringLiteralValue.Reject;
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeModificationPrmKey == 0)
                        result = businessOfficeDbContextRepository.AttachBusinessOfficeData(_businessOfficeViewModel, _entryType);
                    else
                        result = businessOfficeDbContextRepository.AttachBusinessOfficeModificationData(_businessOfficeViewModel, _entryType);
                }

                // BusinessOfficeDetail
                if (result)
                    result = businessOfficeDbContextRepository.AttachBusinessOfficeDetailData(_businessOfficeViewModel.BusinessOfficeDetailViewModel, _entryType);


                //BusinessOfficeCoopRegistration
                if (result)
                {
                    if (configurationDetailRepository.IsRegisteredUnderCooperative())
                        result = businessOfficeDbContextRepository.AttachCooprativeRegistrationData(_businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel, _entryType);
                }

                // BusinessOfficeRBIRegistration
                if (result)
                {
                    if (configurationDetailRepository.IsRegisteredUnderRBI())
                        result = businessOfficeDbContextRepository.AttachRBIRegistrationData(_businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel, _entryType);
                }

                // BusinessOfficePasswordPolicy
                if (result)
                {
                    IEnumerable<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelList = await officeDetailRepository.GetPasswordPolicyEntries(_businessOfficeViewModel.BusinessOfficePrmKey, entriesType);
                    foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachPasswordPolicyData(viewModel, _entryType);
                    }
                }

                // BusinessOfficeMenu
                if (result)
                {
                    IEnumerable<BusinessOfficeMenuViewModel> businessOfficeMenuViewModelList = await officeDetailRepository.GetMenuEntries(_businessOfficeViewModel.BusinessOfficePrmKey, entriesType);
                    foreach (BusinessOfficeMenuViewModel viewModel in businessOfficeMenuViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachBusinessOfficeMenuData(viewModel, _entryType);
                    }
                }

                // BusinessOfficeSpecialPermission
                if (result)
                {
                    IEnumerable<BusinessOfficeSpecialPermissionViewModel> businessOfficeSpecialPermissionViewModelList = await officeDetailRepository.GetSpecialPermissionEntries(_businessOfficeViewModel.BusinessOfficePrmKey, entriesType);
                    foreach (BusinessOfficeSpecialPermissionViewModel viewModel in businessOfficeSpecialPermissionViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachSpecialPermissionData(viewModel, _entryType);
                    }
                }

                // BusinessOfficeTransactionLimit
                if (result)
                {
                    IEnumerable<BusinessOfficeTransactionLimitViewModel> businessOfficeTransactionLimitViewModelList = await officeDetailRepository.GetTransactionLimitEntries(_businessOfficeViewModel.BusinessOfficePrmKey, entriesType);
                    foreach (BusinessOfficeTransactionLimitViewModel viewModel in businessOfficeTransactionLimitViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachTransactionLimitData(viewModel, _entryType);
                    }
                }


                // BusinessOfficeAccountNumber
                if (result)
                {
                    IEnumerable<BusinessOfficeAccountNumberViewModel> businessOfficeAccountNumberViewModelList = await officeDetailRepository.GetAccountNumberEntries(_businessOfficeViewModel.BusinessOfficePrmKey, entriesType);
                    foreach (BusinessOfficeAccountNumberViewModel viewModel in businessOfficeAccountNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachAccountNumberParameterData(viewModel, _entryType);
                    }
                }

                 // BusinessOfficeAgreementNumber
                if (result)
                {
                    IEnumerable<BusinessOfficeAgreementNumberViewModel> businessOfficeAgreementNumberViewModelList = await officeDetailRepository.GetAgreementNumberEntries(_businessOfficeViewModel.BusinessOfficePrmKey, entriesType);
                    foreach (BusinessOfficeAgreementNumberViewModel viewModel in businessOfficeAgreementNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachAgreementNumberParameterData(viewModel, _entryType);
                    }
                }

                // BusinessOfficeApplicationNumber
                if (result)
                {
                    IEnumerable<BusinessOfficeApplicationNumberViewModel> businessOfficeApplicationNumberViewModelList = await officeDetailRepository.GetApplicationNumberEntries(_businessOfficeViewModel.BusinessOfficePrmKey, entriesType);
                    foreach (BusinessOfficeApplicationNumberViewModel viewModel in businessOfficeApplicationNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachApplicationNumberData(viewModel, _entryType);
                    }
                }

                // BusinessOfficeCurrency
                if (result)
                {
                    IEnumerable<BusinessOfficeCurrencyViewModel> businessOfficeCurrencyViewModelList = await officeDetailRepository.GetCurrencyEntries(_businessOfficeViewModel.BusinessOfficePrmKey, entriesType);
                    foreach (BusinessOfficeCurrencyViewModel viewModel in businessOfficeCurrencyViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachCurrencyData(viewModel, _entryType);
                    }
                }

                // BusinessOfficeDepositCertificateNumber
                if (result)
                {
                    IEnumerable<BusinessOfficeDepositCertificateNumberViewModel> businessOfficeDepositCertificateNumberViewModelList = await officeDetailRepository.GetDepositCertificateNumberEntries(_businessOfficeViewModel.BusinessOfficePrmKey, entriesType);
                    foreach (BusinessOfficeDepositCertificateNumberViewModel viewModel in businessOfficeDepositCertificateNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachDepositCertificateNumberData(viewModel, _entryType);
                    }
                }

                // BusinessOfficeSharesCertificateNumberViewModel
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeSharesCertificateNumberViewModel.EnableAutoSharesCertificateNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachSharesCertificateNumberData(_businessOfficeViewModel.BusinessOfficeSharesCertificateNumberViewModel, _entryType);
                    }
                }

                // BusinessOfficePersonInformationNumberViewModel
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficePersonInformationNumberViewModel.EnableAutoPersonInformationNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachPersonInformationNumberData(_businessOfficeViewModel.BusinessOfficePersonInformationNumberViewModel, _entryType);
                    }
                }

                // BusinessOfficePassbookNumber
                if (result)
                {
                    IEnumerable<BusinessOfficePassbookNumberViewModel> businessOfficePassbookNumberViewModelList = await officeDetailRepository.GetPassbookNumberEntries(_businessOfficeViewModel.BusinessOfficePrmKey, entriesType);
                    foreach (BusinessOfficePassbookNumberViewModel viewModel in businessOfficePassbookNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachPassbookNumberData(viewModel, _entryType);
                    }
                }


                // BusinessOfficeMemberNumber
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.EnableAutoMemberNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachMemberNumberData(_businessOfficeViewModel.BusinessOfficeMemberNumberViewModel, _entryType);
                    }
                }
                // BusinessOfficeTransactionParameterViewModel   
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.EnableAutoGenerateTransactionNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachTransactionParameterData(_businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel, _entryType);
                    }
                }

                if (result)
                    result = await businessOfficeDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(BusinessOfficeViewModel _businessOfficeViewModel)
        {
            try
            {
                bool result;

                result = businessOfficeDbContextRepository.AttachBusinessOfficeData(_businessOfficeViewModel, StringLiteralValue.Create);

                // BusinessOfficeDetail
                if (result)
                    result = businessOfficeDbContextRepository.AttachBusinessOfficeDetailData(_businessOfficeViewModel.BusinessOfficeDetailViewModel, StringLiteralValue.Create);

                // BusinessOfficeCoopRegistrationViewModel
                if (result)
                {
                    if (configurationDetailRepository.IsRegisteredUnderCooperative())
                        result = businessOfficeDbContextRepository.AttachCooprativeRegistrationData(_businessOfficeViewModel.BusinessOfficeCoopRegistrationViewModel, StringLiteralValue.Create);
                }

                // BusinessOfficeRBIRegistrationViewModel
                if (result)
                {
                    if (configurationDetailRepository.IsRegisteredUnderRBI())
                        result = businessOfficeDbContextRepository.AttachRBIRegistrationData(_businessOfficeViewModel.BusinessOfficeRBIRegistrationViewModel, StringLiteralValue.Create);

                }

                // BusinessOfficePasswordPolicyViewModel
                if (result)
                {
                    List<BusinessOfficePasswordPolicyViewModel> businessOfficePasswordPolicyViewModelList = new List<BusinessOfficePasswordPolicyViewModel>();
                    businessOfficePasswordPolicyViewModelList = (List<BusinessOfficePasswordPolicyViewModel>)HttpContext.Current.Session["BusinessOfficePasswordPolicy"];
                    foreach (BusinessOfficePasswordPolicyViewModel viewModel in businessOfficePasswordPolicyViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachPasswordPolicyData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeMenuViewModel
                if (result)
                {
                    List<BusinessOfficeMenuViewModel> businessOfficeMenuViewModelList = new List<BusinessOfficeMenuViewModel>();
                    businessOfficeMenuViewModelList = (List<BusinessOfficeMenuViewModel>)HttpContext.Current.Session["BusinessOfficeMenu"];
                    foreach (BusinessOfficeMenuViewModel viewModel in businessOfficeMenuViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachBusinessOfficeMenuData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeSpecialPermissionViewModel
                if (result)
                {
                    List<BusinessOfficeSpecialPermissionViewModel> businessOfficeSpecialPermissionViewModelList = new List<BusinessOfficeSpecialPermissionViewModel>();
                    businessOfficeSpecialPermissionViewModelList = (List<BusinessOfficeSpecialPermissionViewModel>)HttpContext.Current.Session["BusinessOfficeSpecialPermission"];
                    foreach (BusinessOfficeSpecialPermissionViewModel viewModel in businessOfficeSpecialPermissionViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachSpecialPermissionData(viewModel, StringLiteralValue.Create);
                    }

                }

                // BusinessOfficeTransactionLimitViewModel
                if (result)
                {
                    List<BusinessOfficeTransactionLimitViewModel> businessOfficeTransactionLimitViewModelList = new List<BusinessOfficeTransactionLimitViewModel>();
                    businessOfficeTransactionLimitViewModelList = (List<BusinessOfficeTransactionLimitViewModel>)HttpContext.Current.Session["BusinessOfficeTransactionLimit"];
                    foreach (BusinessOfficeTransactionLimitViewModel viewModel in businessOfficeTransactionLimitViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachTransactionLimitData(viewModel, StringLiteralValue.Create);
                    }

                }

                // BusinessOfficeAccountNumberViewModel
                if (result)
                {
                    List<BusinessOfficeAccountNumberViewModel> businessOfficeAccountNumberViewModelList = new List<BusinessOfficeAccountNumberViewModel>();
                    businessOfficeAccountNumberViewModelList = (List<BusinessOfficeAccountNumberViewModel>)HttpContext.Current.Session["BusinessOfficeAccountNumber"];
                    foreach (BusinessOfficeAccountNumberViewModel viewModel in businessOfficeAccountNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachAccountNumberParameterData(viewModel, StringLiteralValue.Create);
                    }
                }

                 // BusinessOfficeAgreementNumberViewModel
                if (result)
                {
                    List<BusinessOfficeAgreementNumberViewModel> businessOfficeAgreementNumberViewModelList = new List<BusinessOfficeAgreementNumberViewModel>();
                    businessOfficeAgreementNumberViewModelList = (List<BusinessOfficeAgreementNumberViewModel>)HttpContext.Current.Session["BusinessOfficeAgreementNumber"];
                    foreach (BusinessOfficeAgreementNumberViewModel viewModel in businessOfficeAgreementNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachAgreementNumberParameterData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeApplicationNumberViewModel
                if (result)
                {
                    List<BusinessOfficeApplicationNumberViewModel> businessOfficeApplicationNumberViewModelList = new List<BusinessOfficeApplicationNumberViewModel>();
                    businessOfficeApplicationNumberViewModelList = (List<BusinessOfficeApplicationNumberViewModel>)HttpContext.Current.Session["BusinessOfficeApplicationNumber"];
                    foreach (BusinessOfficeApplicationNumberViewModel viewModel in businessOfficeApplicationNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachApplicationNumberData(viewModel, StringLiteralValue.Create);
                    }

                }

                // BusinessOfficeCurrencyViewModel
                if (result)
                {
                    List<BusinessOfficeCurrencyViewModel> businessOfficeCurrencyViewModelList = new List<BusinessOfficeCurrencyViewModel>();
                    businessOfficeCurrencyViewModelList = (List<BusinessOfficeCurrencyViewModel>)HttpContext.Current.Session["BusinessOfficeCurrency"];
                    foreach (BusinessOfficeCurrencyViewModel viewModel in businessOfficeCurrencyViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachCurrencyData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeDepositCertificateNumberViewModel
                if (result)
                {
                    List<BusinessOfficeDepositCertificateNumberViewModel> businessOfficeDepositCertificateNumberViewModelList = new List<BusinessOfficeDepositCertificateNumberViewModel>();
                    businessOfficeDepositCertificateNumberViewModelList = (List<BusinessOfficeDepositCertificateNumberViewModel>)HttpContext.Current.Session["BusinessOfficeDepositCertificateNumber"];
                    foreach (BusinessOfficeDepositCertificateNumberViewModel viewModel in businessOfficeDepositCertificateNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachDepositCertificateNumberData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeSharesCertificateNumberViewModel
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeSharesCertificateNumberViewModel.EnableAutoSharesCertificateNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachSharesCertificateNumberData(_businessOfficeViewModel.BusinessOfficeSharesCertificateNumberViewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficePersonInformationNumberViewModel
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficePersonInformationNumberViewModel.EnableAutoPersonInformationNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachPersonInformationNumberData(_businessOfficeViewModel.BusinessOfficePersonInformationNumberViewModel, StringLiteralValue.Create);
                    }
                }
                // BusinessOfficePassbookNumberViewModel
                if (result)
                {
                    List<BusinessOfficePassbookNumberViewModel> businessOfficePassbookNumberViewModelList = new List<BusinessOfficePassbookNumberViewModel>();
                    businessOfficePassbookNumberViewModelList = (List<BusinessOfficePassbookNumberViewModel>)HttpContext.Current.Session["BusinessOfficePassbookNumber"];
                    foreach (BusinessOfficePassbookNumberViewModel viewModel in businessOfficePassbookNumberViewModelList)
                    {
                        result = businessOfficeDbContextRepository.AttachPassbookNumberData(viewModel, StringLiteralValue.Create);
                    }
                }

                // BusinessOfficeMemberNumberViewModel
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeMemberNumberViewModel.EnableAutoMemberNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachMemberNumberData(_businessOfficeViewModel.BusinessOfficeMemberNumberViewModel, StringLiteralValue.Create);
                    }
                }
                // BusinessOfficeTransactionParameterViewModel   
                if (result)
                {
                    if (_businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel.EnableAutoGenerateTransactionNumber == true)
                    {
                        result = businessOfficeDbContextRepository.AttachTransactionParameterData(_businessOfficeViewModel.BusinessOfficeTransactionParameterViewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                    result = await businessOfficeDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}
