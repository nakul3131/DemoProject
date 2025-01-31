using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Constants;
using DemoProject.Services.Utility.SmsSender;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerDepositAccountRepository : ICustomerDepositAccountRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ICustomerDetailRepository customerDetailRepository;
        private readonly ISchemeDetailRepository schemeDetailRepository;
        private readonly ICustomerAccountDbContextRepository customerAccountDbContextRepository;
        SmsSender sms = new SmsSender();

        public EFCustomerDepositAccountRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, ICustomerDetailRepository _customerDetailRepository, ISchemeDetailRepository _schemeDetailRepository, ICustomerAccountDbContextRepository _customerAccountDbContextRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            customerDetailRepository = _customerDetailRepository;
            customerAccountDbContextRepository = _customerAccountDbContextRepository;
            schemeDetailRepository = _schemeDetailRepository;
        }

        public async Task<bool> Amend(DepositCustomerAccountViewModel _depositCustomerAccountViewModel)
        {
            try
            {
                bool result;

                // Get Input Visibility & Other Configuration
                CustomerDepositAccountOpeningConfigViewModel customerDepositAccountOpeningConfigViewModel = await schemeDetailRepository.GetDepositSchemeConfigDetail(_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

                //  AlternateAccountNumber2 
                if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 == false)
                    _depositCustomerAccountViewModel.AlternateAccountNumber2 = _depositCustomerAccountViewModel.AlternateAccountNumber2 = "None";

                // AlternateAccountNumber3 
                if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 == false)
                    _depositCustomerAccountViewModel.AlternateAccountNumber3 = _depositCustomerAccountViewModel.AlternateAccountNumber3 = "None";

                // PassbookNumber 
                if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == false || customerDepositAccountOpeningConfigViewModel.SchemePassbookViewModel.EnableAutoPassbookNumber == true)
                    _depositCustomerAccountViewModel.PassbookNumber = 0;

                result = customerAccountDbContextRepository.AttachDepositCustomerAccountData(_depositCustomerAccountViewModel, StringLiteralValue.Amend);

                // Customer Account Detail
                if (result)
                    result = customerAccountDbContextRepository.AttachCustomerAccountDetailData(_depositCustomerAccountViewModel.CustomerAccountDetailViewModel, StringLiteralValue.Amend);

                // Customer Deposit Account
                if (result)
                    result = customerAccountDbContextRepository.AttachCustomerDepositAccountData(_depositCustomerAccountViewModel.CustomerDepositAccountViewModel, StringLiteralValue.Amend);

                // Interst Rate
                if (result)
                {
                    _depositCustomerAccountViewModel.CustomerAccountInterestRateViewModel.EffectiveDate = _depositCustomerAccountViewModel.AccountOpeningDate;
                    result = customerAccountDbContextRepository.AttachCustomerAccountInterestRateData(_depositCustomerAccountViewModel.CustomerAccountInterestRateViewModel, StringLiteralValue.Amend);
                }

                // Old Entry Amended For PersonAddress 
                IEnumerable<PersonAddressViewModel> personAddressViewModellListForAmend = await customerDetailRepository.GetPersonAddressDetailEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (personAddressViewModellListForAmend != null)
                {
                    foreach (PersonAddressViewModel viewModel in personAddressViewModellListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // Old Entry Amended For CustomerAccountAddressDetail
                IEnumerable<PersonAddressViewModel> customerAccountAddressDetailList = await customerDetailRepository.GetCustomerAccountAddressDetailEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

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
                    List<PersonAddressViewModel> personAddressViewModelList = (List<PersonAddressViewModel>)HttpContext.Current.Session["PersonAddress"];

                    if (personAddressViewModelList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // Old Entry Amended For PersonContactDetail 
                IEnumerable<PersonContactDetailViewModel> personContactDetailViewModelListForAmend = await customerDetailRepository.GetPersonContactDetailEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (personContactDetailViewModelListForAmend != null)
                {
                    foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // Old Entry Amended For CustomerAccountContactDetail 
                IEnumerable<PersonContactDetailViewModel> customerAccountContactDetailList = await customerDetailRepository.GetCustomerAccountContactDetailEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

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
                    List<PersonContactDetailViewModel> personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["PersonContactDetail"];

                    if (personContactDetailViewModelList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // Old Entry Amended For CustomerJointAccount 
                IEnumerable<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelListForAmend = await customerDetailRepository.GetJointAccountHolderEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (customerJointAccountHolderViewModelListForAmend != null)
                {
                    foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Entry Created For CustomerJointAccountHolder 
                if (result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumJointAccountHolder != 0)
                    {
                        List<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["CustomerJointAccountHolder"];

                        if (customerJointAccountHolderViewModelList != null)
                        {
                            foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // Old Entry Amended For CustomerAccountNominee
                IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelListForAmend = await customerDetailRepository.GetNomineeEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

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
                if (result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumNominee != 0)
                    {
                        List<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];

                        if (customerAccountNomineeViewModelList != null)
                        {
                            foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerAccountNomineeData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // Email
                if (result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                        result = customerAccountDbContextRepository.AttachCustomerAccountEmailServiceData(_depositCustomerAccountViewModel.CustomerAccountEmailServiceViewModel, StringLiteralValue.Amend);
                }

                // SMS
                if (result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == true)
                        result = customerAccountDbContextRepository.AttachCustomerAccountSmsServiceData(_depositCustomerAccountViewModel.CustomerAccountSmsServiceViewModel, StringLiteralValue.Amend);
                }

                // CustomerAccountNoticeSchedule
                // Old Record Amended For Amened 
                IEnumerable<CustomerAccountNoticeScheduleViewModel> CustomerAccountNoticeScheduleViewModelListForAmend = await customerDetailRepository.GetNoticeScheduleEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (CustomerAccountNoticeScheduleViewModelListForAmend != null)
                {
                    foreach (CustomerAccountNoticeScheduleViewModel viewModel in CustomerAccountNoticeScheduleViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    List<CustomerAccountNoticeScheduleViewModel> customerAccountNoticeScheduleViewModelList = (List<CustomerAccountNoticeScheduleViewModel>)HttpContext.Current.Session["CustomerAccountNoticeSchedule"];

                    if (customerAccountNoticeScheduleViewModelList != null)
                    {
                        foreach (CustomerAccountNoticeScheduleViewModel viewModel in customerAccountNoticeScheduleViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, StringLiteralValue.Create);
                        }
                    }

                }

                // Cheque Detail
                if (result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableChequeBook == true)
                        result = customerAccountDbContextRepository.AttachCustomerAccountChequeDetailData(_depositCustomerAccountViewModel.CustomerAccountChequeDetailViewModel, StringLiteralValue.Amend);
                }

                // Old Entry Amended For CustomerAccountTurnOverLimit 
                IEnumerable<CustomerAccountTurnOverLimitViewModel> customerAccountTurnOverLimitViewModelListForAmend = await customerDetailRepository.GetTurnOverLimitEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (customerAccountTurnOverLimitViewModelListForAmend != null)
                {
                    foreach (CustomerAccountTurnOverLimitViewModel viewModel in customerAccountTurnOverLimitViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountTurnOverLimitData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Entry Created For CustomerAccountTurnOverLimit 
                if (result)
                {
                    if (_depositCustomerAccountViewModel.EnableTurnOverLimit == true)
                    {
                        List<CustomerAccountTurnOverLimitViewModel> customerAccountTurnOverLimitViewModelList = (List<CustomerAccountTurnOverLimitViewModel>)HttpContext.Current.Session["CustomerAccountTurnOverLimit"];

                        if (customerAccountTurnOverLimitViewModelList != null)
                        {
                            foreach (CustomerAccountTurnOverLimitViewModel viewModel in customerAccountTurnOverLimitViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerAccountTurnOverLimitData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // Old Entry Amended For CustomerDepositAccountAgent 
                IEnumerable<CustomerDepositAccountAgentViewModel> customerDepositAccountAgentViewModelListForAmend = await customerDetailRepository.GetDepositAccountAgentEntries(_depositCustomerAccountViewModel.CustomerDepositAccountViewModel.CustomerDepositAccountPrmKey, StringLiteralValue.Reject);

                if (customerDepositAccountAgentViewModelListForAmend != null)
                {
                    foreach (CustomerDepositAccountAgentViewModel viewModel in customerDepositAccountAgentViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerDepositAccountAgentData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Entry Created For CustomerDepositAccountAgent 
                if (result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeDepositAccountParameterViewModel.EnableAgent == true)
                    {
                        List<CustomerDepositAccountAgentViewModel> customerDepositAccountAgentViewModelList = (List<CustomerDepositAccountAgentViewModel>)HttpContext.Current.Session["CustomerDepositAccountAgent"];

                        if (customerDepositAccountAgentViewModelList != null)
                        {
                            foreach (CustomerDepositAccountAgentViewModel viewModel in customerDepositAccountAgentViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerDepositAccountAgentData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }

                }

                // DEMAND DEPOSIT TYPE
                if (result)
                {
                    if (_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.DepositType == StringLiteralValue.DemandDeposit)
                    {
                        // Sweep Details
                        if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableSweepIn == true)
                            result = customerAccountDbContextRepository.AttachCustomerAccountSweepDetailData(_depositCustomerAccountViewModel.CustomerAccountSweepDetailViewModel, StringLiteralValue.Amend);
                        
                        //PhotoSign
                        if (result)
                        {
                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoSign == true)
                            {
                                if (result)
                                {
                                    if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentUpload != StringLiteralValue.Disable || customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.SignDocumentUpload != StringLiteralValue.Disable)
                                    {
                                        CustomerAccountPhotoSignViewModel customerAccountPhotoSignViewModelForAmend = await customerDetailRepository.GetPhotoSignEntry(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                                        if (_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoPath != null || _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPath != null)
                                        {
                                            customerAccountPhotoSignViewModelForAmend.PhotoPath = _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoPath;
                                            customerAccountPhotoSignViewModelForAmend.SignPath = _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPath;

                                            //EnablePhotoDocumentUploadInLocalStorage
                                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInLocalStorage == true)
                                            {
                                                if (_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoPath != null)
                                                    result = customerAccountDbContextRepository.AttachPhotoDocumentInLocalStorage(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentLocalStoragePath, customerAccountPhotoSignViewModelForAmend, StringLiteralValue.Amend);
                                                else
                                                {
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoNameOfFile = customerAccountPhotoSignViewModelForAmend.PhotoNameOfFile;
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoLocalStoragePath = customerAccountPhotoSignViewModelForAmend.PhotoLocalStoragePath;
                                                }
                                            }
                                            else

                                                result = customerAccountDbContextRepository.AttachPhotoDocumentInDatabaseStorage(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, customerAccountPhotoSignViewModelForAmend, StringLiteralValue.Amend);

                                            //EnableSignDocumentUploadInLocalStorage
                                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableSignDocumentUploadInLocalStorage == true)
                                            {
                                                if (_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPath != null)
                                                    result = customerAccountDbContextRepository.AttachSignDocumentInLocalStorage(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.SignDocumentLocalStoragePath, customerAccountPhotoSignViewModelForAmend, StringLiteralValue.Amend);
                                                else
                                                {
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignNameOfFile = customerAccountPhotoSignViewModelForAmend.SignNameOfFile;
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignLocalStoragePath = customerAccountPhotoSignViewModelForAmend.SignLocalStoragePath;
                                                }
                                            }
                                            else
                                                result = customerAccountDbContextRepository.AttachSignDocumentInDatabaseStorage(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, customerAccountPhotoSignViewModelForAmend, StringLiteralValue.Amend);


                                            //CustomerAccountPhotoSignData
                                            result = customerAccountDbContextRepository.AttachCustomerAccountPhotoSignData(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, StringLiteralValue.Amend);

                                        }

                                        else
                                            result = customerAccountDbContextRepository.AttachCustomerAccountPhotoSignData(customerAccountPhotoSignViewModelForAmend, StringLiteralValue.Amend);
                                    }
                                }
                            }
                        }


                        // Old Entry Amended For CustomerAccountBeneficiaryDetail 
                        IEnumerable<CustomerAccountBeneficiaryDetailViewModel> customerAccountBeneficiaryDetailViewModelListForAmend = await customerDetailRepository.GetBeneficiaryDetailEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                        if (customerAccountBeneficiaryDetailViewModelListForAmend != null)
                        {
                            foreach (CustomerAccountBeneficiaryDetailViewModel viewModel in customerAccountBeneficiaryDetailViewModelListForAmend)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerAccountBeneficiaryDetailData(viewModel, StringLiteralValue.Amend);
                            }
                        }

                        // New Entry Created For Customer Account Beneficiary Detail View Model List 
                        if (result)
                        {
                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableBeneficiaryDetail == true)
                            {
                                List<CustomerAccountBeneficiaryDetailViewModel> customerAccountBeneficiaryDetailViewModelList = (List<CustomerAccountBeneficiaryDetailViewModel>)HttpContext.Current.Session["CustomerAccountBeneficiaryDetail"];

                                if (customerAccountBeneficiaryDetailViewModelList != null)
                                {
                                    foreach (CustomerAccountBeneficiaryDetailViewModel viewModel in customerAccountBeneficiaryDetailViewModelList)
                                    {
                                        result = customerAccountDbContextRepository.AttachCustomerAccountBeneficiaryDetailData(viewModel, StringLiteralValue.Create);
                                    }
                                }
                            }
                        }

                        // Old Entry Amended For CustomerAccountReferencePersonDetail 
                        IEnumerable<CustomerAccountReferencePersonDetailViewModel> customerAccountReferencePersonDetailViewModelListForAmend = await customerDetailRepository.GetReferencePersonEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                        if (customerAccountReferencePersonDetailViewModelListForAmend != null)
                        {
                            foreach (CustomerAccountReferencePersonDetailViewModel viewModel in customerAccountReferencePersonDetailViewModelListForAmend)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerAccountReferencePersonDetailData(viewModel, StringLiteralValue.Amend);
                            }
                        }

                        // New Entry Created For CustomerAccountReferencePersonDetailViewModel 
                        if (result)
                        {
                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableReferencePersonDetail == true)
                            {
                                List<CustomerAccountReferencePersonDetailViewModel> customerAccountReferencePersonDetailViewModelList = (List<CustomerAccountReferencePersonDetailViewModel>)HttpContext.Current.Session["CustomerAccountReferencePersonDetail"];

                                if (customerAccountReferencePersonDetailViewModelList != null)
                                {
                                    foreach (CustomerAccountReferencePersonDetailViewModel viewModel in customerAccountReferencePersonDetailViewModelList)
                                    {
                                        result = customerAccountDbContextRepository.AttachCustomerAccountReferencePersonDetailData(viewModel, StringLiteralValue.Create);
                                    }
                                }
                            }
                        }
                    }
                }

                // FIXED DEPOSIT
                if (result)
                {
                    if (_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.DepositType == StringLiteralValue.FixedDeposit)
                    {
                        // Periodic Interest Payout Validation
                        if (customerDepositAccountOpeningConfigViewModel.SchemeDepositInterestParameterViewModel.EnablePeriodicInterestPayout == false || _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.TotalInterestAmount < 1200)
                        {
                            _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.InterestPayoutFrequency = StringLiteralValue.AtMaturity;
                            _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.InterestPayoutAmount = 0;
                            _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.InterestPayoutDay = 0;
                        }

                        // Maturity Instruction Validation
                        if (_depositCustomerAccountViewModel.CustomerDepositAccountViewModel.EnableAutoCloseOnMaturity == true || _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.EnableAutoRenewOnMaturity == true)
                            _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.MaturityInstruction = StringLiteralValue.DoNotRenew;

                        result = customerAccountDbContextRepository.AttachCustomerTermDepositAccountDetailData(_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel, StringLiteralValue.Amend);
                    }
                }

                // Old Entry Amended For Standing Instruction 
                IEnumerable<CustomerAccountStandingInstructionViewModel> customerAccountStandingInstructionViewModelListForAmend = await customerDetailRepository.GetStandingInstructionEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (customerAccountStandingInstructionViewModelListForAmend != null)
                {
                    foreach (CustomerAccountStandingInstructionViewModel viewModel in customerAccountStandingInstructionViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(viewModel, StringLiteralValue.Amend, viewModel.InstructionFor);
                    }
                }

                // Standing Insturction
                if(result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableStandingInstruction == true)
                    {
                        // Auto Debit
                        if (_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel.EnableAutoDebit == true)
                            result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Create, StringLiteralValue.DebitAccount);

                        // Auto Close
                        if (_depositCustomerAccountViewModel.CustomerDepositAccountViewModel.EnableAutoCloseOnMaturity == true)
                        {
                            // Credit
                            result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Create, StringLiteralValue.CreditAccount);

                            // Credit Interest
                            result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Create, StringLiteralValue.CreditInterestAccount);
                        }

                        // Auto Renew - Renew Only Principal i.e. Credit Interest Account
                        if (_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.EnableAutoRenewOnMaturity == true && accountDetailRepository.GetRenewTypeSysNameById(_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.RenewTypeId) == "Principal")
                            result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Create, StringLiteralValue.CreditInterestAccount);
                    }
                }

                // Document *** Review And Uncomment
                // Old Entry Amended For CustomerAccountDocument 
                IEnumerable<CustomerAccountDocumentViewModel> customerAccountDocumentViewModelListForAmend = await customerDetailRepository.GetDocumentEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (customerAccountDocumentViewModelListForAmend != null)
                {
                    //foreach (CustomerAccountDocumentViewModel viewModel in customerAccountDocumentViewModelListForAmend)
                    //{
                    //    result = customerAccountDbContextRepository.AttachCustomerAccountDocumentData(viewModel, StringLiteralValue.Amend);
                    //}
                }

                // New Entry Created For CustomerAccountDocument 
                if (result)
                {
                    //List<CustomerAccountDocumentViewModel> customerAccountDocumentViewModelList = (List<CustomerAccountDocumentViewModel>)HttpContext.Current.Session["CustomerAccountDocument"];

                    //if (customerAccountDocumentViewModelList != null)
                    //{
                    //    foreach (CustomerAccountDocumentViewModel viewModel in customerAccountDocumentViewModelList)
                    //    {
                    //        result = customerAccountDbContextRepository.AttachCustomerAccountDocumentData(viewModel, StringLiteralValue.Create);
                    //    }
                    //}
                }

                // Final Method
                if (result)
                    result = await customerAccountDbContextRepository.SaveData();

                if (result)
                {
                    //Delete Old Image When New Image Uploaded Or Deleted Existing Image when PhotoUpload is Optional.
                    result = customerAccountDbContextRepository.DeletePhotoOfDeletedRecord();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> GetSessionValues(DepositCustomerAccountViewModel depositCustomerAccountViewModel, string _entryType)
        {
            try
            {
                CustomerDepositAccountViewModel customerDepositAccountViewModel = await customerDetailRepository.GetDepositAccountEntry(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["CustomerJointAccountHolder"] = await customerDetailRepository.GetJointAccountHolderEntries(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = await customerDetailRepository.GetNomineeEntries(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                foreach (CustomerAccountNomineeViewModel customerAccountNomineeViewModel in customerAccountNomineeViewModelList)
                {
                    customerAccountNomineeViewModel.CustomerAccountNomineeGuardianViewModelList = customerDetailRepository.GetNomineeGuardianEntries(customerAccountNomineeViewModel.CustomerAccountNomineePrmKey, _entryType);
                }

                HttpContext.Current.Session["CustomerAccountNominee"] = customerAccountNomineeViewModelList;

                if (depositCustomerAccountViewModel.CustomerDepositAccountViewModel != null)
                    HttpContext.Current.Session["CustomerDepositAccountAgent"] = await customerDetailRepository.GetDepositAccountAgentEntries(depositCustomerAccountViewModel.CustomerDepositAccountViewModel.CustomerDepositAccountPrmKey, _entryType);

                HttpContext.Current.Session["CustomerAccountTurnOverLimit"] = await customerDetailRepository.GetTurnOverLimitEntries(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);
                //HttpContext.Current.Session["CustomerAccountDocument"] = await customerDetailRepository.GetDocumentEntries(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);
                HttpContext.Current.Session["CustomerAccountReferencePersonDetail"] = await customerDetailRepository.GetReferencePersonEntries(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);
                HttpContext.Current.Session["CustomerAccountContactDetail"] = await customerDetailRepository.GetCustomerAccountContactDetailEntries(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);
                HttpContext.Current.Session["CustomerAccountAddressDetail"] = await customerDetailRepository.GetCustomerAccountAddressDetailEntries(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);
                HttpContext.Current.Session["CustomerAccountNoticeSchedule"] = await customerDetailRepository.GetCustomerAccountNoticeScheduleEntries(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);
                HttpContext.Current.Session["CustomerAccountBeneficiaryDetail"] = await customerDetailRepository.GetBeneficiaryDetailEntries(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(DepositCustomerAccountViewModel _depositCustomerAccountViewModel)
        {
            try
            {
                bool result =true;
                byte iterationCount = 1;

                if (_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.DepositType == StringLiteralValue.FixedDeposit)
                    iterationCount = Convert.ToByte(_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.NoOfDeposits < 2 ? 1 : _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.NoOfDeposits);

                for (byte i = 0; i < iterationCount; i++)
                {
                    // Get Input Visibility & Other Configuration
                    CustomerDepositAccountOpeningConfigViewModel customerDepositAccountOpeningConfigViewModel = await schemeDetailRepository.GetDepositSchemeConfigDetail(_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

                    //  AlternateAccountNumber2 
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 == false)
                        _depositCustomerAccountViewModel.AlternateAccountNumber2 = _depositCustomerAccountViewModel.AlternateAccountNumber2 = "None";

                    // AlternateAccountNumber3 
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 == false)
                        _depositCustomerAccountViewModel.AlternateAccountNumber3 = _depositCustomerAccountViewModel.AlternateAccountNumber3 = "None";

                    // PassbookNumber 
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnablePassbookDetail == false || customerDepositAccountOpeningConfigViewModel.SchemePassbookViewModel.EnableAutoPassbookNumber == true)
                        _depositCustomerAccountViewModel.PassbookNumber = 0;

                    result = customerAccountDbContextRepository.AttachDepositCustomerAccountData(_depositCustomerAccountViewModel, StringLiteralValue.Create);

                    // Customer Account Detail
                    if (result)
                        result = customerAccountDbContextRepository.AttachCustomerAccountDetailData(_depositCustomerAccountViewModel.CustomerAccountDetailViewModel, StringLiteralValue.Create);

                    // Customer Deposit Account
                    if (result)
                        result = customerAccountDbContextRepository.AttachCustomerDepositAccountData(_depositCustomerAccountViewModel.CustomerDepositAccountViewModel, StringLiteralValue.Create);

                    // Interst Rate
                    if (result)
                    {
                        _depositCustomerAccountViewModel.CustomerAccountInterestRateViewModel.EffectiveDate = _depositCustomerAccountViewModel.AccountOpeningDate;
                        result = customerAccountDbContextRepository.AttachCustomerAccountInterestRateData(_depositCustomerAccountViewModel.CustomerAccountInterestRateViewModel, StringLiteralValue.Create);
                    }

                    // Address
                    if (result)
                    {
                        List<PersonAddressViewModel> personAddressViewModelList = (List<PersonAddressViewModel>)HttpContext.Current.Session["PersonAddress"];

                        if (personAddressViewModelList != null)
                        {
                            foreach (PersonAddressViewModel viewModel in personAddressViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }

                    // Contact
                    if (result)
                    {
                        List<PersonContactDetailViewModel> personContactDetailViewModelList = new List<PersonContactDetailViewModel>();
                        personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["PersonContactDetail"];

                        if (personContactDetailViewModelList != null)
                        {
                            foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }

                    // Joint Account
                    if (result)
                    {
                        if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumJointAccountHolder != 0)
                        {
                            List<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["CustomerJointAccountHolder"];

                            if (customerJointAccountHolderViewModelList != null)
                            {
                                foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelList)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                    }

                    // Nominee
                    if (result)
                    {
                        if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumNominee != 0)
                        {
                            List<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];

                            if (customerAccountNomineeViewModelList != null)
                            {
                                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerAccountNomineeData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                    }

                    // Email
                    if (result)
                    {
                        if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                            result = customerAccountDbContextRepository.AttachCustomerAccountEmailServiceData(_depositCustomerAccountViewModel.CustomerAccountEmailServiceViewModel, StringLiteralValue.Create);
                    }

                    // SMS
                    if (result)
                    {
                        if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == true)
                            result = customerAccountDbContextRepository.AttachCustomerAccountSmsServiceData(_depositCustomerAccountViewModel.CustomerAccountSmsServiceViewModel, StringLiteralValue.Create);
                    }

                    // NoticeSchedule
                    if (result)
                    {
                        if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == true || customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
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
                    }

                    // TurnOverLimit
                    if (result)
                    {
                        if (_depositCustomerAccountViewModel.EnableTurnOverLimit == true)
                        {
                            List<CustomerAccountTurnOverLimitViewModel> customerAccountTurnOverLimitViewModelList = (List<CustomerAccountTurnOverLimitViewModel>)HttpContext.Current.Session["CustomerAccountTurnOverLimit"];

                            if (customerAccountTurnOverLimitViewModelList != null)
                            {
                                foreach (CustomerAccountTurnOverLimitViewModel viewModel in customerAccountTurnOverLimitViewModelList)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerAccountTurnOverLimitData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                    }

                    // Agent
                    if (result)
                    {
                        if (customerDepositAccountOpeningConfigViewModel.SchemeDepositAccountParameterViewModel.EnableAgent == true)
                        {
                            List<CustomerDepositAccountAgentViewModel> customerDepositAccountAgentViewModelList = (List<CustomerDepositAccountAgentViewModel>)HttpContext.Current.Session["CustomerDepositAccountAgent"];

                            if (customerDepositAccountAgentViewModelList != null)
                            {
                                foreach (CustomerDepositAccountAgentViewModel viewModel in customerDepositAccountAgentViewModelList)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerDepositAccountAgentData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }

                    }

                    // DEMAND DEPOSIT TYPE
                    if (result)
                    {
                        if (_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.DepositType == StringLiteralValue.DemandDeposit)
                        {
                            // Cheque Detail
                            if (result)
                            {
                                if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableChequeBook == true)
                                    result = customerAccountDbContextRepository.AttachCustomerAccountChequeDetailData(_depositCustomerAccountViewModel.CustomerAccountChequeDetailViewModel, StringLiteralValue.Create);
                            }


                            // Sweep Details
                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableSweepIn == true)
                                result = customerAccountDbContextRepository.AttachCustomerAccountSweepDetailData(_depositCustomerAccountViewModel.CustomerAccountSweepDetailViewModel, StringLiteralValue.Create);

                            // Photo Sign
                            if (result)
                            {
                                if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoSign == true)
                                {
                                    // PhotoDocumentUploadInLocalStorage
                                    if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentUpload != "D")
                                    {
                                        if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoDocumentUploadInLocalStorage == true)
                                        {
                                            if (_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoPath != null)
                                            {
                                                result = customerAccountDbContextRepository.AttachPhotoDocumentInLocalStorage(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoNameOfFile = "None";
                                                _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoLocalStoragePath = "None";
                                            }
                                        }
                                        else
                                        {
                                            if (_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoPath != null)
                                            {
                                                result = customerAccountDbContextRepository.AttachPhotoDocumentInDatabaseStorage(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoNameOfFile = "None";
                                                _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoLocalStoragePath = "None";
                                            }
                                        }
                                    }

                                    // SignDocumentUploadInLocalStorage
                                    if (result)
                                    {
                                        if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.SignDocumentUpload != "D")
                                        {
                                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableSignDocumentUploadInLocalStorage == true)
                                            {
                                                if (_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPath != null)
                                                {
                                                    result = customerAccountDbContextRepository.AttachSignDocumentInLocalStorage(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.SignDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                                }
                                                else
                                                {
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignNameOfFile = "None";
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignLocalStoragePath = "None";
                                                }
                                            }
                                            else
                                            {
                                                if (_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignPath != null)
                                                {
                                                    result = customerAccountDbContextRepository.AttachSignDocumentInDatabaseStorage(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, null, StringLiteralValue.Create);
                                                }
                                                else
                                                {
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignNameOfFile = "None";
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignLocalStoragePath = "None";
                                                }
                                            }
                                        }
                                    }
                                    // PhotoSign
                                    if (result)
                                    {
                                        if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoSign == true)
                                        {
                                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentUpload != "D" || customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.SignDocumentUpload != "D")
                                            {
                                                // If PhotoDocumentUpload is Disable Then Add Default Values.
                                                if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.PhotoDocumentUpload == "D")
                                                {
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoNameOfFile = "None";
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoLocalStoragePath = "None";
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.PhotoFileCaption = "None";
                                                }

                                                // If SignDocumentUpload is Disable Then Add Default Values.
                                                if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.SignDocumentUpload == "D")
                                                {
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignNameOfFile = "None";
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignLocalStoragePath = "None";
                                                    _depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel.SignFileCaption = "None";
                                                }

                                                result = customerAccountDbContextRepository.AttachCustomerAccountPhotoSignData(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, StringLiteralValue.Create);
                                            }
                                        }
                                    }

                                }
                            }

                            // Beneficiary Detail
                            if (result)
                            {
                                if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableBeneficiaryDetail == true)
                                {
                                    List<CustomerAccountBeneficiaryDetailViewModel> customerAccountBeneficiaryDetailViewModelList = (List<CustomerAccountBeneficiaryDetailViewModel>)HttpContext.Current.Session["CustomerAccountBeneficiaryDetail"];

                                    if (customerAccountBeneficiaryDetailViewModelList != null)
                                    {
                                        foreach (CustomerAccountBeneficiaryDetailViewModel viewModel in customerAccountBeneficiaryDetailViewModelList)
                                        {
                                            result = customerAccountDbContextRepository.AttachCustomerAccountBeneficiaryDetailData(viewModel, StringLiteralValue.Create);
                                        }
                                    }
                                }
                            }

                            // Reference Person Detail
                            if (result)
                            {
                                if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableReferencePersonDetail == true)
                                {
                                    List<CustomerAccountReferencePersonDetailViewModel> customerAccountReferencePersonDetailViewModelList = (List<CustomerAccountReferencePersonDetailViewModel>)HttpContext.Current.Session["CustomerAccountReferencePersonDetail"];

                                    if (customerAccountReferencePersonDetailViewModelList != null)
                                    {
                                        foreach (CustomerAccountReferencePersonDetailViewModel viewModel in customerAccountReferencePersonDetailViewModelList)
                                        {
                                            result = customerAccountDbContextRepository.AttachCustomerAccountReferencePersonDetailData(viewModel, StringLiteralValue.Create);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // FIXED DEPOSIT / TERM DEPOSIT
                    if (result)
                    {
                        if (_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.DepositType == StringLiteralValue.FixedDeposit)
                        {
                            CustomerTermDepositAccountDetailViewModel viewModel = _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel;

                            if (customerDepositAccountOpeningConfigViewModel.SchemeDepositInterestParameterViewModel.EnablePeriodicInterestPayout == false ||  _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.TotalInterestAmount < 1200)
                            {
                                viewModel.InterestPayoutFrequency = StringLiteralValue.AtMaturity;
                                viewModel.InterestPayoutAmount = 0;
                                viewModel.InterestPayoutDay = 0;
                            }

                            if (_depositCustomerAccountViewModel.CustomerDepositAccountViewModel.EnableAutoCloseOnMaturity == true || _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.EnableAutoRenewOnMaturity == true)
                                _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.MaturityInstruction = StringLiteralValue.DoNotRenew;

                            if (_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.NoOfDeposits > 1)
                            {
                                viewModel.DepositAmount = _depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.NoOfDepositsAmount[i];
                                result = customerAccountDbContextRepository.AttachCustomerTermDepositAccountDetailData(viewModel, StringLiteralValue.Create);
                            }
                            else
                                result = customerAccountDbContextRepository.AttachCustomerTermDepositAccountDetailData(viewModel, StringLiteralValue.Create);
                        }
                    }


                    // Standing Insturction
                    if(customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableStandingInstruction == true)
                    {
                        // Auto Debit
                        if(_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel.EnableAutoDebit == true)
                            result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Create, StringLiteralValue.DebitAccount);

                        // Auto Close
                        if (_depositCustomerAccountViewModel.CustomerDepositAccountViewModel.EnableAutoCloseOnMaturity == true)
                        {
                            // Credit
                            result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Create, StringLiteralValue.CreditAccount);

                            // Credit Interest
                            result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Create, StringLiteralValue.CreditInterestAccount);
                        }

                        // Auto Renew - Renew Only Principal i.e. Credit Interest Account
                        if (_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.EnableAutoRenewOnMaturity == true && accountDetailRepository.GetRenewTypeSysNameById(_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel.RenewTypeId) == "Principal")
                            result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Create, StringLiteralValue.CreditInterestAccount);
                    }

                    // Document *** Review And Uncomment
                    if (result)
                    {
                        //    List<CustomerAccountDocumentViewModel> customerAccountDocumentViewModelList = (List<CustomerAccountDocumentViewModel>)HttpContext.Current.Session["CustomerAccountDocument"];
                        //    if (customerAccountDocumentViewModelList != null)
                        //    {
                        //        foreach (CustomerAccountDocumentViewModel viewModel in customerAccountDocumentViewModelList)
                        //        {
                        //            result = customerAccountDbContextRepository.AttachCustomerAccountDocumentData(viewModel, StringLiteralValue.Create);
                        //        }
                        //    }
                    }

                    // Final Method
                    if (result)
                        result = await customerAccountDbContextRepository.SaveData();
                }

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

        public async Task<bool> VerifyRejectDelete(DepositCustomerAccountViewModel _depositCustomerAccountViewModel, string _entryType)
        {
            try
            {
                string entriesType;

                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                    entriesType = StringLiteralValue.Unverified;
                else
                    entriesType = StringLiteralValue.Reject;
                bool result;

                // Get Input Visibility & Other Configuration
                CustomerDepositAccountOpeningConfigViewModel customerDepositAccountOpeningConfigViewModel = await schemeDetailRepository.GetDepositSchemeConfigDetail(_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

                result = customerAccountDbContextRepository.AttachDepositCustomerAccountData(_depositCustomerAccountViewModel, _entryType);

                // CustomerAccountDetail
                if (result)
                {
                    CustomerAccountDetailViewModel customerAccountDetailViewModel = await customerDetailRepository.GetAccountDetailEntry(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountDetailViewModel != null)
                        result = customerAccountDbContextRepository.AttachCustomerAccountDetailData(_depositCustomerAccountViewModel.CustomerAccountDetailViewModel, _entryType);
                }

                // CustomerDepositAccount 
                if (result)
                {
                    CustomerDepositAccountViewModel customerDepositAccountViewModel = await customerDetailRepository.GetDepositAccountEntry(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerDepositAccountViewModel != null)
                        result = customerAccountDbContextRepository.AttachCustomerDepositAccountData(_depositCustomerAccountViewModel.CustomerDepositAccountViewModel, _entryType);
                }

                // CustomerAccountInterestRate 
                if (result)
                {
                    CustomerAccountInterestRateViewModel customerAccountInterestRateViewModel = await customerDetailRepository.GetInterestRateEntry(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountInterestRateViewModel != null)
                        result = customerAccountDbContextRepository.AttachCustomerAccountInterestRateData(_depositCustomerAccountViewModel.CustomerAccountInterestRateViewModel, _entryType);
                }

                // PersonAddress
                if (result)
                {
                    IEnumerable<PersonAddressViewModel> personAddressViewModellList = await customerDetailRepository.GetPersonAddressDetailEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (personAddressViewModellList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModellList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, _entryType);
                        }
                    }
                }

                // CustomerAccountAddressDetail
                if (result)
                {
                    IEnumerable<PersonAddressViewModel> customerAccountAddressDetailList = await customerDetailRepository.GetCustomerAccountAddressDetailEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountAddressDetailList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in customerAccountAddressDetailList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, _entryType);
                        }
                    }
                }

                // CustomerAccountContactDetail
                if (result)
                {
                    IEnumerable<PersonContactDetailViewModel> customerAccountContactDetailList = await customerDetailRepository.GetCustomerAccountContactDetailEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountContactDetailList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in customerAccountContactDetailList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, _entryType);
                        }
                    }

                }

                // PersonContactDetail 
                if (result)
                {
                    IEnumerable<PersonContactDetailViewModel> personContactDetailViewModelList = await customerDetailRepository.GetPersonContactDetailEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (personContactDetailViewModelList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, _entryType);
                        }
                    }
                }

                // JointAccountHolder
                if (result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumJointAccountHolder != 0)
                    {
                        IEnumerable<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelList = await customerDetailRepository.GetJointAccountHolderEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (customerJointAccountHolderViewModelList != null)
                        {
                            foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, _entryType);
                            }
                        }
                    }
                }

                // Nominee And NomineeGuardian
                if (result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumNominee != 0)
                    {
                        IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = await customerDetailRepository.GetNomineeEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (customerAccountNomineeViewModelList != null)
                        {
                            foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                            {
                                if (viewModel.NomineeAge < 18)
                                    viewModel.CustomerAccountNomineeGuardianViewModel = customerDetailRepository.GetNomineeGuardianEntry(viewModel.CustomerAccountNomineePrmKey, entriesType);

                                result = customerAccountDbContextRepository.AttachCustomerAccountNomineeData(viewModel, _entryType);
                            }
                        }
                    }
                }

                // EmailService 
                if (result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                    {
                        CustomerAccountEmailServiceViewModel customerAccountEmailServiceViewModel = await customerDetailRepository.GetEmailServiceEntry(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (customerAccountEmailServiceViewModel != null)
                            result = customerAccountDbContextRepository.AttachCustomerAccountEmailServiceData(_depositCustomerAccountViewModel.CustomerAccountEmailServiceViewModel, _entryType);
                    }
                }

                // SmsService 
                if (result)
                {
                    if (customerDepositAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == true)
                    {
                        CustomerAccountSmsServiceViewModel customerAccountSmsServiceViewModel = await customerDetailRepository.GetSmsServiceEntry(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (customerAccountSmsServiceViewModel != null)
                            result = customerAccountDbContextRepository.AttachCustomerAccountSmsServiceData(_depositCustomerAccountViewModel.CustomerAccountSmsServiceViewModel, _entryType);
                    }
                }

                // NoticeSchedule
                if (result)
                {
                    // Get SchemeNoticeSchedule Details From Session Object
                    IEnumerable<CustomerAccountNoticeScheduleViewModel> customerAccountNoticeScheduleViewModelList = await customerDetailRepository.GetNoticeScheduleEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountNoticeScheduleViewModelList != null)
                    {
                        foreach (CustomerAccountNoticeScheduleViewModel viewModel in customerAccountNoticeScheduleViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, _entryType);
                        }
                    }
                }

                // ChequeDetail 
                if (result)
                {
                    CustomerAccountChequeDetailViewModel customerAccountChequeDetailViewModel = await customerDetailRepository.GetChequeDetailEntry(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountChequeDetailViewModel != null)
                        result = customerAccountDbContextRepository.AttachCustomerAccountChequeDetailData(_depositCustomerAccountViewModel.CustomerAccountChequeDetailViewModel, _entryType);
                }

                // TurnOverLimit 
                if (result)
                {
                    IEnumerable<CustomerAccountTurnOverLimitViewModel> customerAccountTurnOverLimitViewModelList = await customerDetailRepository.GetTurnOverLimitEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountTurnOverLimitViewModelList != null)
                    {
                        foreach (CustomerAccountTurnOverLimitViewModel viewModel in customerAccountTurnOverLimitViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountTurnOverLimitData(viewModel, _entryType);
                        }
                    }
                }

                // Agent
                if (result)
                {
                    IEnumerable<CustomerDepositAccountAgentViewModel> customerDepositAccountAgentList = await customerDetailRepository.GetDepositAccountAgentEntries(_depositCustomerAccountViewModel.CustomerDepositAccountViewModel.CustomerDepositAccountPrmKey, entriesType);

                    if (customerDepositAccountAgentList != null)
                    {
                        foreach (CustomerDepositAccountAgentViewModel viewModel in customerDepositAccountAgentList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerDepositAccountAgentData(viewModel, _entryType);
                        }
                    }
                }

                // DEMAND DEPOSIT TYPE
                if (result)
                {
                    if (_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.DepositType == StringLiteralValue.DemandDeposit)
                    {
                        // Sweep Details
                        if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableSweepIn == true)
                        {
                            // CustomerAccountSweepDetail 
                            CustomerAccountSweepDetailViewModel customerAccountSweepDetailViewModel = await customerDetailRepository.GetSweepDetailEntry(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                            if (customerAccountSweepDetailViewModel != null)
                                result = customerAccountDbContextRepository.AttachCustomerAccountSweepDetailData(_depositCustomerAccountViewModel.CustomerAccountSweepDetailViewModel, _entryType);
                        }

                        // Photo Sign
                        if (result)
                        {
                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnablePhotoSign == true)
                            {
                                CustomerAccountPhotoSignViewModel customerAccountPhotoSignViewModel = await customerDetailRepository.GetPhotoSignEntry(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                                if (customerAccountPhotoSignViewModel != null)
                                    result = customerAccountDbContextRepository.AttachCustomerAccountPhotoSignData(_depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel, _entryType);
                            }
                        }

                        // Beneficiary Detail
                        if (result)
                        {
                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableBeneficiaryDetail == true)
                            {
                                IEnumerable<CustomerAccountBeneficiaryDetailViewModel> customerAccountBeneficiaryDetailList = await customerDetailRepository.GetBeneficiaryDetailEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                                if (customerAccountBeneficiaryDetailList != null)
                                {
                                    foreach (CustomerAccountBeneficiaryDetailViewModel viewModel in customerAccountBeneficiaryDetailList)
                                    {
                                        result = customerAccountDbContextRepository.AttachCustomerAccountBeneficiaryDetailData(viewModel, _entryType);
                                    }
                                }
                            }
                        }

                        // Reference Person Detail
                        if (result)
                        {
                            if (customerDepositAccountOpeningConfigViewModel.SchemeDemandDepositDetailViewModel.EnableReferencePersonDetail == true)
                            {
                                IEnumerable<CustomerAccountReferencePersonDetailViewModel> customerAccountReferencePersonDetailViewModelList = await customerDetailRepository.GetReferencePersonEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                                if (customerAccountReferencePersonDetailViewModelList != null)
                                {
                                    foreach (CustomerAccountReferencePersonDetailViewModel viewModel in customerAccountReferencePersonDetailViewModelList)
                                    {
                                        result = customerAccountDbContextRepository.AttachCustomerAccountReferencePersonDetailData(viewModel, _entryType);
                                    }
                                }
                            }
                        }
                    }
                }

                // FIXED DEPOSIT
                if (_depositCustomerAccountViewModel.CustomerAccountDetailViewModel.DepositType == StringLiteralValue.FixedDeposit)
                {
                    if (result)
                        result = customerAccountDbContextRepository.AttachCustomerTermDepositAccountDetailData(_depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel, _entryType);
                }

                // Old Entry Amended For Standing Instruction 
                IEnumerable<CustomerAccountStandingInstructionViewModel> customerAccountStandingInstructionViewModelList = await customerDetailRepository.GetStandingInstructionEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                if (customerAccountStandingInstructionViewModelList != null)
                {
                    foreach (CustomerAccountStandingInstructionViewModel viewModel in customerAccountStandingInstructionViewModelList)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(viewModel, _entryType, viewModel.InstructionFor);
                    }
                }

                // Document *** Review And Uncomment
                if (result)
                {
                    //IEnumerable<CustomerAccountDocumentViewModel> customerAccountDocumentViewModelList = await customerDetailRepository.GetDocumentEntries(_depositCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    //if (customerAccountDocumentViewModelList != null)
                    //{
                    //    foreach (CustomerAccountDocumentViewModel viewModel in customerAccountDocumentViewModelList)
                    //    {
                    //        result = customerAccountDbContextRepository.AttachCustomerAccountDocumentData(viewModel, _entryType);
                    //    }
                    //}
                }

                // Verify SMS Alert Send to Customer
                string response = sms.SendAccountOpeningSms(_depositCustomerAccountViewModel.CustomerAccountPrmKey).Result;

                // Final Method
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

        public async Task<DepositCustomerAccountViewModel> GetDepositCustomerAccountEntry(Guid _customerAccountId, string _entryType)
        {
            try
            {

                DepositCustomerAccountViewModel depositCustomerAccountViewModel = await context.Database.SqlQuery<DepositCustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountEntry (@CustomerAccountId, @EntriesType)", new SqlParameter("@CustomerAccountId", _customerAccountId), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                depositCustomerAccountViewModel.CustomerDepositAccountViewModel = await customerDetailRepository.GetDepositAccountEntry(depositCustomerAccountViewModel.PrmKey, _entryType);

                depositCustomerAccountViewModel.CustomerAccountDetailViewModel = await customerDetailRepository.GetAccountDetailEntry(depositCustomerAccountViewModel.PrmKey, _entryType);

                depositCustomerAccountViewModel.CustomerAccountInterestRateViewModel = await customerDetailRepository.GetCustomerAccountInterestRateEntry(depositCustomerAccountViewModel.PrmKey, _entryType);

                depositCustomerAccountViewModel.CustomerAccountSweepDetailViewModel = await customerDetailRepository.GetSweepDetailEntry(depositCustomerAccountViewModel.PrmKey, _entryType);

                depositCustomerAccountViewModel.CustomerAccountPhotoSignViewModel = await customerDetailRepository.GetPhotoSignEntry(depositCustomerAccountViewModel.PrmKey, _entryType);

                depositCustomerAccountViewModel.CustomerAccountEmailServiceViewModel = await customerDetailRepository.GetEmailServiceEntry(depositCustomerAccountViewModel.PrmKey, _entryType);

                depositCustomerAccountViewModel.CustomerAccountSmsServiceViewModel = await customerDetailRepository.GetSmsServiceEntry(depositCustomerAccountViewModel.PrmKey, _entryType);

                depositCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel = await customerDetailRepository.GetStandingInstructionEntry(depositCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                depositCustomerAccountViewModel.CustomerAccountChequeDetailViewModel = await customerDetailRepository.GetChequeDetailEntry(depositCustomerAccountViewModel.PrmKey, _entryType);

                depositCustomerAccountViewModel.CustomerTermDepositAccountDetailViewModel = await customerDetailRepository.GetTermDepositAccountDetailEntry(depositCustomerAccountViewModel.CustomerDepositAccountViewModel.CustomerDepositAccountPrmKey, _entryType);

                return depositCustomerAccountViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<DepositCustomerAccountIndexViewModel>> GetDepositCustomerAccountIndex(string _entryType)
        {
            try
            {
                var w = await context.Database.SqlQuery<DepositCustomerAccountIndexViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return w;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}






