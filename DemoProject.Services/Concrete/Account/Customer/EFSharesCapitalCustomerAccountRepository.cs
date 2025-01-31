using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Constants;
using DemoProject.Services.Utility.SmsSender;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFSharesCapitalCustomerAccountRepository : ISharesCapitalCustomerAccountRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ICustomerAccountDbContextRepository customerAccountDbContextRepository;
        private readonly ICustomerDetailRepository customerDetailRepository;
        private readonly IDepositSchemeParameterRepository depositSchemeParameterRepository;
        private readonly ISchemeDetailRepository schemeDetailRepository;

        SmsSender sms = new SmsSender();

        public EFSharesCapitalCustomerAccountRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IConfigurationDetailRepository _configurationDetailRepository, ISchemeDetailRepository _schemeDetailRepository, ICustomerDetailRepository _customerDetailRepository, ICustomerAccountDbContextRepository _customerAccountDbContextRepository, IDepositSchemeParameterRepository _depositSchemeParameterRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            schemeDetailRepository = _schemeDetailRepository;
            customerDetailRepository = _customerDetailRepository;
            customerAccountDbContextRepository = _customerAccountDbContextRepository;
            depositSchemeParameterRepository = _depositSchemeParameterRepository;
        }


        public long GetPrmKeyById(Guid _customerAccountId)
        {
            var a = context.CustomerAccounts
                    .Where(c => c.CustomerAccountId == _customerAccountId)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return a;
        }


        public async Task<bool> Amend(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel)
        {
            try
            {
                // Get Input Visibility & Other Configuration
                CustomerSharesAccountOpeningConfigViewModel customerSharesAccountOpeningConfigViewModel = await schemeDetailRepository.GetSharesCapitalSchemeConfigDetail(_sharesCapitalCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

                // If AlternateAccountNumber2 Setting In SchemeAccountParameter
                if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 == true)
                {
                    _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber2 = _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber2;
                }
                else
                {
                    _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber2 = "None";
                }

                // If AlternateAccountNumber3 Setting In SchemeAccountParameter
                if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 == true)
                {
                    _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber3 = _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber3;
                }
                else
                {
                    _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber3 = _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber3 = "None";
                }

                // Set PassbookNumber Value
                _sharesCapitalCustomerAccountViewModel.PassbookNumber = _sharesCapitalCustomerAccountViewModel.PassbookNumber = 0;

                // If MemberNumber Setting In SchemeSharesCapitalAccountParameter 
                if (customerSharesAccountOpeningConfigViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableAutoMemberNumber == false)
                {
                    _sharesCapitalCustomerAccountViewModel.CustomerSharesCapitalAccountViewModel.MemberNumber = _sharesCapitalCustomerAccountViewModel.CustomerSharesCapitalAccountViewModel.MemberNumber;
                }
                else
                {
                    _sharesCapitalCustomerAccountViewModel.CustomerSharesCapitalAccountViewModel.MemberNumber = _sharesCapitalCustomerAccountViewModel.CustomerSharesCapitalAccountViewModel.MemberNumber = 0;
                }

                bool result;

                result = customerAccountDbContextRepository.AttachSharesCapitalCustomerAccountData(_sharesCapitalCustomerAccountViewModel, StringLiteralValue.Amend);

                if (result)
                    result = customerAccountDbContextRepository.AttachCustomerSharesCapitalAccountData(_sharesCapitalCustomerAccountViewModel.CustomerSharesCapitalAccountViewModel, StringLiteralValue.Amend);

                if (result)
                    result = customerAccountDbContextRepository.AttachCustomerAccountDetailData(_sharesCapitalCustomerAccountViewModel.CustomerAccountDetailViewModel, StringLiteralValue.Amend);

                if (result)
                {
                    if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == true)
                        result = customerAccountDbContextRepository.AttachCustomerAccountSmsServiceData(_sharesCapitalCustomerAccountViewModel.CustomerAccountSmsServiceViewModel, StringLiteralValue.Amend);
                }

                if (result)
                {
                    if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                        result = customerAccountDbContextRepository.AttachCustomerAccountEmailServiceData(_sharesCapitalCustomerAccountViewModel.CustomerAccountEmailServiceViewModel, StringLiteralValue.Amend);
                }

                // Old Entry Amended For CustomerJointAccount 
                IEnumerable<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelListForAmend = await customerDetailRepository.GetJointAccountHolderEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (customerJointAccountHolderViewModelListForAmend != null)
                {
                    foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Entry Created For CustomerJointAccountHolder 
                if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumJointAccountHolder != 0)
                {
                    // Insert New Updated Record - Get Record From Session Object
                    List<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelList = new List<CustomerJointAccountHolderViewModel>();

                    customerJointAccountHolderViewModelList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["CustomerJointAccountHolder"];

                    if (customerJointAccountHolderViewModelList != null)
                    {
                        foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // Old Entry Amended For CustomerAccountNominee
                IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelListForAmend = await customerDetailRepository.GetNomineeEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (customerAccountNomineeViewModelListForAmend != null)
                {
                    foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelListForAmend)
                    {
                        if (viewModel.NomineeAge < 18)
                            viewModel.CustomerAccountNomineeGuardianViewModel = customerDetailRepository.GetNomineeGuardianEntry(viewModel.CustomerAccountNomineePrmKey, StringLiteralValue.Reject);

                        result = customerAccountDbContextRepository.AttachCustomerAccountNomineeData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Entry Created For CustomerAccountNominee 
                if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumNominee != 0)
                {
                    List<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = new List<CustomerAccountNomineeViewModel>();
                    customerAccountNomineeViewModelList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];

                    if (customerAccountNomineeViewModelList != null)
                    {
                        foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountNomineeData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // Old Entry Amended For PersonAddress 
                IEnumerable<PersonAddressViewModel> personAddressViewModellListForAmend = await customerDetailRepository.GetPersonAddressDetailEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (personAddressViewModellListForAmend != null)
                {
                    foreach (PersonAddressViewModel viewModel in personAddressViewModellListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // Old Entry Amended For CustomerAccountAddressDetail
                IEnumerable<PersonAddressViewModel> customerAccountAddressDetailList = await customerDetailRepository.GetCustomerAccountAddressDetailEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (customerAccountAddressDetailList != null)
                {
                    foreach (PersonAddressViewModel viewModel in customerAccountAddressDetailList)
                    {
                        result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Entry Created For PersonAddress And CustomerAccountAddressDetail
                if (result)
                {
                    List<PersonAddressViewModel> personAddressViewModelList = new List<PersonAddressViewModel>();
                    personAddressViewModelList = (List<PersonAddressViewModel>)HttpContext.Current.Session["CustomerAccountAddressDetail"];

                    if (personAddressViewModelList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelList)
                        {
                            viewModel.CustomerAccountPrmKey = _sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey;
                            result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // Old Entry Amended For PersonContactDetail 
                IEnumerable<PersonContactDetailViewModel> personContactDetailViewModelListForAmend = await customerDetailRepository.GetPersonContactDetailEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (personContactDetailViewModelListForAmend != null)
                {
                    foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // Old Entry Amended For CustomerAccountContactDetail 
                IEnumerable<PersonContactDetailViewModel> customerAccountContactDetailList = await customerDetailRepository.GetCustomerAccountContactDetailEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (customerAccountContactDetailList != null)
                {
                    foreach (PersonContactDetailViewModel viewModel in customerAccountContactDetailList)
                    {
                        result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Entry Created For PersonContactDetail And CustomerAccountContactDetail
                if (result)
                {
                    List<PersonContactDetailViewModel> personContactDetailViewModelList = new List<PersonContactDetailViewModel>();
                    personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["CustomerAccountContactDetail"];

                    if (personContactDetailViewModelList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                        {
                            viewModel.CustomerAccountPrmKey = _sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey;
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // Old Entry Amended For CustomerAccountTurnOverLimit 
                IEnumerable<CustomerAccountTurnOverLimitViewModel> customerAccountTurnOverLimitViewModelListForAmend = await customerDetailRepository.GetTurnOverLimitEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (customerAccountTurnOverLimitViewModelListForAmend != null)
                {
                    foreach (CustomerAccountTurnOverLimitViewModel viewModel in customerAccountTurnOverLimitViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountTurnOverLimitData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Entry Created For PersonContactDetail And CustomerAccountTurnOverLimit 
                if (_sharesCapitalCustomerAccountViewModel.EnableTurnOverLimit == true)
                {
                    if (result)
                    {
                        List<CustomerAccountTurnOverLimitViewModel> customerAccountTurnOverLimitViewModelList = new List<CustomerAccountTurnOverLimitViewModel>();
                        customerAccountTurnOverLimitViewModelList = (List<CustomerAccountTurnOverLimitViewModel>)HttpContext.Current.Session["CustomerAccountTurnOverLimit"];

                        if (customerAccountTurnOverLimitViewModelList != null)
                        {
                            foreach (CustomerAccountTurnOverLimitViewModel viewModel in customerAccountTurnOverLimitViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerAccountTurnOverLimitData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // CustomerAccountNoticeSchedule
                // Old Record Amended For Amened 
                IEnumerable<CustomerAccountNoticeScheduleViewModel> CustomerAccountNoticeScheduleViewModelListForAmend = await customerDetailRepository.GetNoticeScheduleEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (CustomerAccountNoticeScheduleViewModelListForAmend.Count() > 0)
                {
                    foreach (CustomerAccountNoticeScheduleViewModel viewModel in CustomerAccountNoticeScheduleViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    List<CustomerAccountNoticeScheduleViewModel> customerAccountNoticeScheduleViewModelList = new List<CustomerAccountNoticeScheduleViewModel>();
                    customerAccountNoticeScheduleViewModelList = (List<CustomerAccountNoticeScheduleViewModel>)HttpContext.Current.Session["CustomerAccountNoticeSchedule"];

                    if (customerAccountNoticeScheduleViewModelList != null)
                    {
                        foreach (CustomerAccountNoticeScheduleViewModel viewModel in customerAccountNoticeScheduleViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, StringLiteralValue.Create);
                        }
                    }

                }

                if (result)
                    result = await customerAccountDbContextRepository.SaveData();

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

        public async Task<bool> GetSessionValues(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel, string _entryType)
        {
            try
            {
                HttpContext.Current.Session["CustomerAccountAddressDetail"] = await customerDetailRepository.GetCustomerAccountAddressDetailEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["CustomerAccountContactDetail"] = await customerDetailRepository.GetCustomerAccountContactDetailEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["CustomerJointAccountHolder"] = await customerDetailRepository.GetJointAccountHolderEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["CustomerAccountNominee"] = await customerDetailRepository.GetNomineeEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = await customerDetailRepository.GetNomineeEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);
                foreach (CustomerAccountNomineeViewModel customerAccountNomineeViewModel in customerAccountNomineeViewModelList)
                {
                    customerAccountNomineeViewModel.CustomerAccountNomineeGuardianViewModelList = customerDetailRepository.GetNomineeGuardianEntries(customerAccountNomineeViewModel.PrmKey, _entryType);
                }

                HttpContext.Current.Session["CustomerAccountNominee"] = customerAccountNomineeViewModelList;

                HttpContext.Current.Session["CustomerAccountTurnOverLimit"] = await customerDetailRepository.GetTurnOverLimitEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["CustomerAccountNoticeSchedule"] = await customerDetailRepository.GetCustomerAccountNoticeScheduleEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> IsValidAccountNumber(Guid _schemeId, int _accountNumber)
        {
            try
            {
                var s = (short)HttpContext.Current.Session["UserHomeBranchPrmKey"];
                short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);
                var a = await context.Database.SqlQuery<bool>("SELECT dbo.IsValidAccountNumber (@SchemePrmKey, @BusinessOfficePrmKey, @AccountNumber)", new SqlParameter("@SchemePrmKey", schemePrmKey), new SqlParameter("@BusinessOfficePrmKey", (short)HttpContext.Current.Session["UserHomeBranchPrmKey"]), new SqlParameter("AccountNumber", _accountNumber)).FirstOrDefaultAsync();

                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> IsValidMemberNumber(Guid _schemeId, int _memberNumber)
        {
            try
            {
                short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);

                return await context.Database.SqlQuery<bool>("SELECT dbo.IsValidMemberNumber (@SchemePrmKey, @BusinessOfficePrmKey, @MemberNumber)", new SqlParameter("@SchemePrmKey", schemePrmKey), new SqlParameter("@BusinessOfficePrmKey", (short)HttpContext.Current.Session["UserHomeBranchPrmKey"]), new SqlParameter("MemberNumber", _memberNumber)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> IsVisibleAccountNumber(Guid _schemeId)
        {
            try
            {
                short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);

                return await context.SchemeCustomerAccountNumbers
                                    .Where(a => a.SchemePrmKey == schemePrmKey && a.EntryStatus == StringLiteralValue.Verify)
                                    .Select(a => a.EnableCustomizeAccountNumber).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> IsVisibleMemberNumber(Guid _schemeId)
        {
            try
            {
                short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);

                return await context.SchemeCustomerAccountNumbers
                                    .Where(a => a.SchemePrmKey == schemePrmKey && a.EntryStatus == StringLiteralValue.Verify)
                                    .Select(a => a.EnableCustomizeAccountNumber).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel)
        {
            try
            {
                CustomerSharesAccountOpeningConfigViewModel customerSharesAccountOpeningConfigViewModel = await schemeDetailRepository.GetSharesCapitalSchemeConfigDetail(_sharesCapitalCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

                // If AlternateAccountNumber2 Setting In SchemeAccountParameter
                if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 == false)
                    _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber2 = _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber2 = "None";

                // If AlternateAccountNumber3 Setting In SchemeAccountParameter
                if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 == false)
                    _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber3 = _sharesCapitalCustomerAccountViewModel.AlternateAccountNumber3 = "None";

                _sharesCapitalCustomerAccountViewModel.PassbookNumber = 0;

                // If MemberNumber Setting In SchemeSharesCapitalAccountParameter 
                if (customerSharesAccountOpeningConfigViewModel.SchemeSharesCapitalAccountParameterViewModel.EnableAutoMemberNumber == true)
                    _sharesCapitalCustomerAccountViewModel.CustomerSharesCapitalAccountViewModel.MemberNumber = 0;

                bool result;

                result = customerAccountDbContextRepository.AttachSharesCapitalCustomerAccountData(_sharesCapitalCustomerAccountViewModel, StringLiteralValue.Create);

                if (result)
                    result = customerAccountDbContextRepository.AttachCustomerSharesCapitalAccountData(_sharesCapitalCustomerAccountViewModel.CustomerSharesCapitalAccountViewModel, StringLiteralValue.Create);

                if (result)
                    result = customerAccountDbContextRepository.AttachCustomerAccountDetailData(_sharesCapitalCustomerAccountViewModel.CustomerAccountDetailViewModel, StringLiteralValue.Create);

                if (result)
                {
                    if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == true)
                        result = customerAccountDbContextRepository.AttachCustomerAccountSmsServiceData(_sharesCapitalCustomerAccountViewModel.CustomerAccountSmsServiceViewModel, StringLiteralValue.Create);
                }

                if (result)
                {
                    if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                        result = customerAccountDbContextRepository.AttachCustomerAccountEmailServiceData(_sharesCapitalCustomerAccountViewModel.CustomerAccountEmailServiceViewModel, StringLiteralValue.Create);
                }

                if (result)
                {
                    if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumJointAccountHolder != 0)
                    {
                        List<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelList = new List<CustomerJointAccountHolderViewModel>();
                        customerJointAccountHolderViewModelList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["CustomerJointAccountHolder"];

                        if (customerJointAccountHolderViewModelList != null)
                        {
                            foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                if (result)
                {
                    if (customerSharesAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumNominee != 0)
                    {
                        List<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = new List<CustomerAccountNomineeViewModel>();
                        customerAccountNomineeViewModelList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];

                        if (customerAccountNomineeViewModelList != null)
                        {
                            foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerAccountNomineeData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                if (result)
                {
                    List<PersonAddressViewModel> personAddressViewModelList = new List<PersonAddressViewModel>();
                    personAddressViewModelList = (List<PersonAddressViewModel>)HttpContext.Current.Session["CustomerAccountAddressDetail"];

                    if (personAddressViewModelList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                if (result)
                {
                    List<PersonContactDetailViewModel> personContactDetailViewModelList = new List<PersonContactDetailViewModel>();
                    personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["CustomerAccountContactDetail"];

                    if (personContactDetailViewModelList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                if (result)
                {
                    List<CustomerAccountTurnOverLimitViewModel> customerAccountTurnOverLimitViewModelList = new List<CustomerAccountTurnOverLimitViewModel>();
                    customerAccountTurnOverLimitViewModelList = (List<CustomerAccountTurnOverLimitViewModel>)HttpContext.Current.Session["CustomerAccountTurnOverLimit"];

                    if (customerAccountTurnOverLimitViewModelList != null)
                    {
                        foreach (CustomerAccountTurnOverLimitViewModel viewModel in customerAccountTurnOverLimitViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountTurnOverLimitData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // CustomerAccountNoticeSchedule
                if (result)
                {
                    List<CustomerAccountNoticeScheduleViewModel> customerAccountNoticeScheduleViewModelList = new List<CustomerAccountNoticeScheduleViewModel>();
                    customerAccountNoticeScheduleViewModelList = (List<CustomerAccountNoticeScheduleViewModel>)HttpContext.Current.Session["CustomerAccountNoticeSchedule"];

                    if (customerAccountNoticeScheduleViewModelList != null)
                    {
                        foreach (CustomerAccountNoticeScheduleViewModel viewModel in customerAccountNoticeScheduleViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }


                if (result)
                    result = await customerAccountDbContextRepository.SaveData();

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

        public async Task<bool> VerifyRejectDelete(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel, string _entryType)
        {
            try
            {
                string entriesType;
                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                    entriesType = StringLiteralValue.Unverified;
                else
                    entriesType = StringLiteralValue.Reject;
                bool result;
                result = customerAccountDbContextRepository.AttachSharesCapitalCustomerAccountData(_sharesCapitalCustomerAccountViewModel, _entryType);

                // CustomerSharesCapitalAccount 
                CustomerSharesCapitalAccountViewModel customerSharesCapitalAccountViewModel = await customerDetailRepository.GetSharesCapitalAccountEntry(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (customerSharesCapitalAccountViewModel != null)
                {
                    result = customerAccountDbContextRepository.AttachCustomerSharesCapitalAccountData(_sharesCapitalCustomerAccountViewModel.CustomerSharesCapitalAccountViewModel, _entryType);
                }

                // CustomerAccountDetail 
                CustomerAccountDetailViewModel customerAccountDetailViewModel = await customerDetailRepository.GetAccountDetailEntry(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (customerAccountDetailViewModel != null)
                {
                    result = customerAccountDbContextRepository.AttachCustomerAccountDetailData(_sharesCapitalCustomerAccountViewModel.CustomerAccountDetailViewModel, _entryType);
                }

                // CustomerAccountSmsService 
                CustomerAccountSmsServiceViewModel customerAccountSmsServiceViewModel = await customerDetailRepository.GetSmsServiceEntry(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (customerAccountSmsServiceViewModel != null)
                {
                    result = customerAccountDbContextRepository.AttachCustomerAccountSmsServiceData(_sharesCapitalCustomerAccountViewModel.CustomerAccountSmsServiceViewModel, _entryType);
                }

                // CustomerAccountEmailService 
                CustomerAccountEmailServiceViewModel customerAccountEmailServiceViewModel = await customerDetailRepository.GetEmailServiceEntry(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (customerAccountEmailServiceViewModel != null)
                {
                    result = customerAccountDbContextRepository.AttachCustomerAccountEmailServiceData(_sharesCapitalCustomerAccountViewModel.CustomerAccountEmailServiceViewModel, _entryType);
                }

                IEnumerable<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelList = await customerDetailRepository.GetJointAccountHolderEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (customerJointAccountHolderViewModelList != null)
                {
                    foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelList)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, _entryType);
                    }
                }

                // CustomerAccountNominee And CustomerAccountNomineeGuardian
                IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = await customerDetailRepository.GetNomineeEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (customerAccountNomineeViewModelList != null)
                {
                    foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                    {
                        if (viewModel.NomineeAge < 18)
                            viewModel.CustomerAccountNomineeGuardianViewModel = customerDetailRepository.GetNomineeGuardianEntry(viewModel.CustomerAccountNomineePrmKey, entriesType);

                        result = customerAccountDbContextRepository.AttachCustomerAccountNomineeData(viewModel, _entryType);
                    }
                }

                // PersonAddress
                IEnumerable<PersonAddressViewModel> personAddressViewModellList = await customerDetailRepository.GetPersonAddressDetailEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (personAddressViewModellList != null)
                {
                    foreach (PersonAddressViewModel viewModel in personAddressViewModellList)
                    {
                        result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, _entryType);
                    }
                }

                // CustomerAccountAddressDetail
                IEnumerable<PersonAddressViewModel> customerAccountAddressDetailList = await customerDetailRepository.GetCustomerAccountAddressDetailEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (customerAccountAddressDetailList != null)
                {
                    foreach (PersonAddressViewModel viewModel in customerAccountAddressDetailList)
                    {
                        result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, _entryType);
                    }
                }

                // PersonContactDetail 
                IEnumerable<PersonContactDetailViewModel> personContactDetailViewModelList = await customerDetailRepository.GetPersonContactDetailEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (personContactDetailViewModelList != null)
                {
                    foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                    {
                        result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, _entryType);
                    }
                }

                // CustomerAccountContactDetail 
                IEnumerable<PersonContactDetailViewModel> customerAccountContactDetailList = await customerDetailRepository.GetCustomerAccountContactDetailEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (customerAccountContactDetailList != null)
                {
                    foreach (PersonContactDetailViewModel viewModel in customerAccountContactDetailList)
                    {
                        result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, _entryType);
                    }
                }

                // CustomerAccountTurnOverLimit 
                IEnumerable<CustomerAccountTurnOverLimitViewModel> customerAccountTurnOverLimitViewModelList = await customerDetailRepository.GetTurnOverLimitEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (customerAccountTurnOverLimitViewModelList != null)
                {
                    foreach (CustomerAccountTurnOverLimitViewModel viewModel in customerAccountTurnOverLimitViewModelList)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountTurnOverLimitData(viewModel, _entryType);
                    }
                }


                // CustomerAccountNoticeSchedule
                if (result)
                {
                    // Get SchemeNoticeSchedule Details From Session Object
                    IEnumerable<CustomerAccountNoticeScheduleViewModel> customerAccountNoticeScheduleViewModelList = await customerDetailRepository.GetNoticeScheduleEntries(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountNoticeScheduleViewModelList != null)
                    {
                        foreach (CustomerAccountNoticeScheduleViewModel viewModel in customerAccountNoticeScheduleViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, _entryType);
                        }
                    }
                }

                result = await customerAccountDbContextRepository.SaveData();

                if (result)
                {
                    // Verify SMS Alert Send to Customer
                    string response = sms.SendMembershipApprovalSms(_sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey).Result;
                    return true;
                }

                else
                    return false;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<SharesCapitalCustomerAccountIndexViewModel>> GetSharesCapitalCustomerAccountIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalCustomerAccountIndexViewModel>("SELECT * FROM dbo.GetCustomerSharesCapitalAccountEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesCapitalCustomerAccountViewModel> GetSharesCapitalCustomerAccountEntry(Guid _customerAccountId, string _entryType)
        {
            try
            {
                SharesCapitalCustomerAccountViewModel sharesCapitalCustomerAccountViewModel = await context.Database.SqlQuery<SharesCapitalCustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerSharesCapitalAccountEntry (@CustomerAccountId, @EntryType)", new SqlParameter("@CustomerAccountId", _customerAccountId), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                //Get Prmkey By ID
                long _customerAccountPrmKey = GetPrmKeyById(_customerAccountId);

                // CustomerAccountDetail
                sharesCapitalCustomerAccountViewModel.CustomerAccountDetailViewModel = await customerDetailRepository.GetAccountDetailEntry(_customerAccountPrmKey, _entryType);

                // CustomerSharesCapitalAccount
                sharesCapitalCustomerAccountViewModel.CustomerSharesCapitalAccountViewModel = await customerDetailRepository.GetSharesCapitalAccountEntry(_customerAccountPrmKey, _entryType);

                //CustomerAccountEmail
                sharesCapitalCustomerAccountViewModel.CustomerAccountEmailServiceViewModel = await customerDetailRepository.GetEmailServiceEntry(_customerAccountPrmKey, _entryType);

                //CustomerAccountSMS
                sharesCapitalCustomerAccountViewModel.CustomerAccountSmsServiceViewModel = await customerDetailRepository.GetSmsServiceEntry(_customerAccountPrmKey, _entryType);

                return sharesCapitalCustomerAccountViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesCapitalCustomerAccountViewModel> GetUnVerifiedEntry(Guid _customerAccountId)
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalCustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerSharesCapitalAccountEntry (@CustomerAccountId, @EntryType)", new SqlParameter("@CustomerAccountId", _customerAccountId), new SqlParameter("EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesCapitalCustomerAccountViewModel> GetVerifiedEntry(Guid _customerAccountId)
        {
            try
            {
                return await context.Database.SqlQuery<SharesCapitalCustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerSharesCapitalAccountEntry (@CustomerAccountId, @EntryType)", new SqlParameter("@CustomerAccountId", _customerAccountId), new SqlParameter("EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }



        public List<SelectListItem> SharesCapitalCustomerAccountDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.CustomerAccounts
                            join mf in context.CustomerAccountModifications on d.PrmKey equals mf.CustomerAccountPrmKey into bm
                            from mf in bm.DefaultIfEmpty()
                                //join t in context.CustomerAccountTranslations on d.PrmKey equals t.SharesCapitalCustomerAccountPrmKey into bt
                                //from t in bt.DefaultIfEmpty()
                            where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    //&& (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    //                            && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                                    || (d.EntryStatus == StringLiteralValue.Verify)
                            //&& (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                            //                            && (d.IsModified.Equals(false))
                            //&& (t.LanguagePrmKey == regionalLanguagePrmKey)
                            //                    orderby d.NameOfSharesCapitalCustomerAccount
                            select new SelectListItem
                            {
                                Value = d.CustomerAccountId.ToString(),
                                Text = ""//((mf.NameOfSharesCapitalCustomerAccount.Equals(null)) ? d.NameOfSharesCapitalCustomerAccount.Trim() + " ---> " + (t.TransNameOfSharesCapitalCustomerAccount.Equals(null) ? " " : t.TransNameOfSharesCapitalCustomerAccount.Trim()) : mf.NameOfSharesCapitalCustomerAccount + " ---> " + (t.TransNameOfSharesCapitalCustomerAccount.Equals(null) ? " " : t.TransNameOfSharesCapitalCustomerAccount.Trim()))
                            }).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.CustomerAccounts
                        join mf in context.CustomerAccountModifications on d.PrmKey equals mf.CustomerAccountPrmKey into bm
                        from mf in bm.DefaultIfEmpty()
                        where (d.EntryStatus.Equals(StringLiteralValue.Verify) && d.ActivationStatus.Equals(StringLiteralValue.Active)
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null)))
                        select new SelectListItem
                        {
                            Value = d.CustomerAccountId.ToString(),
                            Text = "" //((mf.NameOfCustomerAccount.Equals(null)) ? d.NameOfSharesCapitalCustomerAccount.Trim() : mf.NameOfSharesCapitalCustomerAccount)
                        }).ToList();
            }
        }

        public List<SelectListItem> SharesCapitalCustomerAccountDropdownListForEmployeeCatgory
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from d in context.CustomerAccounts
                            join mf in context.CustomerAccountModifications on d.PrmKey equals mf.CustomerAccountPrmKey into dm
                            from mf in dm.DefaultIfEmpty()

                            where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                                    //                                    && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    //                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    //                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                    //                                    && (d.CustomerAccountCategory.Equals("EMP"))
                                    || (d.EntryStatus == StringLiteralValue.Verify)
                            //                                    && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                            //                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                            //                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                            //&& (d.IsModified.Equals(false))
                            //&& (d.SharesCapitalCustomerAccountCategory.Equals("EMP"))
                            //                            orderby d.NameOfSharesCapitalCustomerAccount
                            select new SelectListItem
                            {
                                Value = d.CustomerAccountId.ToString(),
                                Text = "" //((mf.NameOfSharesCapitalCustomerAccount.Equals(null)) ? d.NameOfSharesCapitalCustomerAccount.Trim() + " ---> " + (t.TransNameOfSharesCapitalCustomerAccount.Equals(null) ? " " : t.TransNameOfSharesCapitalCustomerAccount.Trim()) : mf.NameOfSharesCapitalCustomerAccount + " ---> " + (t.TransNameOfSharesCapitalCustomerAccount.Equals(null) ? " " : t.TransNameOfSharesCapitalCustomerAccount.Trim()))
                            }).ToList();
                }

                // Default List In Defaul Language (i.e. English)
                return (from d in context.CustomerAccounts
                        join mf in context.CustomerAccountModifications on d.PrmKey equals mf.CustomerAccountPrmKey into dm
                        from mf in dm.DefaultIfEmpty()
                        where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                                //                                && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                || (d.EntryStatus == StringLiteralValue.Verify)
                        //                                && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                        //                                && (d.IsModified.Equals(false))
                        //                        orderby d.NameOfSharesCapitalCustomerAccount
                        select new SelectListItem
                        {
                            Value = d.CustomerAccountId.ToString(),
                            Text = "" //((mf.NameOfSharesCapitalCustomerAccount.Equals(null)) ? d.NameOfSharesCapitalCustomerAccount.Trim() : mf.NameOfSharesCapitalCustomerAccount.Trim())
                        }).ToList();
            }
        }









    }
}
