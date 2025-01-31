using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Account.SystemEntity;
using System.IO;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.ViewModel.Account.Layout;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Services.Abstract.PersonInformation.Parameter;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFLoanCustomerAccountRepository : ILoanCustomerAccountRepository
    {
        private readonly EFDbContext context;
        private readonly ICustomerDetailRepository customerDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly ISchemeDetailRepository schemeDetailRepository;
        private readonly ICustomerAccountDbContextRepository customerAccountDbContextRepository;
        private readonly IPersonInformationParameterRepository personInformationParameterRepository;

        public EFLoanCustomerAccountRepository(RepositoryConnection _connection, IPersonInformationParameterRepository _personInformationParameterRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, ICustomerDetailRepository _customerDetailRepository, IAccountDetailRepository _accountDetailRepository, ISchemeDetailRepository _schemeDetailRepository, ICustomerAccountDbContextRepository _customerAccountDbContextRepository)
        {
            context = _connection.EFDbContext;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            customerDetailRepository = _customerDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            schemeDetailRepository = _schemeDetailRepository;
            customerAccountDbContextRepository = _customerAccountDbContextRepository;
            personInformationParameterRepository = _personInformationParameterRepository;
        }

        public async Task<bool> Amend(LoanCustomerAccountViewModel _loanCustomerAccountViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                // Get Account Opening Configuration From Scheme
                CustomerLoanAccountOpeningConfigViewModel customerLoanAccountOpeningConfigViewModel = await schemeDetailRepository.GetCustomerLoanAccountConfigDetail(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

                // Get Loan Type For Valid Tables Insertion
                string loanType = accountDetailRepository.GetSysNameOfLoanTypeByLoanTypeId(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.LoanTypeId);
                string occupation = accountDetailRepository.GetSysNameOfOccupationById(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.OccupationId);
                bool result = true;

                // List For Delete Photo From Local Storage If It Deleted Or Changed In Amend Operation
                List<string> rejectedFilesName = new List<string>();
                List<string> savedFilesName = new List<string>();

                
                //LoanCustomerAccount
                if (result)
                {
                    result = customerAccountDbContextRepository.AttachLoanCustomerAccountData(_loanCustomerAccountViewModel, StringLiteralValue.Amend);
                }

                //CustomerLoanAccount
                if (result)
                {
                    result = customerAccountDbContextRepository.AttachCustomerLoanAccountData(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel, StringLiteralValue.Amend);
                }

                // CustomerAccountDetail
                if (result)
                {
                    result = customerAccountDbContextRepository.AttachCustomerAccountDetailData(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel, StringLiteralValue.Amend);
                }

                //EmploymentDetail
                if (result)
                {
                    if (_loanCustomerAccountViewModel.PersonEmploymentDetailViewModel.PrmKey > 0)
                    {
                        if ((occupation == StringLiteralValue.Salaried || occupation == StringLiteralValue.SelfEmployedBusiness || occupation == StringLiteralValue.SelfEmployedProfessional) && _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.IsEmployee == false)
                        {
                            result = customerAccountDbContextRepository.AttachPersonEmploymentDetailData(_loanCustomerAccountViewModel.PersonEmploymentDetailViewModel, StringLiteralValue.Amend);
                        }
                        else
                        {
                            result = customerAccountDbContextRepository.AttachPersonEmploymentDetailData(_loanCustomerAccountViewModel.PersonEmploymentDetailViewModel, StringLiteralValue.Delete);

                        }
                    }
                    else
                    {
                        if ((occupation == StringLiteralValue.Salaried || occupation == StringLiteralValue.SelfEmployedBusiness || occupation == StringLiteralValue.SelfEmployedProfessional) && _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.IsEmployee == false)
                        {
                            result = customerAccountDbContextRepository.AttachPersonEmploymentDetailData(_loanCustomerAccountViewModel.PersonEmploymentDetailViewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // CustomerAccountInterestRate
                if (result)
                {
                    _loanCustomerAccountViewModel.CustomerAccountInterestRateViewModel.EffectiveDate = _loanCustomerAccountViewModel.AccountOpeningDate;
                    result = customerAccountDbContextRepository.AttachCustomerAccountInterestRateData(_loanCustomerAccountViewModel.CustomerAccountInterestRateViewModel, StringLiteralValue.Amend);
                }

                // CustomerAccountSmsService
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == true)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountSmsServiceData(_loanCustomerAccountViewModel.CustomerAccountSmsServiceViewModel, StringLiteralValue.Amend);
                    }
                }

                //CustomerAccountEmailService
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountEmailServiceData(_loanCustomerAccountViewModel.CustomerAccountEmailServiceViewModel, StringLiteralValue.Amend);
                    }
                }

                // Old Entry Amended For CustomerJointAccount 
                if (result)
                {
                    IEnumerable<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelListForAmend = await customerDetailRepository.GetJointAccountHolderEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                    if (customerJointAccountHolderViewModelListForAmend != null)
                    {
                        foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelListForAmend)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    // New Entry Created For CustomerJointAccountHolder 
                    if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumJointAccountHolder != 0)
                    {
                        // Insert New Updated Record - Get Record From Session Object
                        List<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelList = new List<CustomerJointAccountHolderViewModel>();

                        customerJointAccountHolderViewModelList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["JointAccountHolder"];

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
                if (result)
                {
                    IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelListForAmend = await customerDetailRepository.GetNomineeEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                    if (customerAccountNomineeViewModelListForAmend != null)
                    {
                        foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelListForAmend)
                        {
                            if (viewModel.NomineeAge < 18)
                            {
                                viewModel.CustomerAccountNomineeGuardianViewModel = customerDetailRepository.GetNomineeGuardianEntry(viewModel.CustomerAccountNomineePrmKey, StringLiteralValue.Reject);
                            }
                            result = customerAccountDbContextRepository.AttachCustomerAccountNomineeData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    // New Entry Created For CustomerAccountNominee 
                    if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumNominee != 0)
                    {
                        List<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = new List<CustomerAccountNomineeViewModel>();
                        customerAccountNomineeViewModelList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["Nominee"];

                        if (customerAccountNomineeViewModelList != null)
                        {
                            foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerAccountNomineeData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // Old Entry Amended For PersonAddress 
                if (result)
                {
                    IEnumerable<PersonAddressViewModel> personAddressViewModellListForAmend = await customerDetailRepository.GetPersonAddressDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                    if (personAddressViewModellListForAmend != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModellListForAmend)
                        {
                            result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                    // Old Entry Amended For CustomerAccountAddressDetail
                    IEnumerable<PersonAddressViewModel> customerAccountAddressDetailList = await customerDetailRepository.GetCustomerAccountAddressDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                    if (customerAccountAddressDetailList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in customerAccountAddressDetailList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // New Entry Created For PersonAddress And CustomerAccountAddressDetail
                if (result)
                {
                    List<PersonAddressViewModel> personAddressViewModelList = new List<PersonAddressViewModel>();
                    personAddressViewModelList = (List<PersonAddressViewModel>)HttpContext.Current.Session["AddressDetail"];

                    if (personAddressViewModelList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelList)
                        {
                            viewModel.CustomerAccountPrmKey = _loanCustomerAccountViewModel.CustomerAccountPrmKey;
                            result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // Old Entry Amended For PersonContactDetail 
                if (result)
                {
                    IEnumerable<PersonContactDetailViewModel> personContactDetailViewModelListForAmend = await customerDetailRepository.GetPersonContactDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                    if (personContactDetailViewModelListForAmend != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelListForAmend)
                        {
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // Old Entry Amended For CustomerAccountContactDetail 
                if (result)
                {
                    IEnumerable<PersonContactDetailViewModel> customerAccountContactDetailList = await customerDetailRepository.GetCustomerAccountContactDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                    if (customerAccountContactDetailList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in customerAccountContactDetailList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Amend);
                        }
                    }

                }

                // New Entry Created For PersonContactDetail And CustomerAccountContactDetail
                if (result)
                {
                    List<PersonContactDetailViewModel> personContactDetailViewModelList = new List<PersonContactDetailViewModel>();
                    personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["ContactDetail"];

                    if (personContactDetailViewModelList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                        {
                            viewModel.CustomerAccountPrmKey = _loanCustomerAccountViewModel.CustomerAccountPrmKey;
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // Old Entry Amended For PersonBorrowingDetail 
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableBorrowingDetail == true)
                    {
                        IEnumerable<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelListForAmend = await customerDetailRepository.GetPersonBorrowingDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                        if (personBorrowingDetailViewModelListForAmend != null)
                        {
                            foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelListForAmend)
                            {
                                result = customerAccountDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }
                }

                // Old Entry Amended For CustomerAccountBorrowingDetail 
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableBorrowingDetail == true)
                    {
                        IEnumerable<PersonBorrowingDetailViewModel> customerAccountBorrowingDetailList = await customerDetailRepository.GetCustomerAccountBorrowingDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                        if (customerAccountBorrowingDetailList != null)
                        {
                            foreach (PersonBorrowingDetailViewModel viewModel in customerAccountBorrowingDetailList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }
                }

                // New Entry Created For PersonBorrowingDetail And CustomerAccountBorrowingDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableBorrowingDetail == true)
                    {
                        List<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelList = new List<PersonBorrowingDetailViewModel>();
                        personBorrowingDetailViewModelList = (List<PersonBorrowingDetailViewModel>)HttpContext.Current.Session["BorrowingDetail"];

                        if (personBorrowingDetailViewModelList != null)
                        {
                            foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelList)
                            {
                                viewModel.CustomerAccountPrmKey = _loanCustomerAccountViewModel.CustomerAccountPrmKey;
                                result = customerAccountDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // PersonCourtCase
                // Amend Old PersonCourtCase
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCourtCaseDetail == true)
                    {
                        IEnumerable<PersonCourtCaseViewModel> personCourtCaseViewModelListForAmend = await customerDetailRepository.GetPersonCourtCaseEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                        if (personCourtCaseViewModelListForAmend != null)
                        {
                            foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelListForAmend)
                            {
                                result = customerAccountDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }
                }

                // Amend Old CustomerAccount CourtCase
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCourtCaseDetail == true)
                    {
                        IEnumerable<PersonCourtCaseViewModel> customerAccountCourtCaseList = await customerDetailRepository.GetCustomerAccountCourtCaseEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                        if (customerAccountCourtCaseList != null)
                        {
                            foreach (PersonCourtCaseViewModel viewModel in customerAccountCourtCaseList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCourtCaseDetail == true)
                    {
                        List<PersonCourtCaseViewModel> personCourtCaseViewModelList = new List<PersonCourtCaseViewModel>();
                        personCourtCaseViewModelList = (List<PersonCourtCaseViewModel>)HttpContext.Current.Session["PersonCourtCase"];

                        if (personCourtCaseViewModelList != null)
                        {
                            foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelList)
                            {
                                viewModel.CustomerAccountPrmKey = _loanCustomerAccountViewModel.CustomerAccountPrmKey;
                                result = customerAccountDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // PersonIncomeTaxDetail
                // Amend Old Record (i.e. Existing In Db)
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableIncomeTaxDetail == true)
                    {
                        IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelListForAmend = await customerDetailRepository.GetPersonIncomeTaxDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                        if (personIncomeTaxDetailViewModelListForAmend != null)
                        {
                            foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModelListForAmend)
                            {
                                result = customerAccountDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, StringLiteralValue.Amend);

                                if (viewModel.PersonIncomeTaxDetailDocumentPrmKey > 0)
                                {
                                    rejectedFilesName.Add(viewModel.NameOfFile);
                                    result = customerAccountDbContextRepository.AttachPersonIncomeTaxDocumentData(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                                }
                            }
                        }
                    }
                }

                // Amend CustomerAccount IncomeTaxDetail Old Record (i.e. Existing In Db)
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableIncomeTaxDetail == true)
                    {
                        IEnumerable<PersonIncomeTaxDetailViewModel> customerAccountIncomeTaxDetailList = await customerDetailRepository.GetCustomerAccountIncomeTaxDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                        if (customerAccountIncomeTaxDetailList != null)
                        {
                            foreach (PersonIncomeTaxDetailViewModel viewModel in customerAccountIncomeTaxDetailList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableIncomeTaxDetail == true)
                    {
                        // Insert Record From Session Object
                        List<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelList = (List<PersonIncomeTaxDetailViewModel>)HttpContext.Current.Session["PersonIncomeTaxDetail"];

                        if (personIncomeTaxDetailViewModelList != null)
                        {
                            foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.LocalStoragePath;
                                string oldFileName = viewModel.NameOfFile;
                                viewModel.Remark = _loanCustomerAccountViewModel.Remark;
                                //viewModel.PersonPrmKey = _loanCustomerAccountViewModel.PersonPrmKey;

                                result = customerAccountDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, StringLiteralValue.Create);

                                if (personIncomeTaxDetailViewModelList != null)
                                {
                                    if (personInformationParameterViewModel.IncomeTaxDocumentUpload != StringLiteralValue.Disable)
                                    {
                                        // EnableIncomeTaxDocumentUploadInLocalStorage
                                        if (personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInLocalStorage == true)
                                        {
                                            //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
                                            if (viewModel.PhotoPathTax != null)
                                            {
                                                result = customerAccountDbContextRepository.AttachIncomeTaxDetailDocumentInLocalStorage(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }

                                        // If Db Storage
                                        else
                                        {
                                            if (viewModel.PhotoPathTax != null)
                                            {
                                                result = customerAccountDbContextRepository.AttachIncomeTaxDetailDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }
                                        savedFilesName.Add(viewModel.NameOfFile);   
                                        result = customerAccountDbContextRepository.AttachPersonIncomeTaxDocumentData(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                    }

                                }
                            }

                            //To Collect Only Deleted Or Changed Photo Records
                            if (rejectedFilesName != null && savedFilesName != null)
                            {
                                for (byte i = 0; i < rejectedFilesName.Count; i++)
                                {
                                    if (!savedFilesName.Contains(rejectedFilesName[i]))
                                    {
                                        string serverDestinationPath = HttpContext.Current.Server.MapPath(personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath);
                                        string pathToDelete = Path.Combine(serverDestinationPath, rejectedFilesName[i]);
                                        result = customerAccountDbContextRepository.DeletePhotoForDeletedRecord(pathToDelete);
                                    }
                                }
                            }
                            rejectedFilesName.Clear();
                            savedFilesName.Clear();

                        }
                    }
                }

                //PersonAdditionalIncomeDetail
                //Amend Old Record
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableAdditionalIncomeDetail == true)
                    {
                        IEnumerable<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModelsListForAmend = await customerDetailRepository.GetPersonAdditionalIncomeDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);
                        if (personAdditionalIncomeDetailViewModelsListForAmend != null)
                        {
                            foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModelsListForAmend)
                            {
                                result = customerAccountDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Amend);

                            }
                        }
                    }
                }

                //Amend CustomerAccountAdditionalIncomeDetail Old Record
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableAdditionalIncomeDetail == true)
                    {
                        IEnumerable<PersonAdditionalIncomeDetailViewModel> customerAccountAdditionalIncomeDetailList = await customerDetailRepository.GetCustomerAccountAdditionalIncomeDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);
                        if (customerAccountAdditionalIncomeDetailList != null)
                        {
                            foreach (PersonAdditionalIncomeDetailViewModel viewModel in customerAccountAdditionalIncomeDetailList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Amend);

                            }
                        }
                    }
                }

                // New Record Create For Amened 
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableAdditionalIncomeDetail == true)
                    {
                        List<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModelList = new List<PersonAdditionalIncomeDetailViewModel>();
                        personAdditionalIncomeDetailViewModelList = (List<PersonAdditionalIncomeDetailViewModel>)HttpContext.Current.Session["PersonAdditionalIncomeDetail"];

                        if (personAdditionalIncomeDetailViewModelList != null)
                        {
                            foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModelList)
                            {
                                viewModel.CustomerAccountPrmKey = _loanCustomerAccountViewModel.CustomerAccountPrmKey;
                                result = customerAccountDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // Old Entry Amended For CustomerLoanAccountGuarantorDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableGuarantorDetail == true)
                    {

                        IEnumerable<CustomerLoanAccountGuarantorDetailViewModel> customerLoanAccountGuarantorDetailViewModelListForAmend = await customerDetailRepository.GetLoanAccountGuarantorDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, StringLiteralValue.Reject);

                        if (customerLoanAccountGuarantorDetailViewModelListForAmend != null)
                        {
                            foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerLoanAccountGuarantorDetailViewModelListForAmend)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAccountGuarantorDetailData(viewModel, StringLiteralValue.Amend);
                            }
                        }
                    }
                    // New Entry Created For CustomerLoanAccountGuarantorDetail 
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableGuarantorDetail == true)
                    {

                        List<CustomerLoanAccountGuarantorDetailViewModel> customerLoanAccountGuarantorDetailViewModelList = new List<CustomerLoanAccountGuarantorDetailViewModel>();
                        customerLoanAccountGuarantorDetailViewModelList = (List<CustomerLoanAccountGuarantorDetailViewModel>)HttpContext.Current.Session["GuarantorDetail"];

                        if (customerLoanAccountGuarantorDetailViewModelList != null)
                        {
                            foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerLoanAccountGuarantorDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAccountGuarantorDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //StandingInstruction
                if (_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel.CustomerAccountStandingInstructionPrmKey > 0)
                {

                    CustomerAccountStandingInstructionViewModel customerAccountStandingInstructionViewModel = await customerDetailRepository.GetStandingInstructionEntry(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);
                    if (customerAccountStandingInstructionViewModel != null)
                    {
                        if (_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel.EnableAutoDebit == true)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Amend, StringLiteralValue.DebitAccount);
                        }
                        else
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Delete, StringLiteralValue.DebitAccount);
                        }
                    }

                }
                else
                {
                    if (_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel.EnableAutoDebit == true)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Create, StringLiteralValue.DebitAccount);
                    }
                }

                // CustomerLoanAgainstDepositCollateralDetail
                if (result)
                {
                    if ((loanType == StringLiteralValue.CashCreditLoan && customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.EnableFixedDepositAsCollateral == true) || loanType == StringLiteralValue.LoanAgainstDeposit || customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableDepositAsCollateral == true)
                    {
                        IEnumerable<CustomerLoanAgainstDepositCollateralDetailViewModel> customerLoanAgainstDepositCollateralDetailViewModelListForAmend = await customerDetailRepository.GetCustomerLoanAgainstDepositCollateralDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, StringLiteralValue.Reject);

                        if (customerLoanAgainstDepositCollateralDetailViewModelListForAmend != null)
                        {
                            foreach (CustomerLoanAgainstDepositCollateralDetailViewModel viewModel in customerLoanAgainstDepositCollateralDetailViewModelListForAmend)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAgainstDepositCollateralDetailData(viewModel, StringLiteralValue.Amend);

                            }
                        }
                    }
                }

                // New Entry Created For CustomerLoanAgainstDepositCollateralDetail 
                if (result)
                {
                    if ((loanType == StringLiteralValue.CashCreditLoan && customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.EnableFixedDepositAsCollateral == true) || loanType == StringLiteralValue.LoanAgainstDeposit || customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableDepositAsCollateral == true)
                    {
                        List<CustomerLoanAgainstDepositCollateralDetailViewModel> customerLoanAgainstDepositCollateralDetailViewModelList = new List<CustomerLoanAgainstDepositCollateralDetailViewModel>();
                        customerLoanAgainstDepositCollateralDetailViewModelList = (List<CustomerLoanAgainstDepositCollateralDetailViewModel>)HttpContext.Current.Session["LoanAgainstDepositCollateralDetail"];

                        if (customerLoanAgainstDepositCollateralDetailViewModelList != null)
                        {
                            foreach (CustomerLoanAgainstDepositCollateralDetailViewModel viewModel in customerLoanAgainstDepositCollateralDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAgainstDepositCollateralDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //Loan Type
                if (result)
                {
                    //Loan Against Property & Home Loan
                    if (loanType == StringLiteralValue.LoanAgainstProperty || loanType == StringLiteralValue.HomeLoan)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerLoanAgainstPropertyCollateralDetailData(_loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel, StringLiteralValue.Amend);
                    }

                    //Business Loan
                    if (loanType == StringLiteralValue.ShortTermBusinessLoan)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerBusinessLoanCollateralDetailData(_loanCustomerAccountViewModel.CustomerBusinessLoanCollateralDetailViewModel, StringLiteralValue.Amend);
                    }

                    //Vehicle Loan
                    if (loanType == StringLiteralValue.VehicleLoan)
                    {
                        //CustomerVehicleLoanCollateralDetail

                        result = customerAccountDbContextRepository.AttachCustomerVehicleLoanCollateralDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel, StringLiteralValue.Amend);

                        if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel != null)
                        {
                            if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.IsUsedForCommercial == true)
                            {
                                //CustomerVehicleLoanPermitDetail
                                if (result)
                                {
                                    if (_loanCustomerAccountViewModel.CustomerVehicleLoanPermitDetailViewModel.CustomerVehicleLoanPermitDetailPrmKey > 0)
                                    {
                                        if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.IsUsedForCommercial == true)
                                        {
                                            result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPermitDetail(_loanCustomerAccountViewModel.CustomerVehicleLoanPermitDetailViewModel, StringLiteralValue.Amend);
                                        }
                                        else
                                        {
                                            result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPermitDetail(_loanCustomerAccountViewModel.CustomerVehicleLoanPermitDetailViewModel, StringLiteralValue.Delete);
                                        }
                                    }
                                    else
                                    {
                                        if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.IsUsedForCommercial == true)
                                        {
                                            result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPermitDetail(_loanCustomerAccountViewModel.CustomerVehicleLoanPermitDetailViewModel, StringLiteralValue.Create);
                                        }
                                    }
                                }

                                //CustomerVehicleLoanContractDetail
                                if (result)
                                {

                                    if (_loanCustomerAccountViewModel.CustomerVehicleLoanContractDetailViewModel.CustomerVehicleLoanContractDetailPrmKey > 0)
                                    {
                                        if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.IsUsedForCommercial == true && _loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.HasContract == true)
                                        {
                                            result = customerAccountDbContextRepository.AttachCustomerVehicleLoanContractDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanContractDetailViewModel, StringLiteralValue.Amend);
                                        }
                                        else
                                        {
                                            result = customerAccountDbContextRepository.AttachCustomerVehicleLoanContractDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanContractDetailViewModel, StringLiteralValue.Delete);
                                        }
                                    }
                                    else
                                    {
                                        if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.IsUsedForCommercial == true && _loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.HasContract == true)
                                        {
                                            result = customerAccountDbContextRepository.AttachCustomerVehicleLoanContractDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanContractDetailViewModel, StringLiteralValue.Create);
                                        }
                                    }
                                }
                            }

                            //CustomerPreOwnedVehicleLoanInspection
                            if (result)
                            {
                                if (_loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel.CustomerPreOwnedVehicleLoanInspectionPrmKey > 0)
                                {
                                    if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.LoanPurpose != StringLiteralValue.NewVehicle && customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.EnableVehicleInspection == true)
                                    {
                                        result = customerAccountDbContextRepository.AttachCustomerPreOwnedVehicleLoanInspectionData(_loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel, StringLiteralValue.Amend);
                                    }
                                    else
                                    {
                                        result = customerAccountDbContextRepository.AttachCustomerPreOwnedVehicleLoanInspectionData(_loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel, StringLiteralValue.Delete);
                                    }
                                }
                                else
                                {
                                    if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.LoanPurpose != StringLiteralValue.NewVehicle && customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.EnableVehicleInspection == true)
                                    {
                                        result = customerAccountDbContextRepository.AttachCustomerPreOwnedVehicleLoanInspectionData(_loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel, StringLiteralValue.Create);
                                    }
                                }

                            }
                        }

                        //CustomerLoanAccountVehicleInsuranceDetail
                        if (result)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerLoanAccountVehicleInsuranceDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanInsuranceDetailViewModel, StringLiteralValue.Amend);
                        }
                        
                        // CustomerVehicleLoanPhoto
                        if (result)
                        {
                            IEnumerable<CustomerVehicleLoanPhotoViewModel> customerVehicleLoanPhotoViewModelListForAmend = await customerDetailRepository.GetVehicleLoanPhotoEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, StringLiteralValue.Reject);
                            foreach (CustomerVehicleLoanPhotoViewModel viewModel in customerVehicleLoanPhotoViewModelListForAmend)
                            {
                                if (customerVehicleLoanPhotoViewModelListForAmend != null)
                                {
                                    // Add Rejected Record In rejectedFileName 
                                    rejectedFilesName.Add(viewModel.NameOfFile);
                                    result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPhotoData(viewModel, customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.StoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                                }
                            }

                            // New Record Create For Amened CustomerVehicleLoanPhoto
                            if (result)
                            {
                                List<CustomerVehicleLoanPhotoViewModel> CustomerVehicleLoanPhotoViewModelList = (List<CustomerVehicleLoanPhotoViewModel>)HttpContext.Current.Session["VehicleLoanPhoto"];

                                if (CustomerVehicleLoanPhotoViewModelList != null)
                                {
                                    foreach (CustomerVehicleLoanPhotoViewModel viewModel in CustomerVehicleLoanPhotoViewModelList)
                                    {
                                        string oldLocalStoragePath = viewModel.LocalStoragePath;

                                        string oldFileName = viewModel.NameOfFile;

                                        if (customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.PhotoUpload != StringLiteralValue.Disable)
                                        {
                                            // If Local Storage
                                            if (customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.EnablePhotoUploadInLocalStorage == true)
                                            {
                                                if (viewModel.PhotoPath != null)
                                                {
                                                    result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPhotoInLocalStorage(viewModel, customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.StoragePath, null, StringLiteralValue.Create);
                                                }
                                                else
                                                {
                                                    viewModel.NameOfFile = oldFileName;
                                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                                }
                                            }

                                            // If Db Storage
                                            else
                                            {
                                                if (viewModel.PhotoPath != null)
                                                {
                                                    result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPhotoInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                                }
                                                else
                                                {
                                                    viewModel.NameOfFile = "None";
                                                    viewModel.LocalStoragePath = "None";
                                                }
                                            }

                                            // Add New Saved Record in savedFileNameList
                                            savedFilesName.Add(viewModel.NameOfFile);
                                            
                                            result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPhotoData(viewModel, customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.StoragePath, oldFileName, StringLiteralValue.Create);

                                        }
                                    }

                                    //To Collect Only Deleted Or Changed Photo Records
                                    if (rejectedFilesName != null && savedFilesName != null)
                                    {
                                        for (byte i = 0; i < rejectedFilesName.Count; i++)
                                        {
                                            if (!savedFilesName.Contains(rejectedFilesName[i]))
                                            {
                                                string serverDestinationPath = HttpContext.Current.Server.MapPath(customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.StoragePath);
                                                string pathToDelete = Path.Combine(serverDestinationPath, rejectedFilesName[i]);
                                                result = customerAccountDbContextRepository.DeletePhotoForDeletedRecord(pathToDelete);
                                            }
                                        }
                                    }
                                    rejectedFilesName.Clear();
                                    savedFilesName.Clear();

                                }
                            }
                        }
                    }

                    // Old Entry Amended For CustomerGoldLoanCollateralDetailViewModel
                    if (loanType == StringLiteralValue.GoldLoan)
                    {
                        IEnumerable<CustomerGoldLoanCollateralDetailViewModel> customerGoldLoanCollateralDetailViewModelListForAmend = await customerDetailRepository.GetGoldLoanCollateralDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, StringLiteralValue.Reject);

                        if (customerGoldLoanCollateralDetailViewModelListForAmend != null)
                        {
                            foreach (CustomerGoldLoanCollateralDetailViewModel viewModel in customerGoldLoanCollateralDetailViewModelListForAmend)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralDetailData(viewModel, StringLiteralValue.Amend);
                            }
                        }

                        // New Entry Created For CustomerGoldLoanCollateralDetailViewModel 
                        if (result)
                        {
                            List<CustomerGoldLoanCollateralDetailViewModel> customerGoldLoanCollateralDetailViewModelList = new List<CustomerGoldLoanCollateralDetailViewModel>();
                            customerGoldLoanCollateralDetailViewModelList = (List<CustomerGoldLoanCollateralDetailViewModel>)HttpContext.Current.Session["GoldLoanCollateralDetail"];

                            if (customerGoldLoanCollateralDetailViewModelList != null)
                            {
                                foreach (CustomerGoldLoanCollateralDetailViewModel viewModel in customerGoldLoanCollateralDetailViewModelList)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralDetailData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }

                        //Old Entry For CustomerGoldLoanCollateralPhotoViewModel
                        if (result)
                        {
                            if (customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.EnableGoldPhoto == true)
                            {
                                IEnumerable<CustomerGoldLoanCollateralPhotoViewModel> customerGoldLoanCollateralPhotoViewModelListForAmend = await customerDetailRepository.GetGoldLoanCollateralPhotoEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, StringLiteralValue.Reject);
                                foreach (CustomerGoldLoanCollateralPhotoViewModel viewModel in customerGoldLoanCollateralPhotoViewModelListForAmend)
                                {
                                    if (customerGoldLoanCollateralPhotoViewModelListForAmend != null)
                                    {
                                        // Add Rejected Record In rejectedFileName 
                                        rejectedFilesName.Add(viewModel.NameOfFile);
                                        result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralPhotoData(viewModel, customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.GoldPhotoLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                                    }
                                }
                            }
                        }

                        //New Entry For CustomerGoldLoanCollateralPhotoViewModel
                        if (result)
                        {
                            if (customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.EnableGoldPhoto == true)
                            {
                                List<CustomerGoldLoanCollateralPhotoViewModel> customerGoldLoanCollateralPhotoViewModelList = (List<CustomerGoldLoanCollateralPhotoViewModel>)HttpContext.Current.Session["GoldLoanCollateralPhoto"];

                                if (customerGoldLoanCollateralPhotoViewModelList != null)
                                {
                                    foreach (CustomerGoldLoanCollateralPhotoViewModel viewModel in customerGoldLoanCollateralPhotoViewModelList)
                                    {
                                        string oldLocalStoragePath = viewModel.LocalStoragePath;

                                        string oldFileName = viewModel.NameOfFile;

                                        if (customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.GoldPhotoUpload != StringLiteralValue.Disable)
                                        {
                                            // If Local Storage
                                            if (customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.EnableGoldPhotoUploadInLocalStorage == true)
                                            {
                                                if (viewModel.PhotoPath != null)
                                                {
                                                    result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralPhotoInLocalStorage(viewModel, customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.GoldPhotoLocalStoragePath, null, StringLiteralValue.Create);
                                                }
                                                else
                                                {
                                                    viewModel.NameOfFile = oldFileName;
                                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                                }
                                            }

                                            // If Db Storage
                                            else
                                            {
                                                if (viewModel.PhotoPath != null)
                                                {
                                                    result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralPhotoInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                                }
                                                else
                                                {
                                                    viewModel.NameOfFile = oldFileName;
                                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                                }
                                            }

                                            // Add New Saved Record in savedFileNameList
                                            savedFilesName.Add(viewModel.NameOfFile);

                                            result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralPhotoData(viewModel, customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.GoldPhotoLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                        }

                                    }

                                    //To Collect Only Deleted Or Changed Photo Records
                                    if (rejectedFilesName != null && savedFilesName != null)
                                    {
                                        for (byte i = 0; i < rejectedFilesName.Count; i++)
                                        {
                                            if (!savedFilesName.Contains(rejectedFilesName[i]))
                                            {
                                                string serverDestinationPath = HttpContext.Current.Server.MapPath(customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.GoldPhotoLocalStoragePath);
                                                string pathToDelete = Path.Combine(serverDestinationPath, rejectedFilesName[i]);
                                                result = customerAccountDbContextRepository.DeletePhotoForDeletedRecord(pathToDelete);
                                            }
                                        }
                                    }
                                    rejectedFilesName.Clear();
                                    savedFilesName.Clear();
                                }

                            }
                        }

                    }

                    // Consumer Durable Loan
                    if (loanType == StringLiteralValue.ConsumerDurableLoan)
                    {
                        IEnumerable<CustomerConsumerLoanCollateralDetailViewModel> customerConsumerLoanCollateralDetailViewModelListForAmend = await customerDetailRepository.GetConsumerLoanCollateralDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, StringLiteralValue.Reject);

                        if (customerConsumerLoanCollateralDetailViewModelListForAmend != null)
                        {
                            foreach (CustomerConsumerLoanCollateralDetailViewModel viewModel in customerConsumerLoanCollateralDetailViewModelListForAmend)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerConsumerLoanCollateralDetailData(viewModel, StringLiteralValue.Amend);
                            }
                        }

                        // New Entry Created For CustomerConsumerLoanCollateralDetailViewModel 
                        if (result)
                        {
                            List<CustomerConsumerLoanCollateralDetailViewModel> customerConsumerLoanCollateralDetailViewModelList = new List<CustomerConsumerLoanCollateralDetailViewModel>();
                            customerConsumerLoanCollateralDetailViewModelList = (List<CustomerConsumerLoanCollateralDetailViewModel>)HttpContext.Current.Session["ConsumerLoanCollateralDetail"];

                            if (customerConsumerLoanCollateralDetailViewModelList != null)
                            {
                                foreach (CustomerConsumerLoanCollateralDetailViewModel viewModel in customerConsumerLoanCollateralDetailViewModelList)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerConsumerLoanCollateralDetailData(viewModel, StringLiteralValue.Create);
                                }
                            }
                        }

                    }

                    // Cash Credit Loan
                    if (loanType == StringLiteralValue.CashCreditLoan)
                    {
                        if (_loanCustomerAccountViewModel.CustomerCashCreditLoanAccountViewModel.CustomerCashCreditLoanAccountPrmKey > 0)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerCashCreditLoanAccountData(_loanCustomerAccountViewModel.CustomerCashCreditLoanAccountViewModel, StringLiteralValue.Amend);
                        }
                    }

                    //Educational Loan
                    if (loanType == StringLiteralValue.EducationalLoan)
                    {
                        if (_loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel.CustomerEducationalLoanDetailPrmKey > 0)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerEducationalLoanDetailData(_loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                // CustomerLoanFieldInvestigation
                if (result)
                {
                    if (_loanCustomerAccountViewModel.CustomerLoanFieldInvestigationViewModel.CustomerLoanFieldInvestigationPrmKey > 0)
                    {
                        if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableFieldInvestigation)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerLoanFieldInvestigationData(_loanCustomerAccountViewModel.CustomerLoanFieldInvestigationViewModel, StringLiteralValue.Amend);
                        }
                        else
                        {
                            result = customerAccountDbContextRepository.AttachCustomerLoanFieldInvestigationData(_loanCustomerAccountViewModel.CustomerLoanFieldInvestigationViewModel, StringLiteralValue.Delete);
                        }
                    }
                    else
                    {
                        if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableFieldInvestigation)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerLoanFieldInvestigationData(_loanCustomerAccountViewModel.CustomerLoanFieldInvestigationViewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // CustomerLoanAccountDebtToIncomeRatio
                if (result)
                {
                    if (_loanCustomerAccountViewModel.CustomerLoanAccountDebtToIncomeRatioViewModel.CustomerLoanAccountDebtToIncomeRatioPrmKey > 0)
                    {
                        if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCaptureDebtToIncomeRatio == true)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerLoanAccountDebtToIncomeRatioData(_loanCustomerAccountViewModel.CustomerLoanAccountDebtToIncomeRatioViewModel, StringLiteralValue.Amend);
                        }
                    }
                }

                //CustomerAccountDocument
                //Old Record Amend
                if (result)
                {
                    IEnumerable<CustomerAccountDocumentViewModel> customerAccountDocumentViewModelListForAmend = await customerDetailRepository.GetDocumentEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);
                    foreach (CustomerAccountDocumentViewModel viewModel in customerAccountDocumentViewModelListForAmend)
                    {
                        SchemeDocumentViewModel schemeDocumentViewModel = await schemeDetailRepository.GetDocumentEntry(accountDetailRepository.GetSchemePrmKeyById(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId), viewModel.DocumentPrmKey, StringLiteralValue.Verify);

                        if (customerAccountDocumentViewModelListForAmend != null)
                        {
                            // Add Rejected Record In rejectedFileName 
                            rejectedFilesName.Add(viewModel.NameOfFile);
                            result = customerAccountDbContextRepository.AttachCustomerAccountDocumentData(viewModel, schemeDocumentViewModel.DocumentLocalStoragePath, viewModel.NameOfFile, StringLiteralValue.Amend);
                        }
                    }

                }

                // New Record Create For Amend
                if (result)
                {
                    List<CustomerAccountDocumentViewModel> CustomerAccountDocumentViewModelList = (List<CustomerAccountDocumentViewModel>)HttpContext.Current.Session["Document"];
                    SchemeDocumentViewModel schemeDocumentViewModel = new SchemeDocumentViewModel();
                    if (CustomerAccountDocumentViewModelList != null)
                    {
                        foreach (CustomerAccountDocumentViewModel viewModel in CustomerAccountDocumentViewModelList)
                        {
                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;

                            viewModel.DocumentPrmKey = accountDetailRepository.GetDocumentPrmKeyId(viewModel.DocumentId);

                            schemeDocumentViewModel = await schemeDetailRepository.GetDocumentEntry(accountDetailRepository.GetSchemePrmKeyById(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId), viewModel.DocumentPrmKey, StringLiteralValue.Verify);
                            if (schemeDocumentViewModel.EnableDocumentUploadInLocalStorage == true)
                            {
                                if (viewModel.FileUploader != null)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerAccountDocumentInLocalStorage(viewModel, schemeDocumentViewModel.DocumentLocalStoragePath, null, StringLiteralValue.Create);
                                }
                                else
                                {
                                    viewModel.NameOfFile = oldFileName;
                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                }
                            }

                            // If Db Storage
                            else
                            {
                                if (viewModel.FileUploader != null)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerAccountDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                }
                                else
                                {
                                    viewModel.NameOfFile = oldFileName;
                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                }
                            }

                            // Add New Saved Record in savedFileNameList
                            savedFilesName.Add(viewModel.NameOfFile);

                            result = customerAccountDbContextRepository.AttachCustomerAccountDocumentData(viewModel, schemeDocumentViewModel.DocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);

                        }

                        //To Collect Only Deleted Or Changed Photo Records
                        if (rejectedFilesName != null && savedFilesName != null)
                        {
                            for (byte i = 0; i < rejectedFilesName.Count; i++)
                            {
                                if (!savedFilesName.Contains(rejectedFilesName[i]))
                                {
                                    string serverDestinationPath = HttpContext.Current.Server.MapPath(schemeDocumentViewModel.DocumentLocalStoragePath);
                                    string pathToDelete = Path.Combine(serverDestinationPath, rejectedFilesName[i]);
                                    result = customerAccountDbContextRepository.DeletePhotoForDeletedRecord(pathToDelete);
                                }
                            }
                        }
                        rejectedFilesName.Clear();
                        savedFilesName.Clear();
                    }
                }

                // CustomerAccountNoticeSchedule
                // Old Record Amended For Amend 
                IEnumerable<CustomerAccountNoticeScheduleViewModel> CustomerAccountNoticeScheduleViewModelListForAmend = await customerDetailRepository.GetNoticeScheduleEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, StringLiteralValue.Reject);

                if (CustomerAccountNoticeScheduleViewModelListForAmend.Count() > 0)
                {
                    foreach (CustomerAccountNoticeScheduleViewModel viewModel in CustomerAccountNoticeScheduleViewModelListForAmend)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // New Record Create For Amend 
                if (result)
                {
                    List<CustomerAccountNoticeScheduleViewModel> customerAccountNoticeScheduleViewModelList = new List<CustomerAccountNoticeScheduleViewModel>();
                    customerAccountNoticeScheduleViewModelList = (List<CustomerAccountNoticeScheduleViewModel>)HttpContext.Current.Session["NoticeSchedule"];

                    if (customerAccountNoticeScheduleViewModelList != null)
                    {
                        foreach (CustomerAccountNoticeScheduleViewModel viewModel in customerAccountNoticeScheduleViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, StringLiteralValue.Create);
                        }
                    }

                }

                //CustomerLoanAcquaintanceDetail
                // Old Record Amended For Amend 
                if (result)
                {
                    // Get CustomerLoanAcquaintanceDetail From Session Object
                    IEnumerable<CustomerLoanAcquaintanceDetailViewModel> CustomerLoanAcquaintanceDetailViewModelListForAmend = await customerDetailRepository.GetAcquaintanceDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, StringLiteralValue.Reject);

                    if (CustomerLoanAcquaintanceDetailViewModelListForAmend.Count() > 0)
                    {
                        foreach (CustomerLoanAcquaintanceDetailViewModel viewModel in CustomerLoanAcquaintanceDetailViewModelListForAmend)
                        {
                            if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableAcquaintanceDetails == true)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAcquaintanceDetail(viewModel, StringLiteralValue.Amend);
                            }
                            else
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAcquaintanceDetail(viewModel, StringLiteralValue.Delete);
                            }
                        }
                    }
                }

                // New Record Create For Amend 
                if (result)
                {
                    List<CustomerLoanAcquaintanceDetailViewModel> customerLoanAcquaintanceDetailViewModelList = new List<CustomerLoanAcquaintanceDetailViewModel>();
                    customerLoanAcquaintanceDetailViewModelList = (List<CustomerLoanAcquaintanceDetailViewModel>)HttpContext.Current.Session["AcquaintanceDetail"];

                    if (customerLoanAcquaintanceDetailViewModelList != null)
                    {
                        foreach (CustomerLoanAcquaintanceDetailViewModel viewModel in customerLoanAcquaintanceDetailViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerLoanAcquaintanceDetail(viewModel, StringLiteralValue.Create);
                        }
                    }

                }

                //Final Method
                if (result)
                {
                    result = await customerAccountDbContextRepository.SaveData();
                }

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

        public List<SelectListItem> LoanCustomerAccountDropdownList
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
                                //join t in context.CustomerAccountTranslations on d.PrmKey equals t.LoanCustomerAccountPrmKey into bt
                                //from t in bt.DefaultIfEmpty()
                            where (d.EntryStatus.Equals(StringLiteralValue.Verify))
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    //&& (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    //                            && (d.ActivationStatus.Equals(StringLiteralValue.Active))
                                    || (d.EntryStatus == StringLiteralValue.Verify)
                            //&& (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                            //                            && (d.IsModified.Equals(false))
                            //&& (t.LanguagePrmKey == regionalLanguagePrmKey)
                            //                    orderby d.NameOfLoanCustomerAccount
                            select new SelectListItem
                            {
                                Value = d.CustomerAccountId.ToString(),
                                Text = ""//((mf.NameOfLoanCustomerAccount.Equals(null)) ? d.NameOfLoanCustomerAccount.Trim() + " ---> " + (t.TransNameOfLoanCustomerAccount.Equals(null) ? " " : t.TransNameOfLoanCustomerAccount.Trim()) : mf.NameOfLoanCustomerAccount + " ---> " + (t.TransNameOfLoanCustomerAccount.Equals(null) ? " " : t.TransNameOfLoanCustomerAccount.Trim()))
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
                            Text = "" //((mf.NameOfCustomerAccount.Equals(null)) ? d.NameOfLoanCustomerAccount.Trim() : mf.NameOfLoanCustomerAccount)
                        }).ToList();
            }
        }

        public async Task<IEnumerable<LoanCustomerAccountIndexViewModel>> GetLoanCustomerAccountIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<LoanCustomerAccountIndexViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public List<SelectListItem> LoanCustomerAccountDropdownListForEmployeeCatgory
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
                            //&& (d.LoanCustomerAccountCategory.Equals("EMP"))
                            //                            orderby d.NameOfLoanCustomerAccount
                            select new SelectListItem
                            {
                                Value = d.CustomerAccountId.ToString(),
                                Text = "" //((mf.NameOfLoanCustomerAccount.Equals(null)) ? d.NameOfLoanCustomerAccount.Trim() + " ---> " + (t.TransNameOfLoanCustomerAccount.Equals(null) ? " " : t.TransNameOfLoanCustomerAccount.Trim()) : mf.NameOfLoanCustomerAccount + " ---> " + (t.TransNameOfLoanCustomerAccount.Equals(null) ? " " : t.TransNameOfLoanCustomerAccount.Trim()))
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
                        //                        orderby d.NameOfLoanCustomerAccount
                        select new SelectListItem
                        {
                            Value = d.CustomerAccountId.ToString(),
                            Text = "" //((mf.NameOfLoanCustomerAccount.Equals(null)) ? d.NameOfLoanCustomerAccount.Trim() : mf.NameOfLoanCustomerAccount.Trim())
                        }).ToList();
            }
        }

        public async Task<bool> GetSessionValues(LoanCustomerAccountViewModel _loanCustomerAccountViewModel, string _entryType)
        {
            try
            {

                HttpContext.Current.Session["JointAccountHolder"] = await customerDetailRepository.GetJointAccountHolderEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["Nominee"] = await customerDetailRepository.GetNomineeEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = await customerDetailRepository.GetNomineeEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);
                foreach (CustomerAccountNomineeViewModel customerAccountNomineeViewModel in customerAccountNomineeViewModelList)
                {
                    customerAccountNomineeViewModel.CustomerAccountNomineeGuardianViewModelList = customerDetailRepository.GetNomineeGuardianEntries(customerAccountNomineeViewModel.PrmKey, _entryType);
                }
                HttpContext.Current.Session["Nominee"] = customerAccountNomineeViewModelList;

                HttpContext.Current.Session["GuarantorDetail"] = await customerDetailRepository.GetLoanAccountGuarantorDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, _entryType);

                if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel != null)
                {
                    HttpContext.Current.Session["VehicleLoanPhoto"] = await customerDetailRepository.GetVehicleLoanPhotoEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, _entryType);
                }

                HttpContext.Current.Session["BorrowingDetail"] = await customerDetailRepository.GetCustomerAccountBorrowingDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["ContactDetail"] = await customerDetailRepository.GetCustomerAccountContactDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["AddressDetail"] = await customerDetailRepository.GetCustomerAccountAddressDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["GoldLoanCollateralDetail"] = await customerDetailRepository.GetGoldLoanCollateralDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, _entryType);

                HttpContext.Current.Session["GoldLoanCollateralPhoto"] = await customerDetailRepository.GetGoldLoanCollateralPhotoEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, _entryType);

                HttpContext.Current.Session["Document"] = await customerDetailRepository.GetDocumentEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["NoticeSchedule"] = await customerDetailRepository.GetNoticeScheduleEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["AcquaintanceDetail"] = await customerDetailRepository.GetAcquaintanceDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, _entryType);

                HttpContext.Current.Session["PersonIncomeTaxDetail"] = await customerDetailRepository.GetCustomerAccountIncomeTaxDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["PersonCourtCase"] = await customerDetailRepository.GetCustomerAccountCourtCaseEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["PersonAdditionalIncomeDetail"] = await customerDetailRepository.GetCustomerAccountAdditionalIncomeDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, _entryType);

                HttpContext.Current.Session["LoanAgainstDepositCollateralDetail"] = await customerDetailRepository.GetCustomerLoanAgainstDepositCollateralDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, _entryType);

                HttpContext.Current.Session["ConsumerLoanCollateralDetail"] = await customerDetailRepository.GetConsumerLoanCollateralDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, _entryType);
               
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public int GetCustomerLoanAccountPrmKeyByCustomerAccountId(Guid _customerAccountId)
        {
            long customerAccountPrmKey = accountDetailRepository.GetCustomerAccountPrmKeyById(_customerAccountId);

            return context.CustomerLoanAccounts
                    .Where(c => c.CustomerAccountPrmKey == customerAccountPrmKey)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public int GetCustomerLoanAccountPrmKeyByCustomerAccountId(Guid _customerAccountId, string _entyrStatus)
        {
            long customerAccountPrmKey = accountDetailRepository.GetCustomerAccountPrmKeyById(_customerAccountId);

            if (_entyrStatus == StringLiteralValue.Unverified)
            {
                _entyrStatus = StringLiteralValue.Create;
            }

            var a = context.CustomerLoanAccounts
                    .Where(c => c.CustomerAccountPrmKey == customerAccountPrmKey && c.EntryStatus == _entyrStatus)
                    .OrderByDescending(c => c.PrmKey)
                    .Select(c => c.PrmKey).FirstOrDefault();
            return a;
        }

        public async Task<LoanCustomerAccountViewModel> GetLoanCustomerAccountEntry(Guid _customerAccountId, string _entryType)
        {
            try
            {
                long customerAccountPrmkey = accountDetailRepository.GetCustomerAccountPrmKeyById(_customerAccountId);

                LoanCustomerAccountViewModel loanCustomerAccountViewModel = await context.Database.SqlQuery<LoanCustomerAccountViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountEntry (@CustomerAccountPrmKey, @EntriesType)", new SqlParameter("@CustomerAccountPrmKey", customerAccountPrmkey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
                loanCustomerAccountViewModel.CustomerAccountDetailViewModel = await customerDetailRepository.GetAccountDetailEntry(customerAccountPrmkey, _entryType);
                loanCustomerAccountViewModel.CustomerLoanAccountViewModel = await customerDetailRepository.GetLoanAccountEntry(customerAccountPrmkey, _entryType);
                loanCustomerAccountViewModel.CustomerAccountInterestRateViewModel = await customerDetailRepository.GetInterestRateEntry(customerAccountPrmkey, _entryType);

                int customerLoanAccountPrmKey = loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey;
                string loanType = accountDetailRepository.GetSysNameOfLoanTypeBySchemePrmKey(loanCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemePrmKey);

                CustomerLoanAccountOpeningConfigViewModel customerLoanAccountOpeningConfigViewModel = await schemeDetailRepository.GetCustomerLoanAccountConfigDetail(loanCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

               
                if(loanCustomerAccountViewModel.CustomerLoanAccountViewModel.IsEmployee== false)
                {
                    loanCustomerAccountViewModel.PersonEmploymentDetailViewModel= await customerDetailRepository.GetCustomerAccountEmploymentDetailEntry(customerAccountPrmkey, _entryType);
                }
                
                loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel = await customerDetailRepository.GetStandingInstructionEntry(customerAccountPrmkey, _entryType);
               

                // Email Service
                if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService)
                {
                    loanCustomerAccountViewModel.CustomerAccountEmailServiceViewModel = await customerDetailRepository.GetEmailServiceEntry(customerAccountPrmkey, _entryType);
                }

                // SMS Service
                if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService)
                {
                    loanCustomerAccountViewModel.CustomerAccountSmsServiceViewModel = await customerDetailRepository.GetSmsServiceEntry(customerAccountPrmkey, _entryType);
                }

                // Field Investigation
                if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableFieldInvestigation)
                {
                    loanCustomerAccountViewModel.CustomerLoanFieldInvestigationViewModel = await customerDetailRepository.GetLoanFieldInvestigationEntry(customerLoanAccountPrmKey, _entryType);
                }

                // Debt To Income Ratio
                if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCaptureDebtToIncomeRatio)
                {
                    loanCustomerAccountViewModel.CustomerLoanAccountDebtToIncomeRatioViewModel = await customerDetailRepository.GetCustomerLoanAccountDebtToIncomeRatioEntry(customerLoanAccountPrmKey, _entryType);
                }

                // ********* LOAN TYPE ************
                // Business Loan
                if (loanType == StringLiteralValue.ShortTermBusinessLoan)
                {
                    loanCustomerAccountViewModel.CustomerBusinessLoanCollateralDetailViewModel = await customerDetailRepository.GetCustomerBusinessLoanCollateralDetailEntry(customerLoanAccountPrmKey, _entryType);
                }

                // Cash Credit Loan
                if (loanType == StringLiteralValue.CashCreditLoan)
                {
                    loanCustomerAccountViewModel.CustomerCashCreditLoanAccountViewModel = await customerDetailRepository.GetCustomerCashCreditLoanAccountEntry(customerLoanAccountPrmKey, _entryType);
                }

                // Educational Loan
                if (loanType == StringLiteralValue.EducationalLoan)
                {
                    loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel = await customerDetailRepository.GetCustomerEducationalLoanDetailEntry(customerLoanAccountPrmKey, _entryType);
                }

                // Loan Against Property Or Home Loan
                if (loanType == StringLiteralValue.LoanAgainstProperty || loanType == StringLiteralValue.HomeLoan)
                {
                    loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel = await customerDetailRepository.GetCustomerLoanAgainstPropertyCollateralDetailEntry(customerLoanAccountPrmKey, _entryType);
                }

                // Vehicle Loan
                if (loanType == StringLiteralValue.VehicleLoan)
                {
                    loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel = await customerDetailRepository.GetVehicleLoanCollateralDetailEntry(customerLoanAccountPrmKey, _entryType);

                    if (loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel != null)
                    {
                        if (loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.LoanPurpose != StringLiteralValue.NewVehicle && customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.EnableVehicleInspection == true)
                        {
                            loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel = await customerDetailRepository.GetPreOwnedVehicleLoanInspectionEntry(customerLoanAccountPrmKey, _entryType);
                        }

                        if (loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.IsUsedForCommercial == true)
                        {
                            loanCustomerAccountViewModel.CustomerVehicleLoanPermitDetailViewModel = await customerDetailRepository.GetCustomerVehicleLoanPermitDetailEntry(customerLoanAccountPrmKey, _entryType);

                            if (loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.HasContract == true)
                            {
                                loanCustomerAccountViewModel.CustomerVehicleLoanContractDetailViewModel = await customerDetailRepository.GetCustomerVehicleLoanContractDetailEntry(customerLoanAccountPrmKey, _entryType);
                            }
                        }
                    }

                    loanCustomerAccountViewModel.CustomerVehicleLoanInsuranceDetailViewModel = await customerDetailRepository.GetLoanAccountVehicleInsuranceDetailEntry(customerLoanAccountPrmKey, _entryType);
                }

                return loanCustomerAccountViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> IsValidAccountNumber(Guid _schemeId, int _accountNumber)
        {
            try
            {
                short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);

                return await context.Database.SqlQuery<bool>("SELECT dbo.IsValidAccountNumber (@SchemePrmKey, @BusinessOfficePrmKey, @AccountNumber)", new SqlParameter("@SchemePrmKey", schemePrmKey), new SqlParameter("@BusinessOfficePrmKey", (short)HttpContext.Current.Session["UserHomeBranchPrmKey"]), new SqlParameter("AccountNumber", _accountNumber)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> EnablePreOwnedVehiclePhotoUploadInLocalStorage(Guid _schemeId)
        {
            try
            {
                short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);

                return await context.SchemeVehicleLoanParameters
                                    .Where(a => a.SchemePrmKey == schemePrmKey && a.EntryStatus == StringLiteralValue.Verify)
                                    .Select(a => a.EnablePreOwnedVehiclePhotoUploadInLocalStorage).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<string> GetPreOwnedVehiclePhotoUpload(Guid _schemeId)
        {
            try
            {
                short schemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_schemeId);

                return await context.SchemeVehicleLoanParameters
                                    .Where(a => a.SchemePrmKey == schemePrmKey && a.EntryStatus == StringLiteralValue.Verify)
                                    .Select(a => a.PreOwnedVehiclePhotoUpload).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
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

        public async Task<bool> Save(LoanCustomerAccountViewModel _loanCustomerAccountViewModel)
        {
            try
            {
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                CustomerLoanAccountOpeningConfigViewModel customerLoanAccountOpeningConfigViewModel = await schemeDetailRepository.GetCustomerLoanAccountConfigDetail(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);

                // If AlternateAccountNumber2 Setting In SchemeAccountParameter
                if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber2 == false)
                {
                    _loanCustomerAccountViewModel.AlternateAccountNumber2 = _loanCustomerAccountViewModel.AlternateAccountNumber2 = "None";
                }

                // If AlternateAccountNumber3 Setting In SchemeAccountParameter
                if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableAlternateAccountNumber3 == false)
                {
                    _loanCustomerAccountViewModel.AlternateAccountNumber3 = _loanCustomerAccountViewModel.AlternateAccountNumber3 = "None";
                }

                string loanType = accountDetailRepository.GetSysNameOfLoanTypeByLoanTypeId(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.LoanTypeId);
                string occupation = accountDetailRepository.GetSysNameOfOccupationById(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.OccupationId);
                bool result = true;
                
                //LoanCustomerAccount
                if (result)
                {
                    result = customerAccountDbContextRepository.AttachLoanCustomerAccountData(_loanCustomerAccountViewModel, StringLiteralValue.Create);
                }

                //CustomerLoanAccount
                if (result)
                {
                    result = customerAccountDbContextRepository.AttachCustomerLoanAccountData(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel, StringLiteralValue.Create);
                }

                //CustomerAccountDetail
                if (result)
                {
                    result = customerAccountDbContextRepository.AttachCustomerAccountDetailData(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel, StringLiteralValue.Create);
                }

                //EmploymentDetail
                if (result)
                {
                    if ((occupation == StringLiteralValue.Salaried || occupation == StringLiteralValue.SelfEmployedBusiness || occupation == StringLiteralValue.SelfEmployedProfessional) && _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.IsEmployee == false)
                    {
                        result = customerAccountDbContextRepository.AttachPersonEmploymentDetailData(_loanCustomerAccountViewModel.PersonEmploymentDetailViewModel, StringLiteralValue.Create);
                    }
                }

                //CustomerAccountInterestRate
                if (result)
                {
                    _loanCustomerAccountViewModel.CustomerAccountInterestRateViewModel.EffectiveDate = _loanCustomerAccountViewModel.AccountOpeningDate;
                    result = customerAccountDbContextRepository.AttachCustomerAccountInterestRateData(_loanCustomerAccountViewModel.CustomerAccountInterestRateViewModel, StringLiteralValue.Create);
                }

                // CustomerAccountSmsService
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == true)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountSmsServiceData(_loanCustomerAccountViewModel.CustomerAccountSmsServiceViewModel, StringLiteralValue.Create);
                    }
                }

                //CustomerAccountEmailService
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountEmailServiceData(_loanCustomerAccountViewModel.CustomerAccountEmailServiceViewModel, StringLiteralValue.Create);
                    }
                }

                //CustomerJointAccountHolder
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumJointAccountHolder != 0)
                    {
                        List<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelList = new List<CustomerJointAccountHolderViewModel>();
                        customerJointAccountHolderViewModelList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["JointAccountHolder"];

                        if (customerJointAccountHolderViewModelList != null)
                        {
                            foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //Borrowing Detail SchemeLoanAccountParameterViewModel.EnableBorrowingDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableBorrowingDetail == true)
                    {
                        List<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelList = new List<PersonBorrowingDetailViewModel>();
                        personBorrowingDetailViewModelList = (List<PersonBorrowingDetailViewModel>)HttpContext.Current.Session["BorrowingDetail"];

                        if (personBorrowingDetailViewModelList != null)
                        {
                            foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // CourtCaseDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCourtCaseDetail == true)
                    {
                        List<PersonCourtCaseViewModel> personCourtCaseViewModelList = (List<PersonCourtCaseViewModel>)HttpContext.Current.Session["PersonCourtCase"];

                        if (personCourtCaseViewModelList != null)
                        {
                            foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // PersonIncomeTaxDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableIncomeTaxDetail == true)
                    {
                        // Insert Record From Session Object
                        List<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelList = (List<PersonIncomeTaxDetailViewModel>)HttpContext.Current.Session["PersonIncomeTaxDetail"];

                        if (personIncomeTaxDetailViewModelList != null)
                        {
                            foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModelList)
                            {
                                string oldLocalStoragePath = viewModel.LocalStoragePath;
                                string oldFileName = viewModel.NameOfFile;
                                viewModel.Remark = _loanCustomerAccountViewModel.Remark;
                                //viewModel.PersonPrmKey = _loanCustomerAccountViewModel.PersonPrmKey;

                                result = customerAccountDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, StringLiteralValue.Create);

                                if (personIncomeTaxDetailViewModelList != null)
                                {
                                    if (personInformationParameterViewModel.IncomeTaxDocumentUpload != StringLiteralValue.Disable)
                                    {
                                        // EnableIncomeTaxDocumentUploadInLocalStorage
                                        if (personInformationParameterViewModel.EnableIncomeTaxDocumentUploadInLocalStorage == true)
                                        {
                                            //If Photo Changed Then Add New FileName And LocalStoragePath Else Add Old FileName And LocalStoragePath
                                            if (viewModel.PhotoPathTax != null)
                                            {
                                                result = customerAccountDbContextRepository.AttachIncomeTaxDetailDocumentInLocalStorage(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }

                                        // If Db Storage
                                        else
                                        {
                                            if (viewModel.PhotoPathTax != null)
                                            {
                                                result = customerAccountDbContextRepository.AttachIncomeTaxDetailDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }

                                        result = customerAccountDbContextRepository.AttachPersonIncomeTaxDocumentData(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                    }

                                }
                            }
                        }
                    }
                }


                // AdditionalIncomeDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableAdditionalIncomeDetail == true)
                    {
                        List<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModelList = (List<PersonAdditionalIncomeDetailViewModel>)HttpContext.Current.Session["PersonAdditionalIncomeDetail"];

                        if (personAdditionalIncomeDetailViewModelList != null)
                        {
                            foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //CustomerAccountNominee
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.MaximumNominee != 0)
                    {
                        List<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = new List<CustomerAccountNomineeViewModel>();
                        customerAccountNomineeViewModelList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["Nominee"];

                        if (customerAccountNomineeViewModelList != null)
                        {
                            foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerAccountNomineeData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //Address
                if (result)
                {
                    List<PersonAddressViewModel> personAddressViewModelList = new List<PersonAddressViewModel>();
                    personAddressViewModelList = (List<PersonAddressViewModel>)HttpContext.Current.Session["AddressDetail"];

                    if (personAddressViewModelList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in personAddressViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //Contact
                if (result)
                {
                    List<PersonContactDetailViewModel> personContactDetailViewModelList = new List<PersonContactDetailViewModel>();
                    personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["ContactDetail"];

                    if (personContactDetailViewModelList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //CustomerLoanAccountGuarantorDetailViewModel
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableGuarantorDetail == true)
                    {
                        List<CustomerLoanAccountGuarantorDetailViewModel> customerLoanAccountGuarantorDetailViewModelList = new List<CustomerLoanAccountGuarantorDetailViewModel>();
                        customerLoanAccountGuarantorDetailViewModelList = (List<CustomerLoanAccountGuarantorDetailViewModel>)HttpContext.Current.Session["GuarantorDetail"];

                        if (customerLoanAccountGuarantorDetailViewModelList != null)
                        {
                            foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerLoanAccountGuarantorDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAccountGuarantorDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                // Standing Insturction
                if (_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel.EnableAutoDebit == true)
                {
                    result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, StringLiteralValue.Create, StringLiteralValue.DebitAccount);
                }

                //CustomerLoanAgainstDepositCollateralDetail
                if (result)
                {
                    if ((loanType == StringLiteralValue.CashCreditLoan && customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.EnableFixedDepositAsCollateral == true) ||loanType == StringLiteralValue.LoanAgainstDeposit || customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableDepositAsCollateral == true)
                    {
                        List<CustomerLoanAgainstDepositCollateralDetailViewModel> customerLoanAgainstDepositCollateralDetailViewModelList = new List<CustomerLoanAgainstDepositCollateralDetailViewModel>();
                        customerLoanAgainstDepositCollateralDetailViewModelList = (List<CustomerLoanAgainstDepositCollateralDetailViewModel>)HttpContext.Current.Session["LoanAgainstDepositCollateralDetail"];

                        if (customerLoanAgainstDepositCollateralDetailViewModelList != null)
                        {
                            foreach (CustomerLoanAgainstDepositCollateralDetailViewModel viewModel in customerLoanAgainstDepositCollateralDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAgainstDepositCollateralDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //Loan Type
                if (result)
                {
                    //CustomerLoanAgainstPropertyCollateralDetail
                    if (loanType == StringLiteralValue.LoanAgainstProperty || loanType == StringLiteralValue.HomeLoan)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerLoanAgainstPropertyCollateralDetailData(_loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel, StringLiteralValue.Create);
                    }

                    //BusinessLoan
                    if (loanType == StringLiteralValue.ShortTermBusinessLoan)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerBusinessLoanCollateralDetailData(_loanCustomerAccountViewModel.CustomerBusinessLoanCollateralDetailViewModel, StringLiteralValue.Create);
                    }

                    //VehicleLoan
                    if (loanType == StringLiteralValue.VehicleLoan)
                    {
                        //CustomerVehicleLoanCollateralDetail
                        result = customerAccountDbContextRepository.AttachCustomerVehicleLoanCollateralDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel, StringLiteralValue.Create);

                        //CustomerLoanAccountVehicleInsuranceDetail
                        if (result)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerLoanAccountVehicleInsuranceDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanInsuranceDetailViewModel, StringLiteralValue.Create);
                        }

                        if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.IsUsedForCommercial == true)
                        {
                            //CustomerVehicleLoanPermitDetail
                            if (result)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPermitDetail(_loanCustomerAccountViewModel.CustomerVehicleLoanPermitDetailViewModel, StringLiteralValue.Create);
                            }

                            if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.HasContract == true)
                            {
                                //CustomerVehicleLoanContractDetail
                                if (result)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerVehicleLoanContractDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanContractDetailViewModel, StringLiteralValue.Create);
                                }
                            }
                        }
                        if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.LoanPurpose != StringLiteralValue.NewVehicle && customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.EnableVehicleInspection == true)
                        {
                            //CustomerPreOwnedVehicleLoanInspection
                            if (result)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerPreOwnedVehicleLoanInspectionData(_loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel, StringLiteralValue.Create);
                            }
                        }

                        //CustomerVehicleLoanPhoto
                        if (result)
                        {
                            List<CustomerVehicleLoanPhotoViewModel> CustomerVehicleLoanPhotoViewModelList = (List<CustomerVehicleLoanPhotoViewModel>)HttpContext.Current.Session["VehicleLoanPhoto"];

                            if (CustomerVehicleLoanPhotoViewModelList != null)
                            {
                                foreach (CustomerVehicleLoanPhotoViewModel viewModel in CustomerVehicleLoanPhotoViewModelList)
                                {
                                    string oldLocalStoragePath = viewModel.LocalStoragePath;

                                    string oldFileName = viewModel.NameOfFile;

                                    if (customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.PhotoUpload != StringLiteralValue.Disable)
                                    {
                                        // If Local Storage
                                        if (customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.EnablePhotoUploadInLocalStorage == true)
                                        {
                                            if (viewModel.PhotoPath != null)
                                            {
                                                result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPhotoInLocalStorage(viewModel, customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.StoragePath, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }

                                        // If Db Storage
                                        else
                                        {
                                            if (viewModel.PhotoPath != null)
                                            {
                                                result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPhotoInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                            }
                                            else
                                            {
                                                viewModel.NameOfFile = oldFileName;
                                                viewModel.LocalStoragePath = oldLocalStoragePath;
                                            }
                                        }

                                        result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPhotoData(viewModel, customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.StoragePath, oldFileName, StringLiteralValue.Create);
                                    }

                                }
                            }
                        }

                    }

                    //GoldLoan
                    if (loanType == StringLiteralValue.GoldLoan)
                    {
                        List<CustomerGoldLoanCollateralDetailViewModel> customerGoldLoanCollateralDetailViewModelList = new List<CustomerGoldLoanCollateralDetailViewModel>();
                        customerGoldLoanCollateralDetailViewModelList = (List<CustomerGoldLoanCollateralDetailViewModel>)HttpContext.Current.Session["GoldLoanCollateralDetail"];

                        if (customerGoldLoanCollateralDetailViewModelList != null)
                        {
                            foreach (CustomerGoldLoanCollateralDetailViewModel viewModel in customerGoldLoanCollateralDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }

                        // CustomerGoldLoanCollateralPhotoViewModel
                        if (result)
                        {
                            if (customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.EnableGoldPhoto == true)
                            {
                                List<CustomerGoldLoanCollateralPhotoViewModel> customerGoldLoanCollateralPhotoViewModelList = (List<CustomerGoldLoanCollateralPhotoViewModel>)HttpContext.Current.Session["GoldLoanCollateralPhoto"];

                                if (customerGoldLoanCollateralPhotoViewModelList != null)
                                {
                                    foreach (CustomerGoldLoanCollateralPhotoViewModel viewModel in customerGoldLoanCollateralPhotoViewModelList)
                                    {
                                        string oldLocalStoragePath = viewModel.LocalStoragePath;

                                        string oldFileName = viewModel.NameOfFile;

                                        if (viewModel.PhotoCaption == null)
                                        {
                                            viewModel.PhotoCaption = "None";
                                        }

                                        if (customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.GoldPhotoUpload != StringLiteralValue.Disable)
                                        {
                                            // If Local Storage
                                            if (customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.EnableGoldPhotoUploadInLocalStorage == true)
                                            {
                                                if (viewModel.PhotoPath != null)
                                                {
                                                    result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralPhotoInLocalStorage(viewModel, customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.GoldPhotoLocalStoragePath, null, StringLiteralValue.Create);
                                                }
                                                else
                                                {
                                                    viewModel.NameOfFile = oldFileName;
                                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                                }
                                            }

                                            // If Db Storage
                                            else
                                            {
                                                if (viewModel.PhotoPath != null)
                                                {
                                                    result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralPhotoInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                                }
                                                else
                                                {
                                                    viewModel.NameOfFile = oldFileName;
                                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                                }
                                            }

                                            result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralPhotoData(viewModel, customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.GoldPhotoLocalStoragePath, oldFileName, StringLiteralValue.Create);
                                        }

                                    }
                                }
                            }
                        }
                    }

                    //Consumer Durable Loan
                    if (loanType == StringLiteralValue.ConsumerDurableLoan)
                    {
                        List<CustomerConsumerLoanCollateralDetailViewModel> customerConsumerLoanCollateralDetailViewModelList = new List<CustomerConsumerLoanCollateralDetailViewModel>();
                        customerConsumerLoanCollateralDetailViewModelList = (List<CustomerConsumerLoanCollateralDetailViewModel>)HttpContext.Current.Session["ConsumerLoanCollateralDetail"];

                        if (customerConsumerLoanCollateralDetailViewModelList != null)
                        {
                            foreach (CustomerConsumerLoanCollateralDetailViewModel viewModel in customerConsumerLoanCollateralDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerConsumerLoanCollateralDetailData(viewModel, StringLiteralValue.Create);
                            }
                        }

                    }

                    //Cash Credit Loan
                    if (loanType == StringLiteralValue.CashCreditLoan)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerCashCreditLoanAccountData(_loanCustomerAccountViewModel.CustomerCashCreditLoanAccountViewModel, StringLiteralValue.Create);
                    }

                    //Educationa lLoan
                    if (loanType == StringLiteralValue.EducationalLoan)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerEducationalLoanDetailData(_loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel, StringLiteralValue.Create);
                    }

                }

                // CustomerLoanFieldInvestigation
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableFieldInvestigation)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerLoanFieldInvestigationData(_loanCustomerAccountViewModel.CustomerLoanFieldInvestigationViewModel, StringLiteralValue.Create);
                    }
                }

                // CustomerAccountDocument 
                if (result)
                {
                    List<CustomerAccountDocumentViewModel> CustomerAccountDocumentViewModelList = (List<CustomerAccountDocumentViewModel>)HttpContext.Current.Session["Document"];

                    if (CustomerAccountDocumentViewModelList != null)
                    {
                        foreach (CustomerAccountDocumentViewModel viewModel in CustomerAccountDocumentViewModelList)
                        {
                            string oldLocalStoragePath = viewModel.LocalStoragePath;

                            string oldFileName = viewModel.NameOfFile;

                            viewModel.DocumentPrmKey = accountDetailRepository.GetDocumentPrmKeyId(viewModel.DocumentId);

                            SchemeDocumentViewModel schemeDocumentViewModel = await schemeDetailRepository.GetDocumentEntry(accountDetailRepository.GetSchemePrmKeyById(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId), viewModel.DocumentPrmKey, StringLiteralValue.Verify);
                            if (schemeDocumentViewModel.EnableDocumentUploadInLocalStorage == true)
                            {
                                if (viewModel.FileUploader != null)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerAccountDocumentInLocalStorage(viewModel, schemeDocumentViewModel.DocumentLocalStoragePath, null, StringLiteralValue.Create);
                                }
                                else
                                {
                                    viewModel.NameOfFile = oldFileName;
                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                }
                            }

                            // If Db Storage
                            else
                            {
                                if (viewModel.FileUploader != null)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerAccountDocumentInDatabaseStorage(viewModel, null, StringLiteralValue.Create);
                                }
                                else
                                {
                                    viewModel.NameOfFile = oldFileName;
                                    viewModel.LocalStoragePath = oldLocalStoragePath;
                                }
                            }

                            result = customerAccountDbContextRepository.AttachCustomerAccountDocumentData(viewModel, schemeDocumentViewModel.DocumentLocalStoragePath, oldFileName, StringLiteralValue.Create);
                        }
                    }
                }

                // CustomerLoanAccountDebtToIncomeRatio
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCaptureDebtToIncomeRatio == true)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerLoanAccountDebtToIncomeRatioData(_loanCustomerAccountViewModel.CustomerLoanAccountDebtToIncomeRatioViewModel, StringLiteralValue.Create);
                    }
                }

                // CustomerAccountNoticeSchedule
                if (result)
                {
                    List<CustomerAccountNoticeScheduleViewModel> customerAccountNoticeScheduleViewModelList = new List<CustomerAccountNoticeScheduleViewModel>();
                    customerAccountNoticeScheduleViewModelList = (List<CustomerAccountNoticeScheduleViewModel>)HttpContext.Current.Session["NoticeSchedule"];

                    if (customerAccountNoticeScheduleViewModelList != null)
                    {
                        foreach (CustomerAccountNoticeScheduleViewModel viewModel in customerAccountNoticeScheduleViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                //CustomerLoanAcquaintanceDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableAcquaintanceDetails == true)
                    {
                        List<CustomerLoanAcquaintanceDetailViewModel> CustomerLoanAcquaintanceDetailViewModelList = new List<CustomerLoanAcquaintanceDetailViewModel>();
                        CustomerLoanAcquaintanceDetailViewModelList = (List<CustomerLoanAcquaintanceDetailViewModel>)HttpContext.Current.Session["AcquaintanceDetail"];

                        if (CustomerLoanAcquaintanceDetailViewModelList != null)
                        {
                            foreach (CustomerLoanAcquaintanceDetailViewModel viewModel in CustomerLoanAcquaintanceDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAcquaintanceDetail(viewModel, StringLiteralValue.Create);
                            }
                        }
                    }
                }

                //Final Method
                if (result)
                {
                    result = await customerAccountDbContextRepository.SaveData();
                }

                if (result)
                {
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

        public async Task<bool> VerifyRejectDelete(LoanCustomerAccountViewModel _loanCustomerAccountViewModel, string _entryType)
        {
            try
            {
                CustomerLoanAccountOpeningConfigViewModel customerLoanAccountOpeningConfigViewModel = await schemeDetailRepository.GetCustomerLoanAccountConfigDetail(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId);
                PersonInformationParameterViewModel personInformationParameterViewModel = await personInformationParameterRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);

                string loanType = accountDetailRepository.GetSysNameOfLoanTypeByLoanTypeId(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.LoanTypeId);
                string occupation = accountDetailRepository.GetSysNameOfOccupationById(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.OccupationId);
                string entriesType;

                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                    entriesType = StringLiteralValue.Unverified;
                else
                    entriesType = StringLiteralValue.Reject;
                bool result = true;
                
                //LoanCustomerAccount
                if (result)
                {
                    result = customerAccountDbContextRepository.AttachLoanCustomerAccountData(_loanCustomerAccountViewModel, _entryType);
                }

                //CustomerLoanAccount
                if (result)
                {
                    CustomerLoanAccountViewModel customerLoanAccountViewModel = await customerDetailRepository.GetLoanAccountEntry(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);
                    if (customerLoanAccountViewModel != null)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerLoanAccountData(customerLoanAccountViewModel, _entryType);
                    }
                }

                //CustomerAccountDetail
                if (result)
                {
                    CustomerAccountDetailViewModel customerAccountDetailViewModel = await customerDetailRepository.GetAccountDetailEntry(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);
                    if (customerAccountDetailViewModel != null)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountDetailData(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel, _entryType);
                    }
                }

                //EmploymentDetail
                if (result)
                {
                    if ((occupation == StringLiteralValue.Salaried || occupation == StringLiteralValue.SelfEmployedBusiness || occupation == StringLiteralValue.SelfEmployedProfessional) && _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.IsEmployee == false)
                    {
                        result = customerAccountDbContextRepository.AttachPersonEmploymentDetailData(_loanCustomerAccountViewModel.PersonEmploymentDetailViewModel, _entryType);
                    }
                }

                //CustomerAccountInterestRate
                if (result)
                {
                    CustomerAccountInterestRateViewModel customerAccountInterestRateViewModel = await customerDetailRepository.GetCustomerAccountInterestRateEntry(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);
                    if (customerAccountInterestRateViewModel != null)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountInterestRateData(_loanCustomerAccountViewModel.CustomerAccountInterestRateViewModel, _entryType);
                    }
                }

                //CustomerAccountSmsService
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableSmsService == true)
                    {
                        CustomerAccountSmsServiceViewModel customerAccountSmsServiceViewModel = await customerDetailRepository.GetSmsServiceEntry(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);
                        if (customerAccountSmsServiceViewModel != null)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountSmsServiceData(_loanCustomerAccountViewModel.CustomerAccountSmsServiceViewModel, _entryType);
                        }
                    }
                }

                //CustomerAccountEmailService
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeAccountParameterViewModel.EnableEmailService == true)
                    {
                        CustomerAccountEmailServiceViewModel customerAccountEmailServiceViewModel = await customerDetailRepository.GetEmailServiceEntry(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);
                        if (customerAccountEmailServiceViewModel != null)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountEmailServiceData(_loanCustomerAccountViewModel.CustomerAccountEmailServiceViewModel, _entryType);
                        }
                    }
                }

                //CustomerJointAccountHolder
                if (result)
                {
                    IEnumerable<CustomerJointAccountHolderViewModel> customerJointAccountHolderViewModelList = await customerDetailRepository.GetJointAccountHolderEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerJointAccountHolderViewModelList != null)
                    {
                        foreach (CustomerJointAccountHolderViewModel viewModel in customerJointAccountHolderViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerJointAccountHolderData(viewModel, _entryType);
                        }
                    }
                }

                // PersonBorrowingDetail 
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableBorrowingDetail == true)
                    {
                        IEnumerable<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelList = await customerDetailRepository.GetPersonBorrowingDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (personBorrowingDetailViewModelList != null)
                        {
                            foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonBorrowingDetailData(viewModel, _entryType);
                            }
                        }
                    }
                }

                // CustomerAccountBorrowingDetail 
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableBorrowingDetail == true)
                    {
                        IEnumerable<PersonBorrowingDetailViewModel> customerAccountBorrowingDetailList = await customerDetailRepository.GetCustomerAccountBorrowingDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (customerAccountBorrowingDetailList != null)
                        {
                            foreach (PersonBorrowingDetailViewModel viewModel in customerAccountBorrowingDetailList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonBorrowingDetailData(viewModel, _entryType);
                            }
                        }
                    }
                }

                // CustomerAccountNominee And CustomerAccountNomineeGuardian
                if (result)
                {
                    IEnumerable<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = await customerDetailRepository.GetNomineeEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

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

                // PersonAddress
                if (result)
                {
                    IEnumerable<PersonAddressViewModel> personAddressViewModellList = await customerDetailRepository.GetPersonAddressDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

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
                    IEnumerable<PersonAddressViewModel> customerAccountAddressDetailList = await customerDetailRepository.GetCustomerAccountAddressDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountAddressDetailList != null)
                    {
                        foreach (PersonAddressViewModel viewModel in customerAccountAddressDetailList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonAddressData(viewModel, _entryType);
                        }
                    }
                }

                // PersonContactDetail 
                if (result)
                {
                    IEnumerable<PersonContactDetailViewModel> personContactDetailViewModelList = await customerDetailRepository.GetPersonContactDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (personContactDetailViewModelList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, _entryType);
                        }
                    }
                }

                // CustomerAccountContactDetail 
                if (result)
                {
                    IEnumerable<PersonContactDetailViewModel> customerAccountContactDetailList = await customerDetailRepository.GetCustomerAccountContactDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountContactDetailList != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in customerAccountContactDetailList)
                        {
                            result = customerAccountDbContextRepository.AttachPersonContactDetailData(viewModel, _entryType);
                        }
                    }
                }

                //PersonAdditionalIncomeDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableAdditionalIncomeDetail == true)
                    {
                        IEnumerable<PersonAdditionalIncomeDetailViewModel> _personAdditionalIncomeDetailViewModelList = await customerDetailRepository.GetPersonAdditionalIncomeDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (_personAdditionalIncomeDetailViewModelList != null)
                        {
                            foreach (PersonAdditionalIncomeDetailViewModel viewModel in _personAdditionalIncomeDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, _entryType);
                            }
                        }
                    }
                }

                //CustomerAccountAdditionalIncomeDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableAdditionalIncomeDetail == true)
                    {
                        IEnumerable<PersonAdditionalIncomeDetailViewModel> customerAccountAdditionalIncomeDetailViewModelList = await customerDetailRepository.GetCustomerAccountAdditionalIncomeDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (customerAccountAdditionalIncomeDetailViewModelList != null)
                        {
                            foreach (PersonAdditionalIncomeDetailViewModel viewModel in customerAccountAdditionalIncomeDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, _entryType);
                            }
                        }
                    }
                }

                //PersonCourtCase
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCourtCaseDetail == true)
                    {
                        IEnumerable<PersonCourtCaseViewModel> personCourtCaseViewModelList = await customerDetailRepository.GetPersonCourtCaseEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (personCourtCaseViewModelList != null)
                        {
                            foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonCourtCaseData(viewModel, _entryType);
                            }
                        }
                    }
                }

                //CustomerAccountCourtCase
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCourtCaseDetail == true)
                    {
                        IEnumerable<PersonCourtCaseViewModel> customerAccountCourtCaseViewModelList = await customerDetailRepository.GetCustomerAccountCourtCaseEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (customerAccountCourtCaseViewModelList != null)
                        {
                            foreach (PersonCourtCaseViewModel viewModel in customerAccountCourtCaseViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonCourtCaseData(viewModel, _entryType);
                            }
                        }
                    }
                }


                //PersonIncomeTaxDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableIncomeTaxDetail == true)
                    {
                        IEnumerable<PersonIncomeTaxDetailViewModel> personIncomeTaxDetailViewModelList = await customerDetailRepository.GetPersonIncomeTaxDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (personIncomeTaxDetailViewModelList != null)
                        {
                            foreach (PersonIncomeTaxDetailViewModel viewModel in personIncomeTaxDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, _entryType);

                                if (viewModel.PersonIncomeTaxDetailDocumentPrmKey > 0)
                                {
                                    result = customerAccountDbContextRepository.AttachPersonIncomeTaxDocumentData(viewModel, personInformationParameterViewModel.IncomeTaxDocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
                                }
                            }
                        }
                    }
                }


                //CustomerAccountIncomeTaxDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableIncomeTaxDetail == true)
                    {
                        IEnumerable<PersonIncomeTaxDetailViewModel> customerAccountIncomeTaxDetailViewModelList = await customerDetailRepository.GetCustomerAccountIncomeTaxDetailEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                        if (customerAccountIncomeTaxDetailViewModelList != null)
                        {
                            foreach (PersonIncomeTaxDetailViewModel viewModel in customerAccountIncomeTaxDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachPersonIncomeTaxDetailData(viewModel, _entryType);

                            }
                        }
                    }
                }

                //CustomerLoanAccountGuarantorDetailViewModel
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableGuarantorDetail == true)
                    {
                        IEnumerable<CustomerLoanAccountGuarantorDetailViewModel> customerLoanAccountGuarantorDetailViewModel = await customerDetailRepository.GetLoanAccountGuarantorDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                        if (customerLoanAccountGuarantorDetailViewModel != null)
                        {
                            foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerLoanAccountGuarantorDetailViewModel)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAccountGuarantorDetailData(viewModel, _entryType);
                            }
                        }
                    }
                }


                // Standing Insturction
                if (_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel.EnableAutoDebit == true)
                {
                    CustomerAccountStandingInstructionViewModel customerAccountStandingInstructionViewModel = await customerDetailRepository.GetStandingInstructionEntry(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);
                    if (customerAccountStandingInstructionViewModel != null)
                    {
                        result = customerAccountDbContextRepository.AttachCustomerAccountStandingInstructionData(_loanCustomerAccountViewModel.CustomerAccountStandingInstructionViewModel, _entryType, StringLiteralValue.DebitAccount);
                    }

                }

                //CustomerLoanAgainstDepositCollateralDetail
                if (result)
                {
                    if ((loanType == StringLiteralValue.CashCreditLoan && customerLoanAccountOpeningConfigViewModel.SchemeCashCreditLoanParameterViewModel.EnableFixedDepositAsCollateral == true) || loanType == StringLiteralValue.LoanAgainstDeposit || customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableDepositAsCollateral == true)
                    {
                        IEnumerable<CustomerLoanAgainstDepositCollateralDetailViewModel> customerLoanAgainstDepositCollateralDetailViewModelList = await customerDetailRepository.GetCustomerLoanAgainstDepositCollateralDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                        if (customerLoanAgainstDepositCollateralDetailViewModelList != null)
                        {
                            foreach (CustomerLoanAgainstDepositCollateralDetailViewModel viewModel in customerLoanAgainstDepositCollateralDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAgainstDepositCollateralDetailData(viewModel, _entryType);
                            }
                        }
                    }
                }

                if (result)
                {
                    //LoanAgainstProperty
                    if (loanType == StringLiteralValue.LoanAgainstProperty || loanType == StringLiteralValue.HomeLoan)
                    {
                        CustomerLoanAgainstPropertyCollateralDetailViewModel customerLoanAgainstPropertyCollateralDetailViewModel = await customerDetailRepository.GetCustomerLoanAgainstPropertyCollateralDetailEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);
                        if (customerLoanAgainstPropertyCollateralDetailViewModel != null)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerLoanAgainstPropertyCollateralDetailData(_loanCustomerAccountViewModel.CustomerLoanAgainstPropertyCollateralDetailViewModel, _entryType);
                        }
                    }

                    //BusinessLoan
                    if (loanType == StringLiteralValue.ShortTermBusinessLoan)
                    {
                        CustomerBusinessLoanCollateralDetailViewModel customerBusinessLoanCollateralDetailViewModel = await customerDetailRepository.GetCustomerBusinessLoanCollateralDetailEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);
                        if (customerBusinessLoanCollateralDetailViewModel != null)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerBusinessLoanCollateralDetailData(_loanCustomerAccountViewModel.CustomerBusinessLoanCollateralDetailViewModel, _entryType);
                        }
                    }


                    if (loanType == StringLiteralValue.VehicleLoan)
                    {
                        // CustomerVehicleLoanCollateralDetail 
                        CustomerVehicleLoanCollateralDetailViewModel customerVehicleLoanCollateralDetailViewModel = await customerDetailRepository.GetVehicleLoanCollateralDetailEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                        if (customerVehicleLoanCollateralDetailViewModel != null)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerVehicleLoanCollateralDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel, _entryType);

                            if (customerVehicleLoanCollateralDetailViewModel.IsUsedForCommercial == true)
                            {
                                // CustomerVehicleLoanPermitDetail
                                if (result)
                                {
                                    CustomerVehicleLoanPermitDetailViewModel customerVehicleLoanPermitDetailViewModel = await customerDetailRepository.GetCustomerVehicleLoanPermitDetailEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                                    if (customerVehicleLoanPermitDetailViewModel != null)
                                    {
                                        result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPermitDetail(_loanCustomerAccountViewModel.CustomerVehicleLoanPermitDetailViewModel, _entryType);

                                    }
                                }
                                if (customerVehicleLoanCollateralDetailViewModel.HasContract)
                                {
                                    //CustomerVehicleLoanContractDetail
                                    if (result)
                                    {
                                        CustomerVehicleLoanContractDetailViewModel customerVehicleLoanContractDetailViewModel = await customerDetailRepository.GetCustomerVehicleLoanContractDetailEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                                        if (customerVehicleLoanContractDetailViewModel != null)
                                        {
                                            result = customerAccountDbContextRepository.AttachCustomerVehicleLoanContractDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanContractDetailViewModel, _entryType);
                                        }
                                    }
                                }
                            }

                            //CustomerPreOwnedVehicleLoanInspection
                            if (result)
                            {
                                if (_loanCustomerAccountViewModel.CustomerVehicleLoanCollateralDetailViewModel.LoanPurpose != StringLiteralValue.NewVehicle && customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.EnableVehicleInspection == true)
                                {
                                    CustomerPreOwnedVehicleLoanInspectionViewModel customerPreOwnedVehicleLoanInspectionViewModel = await customerDetailRepository.GetPreOwnedVehicleLoanInspectionEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                                    if (customerPreOwnedVehicleLoanInspectionViewModel != null)
                                    {
                                        result = customerAccountDbContextRepository.AttachCustomerPreOwnedVehicleLoanInspectionData(_loanCustomerAccountViewModel.CustomerPreOwnedVehicleLoanInspectionViewModel, _entryType);
                                    }
                                }
                            }
                        }

                        //CustomerLoanAccountVehicleInsuranceDetail
                        if (result)
                        {
                            CustomerVehicleLoanInsuranceDetailViewModel CustomerVehicleLoanInsuranceDetailViewModel = await customerDetailRepository.GetLoanAccountVehicleInsuranceDetailEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                            if (CustomerVehicleLoanInsuranceDetailViewModel != null)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAccountVehicleInsuranceDetailData(_loanCustomerAccountViewModel.CustomerVehicleLoanInsuranceDetailViewModel, _entryType);
                            }
                        }

                        //CustomerVehicleLoanPhoto
                        if (result)
                        {
                            IEnumerable<CustomerVehicleLoanPhotoViewModel> customerVehicleLoanPhotoViewModel = await customerDetailRepository.GetVehicleLoanPhotoEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);
                            foreach (CustomerVehicleLoanPhotoViewModel viewModel in customerVehicleLoanPhotoViewModel)
                            {
                                if (customerVehicleLoanPhotoViewModel != null)
                                {
                                    result = customerAccountDbContextRepository.AttachCustomerVehicleLoanPhotoData(viewModel, customerLoanAccountOpeningConfigViewModel.SchemePreownedVehicleLoanParameterViewModel.StoragePath, viewModel.NameOfFile, _entryType);
                                }
                            }
                        }

                    }

                    //CustomerGoldLoanCollateralDetail
                    if (loanType == StringLiteralValue.GoldLoan)
                    {
                        IEnumerable<CustomerGoldLoanCollateralDetailViewModel> customerGoldLoanCollateralDetailViewModelList = await customerDetailRepository.GetGoldLoanCollateralDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                        if (customerGoldLoanCollateralDetailViewModelList != null)
                        {
                            foreach (CustomerGoldLoanCollateralDetailViewModel viewModel in customerGoldLoanCollateralDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralDetailData(viewModel, _entryType);
                            }
                        }

                        //CustomerGoldLoanCollateralPhoto
                        if (result)
                        {
                            if (customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.EnableGoldPhoto == true)
                            {
                                IEnumerable<CustomerGoldLoanCollateralPhotoViewModel> customerGoldLoanCollateralPhotoViewModelList = await customerDetailRepository.GetGoldLoanCollateralPhotoEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);
                                foreach (CustomerGoldLoanCollateralPhotoViewModel viewModel in customerGoldLoanCollateralPhotoViewModelList)
                                {
                                    if (customerGoldLoanCollateralPhotoViewModelList != null)
                                    {
                                        result = customerAccountDbContextRepository.AttachCustomerGoldLoanCollateralPhotoData(viewModel, customerLoanAccountOpeningConfigViewModel.SchemeGoldLoanParameterViewModel.GoldPhotoLocalStoragePath, viewModel.NameOfFile, _entryType);
                                    }
                                }
                            }
                        }
                    }

                    //Consumer Durable Loan
                    if (loanType == StringLiteralValue.ConsumerDurableLoan)
                    {
                        IEnumerable<CustomerConsumerLoanCollateralDetailViewModel> customerConsumerLoanCollateralDetailViewModelList = await customerDetailRepository.GetConsumerLoanCollateralDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                        if (customerConsumerLoanCollateralDetailViewModelList != null)
                        {
                            foreach (CustomerConsumerLoanCollateralDetailViewModel viewModel in customerConsumerLoanCollateralDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerConsumerLoanCollateralDetailData(viewModel, _entryType);
                            }
                        }
                    }

                    // Cash Credit Loan
                    if (loanType == StringLiteralValue.CashCreditLoan)
                    {
                        CustomerCashCreditLoanAccountViewModel customerCashCreditLoanAccountViewModel = await customerDetailRepository.GetCustomerCashCreditLoanAccountEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                        if (customerCashCreditLoanAccountViewModel != null)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerCashCreditLoanAccountData(_loanCustomerAccountViewModel.CustomerCashCreditLoanAccountViewModel, _entryType);
                        }
                    }

                    // Educational Loan
                    if (loanType == StringLiteralValue.EducationalLoan)
                    {
                        CustomerEducationalLoanDetailViewModel customerEducationalLoanDetailViewModel = await customerDetailRepository.GetCustomerEducationalLoanDetailEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                        if (customerEducationalLoanDetailViewModel != null)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerEducationalLoanDetailData(_loanCustomerAccountViewModel.CustomerEducationalLoanDetailViewModel, _entryType);
                        }
                    }
                }

                // CustomerLoanFieldInvestigation
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableFieldInvestigation)
                    {
                        CustomerLoanFieldInvestigationViewModel customerLoanFieldInvestigationViewModel = await customerDetailRepository.GetLoanFieldInvestigationEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                        if (customerLoanFieldInvestigationViewModel != null)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerLoanFieldInvestigationData(_loanCustomerAccountViewModel.CustomerLoanFieldInvestigationViewModel, _entryType);
                        }
                    }
                }

                // CustomerLoanAccountDebtToIncomeRatio
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableCaptureDebtToIncomeRatio == true)
                    {
                        CustomerLoanAccountDebtToIncomeRatioViewModel customerLoanAccountDebtToIncomeRatioViewModel = await customerDetailRepository.GetCustomerLoanAccountDebtToIncomeRatioEntry(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                        if (customerLoanAccountDebtToIncomeRatioViewModel != null)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerLoanAccountDebtToIncomeRatioData(_loanCustomerAccountViewModel.CustomerLoanAccountDebtToIncomeRatioViewModel, _entryType);

                        }
                    }
                }

                //CustomerAccountDocument
                if (result)
                {
                    IEnumerable<CustomerAccountDocumentViewModel> customerAccountDocumentViewModel = await customerDetailRepository.GetDocumentEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);
                    foreach (CustomerAccountDocumentViewModel viewModel in customerAccountDocumentViewModel)
                    {
                        SchemeDocumentViewModel schemeDocumentViewModel = await schemeDetailRepository.GetDocumentEntry(accountDetailRepository.GetSchemePrmKeyById(_loanCustomerAccountViewModel.CustomerAccountDetailViewModel.SchemeId), viewModel.DocumentPrmKey, StringLiteralValue.Verify);
                        if (customerAccountDocumentViewModel != null)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountDocumentData(viewModel, schemeDocumentViewModel.DocumentLocalStoragePath, viewModel.NameOfFile, _entryType);
                        }
                    }
                }

                // CustomerAccountNoticeSchedule
                if (result)
                {
                    // Get SchemeNoticeSchedule Details From Session Object
                    IEnumerable<CustomerAccountNoticeScheduleViewModel> customerAccountNoticeScheduleViewModelList = await customerDetailRepository.GetNoticeScheduleEntries(_loanCustomerAccountViewModel.CustomerAccountPrmKey, entriesType);

                    if (customerAccountNoticeScheduleViewModelList != null)
                    {
                        foreach (CustomerAccountNoticeScheduleViewModel viewModel in customerAccountNoticeScheduleViewModelList)
                        {
                            result = customerAccountDbContextRepository.AttachCustomerAccountNoticeScheduleData(viewModel, _entryType);
                        }
                    }
                }

                //CustomerLoanAcquaintanceDetail
                if (result)
                {
                    if (customerLoanAccountOpeningConfigViewModel.SchemeLoanAccountParameterViewModel.EnableAcquaintanceDetails == true)
                    {
                        // Get CustomerLoanAcquaintanceDetail From Session Object
                        IEnumerable<CustomerLoanAcquaintanceDetailViewModel> CustomerLoanAcquaintanceDetailViewModelList = await customerDetailRepository.GetAcquaintanceDetailEntries(_loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey, entriesType);

                        if (CustomerLoanAcquaintanceDetailViewModelList != null)
                        {
                            foreach (CustomerLoanAcquaintanceDetailViewModel viewModel in CustomerLoanAcquaintanceDetailViewModelList)
                            {
                                result = customerAccountDbContextRepository.AttachCustomerLoanAcquaintanceDetail(viewModel, _entryType);
                            }
                        }
                    }
                }

                //Final Method
                if (result)
                {
                    result = await customerAccountDbContextRepository.SaveData();
                }

                if (result)
                {
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
    }
}
