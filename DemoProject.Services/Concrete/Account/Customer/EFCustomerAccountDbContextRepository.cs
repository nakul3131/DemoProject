using AutoMapper;
using System;
using System.Data.Entity;
using System.IO;
using System.Threading.Tasks;
using DemoProject.Domain.Entities.Account.Customer;
using DemoProject.Domain.Entities.PersonInformation;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.Layout;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.Management.Conference;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Security;
using System.Collections.Generic;
using System.Web;
using System.Linq;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerAccountDbContextRepository : ICustomerAccountDbContextRepository
    {
        private CustomerAccount customerAccount = new CustomerAccount();
        private CustomerLoanAccount customerLoanAccount = new CustomerLoanAccount();
        private CustomerDepositAccount customerDepositAccount = new CustomerDepositAccount();
        private PersonIncomeTaxDetail personIncomeTaxDetail = new PersonIncomeTaxDetail();

        private EntityState entityState;
        private long customerAccountPrmKey = 0;
        private int customerDepositAccountPrmKey = 0;
        private int customerLoanAccountPrmKey = 0;
        private long personPrmKey = 0;

        private Person person = new Person();
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly ICryptoAlgorithmRepository cryptoAlgorithmRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IMinuteOfMeetingAgendaRepository minuteOfMeetingAgendaRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly ISchemeDetailRepository schemeDetailRepository;

        public EFCustomerAccountDbContextRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IConfigurationDetailRepository _configurationDetailRepository, ICryptoAlgorithmRepository _cryptoAlgorithmRepository, IPersonDetailRepository _personDetailRepository, IMinuteOfMeetingAgendaRepository _minuteOfMeetingAgendaRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ISchemeDetailRepository _schemeDetailRepository, IDepositSchemeParameterRepository _depositSchemeParameterRepository, IManagementDetailRepository _managementDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            cryptoAlgorithmRepository = _cryptoAlgorithmRepository;
            personDetailRepository = _personDetailRepository;
            minuteOfMeetingAgendaRepository = _minuteOfMeetingAgendaRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            schemeDetailRepository = _schemeDetailRepository;
            managementDetailRepository = _managementDetailRepository;

        }

        public bool AttachPersonBorrowingDetailData(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel, string _entryType)
        {
            try
            {
                long personBorrowingDetailPrmKey = _personBorrowingDetailViewModel.PersonBorrowingDetailPrmKey;

                configurationDetailRepository.SetDefaultValues(_personBorrowingDetailViewModel, _entryType);

                _personBorrowingDetailViewModel.PersonBorrowingDetailPrmKey = personBorrowingDetailPrmKey;

                PersonBorrowingDetail personBorrowingDetail = Mapper.Map<PersonBorrowingDetail>(_personBorrowingDetailViewModel);
                PersonBorrowingDetailMakerChecker personBorrowingDetailMakerChecker = Mapper.Map<PersonBorrowingDetailMakerChecker>(_personBorrowingDetailViewModel);

                PersonBorrowingDetailTranslation personBorrowingDetailTranslation = Mapper.Map<PersonBorrowingDetailTranslation>(_personBorrowingDetailViewModel);
                PersonBorrowingDetailTranslationMakerChecker personBorrowingDetailTranslationMakerChecker = Mapper.Map<PersonBorrowingDetailTranslationMakerChecker>(_personBorrowingDetailViewModel);

                CustomerLoanAccountBorrowingDetail customerLoanAccountBorrowingDetail = Mapper.Map<CustomerLoanAccountBorrowingDetail>(_personBorrowingDetailViewModel);
                CustomerLoanAccountBorrowingDetailMakerChecker customerLoanAccountBorrowingDetailMakerChecker = Mapper.Map<CustomerLoanAccountBorrowingDetailMakerChecker>(_personBorrowingDetailViewModel);

                //Get PrmKey By Id
                personBorrowingDetail.PersonPrmKey = personPrmKey;
                customerLoanAccountBorrowingDetail.CustomerAccountPrmKey = customerAccountPrmKey;


                if (_entryType == StringLiteralValue.Create)
                {
                    if (_personBorrowingDetailViewModel.PersonBorrowingDetailPrmKey == 0)
                    {
                        personBorrowingDetailTranslation.PersonBorrowingDetailPrmKey = 0;
                        personBorrowingDetailMakerChecker.PersonBorrowingDetailPrmKey = 0;
                        personBorrowingDetailTranslationMakerChecker.PersonBorrowingDetailTranslationPrmKey = 0;

                        context.PersonBorrowingDetailMakerCheckers.Attach(personBorrowingDetailMakerChecker);
                        context.Entry(personBorrowingDetailMakerChecker).State = EntityState.Added;
                        personBorrowingDetail.PersonBorrowingDetailMakerCheckers.Add(personBorrowingDetailMakerChecker);

                        context.PersonBorrowingDetails.Attach(personBorrowingDetail);
                        context.Entry(personBorrowingDetail).State = EntityState.Added;

                        context.PersonBorrowingDetailTranslations.Attach(personBorrowingDetailTranslation);
                        context.Entry(personBorrowingDetailTranslation).State = EntityState.Added;
                        personBorrowingDetail.PersonBorrowingDetailTranslations.Add(personBorrowingDetailTranslation);

                        context.PersonBorrowingDetailTranslationMakerCheckers.Attach(personBorrowingDetailTranslationMakerChecker);
                        context.Entry(personBorrowingDetailTranslationMakerChecker).State = EntityState.Added;
                        personBorrowingDetailTranslation.PersonBorrowingDetailTranslationMakerCheckers.Add(personBorrowingDetailTranslationMakerChecker);

                    }

                    customerLoanAccountBorrowingDetailMakerChecker.CustomerLoanAccountBorrowingDetailPrmKey = 0;

                    context.CustomerLoanAccountBorrowingDetailMakerCheckers.Attach(customerLoanAccountBorrowingDetailMakerChecker);
                    context.Entry(customerLoanAccountBorrowingDetailMakerChecker).State = EntityState.Added;
                    customerLoanAccountBorrowingDetail.CustomerLoanAccountBorrowingDetailMakerCheckers.Add(customerLoanAccountBorrowingDetailMakerChecker);

                    context.CustomerLoanAccountBorrowingDetails.Attach(customerLoanAccountBorrowingDetail);
                    context.Entry(customerLoanAccountBorrowingDetail).State = EntityState.Added;
                    personBorrowingDetail.CustomerLoanAccountBorrowingDetails.Add(customerLoanAccountBorrowingDetail);
                }
                else
                {
                    if (customerLoanAccountBorrowingDetailMakerChecker.CustomerLoanAccountBorrowingDetailPrmKey > 0)
                    {
                        context.CustomerLoanAccountBorrowingDetailMakerCheckers.Attach(customerLoanAccountBorrowingDetailMakerChecker);
                        context.Entry(customerLoanAccountBorrowingDetailMakerChecker).State = EntityState.Added;
                    }
                    else
                    {
                        context.PersonBorrowingDetailMakerCheckers.Attach(personBorrowingDetailMakerChecker);
                        context.Entry(personBorrowingDetailMakerChecker).State = EntityState.Added;
                        
                        context.PersonBorrowingDetailTranslationMakerCheckers.Attach(personBorrowingDetailTranslationMakerChecker);
                        context.Entry(personBorrowingDetailTranslationMakerChecker).State = EntityState.Added;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonCourtCaseData(PersonCourtCaseViewModel _personCourtCaseViewModel, string _entryType)
        {
            try
            {
                long personCourtCasePrmKey = _personCourtCaseViewModel.PersonCourtCasePrmKey;

                configurationDetailRepository.SetDefaultValues(_personCourtCaseViewModel, _entryType);

                _personCourtCaseViewModel.PersonCourtCasePrmKey = personCourtCasePrmKey;

                _personCourtCaseViewModel.CourtCaseStagePrmKey = personDetailRepository.GetCourtCaseStagePrmKeyById(_personCourtCaseViewModel.CourtCaseStageId);
                _personCourtCaseViewModel.CourtCaseTypePrmKey = personDetailRepository.GetCourtCaseTypePrmKeyById(_personCourtCaseViewModel.CourtCaseTypeId);

                PersonCourtCase personCourtCase = Mapper.Map<PersonCourtCase>(_personCourtCaseViewModel);
                PersonCourtCaseMakerChecker personCourtCaseMakerChecker = Mapper.Map<PersonCourtCaseMakerChecker>(_personCourtCaseViewModel);

                CustomerLoanAccountCourtCaseDetail customerLoanAccountCourtCaseDetail = Mapper.Map<CustomerLoanAccountCourtCaseDetail>(_personCourtCaseViewModel);
                CustomerLoanAccountCourtCaseDetailMakerChecker customerLoanAccountCourtCaseDetailMakerChecker = Mapper.Map<CustomerLoanAccountCourtCaseDetailMakerChecker>(_personCourtCaseViewModel);

                personCourtCase.PersonPrmKey = personPrmKey;
                customerLoanAccountCourtCaseDetail.CustomerAccountPrmKey = customerAccountPrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    if ((_personCourtCaseViewModel.PersonCourtCasePrmKey == 0))
                    {
                        personCourtCaseMakerChecker.PersonCourtCasePrmKey = 0;


                        personCourtCase.PersonPrmKey = personPrmKey;

                        context.PersonCourtCases.Attach(personCourtCase);
                        context.Entry(personCourtCase).State = EntityState.Added;
                        person.PersonCourtCases.Add(personCourtCase);

                        context.PersonCourtCaseMakerCheckers.Attach(personCourtCaseMakerChecker);
                        context.Entry(personCourtCaseMakerChecker).State = EntityState.Added;
                        personCourtCase.PersonCourtCaseMakerCheckers.Add(personCourtCaseMakerChecker);
                    }
                    customerLoanAccountCourtCaseDetailMakerChecker.CustomerLoanAccountCourtCaseDetailPrmKey = 0;

                    context.CustomerLoanAccountCourtCaseDetailMakerCheckers.Attach(customerLoanAccountCourtCaseDetailMakerChecker);
                    context.Entry(customerLoanAccountCourtCaseDetailMakerChecker).State = EntityState.Added;
                    customerLoanAccountCourtCaseDetail.CustomerLoanAccountCourtCaseDetailMakerCheckers.Add(customerLoanAccountCourtCaseDetailMakerChecker);

                    context.CustomerLoanAccountCourtCaseDetails.Attach(customerLoanAccountCourtCaseDetail);
                    context.Entry(customerLoanAccountCourtCaseDetail).State = EntityState.Added;
                    personCourtCase.CustomerLoanAccountCourtCaseDetails.Add(customerLoanAccountCourtCaseDetail);


                }

                else
                {
                    if (customerLoanAccountCourtCaseDetailMakerChecker.CustomerLoanAccountCourtCaseDetailPrmKey > 0)
                    {
                        context.CustomerLoanAccountCourtCaseDetailMakerCheckers.Attach(customerLoanAccountCourtCaseDetailMakerChecker);
                        context.Entry(customerLoanAccountCourtCaseDetailMakerChecker).State = EntityState.Added;
                    }
                    else
                    {
                        context.PersonCourtCaseMakerCheckers.Attach(personCourtCaseMakerChecker);
                        context.Entry(personCourtCaseMakerChecker).State = EntityState.Added;
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonIncomeTaxDetailData(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personIncomeTaxDetailViewModel, _entryType);

                personIncomeTaxDetail = Mapper.Map<PersonIncomeTaxDetail>(_personIncomeTaxDetailViewModel);
                PersonIncomeTaxDetailMakerChecker personIncomeTaxDetailMakerChecker = Mapper.Map<PersonIncomeTaxDetailMakerChecker>(_personIncomeTaxDetailViewModel);

                CustomerLoanAccountIncomeTaxDetail customerLoanAccountIncomeTaxDetail = Mapper.Map<CustomerLoanAccountIncomeTaxDetail>(_personIncomeTaxDetailViewModel);
                CustomerLoanAccountIncomeTaxDetailMakerChecker customerLoanAccountIncomeTaxDetailMakerChecker = Mapper.Map<CustomerLoanAccountIncomeTaxDetailMakerChecker>(_personIncomeTaxDetailViewModel);

                personIncomeTaxDetail.PersonPrmKey = personPrmKey;
                customerLoanAccountIncomeTaxDetail.CustomerAccountPrmKey = customerAccountPrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    if ((_personIncomeTaxDetailViewModel.PersonIncomeTaxDetailPrmKey == 0))
                    {
                        personIncomeTaxDetailMakerChecker.PersonIncomeTaxDetailPrmKey = 0;

                        personIncomeTaxDetail.PersonPrmKey = personPrmKey;

                        context.PersonIncomeTaxDetails.Attach(personIncomeTaxDetail);
                        context.Entry(personIncomeTaxDetail).State = EntityState.Added;
                        person.PersonIncomeTaxDetails.Add(personIncomeTaxDetail);

                        context.PersonIncomeTaxDetailMakerCheckers.Attach(personIncomeTaxDetailMakerChecker);
                        context.Entry(personIncomeTaxDetailMakerChecker).State = EntityState.Added;
                        personIncomeTaxDetail.PersonIncomeTaxDetailMakerCheckers.Add(personIncomeTaxDetailMakerChecker);
                    }
                    customerLoanAccountIncomeTaxDetailMakerChecker.CustomerLoanAccountIncomeTaxDetailPrmKey = 0;

                    context.CustomerLoanAccountIncomeTaxDetailMakerCheckers.Attach(customerLoanAccountIncomeTaxDetailMakerChecker);
                    context.Entry(customerLoanAccountIncomeTaxDetailMakerChecker).State = EntityState.Added;
                    customerLoanAccountIncomeTaxDetail.CustomerLoanAccountIncomeTaxDetailMakerCheckers.Add(customerLoanAccountIncomeTaxDetailMakerChecker);

                    context.CustomerLoanAccountIncomeTaxDetails.Attach(customerLoanAccountIncomeTaxDetail);
                    context.Entry(customerLoanAccountIncomeTaxDetail).State = EntityState.Added;
                    personIncomeTaxDetail.CustomerLoanAccountIncomeTaxDetails.Add(customerLoanAccountIncomeTaxDetail);

                }

                else
                {
                    if (customerLoanAccountIncomeTaxDetailMakerChecker.CustomerLoanAccountIncomeTaxDetailPrmKey > 0)
                    {
                        context.CustomerLoanAccountIncomeTaxDetailMakerCheckers.Attach(customerLoanAccountIncomeTaxDetailMakerChecker);
                        context.Entry(customerLoanAccountIncomeTaxDetailMakerChecker).State = EntityState.Added;
                    }
                    else
                    {
                        context.PersonIncomeTaxDetailMakerCheckers.Attach(personIncomeTaxDetailMakerChecker);
                        context.Entry(personIncomeTaxDetailMakerChecker).State = EntityState.Added;
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonIncomeTaxDocumentData(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _localStoragePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personIncomeTaxDetailViewModel, _entryType);


                PersonIncomeTaxDetailDocument personIncomeTaxDetailDocument = Mapper.Map<PersonIncomeTaxDetailDocument>(_personIncomeTaxDetailViewModel);
                PersonIncomeTaxDetailDocumentMakerChecker personIncomeTaxDetailDocumentMakerChecker = Mapper.Map<PersonIncomeTaxDetailDocumentMakerChecker>(_personIncomeTaxDetailViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    if (personIncomeTaxDetail.PersonPrmKey == 0)
                    {
                        personIncomeTaxDetail.PersonPrmKey = personPrmKey;
                    }
                    personIncomeTaxDetailDocumentMakerChecker.PersonIncomeTaxDetailDocumentPrmKey = 0;
                    context.PersonIncomeTaxDetailDocuments.Attach(personIncomeTaxDetailDocument);
                    context.Entry(personIncomeTaxDetailDocument).State = EntityState.Added;
                    personIncomeTaxDetail.PersonIncomeTaxDetailDocuments.Add(personIncomeTaxDetailDocument);

                    context.PersonIncomeTaxDetailDocumentMakerCheckers.Attach(personIncomeTaxDetailDocumentMakerChecker);
                    context.Entry(personIncomeTaxDetailDocumentMakerChecker).State = EntityState.Added;
                    personIncomeTaxDetailDocument.PersonIncomeTaxDetailDocumentMakerCheckers.Add(personIncomeTaxDetailDocumentMakerChecker);

                }
                else
                {
                    context.PersonIncomeTaxDetailDocumentMakerCheckers.Attach(personIncomeTaxDetailDocumentMakerChecker);
                    context.Entry(personIncomeTaxDetailDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachIncomeTaxDetailDocumentInLocalStorage(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, string _incomeTaxDocumentLocalStoragePath, IEnumerable<PersonIncomeTaxDetailViewModel> _personIncomeTaxDetailViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _personIncomeTaxDetailViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personIncomeTaxDetailViewModel.PhotoPathTax.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _incomeTaxDocumentLocalStoragePath;

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _personIncomeTaxDetailViewModel.NameOfFile);

                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_personIncomeTaxDetailViewModel.PhotoPathTax);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _personIncomeTaxDetailViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _personIncomeTaxDetailViewModel.LocalStoragePath = localStoragePath;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachIncomeTaxDetailDocumentInDatabaseStorage(PersonIncomeTaxDetailViewModel _personIncomeTaxDetailViewModel, IEnumerable<PersonIncomeTaxDetailViewModel> _personIncomeTaxDetailViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _personIncomeTaxDetailViewModel.PhotoPathTax.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _personIncomeTaxDetailViewModel.PhotoCopy = imagecode;

                    _personIncomeTaxDetailViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_personIncomeTaxDetailViewModel.PhotoPathTax.FileName);

                    _personIncomeTaxDetailViewModel.LocalStoragePath = "None";
                }
              

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonAdditionalIncomeDetailData(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel, string _entryType)
        {
            try
            {
                long personAdditionalIncomeDetailPrmKey = _personAdditionalIncomeDetailViewModel.PersonAdditionalIncomeDetailPrmKey;

                configurationDetailRepository.SetDefaultValues(_personAdditionalIncomeDetailViewModel, _entryType);

                _personAdditionalIncomeDetailViewModel.PersonAdditionalIncomeDetailPrmKey = personAdditionalIncomeDetailPrmKey;

                _personAdditionalIncomeDetailViewModel.IncomeSourcePrmKey = personDetailRepository.GetIncomeSourcePrmKeyById(_personAdditionalIncomeDetailViewModel.IncomeSourceId);

                PersonAdditionalIncomeDetail personAdditionalIncomeDetail = Mapper.Map<PersonAdditionalIncomeDetail>(_personAdditionalIncomeDetailViewModel);
                PersonAdditionalIncomeDetailMakerChecker personAdditionalIncomeDetailMakerChecker = Mapper.Map<PersonAdditionalIncomeDetailMakerChecker>(_personAdditionalIncomeDetailViewModel);

                CustomerLoanAccountAdditionalIncomeDetail customerLoanAccountAdditionalIncomeDetail = Mapper.Map<CustomerLoanAccountAdditionalIncomeDetail>(_personAdditionalIncomeDetailViewModel);
                CustomerLoanAccountAdditionalIncomeDetailMakerChecker customerLoanAccountAdditionalIncomeDetailMakerChecker = Mapper.Map<CustomerLoanAccountAdditionalIncomeDetailMakerChecker>(_personAdditionalIncomeDetailViewModel);

                personAdditionalIncomeDetail.PersonPrmKey = personPrmKey;
                customerLoanAccountAdditionalIncomeDetail.CustomerAccountPrmKey = customerAccountPrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    if ((_personAdditionalIncomeDetailViewModel.PersonAdditionalIncomeDetailPrmKey == 0))
                    {
                        personAdditionalIncomeDetailMakerChecker.PersonAdditionalIncomeDetailPrmKey = 0;

                        context.PersonAdditionalIncomeDetails.Attach(personAdditionalIncomeDetail);
                        context.Entry(personAdditionalIncomeDetail).State = EntityState.Added;
                        person.PersonAdditionalIncomeDetails.Add(personAdditionalIncomeDetail);

                        context.PersonAdditionalIncomeDetailMakerCheckers.Attach(personAdditionalIncomeDetailMakerChecker);
                        context.Entry(personAdditionalIncomeDetailMakerChecker).State = EntityState.Added;
                        personAdditionalIncomeDetail.PersonAdditionalIncomeDetailMakerCheckers.Add(personAdditionalIncomeDetailMakerChecker);
                    }

                    customerLoanAccountAdditionalIncomeDetailMakerChecker.CustomerLoanAccountAdditionalIncomeDetailPrmKey = 0;

                    context.CustomerLoanAccountAdditionalIncomeDetailMakerCheckers.Attach(customerLoanAccountAdditionalIncomeDetailMakerChecker);
                    context.Entry(customerLoanAccountAdditionalIncomeDetailMakerChecker).State = EntityState.Added;
                    customerLoanAccountAdditionalIncomeDetail.CustomerAccountAdditionalIncomeDetailMakerCheckers.Add(customerLoanAccountAdditionalIncomeDetailMakerChecker);

                    context.CustomerLoanAccountAdditionalIncomeDetails.Attach(customerLoanAccountAdditionalIncomeDetail);
                    context.Entry(customerLoanAccountAdditionalIncomeDetail).State = EntityState.Added;
                    personAdditionalIncomeDetail.CustomerLoanAccountAdditionalIncomeDetails.Add(customerLoanAccountAdditionalIncomeDetail);

                }

                else
                {
                    if (customerLoanAccountAdditionalIncomeDetailMakerChecker.CustomerLoanAccountAdditionalIncomeDetailPrmKey > 0)
                    {
                        context.CustomerLoanAccountAdditionalIncomeDetailMakerCheckers.Attach(customerLoanAccountAdditionalIncomeDetailMakerChecker);
                        context.Entry(customerLoanAccountAdditionalIncomeDetailMakerChecker).State = EntityState.Added;
                    }
                    else
                    {
                        context.PersonAdditionalIncomeDetailMakerCheckers.Attach(personAdditionalIncomeDetailMakerChecker);
                        context.Entry(personAdditionalIncomeDetailMakerChecker).State = EntityState.Added;
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountDetailData(CustomerAccountDetailViewModel _customerAccountDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountDetailViewModel, _entryType);

                CustomerAccountDetail customerAccountDetail = Mapper.Map<CustomerAccountDetail>(_customerAccountDetailViewModel);
                CustomerAccountDetailMakerChecker customerAccountDetailMakerChecker = Mapper.Map<CustomerAccountDetailMakerChecker>(_customerAccountDetailViewModel);

                customerAccountDetail.PersonPrmKey = personDetailRepository.GetPersonPrmKeyById(_customerAccountDetailViewModel.PersonId);

                // If Not Multi Currency 
                if (configurationDetailRepository.HasMultiCurrency() == false)
                    customerAccountDetail.CurrencyPrmKey = configurationDetailRepository.GetDefaultCurrencyPrmKey();
                else
                    customerAccountDetail.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_customerAccountDetailViewModel.CurrencyId);

                // If Not Has Branches - Set 0 i.e. No Branch / None
                if (configurationDetailRepository.GetNumberOfBranches() == 0)
                    customerAccountDetail.BusinessOfficePrmKey = 0;
                else
                    customerAccountDetail.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_customerAccountDetailViewModel.BusinessOfficeId);

                customerAccountDetail.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_customerAccountDetailViewModel.GeneralLedgerId);
                customerAccountDetail.SchemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_customerAccountDetailViewModel.SchemeId);

                // Required For Person Address And Contact Details
                personPrmKey = customerAccountDetail.PersonPrmKey;

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerAccountDetail.CustomerAccountPrmKey = customerAccountPrmKey;

                    context.CustomerAccountDetails.Attach(customerAccountDetail);
                    context.Entry(customerAccountDetail).State = entityState;
                    customerAccount.CustomerAccountDetails.Add(customerAccountDetail);

                    context.CustomerAccountDetailMakerCheckers.Attach(customerAccountDetailMakerChecker);
                    context.Entry(customerAccountDetailMakerChecker).State = EntityState.Added;
                    customerAccountDetail.CustomerAccountDetailMakerCheckers.Add(customerAccountDetailMakerChecker);
                }
                else
                {
                    context.CustomerAccountDetailMakerCheckers.Attach(customerAccountDetailMakerChecker);
                    context.Entry(customerAccountDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountEmailServiceData(CustomerAccountEmailServiceViewModel _customerAccountEmailServiceViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountEmailServiceViewModel, _entryType);

                CustomerAccountEmailService customerAccountEmailService = Mapper.Map<CustomerAccountEmailService>(_customerAccountEmailServiceViewModel);
                CustomerAccountEmailServiceMakerChecker customerAccountEmailServiceMakerChecker = Mapper.Map<CustomerAccountEmailServiceMakerChecker>(_customerAccountEmailServiceViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerAccountEmailService.CustomerAccountPrmKey = customerAccountPrmKey;

                    context.CustomerAccountEmailServices.Attach(customerAccountEmailService);
                    context.Entry(customerAccountEmailService).State = entityState;
                    customerAccount.CustomerAccountEmailServices.Add(customerAccountEmailService);

                    context.CustomerAccountEmailServiceMakerCheckers.Attach(customerAccountEmailServiceMakerChecker);
                    context.Entry(customerAccountEmailServiceMakerChecker).State = EntityState.Added;
                    customerAccountEmailService.CustomerAccountEmailServiceMakerCheckers.Add(customerAccountEmailServiceMakerChecker);
                }
                else
                {
                    context.CustomerAccountEmailServiceMakerCheckers.Attach(customerAccountEmailServiceMakerChecker);
                    context.Entry(customerAccountEmailServiceMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountNomineeData(CustomerAccountNomineeViewModel _customerAccountNomineeViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountNomineeViewModel, _entryType);

                CustomerAccountNominee customerAccountNominee = Mapper.Map<CustomerAccountNominee>(_customerAccountNomineeViewModel);
                CustomerAccountNomineeMakerChecker customerAccountNomineeMakerChecker = Mapper.Map<CustomerAccountNomineeMakerChecker>(_customerAccountNomineeViewModel);
                CustomerAccountNomineeTranslation customerAccountNomineeTranslation = Mapper.Map<CustomerAccountNomineeTranslation>(_customerAccountNomineeViewModel);
                CustomerAccountNomineeTranslationMakerChecker customerAccountNomineeTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeTranslationMakerChecker>(_customerAccountNomineeViewModel);

                // Get PrmKey By Id
                customerAccountNominee.RelationPrmKey = personDetailRepository.GetRelationPrmKeyById(_customerAccountNomineeViewModel.RelationId);

                // Set Conditional Values
                if (_customerAccountNomineeViewModel.NomineePersonInformationNumber == 0)
                {
                    _customerAccountNomineeViewModel.NomineeAge = configurationDetailRepository.GetAge(_customerAccountNomineeViewModel.BirthDate);
                }
                else
                {
                    _customerAccountNomineeViewModel.NomineeAge = personDetailRepository.GetPersonCurrentAge(_customerAccountNomineeViewModel.NomineePersonInformationNumber);
                }

                if (_entryType == StringLiteralValue.Create)
                {
                    customerAccountNominee.CustomerAccountPrmKey = customerAccountPrmKey;
                    customerAccountNomineeTranslation.CustomerAccountNomineePrmKey = _customerAccountNomineeViewModel.CustomerAccountNomineePrmKey;

                    context.CustomerAccountNominees.Attach(customerAccountNominee);
                    context.Entry(customerAccountNominee).State = EntityState.Added;
                    customerAccount.CustomerAccountNominees.Add(customerAccountNominee);

                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;
                    customerAccountNominee.CustomerAccountNomineeMakerCheckers.Add(customerAccountNomineeMakerChecker);


                    context.CustomerAccountNomineeTranslations.Attach(customerAccountNomineeTranslation);
                    context.Entry(customerAccountNomineeTranslation).State = EntityState.Added;
                    customerAccountNominee.CustomerAccountNomineeTranslations.Add(customerAccountNomineeTranslation);

                    context.CustomerAccountNomineeTranslationMakerCheckers.Attach(customerAccountNomineeTranslationMakerChecker);
                    context.Entry(customerAccountNomineeTranslationMakerChecker).State = EntityState.Added;
                    customerAccountNomineeTranslation.CustomerAccountNomineeTranslationMakerCheckers.Add(customerAccountNomineeTranslationMakerChecker);

                    if (_customerAccountNomineeViewModel.NomineeAge < 18)
                    {
                        foreach (CustomerAccountNomineeGuardianViewModel guardianViewModel in _customerAccountNomineeViewModel.CustomerAccountNomineeGuardianViewModelList)
                        {
                            configurationDetailRepository.SetDefaultValues(guardianViewModel, _entryType);

                            // CustomerAccountNomineeGuardian
                            CustomerAccountNomineeGuardian customerAccountNomineeGuardian = Mapper.Map<CustomerAccountNomineeGuardian>(guardianViewModel);
                            CustomerAccountNomineeGuardianMakerChecker customerAccountNomineeGuardianMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianMakerChecker>(guardianViewModel);

                            // CustomerAccountNomineeGuardianTranslation
                            CustomerAccountNomineeGuardianTranslation customerAccountNomineeGuardianTranslation = Mapper.Map<CustomerAccountNomineeGuardianTranslation>(guardianViewModel);
                            CustomerAccountNomineeGuardianTranslationMakerChecker customerAccountNomineeGuardianTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianTranslationMakerChecker>(guardianViewModel);

                            customerAccountNomineeGuardian.GuardianTypePrmKey = personDetailRepository.GetGuardianTypePrmKeyById(guardianViewModel.GuardianTypeId);

                            context.CustomerAccountNomineeGuardianTranslations.Attach(customerAccountNomineeGuardianTranslation);
                            context.Entry(customerAccountNomineeGuardianTranslation).State = EntityState.Added;
                            customerAccountNomineeGuardian.CustomerAccountNomineeGuardianTranslations.Add(customerAccountNomineeGuardianTranslation);

                            context.CustomerAccountNomineeGuardianTranslationMakerCheckers.Attach(customerAccountNomineeGuardianTranslationMakerChecker);
                            context.Entry(customerAccountNomineeGuardianTranslationMakerChecker).State = EntityState.Added;
                            customerAccountNomineeGuardianTranslation.CustomerAccountNomineeGuardianTranslationMakerCheckers.Add(customerAccountNomineeGuardianTranslationMakerChecker);

                            context.CustomerAccountNomineeGuardians.Attach(customerAccountNomineeGuardian);
                            context.Entry(customerAccountNomineeGuardian).State = EntityState.Added;
                            customerAccountNominee.CustomerAccountNomineeGuardians.Add(customerAccountNomineeGuardian);

                            context.CustomerAccountNomineeGuardianMakerCheckers.Attach(customerAccountNomineeGuardianMakerChecker);
                            context.Entry(customerAccountNomineeGuardianMakerChecker).State = EntityState.Added;
                            customerAccountNomineeGuardian.CustomerAccountNomineeGuardianMakerCheckers.Add(customerAccountNomineeGuardianMakerChecker);
                        }
                    }
                }
                else
                {
                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeTranslationMakerCheckers.Attach(customerAccountNomineeTranslationMakerChecker);
                    context.Entry(customerAccountNomineeTranslationMakerChecker).State = EntityState.Added;

                    if (_customerAccountNomineeViewModel.NomineeAge < 18)
                    {
                        configurationDetailRepository.SetDefaultValues(_customerAccountNomineeViewModel.CustomerAccountNomineeGuardianViewModel, _entryType);

                        // CustomerAccountNomineeGuardian
                        CustomerAccountNomineeGuardianMakerChecker customerAccountNomineeGuardianMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianMakerChecker>(_customerAccountNomineeViewModel.CustomerAccountNomineeGuardianViewModel);

                        // CustomerAccountNomineeGuardianTranslation
                        CustomerAccountNomineeGuardianTranslationMakerChecker customerAccountNomineeGuardianTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianTranslationMakerChecker>(_customerAccountNomineeViewModel.CustomerAccountNomineeGuardianViewModel);

                        context.CustomerAccountNomineeGuardianMakerCheckers.Attach(customerAccountNomineeGuardianMakerChecker);
                        context.Entry(customerAccountNomineeGuardianMakerChecker).State = EntityState.Added;

                        context.CustomerAccountNomineeGuardianTranslationMakerCheckers.Attach(customerAccountNomineeGuardianTranslationMakerChecker);
                        context.Entry(customerAccountNomineeGuardianTranslationMakerChecker).State = EntityState.Added;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountNoticeScheduleData(CustomerAccountNoticeScheduleViewModel _customerAccountNoticeScheduleViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountNoticeScheduleViewModel, _entryType);

                CustomerAccountNoticeSchedule customerAccountNoticeSchedule = Mapper.Map<CustomerAccountNoticeSchedule>(_customerAccountNoticeScheduleViewModel);
                CustomerAccountNoticeScheduleMakerChecker customerAccountNoticeScheduleMakerChecker = Mapper.Map<CustomerAccountNoticeScheduleMakerChecker>(_customerAccountNoticeScheduleViewModel);

                //Get PrmKey By Id
                customerAccountNoticeSchedule.SchedulePrmKey = enterpriseDetailRepository.GetSchedulePrmKeyById(_customerAccountNoticeScheduleViewModel.ScheduleId);
                customerAccountNoticeSchedule.CommunicationMediaPrmKey = managementDetailRepository.GetCommunicationMediaPrmKeyById(_customerAccountNoticeScheduleViewModel.CommunicationMediaId);
                customerAccountNoticeSchedule.NoticeTypePrmKey = managementDetailRepository.GetNoticeTypePrmKeyById(_customerAccountNoticeScheduleViewModel.NoticeTypeId);

                if (_entryType == StringLiteralValue.Create)
                {
                    customerAccountNoticeSchedule.CustomerAccountPrmKey = customerAccountPrmKey;
                    context.CustomerAccountNoticeSchedules.Attach(customerAccountNoticeSchedule);
                    context.Entry(customerAccountNoticeSchedule).State = EntityState.Added;
                    customerAccount.CustomerAccountNoticeSchedules.Add(customerAccountNoticeSchedule);

                    context.CustomerAccountNoticeScheduleMakerCheckers.Attach(customerAccountNoticeScheduleMakerChecker);
                    context.Entry(customerAccountNoticeScheduleMakerChecker).State = EntityState.Added;
                    customerAccountNoticeSchedule.CustomerAccountNoticeScheduleMakerCheckers.Add(customerAccountNoticeScheduleMakerChecker);
                }
                else
                {
                    context.CustomerAccountNoticeScheduleMakerCheckers.Attach(customerAccountNoticeScheduleMakerChecker);
                    context.Entry(customerAccountNoticeScheduleMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountSmsServiceData(CustomerAccountSmsServiceViewModel _customerAccountSmsServiceViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountSmsServiceViewModel, _entryType);

                CustomerAccountSmsService customerAccountSmsService = Mapper.Map<CustomerAccountSmsService>(_customerAccountSmsServiceViewModel);
                CustomerAccountSmsServiceMakerChecker customerAccountSmsServiceMakerChecker = Mapper.Map<CustomerAccountSmsServiceMakerChecker>(_customerAccountSmsServiceViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerAccountSmsService.CustomerAccountPrmKey = customerAccountPrmKey;

                    context.CustomerAccountSmsServices.Attach(customerAccountSmsService);
                    context.Entry(customerAccountSmsService).State = entityState;
                    customerAccount.CustomerAccountSmsServices.Add(customerAccountSmsService);

                    context.CustomerAccountSmsServiceMakerCheckers.Attach(customerAccountSmsServiceMakerChecker);
                    context.Entry(customerAccountSmsServiceMakerChecker).State = EntityState.Added;
                    customerAccountSmsService.CustomerAccountSmsServiceMakerCheckers.Add(customerAccountSmsServiceMakerChecker);
                }
                else
                {
                    context.CustomerAccountSmsServiceMakerCheckers.Attach(customerAccountSmsServiceMakerChecker);
                    context.Entry(customerAccountSmsServiceMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountTurnOverLimitData(CustomerAccountTurnOverLimitViewModel _customerAccountTurnOverLimitViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountTurnOverLimitViewModel, _entryType);

                CustomerAccountTurnOverLimit customerAccountTurnOverLimit = Mapper.Map<CustomerAccountTurnOverLimit>(_customerAccountTurnOverLimitViewModel);
                CustomerAccountTurnOverLimitMakerChecker customerAccountTurnOverLimitMakerChecker = Mapper.Map<CustomerAccountTurnOverLimitMakerChecker>(_customerAccountTurnOverLimitViewModel);

                //Get PrmKey By Id
                customerAccountTurnOverLimit.FrequencyPrmKey = accountDetailRepository.GetFrequencyPrmKeyById(_customerAccountTurnOverLimitViewModel.FrequencyId);
                customerAccountTurnOverLimit.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_customerAccountTurnOverLimitViewModel.TransactionTypeId);

                if (_entryType == StringLiteralValue.Create)
                {
                    customerAccountTurnOverLimit.CustomerAccountPrmKey = customerAccountPrmKey;

                    context.CustomerAccountTurnOverLimits.Attach(customerAccountTurnOverLimit);
                    context.Entry(customerAccountTurnOverLimit).State = EntityState.Added;
                    customerAccount.CustomerAccountTurnOverLimits.Add(customerAccountTurnOverLimit);

                    context.CustomerAccountTurnOverLimitMakerCheckers.Attach(customerAccountTurnOverLimitMakerChecker);
                    context.Entry(customerAccountTurnOverLimitMakerChecker).State = EntityState.Added;
                    customerAccountTurnOverLimit.CustomerAccountTurnOverLimitMakerCheckers.Add(customerAccountTurnOverLimitMakerChecker);
                }
                else
                {
                    context.CustomerAccountTurnOverLimitMakerCheckers.Attach(customerAccountTurnOverLimitMakerChecker);
                    context.Entry(customerAccountTurnOverLimitMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerJointAccountHolderData(CustomerJointAccountHolderViewModel _customerJointAccountHolderViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerJointAccountHolderViewModel, _entryType);

                CustomerJointAccountHolder customerJointAccountHolder = Mapper.Map<CustomerJointAccountHolder>(_customerJointAccountHolderViewModel);
                CustomerJointAccountHolderMakerChecker customerJointAccountHolderMakerChecker = Mapper.Map<CustomerJointAccountHolderMakerChecker>(_customerJointAccountHolderViewModel);

                //Get PrmKey By Id
                customerJointAccountHolder.PersonPrmKey = personDetailRepository.GetPersonPrmKeyById(_customerJointAccountHolderViewModel.PersonId);
                customerJointAccountHolder.JointAccountHolderTypePrmKey = accountDetailRepository.GetJointAccountHolderTypePrmKeyById(_customerJointAccountHolderViewModel.JointAccountHolderTypeId);

                if (_entryType == StringLiteralValue.Create)
                {
                    customerJointAccountHolder.CustomerAccountPrmKey = customerAccountPrmKey;

                    context.CustomerJointAccountHolders.Attach(customerJointAccountHolder);
                    context.Entry(customerJointAccountHolder).State = EntityState.Added;
                    customerAccount.CustomerJointAccountHolders.Add(customerJointAccountHolder);

                    context.CustomerJointAccountHolderMakerCheckers.Attach(customerJointAccountHolderMakerChecker);
                    context.Entry(customerJointAccountHolderMakerChecker).State = EntityState.Added;
                    customerJointAccountHolder.CustomerJointAccountHolderMakerCheckers.Add(customerJointAccountHolderMakerChecker);
                }
                else
                {
                    context.CustomerJointAccountHolderMakerCheckers.Attach(customerJointAccountHolderMakerChecker);
                    context.Entry(customerJointAccountHolderMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerSharesCapitalAccountData(CustomerSharesCapitalAccountViewModel _customerSharesCapitalAccountViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerSharesCapitalAccountViewModel, _entryType);

                CustomerSharesCapitalAccount customerSharesCapitalAccount = Mapper.Map<CustomerSharesCapitalAccount>(_customerSharesCapitalAccountViewModel);
                CustomerSharesCapitalAccountMakerChecker customerSharesCapitalAccountMakerChecker = Mapper.Map<CustomerSharesCapitalAccountMakerChecker>(_customerSharesCapitalAccountViewModel);

                // Set Value
                customerSharesCapitalAccount.MembershipStatus = "ACT";

                customerSharesCapitalAccount.MinuteOfMeetingAgendaPrmKey = accountDetailRepository.GetMinuteOfMeetingAgendaPrmKeyById(_customerSharesCapitalAccountViewModel.MinuteOfMeetingAgendaId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerSharesCapitalAccount.CustomerAccountPrmKey = customerAccountPrmKey;

                    context.CustomerSharesCapitalAccounts.Attach(customerSharesCapitalAccount);
                    context.Entry(customerSharesCapitalAccount).State = entityState;
                    customerAccount.CustomerSharesCapitalAccounts.Add(customerSharesCapitalAccount);

                    context.CustomerSharesCapitalAccountMakerCheckers.Attach(customerSharesCapitalAccountMakerChecker);
                    context.Entry(customerSharesCapitalAccountMakerChecker).State = EntityState.Added;
                    customerSharesCapitalAccount.CustomerSharesCapitalAccountMakerCheckers.Add(customerSharesCapitalAccountMakerChecker);
                }
                else
                {
                    context.CustomerSharesCapitalAccountMakerCheckers.Attach(customerSharesCapitalAccountMakerChecker);
                    context.Entry(customerSharesCapitalAccountMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonAddressData(PersonAddressViewModel _personAddressViewModel, string _entryType)
        {
            try
            {
                long personAddressPrmKey = _personAddressViewModel.PersonAddressPrmKey;

                configurationDetailRepository.SetDefaultValues(_personAddressViewModel, _entryType);

                _personAddressViewModel.PersonAddressPrmKey = personAddressPrmKey;

                PersonAddress personAddress = Mapper.Map<PersonAddress>(_personAddressViewModel);
                PersonAddressMakerChecker personAddressMakerChecker = Mapper.Map<PersonAddressMakerChecker>(_personAddressViewModel);
                PersonAddressTranslation personAddressTranslation = Mapper.Map<PersonAddressTranslation>(_personAddressViewModel);
                PersonAddressTranslationMakerChecker personAddressTranslationMakerChecker = Mapper.Map<PersonAddressTranslationMakerChecker>(_personAddressViewModel);

                CustomerAccountAddressDetail customerAccountAddressDetail = Mapper.Map<CustomerAccountAddressDetail>(_personAddressViewModel);
                CustomerAccountAddressDetailMakerChecker customerAccountAddressDetailMakerChecker = Mapper.Map<CustomerAccountAddressDetailMakerChecker>(_personAddressViewModel);

                //Get PrmKey By Id
                personAddress.AddressTypePrmKey = personDetailRepository.GetAddressTypePrmKeyById(_personAddressViewModel.AddressTypeId);
                personAddress.CityPrmKey = personDetailRepository.GetCenterPrmKeyById(_personAddressViewModel.CityId);
                personAddress.ResidenceTypePrmKey = personDetailRepository.GetResidenceTypePrmKeyById(_personAddressViewModel.ResidenceTypeId);
                personAddress.OwnershipTypePrmKey = personDetailRepository.GetOwnershipTypePrmKeyById(_personAddressViewModel.OwnershipTypeId);
                personAddress.PersonPrmKey = personPrmKey;
                customerAccountAddressDetail.CustomerAccountPrmKey = customerAccountPrmKey;

                if (_entryType == StringLiteralValue.Create)
                {
                    if ((_personAddressViewModel.PersonAddressPrmKey == 0))
                    {
                        personAddressTranslation.PersonAddressPrmKey = 0;
                        personAddressMakerChecker.PersonAddressPrmKey = 0;
                        personAddressTranslationMakerChecker.PersonAddressTranslationPrmKey = 0;

                        context.PersonAddressTranslationMakerCheckers.Attach(personAddressTranslationMakerChecker);
                        context.Entry(personAddressTranslationMakerChecker).State = EntityState.Added;
                        personAddressTranslation.PersonAddressTranslationMakerCheckers.Add(personAddressTranslationMakerChecker);

                        context.PersonAddressTranslations.Attach(personAddressTranslation);
                        context.Entry(personAddressTranslation).State = EntityState.Added;

                        context.PersonAddressMakerCheckers.Attach(personAddressMakerChecker);
                        context.Entry(personAddressMakerChecker).State = EntityState.Added;
                        personAddress.PersonAddressMakerCheckers.Add(personAddressMakerChecker);

                        context.PersonAddresses.Attach(personAddress);
                        context.Entry(personAddress).State = EntityState.Added;
                    }

                    customerAccountAddressDetailMakerChecker.CustomerAccountAddressDetailPrmKey = 0;

                    context.CustomerAccountAddressDetailMakerCheckers.Attach(customerAccountAddressDetailMakerChecker);
                    context.Entry(customerAccountAddressDetailMakerChecker).State = EntityState.Added;
                    customerAccountAddressDetail.CustomerAccountAddressDetailMakerCheckers.Add(customerAccountAddressDetailMakerChecker);

                    context.CustomerAccountAddressDetails.Attach(customerAccountAddressDetail);
                    context.Entry(customerAccountAddressDetail).State = EntityState.Added;
                    personAddress.CustomerAccountAddressDetails.Add(customerAccountAddressDetail);
                }
                else
                {
                    if (customerAccountAddressDetailMakerChecker.CustomerAccountAddressDetailPrmKey > 0)
                    {
                        context.CustomerAccountAddressDetailMakerCheckers.Attach(customerAccountAddressDetailMakerChecker);
                        context.Entry(customerAccountAddressDetailMakerChecker).State = EntityState.Added;
                    }
                    else
                    {
                        context.PersonAddressTranslationMakerCheckers.Attach(personAddressTranslationMakerChecker);
                        context.Entry(personAddressTranslationMakerChecker).State = EntityState.Added;

                        context.PersonAddressMakerCheckers.Attach(personAddressMakerChecker);
                        context.Entry(personAddressMakerChecker).State = EntityState.Added;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonContactDetailData(PersonContactDetailViewModel _personContactDetailViewModel, string _entryType)
        {
            try
            {
                long personContactDetailPrmKey = _personContactDetailViewModel.PersonContactDetailPrmKey;

                configurationDetailRepository.SetDefaultValues(_personContactDetailViewModel, _entryType);

                _personContactDetailViewModel.PersonContactDetailPrmKey = personContactDetailPrmKey;

                PersonContactDetail personContactDetail = Mapper.Map<PersonContactDetail>(_personContactDetailViewModel);
                PersonContactDetailMakerChecker personContactDetailMakerChecker = Mapper.Map<PersonContactDetailMakerChecker>(_personContactDetailViewModel);

                CustomerAccountContactDetail customerAccountContactDetail = Mapper.Map<CustomerAccountContactDetail>(_personContactDetailViewModel);
                CustomerAccountContactDetailMakerChecker customerAccountContactDetailMakerChecker = Mapper.Map<CustomerAccountContactDetailMakerChecker>(_personContactDetailViewModel);

                //Get PrmKey By Id
                personContactDetail.ContactTypePrmKey = personDetailRepository.GetContactTypePrmKeyById(_personContactDetailViewModel.ContactTypeId);
                personContactDetail.PersonPrmKey = personPrmKey;
                customerAccountContactDetail.CustomerAccountPrmKey = customerAccountPrmKey;


                if (_entryType == StringLiteralValue.Create)
                {
                    if (_personContactDetailViewModel.PersonContactDetailPrmKey == 0)
                    {
                        context.PersonContactDetailMakerCheckers.Attach(personContactDetailMakerChecker);
                        context.Entry(personContactDetailMakerChecker).State = EntityState.Added;
                        personContactDetail.PersonContactDetailMakerCheckers.Add(personContactDetailMakerChecker);

                        context.PersonContactDetails.Attach(personContactDetail);
                        context.Entry(personContactDetail).State = EntityState.Added;
                    }

                    customerAccountContactDetailMakerChecker.CustomerAccountContactDetailPrmKey = 0;

                    context.CustomerAccountContactDetailMakerCheckers.Attach(customerAccountContactDetailMakerChecker);
                    context.Entry(customerAccountContactDetailMakerChecker).State = EntityState.Added;
                    customerAccountContactDetail.CustomerAccountContactDetailMakerCheckers.Add(customerAccountContactDetailMakerChecker);

                    context.CustomerAccountContactDetails.Attach(customerAccountContactDetail);
                    context.Entry(customerAccountContactDetail).State = EntityState.Added;
                    personContactDetail.CustomerAccountContactDetails.Add(customerAccountContactDetail);
                }
                else
                {
                    if (customerAccountContactDetailMakerChecker.CustomerAccountContactDetailPrmKey > 0)
                    {
                        context.CustomerAccountContactDetailMakerCheckers.Attach(customerAccountContactDetailMakerChecker);
                        context.Entry(customerAccountContactDetailMakerChecker).State = EntityState.Added;
                    }
                    else
                    {
                        context.PersonContactDetailMakerCheckers.Attach(personContactDetailMakerChecker);
                        context.Entry(personContactDetailMakerChecker).State = EntityState.Added;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSharesCapitalCustomerAccountData(SharesCapitalCustomerAccountViewModel _sharesCapitalCustomerAccountViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_sharesCapitalCustomerAccountViewModel, _entryType);

                CustomerAccount customerAccount = Mapper.Map<CustomerAccount>(_sharesCapitalCustomerAccountViewModel);
                CustomerAccountMakerChecker customerAccountMakerChecker = Mapper.Map<CustomerAccountMakerChecker>(_sharesCapitalCustomerAccountViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerAccountPrmKey = _sharesCapitalCustomerAccountViewModel.CustomerAccountPrmKey;
                    customerAccount.PrmKey = customerAccountPrmKey;

                    context.CustomerAccounts.Attach(customerAccount);
                    context.Entry(customerAccount).State = entityState;

                    context.CustomerAccountMakerCheckers.Attach(customerAccountMakerChecker);
                    context.Entry(customerAccountMakerChecker).State = EntityState.Added;
                    customerAccount.CustomerAccountMakerCheckers.Add(customerAccountMakerChecker);
                }
                else
                {
                    context.CustomerAccountMakerCheckers.Attach(customerAccountMakerChecker);
                    context.Entry(customerAccountMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //CustomerDepositAccount
        public bool AttachCustomerAccountBeneficiaryDetailData(CustomerAccountBeneficiaryDetailViewModel _customerAccountBeneficiaryDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountBeneficiaryDetailViewModel, _entryType);

                CustomerAccountBeneficiaryDetail customerAccountBeneficiaryDetail = Mapper.Map<CustomerAccountBeneficiaryDetail>(_customerAccountBeneficiaryDetailViewModel);
                CustomerAccountBeneficiaryDetailMakerChecker customerAccountBeneficiaryDetailMakerChecker = Mapper.Map<CustomerAccountBeneficiaryDetailMakerChecker>(_customerAccountBeneficiaryDetailViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    customerAccountBeneficiaryDetail.CustomerAccountTypePrmKey = accountDetailRepository.GetCustomerAccountTypePrmKeyById(_customerAccountBeneficiaryDetailViewModel.CustomerAccountTypeId);
                    customerAccountBeneficiaryDetail.CustomerAccountPrmKey = customerAccountPrmKey;

                    context.CustomerAccountBeneficiaryDetails.Attach(customerAccountBeneficiaryDetail);
                    context.Entry(customerAccountBeneficiaryDetail).State = EntityState.Added;
                    customerAccount.CustomerAccountBeneficiaryDetails.Add(customerAccountBeneficiaryDetail);

                    context.CustomerAccountBeneficiaryDetailMakerCheckers.Attach(customerAccountBeneficiaryDetailMakerChecker);
                    context.Entry(customerAccountBeneficiaryDetailMakerChecker).State = EntityState.Added;
                    customerAccountBeneficiaryDetail.CustomerAccountBeneficiaryDetailMakerCheckers.Add(customerAccountBeneficiaryDetailMakerChecker);
                }
                else
                {
                    context.CustomerAccountBeneficiaryDetailMakerCheckers.Attach(customerAccountBeneficiaryDetailMakerChecker);
                    context.Entry(customerAccountBeneficiaryDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountChequeDetailData(CustomerAccountChequeDetailViewModel _customerAccountChequeDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountChequeDetailViewModel, _entryType);

                CustomerAccountChequeDetail customerAccountChequeDetail = Mapper.Map<CustomerAccountChequeDetail>(_customerAccountChequeDetailViewModel);
                CustomerAccountChequeDetailMakerChecker customerAccountChequeDetailMakerChecker = Mapper.Map<CustomerAccountChequeDetailMakerChecker>(_customerAccountChequeDetailViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerAccountChequeDetail.CustomerAccountPrmKey = customerAccountPrmKey;
                    customerAccountChequeDetail.ChequeBookMasterPrmKey = accountDetailRepository.GetChequeBookMasterPrmKeyById(_customerAccountChequeDetailViewModel.ChequeBookMasterId);
                    customerAccountChequeDetail.Status = StringLiteralValue.NotUsed;

                    context.CustomerAccountChequeDetails.Attach(customerAccountChequeDetail);
                    context.Entry(customerAccountChequeDetail).State = entityState;
                    customerAccount.CustomerAccountChequeDetails.Add(customerAccountChequeDetail);

                    context.CustomerAccountChequeDetailMakerCheckers.Attach(customerAccountChequeDetailMakerChecker);
                    context.Entry(customerAccountChequeDetailMakerChecker).State = EntityState.Added;
                    customerAccountChequeDetail.CustomerAccountChequeDetailMakerCheckers.Add(customerAccountChequeDetailMakerChecker);
                }
                else
                {
                    context.CustomerAccountChequeDetailMakerCheckers.Attach(customerAccountChequeDetailMakerChecker);
                    context.Entry(customerAccountChequeDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountDocumentData(CustomerAccountDocumentViewModel _customerAccountDocumentViewModel, string _storagePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountDocumentViewModel, _entryType);

                CustomerAccountDocument customerAccountDocument = Mapper.Map<CustomerAccountDocument>(_customerAccountDocumentViewModel);

                CustomerAccountDocumentMakerChecker customerAccountDocumentMakerChecker = Mapper.Map<CustomerAccountDocumentMakerChecker>(_customerAccountDocumentViewModel);
                customerAccountDocument.CustomerAccountPrmKey = customerAccountPrmKey;

                customerAccountDocument.DocumentPrmKey = accountDetailRepository.GetDocumentPrmKeyId(_customerAccountDocumentViewModel.DocumentId);
                if (_entryType == StringLiteralValue.Create)
                {
                    context.CustomerAccountDocuments.Attach(customerAccountDocument);
                    context.Entry(customerAccountDocument).State = EntityState.Added;
                    //customerAccount.CustomerAccountDocuments.Add(customerAccountDocument);

                    context.CustomerAccountDocumentMakerCheckers.Attach(customerAccountDocumentMakerChecker);
                    context.Entry(customerAccountDocumentMakerChecker).State = EntityState.Added;
                    customerAccountDocument.CustomerAccountDocumentMakerCheckers.Add(customerAccountDocumentMakerChecker);

                    ////Delete Old Image When New Image Uploaded Or Deleted Existing Image when PhotoUpload is Optional.
                    //if ((_oldFileName != _customerAccountDocumentViewModel.NameOfFile && _oldFileName != "None") || _customerAccountDocumentViewModel.FileCaption == "NotApplicable")
                    //{
                    //    string serverDestinationPath = " ";

                    //    // Get Destination Path From Database
                    //    string destinationPath = _storagePath;

                    //    // Check if the destination path contains a tilde ('~') operator.
                    //    if (destinationPath.IndexOf('~') > -1)
                    //    {
                    //        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    //    }

                    //    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    //    _customerAccountDocumentViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                    //    oldRecord.Add("OldRecord");
                    //    localStorageFilePathListForOldRecord.Add(_customerAccountDocumentViewModel.LocalStoragePath);
                    //    httpPostedFileBaseListForOldRecord.Add(_customerAccountDocumentViewModel.PhotoPath);
                    //}


                }
                else
                {
                    context.CustomerAccountDocumentMakerCheckers.Attach(customerAccountDocumentMakerChecker);
                    context.Entry(customerAccountDocumentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountInterestRateData(CustomerAccountInterestRateViewModel _customerAccountInterestRateViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountInterestRateViewModel, _entryType);

                CustomerAccountInterestRate customerAccountInterestRate = Mapper.Map<CustomerAccountInterestRate>(_customerAccountInterestRateViewModel);
                CustomerAccountInterestRateMakerChecker customerAccountInterestRateMakerChecker = Mapper.Map<CustomerAccountInterestRateMakerChecker>(_customerAccountInterestRateViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerAccountInterestRate.CustomerAccountPrmKey = customerAccountPrmKey;

                    context.CustomerAccountInterestRates.Attach(customerAccountInterestRate);
                    context.Entry(customerAccountInterestRate).State = entityState;
                    customerAccount.CustomerAccountInterestRates.Add(customerAccountInterestRate);

                    context.CustomerAccountInterestRateMakerCheckers.Attach(customerAccountInterestRateMakerChecker);
                    context.Entry(customerAccountInterestRateMakerChecker).State = EntityState.Added;
                    customerAccountInterestRate.CustomerAccountInterestRateMakerCheckers.Add(customerAccountInterestRateMakerChecker);
                }
                else
                {
                    context.CustomerAccountInterestRateMakerCheckers.Attach(customerAccountInterestRateMakerChecker);
                    context.Entry(customerAccountInterestRateMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerLoanAgainstPropertyCollateralDetailData(CustomerLoanAgainstPropertyCollateralDetailViewModel _customerLoanAgainstPropertyCollateralDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerLoanAgainstPropertyCollateralDetailViewModel, _entryType);

                CustomerLoanAgainstPropertyCollateralDetail customerLoanAgainstPropertyCollateralDetail = Mapper.Map<CustomerLoanAgainstPropertyCollateralDetail>(_customerLoanAgainstPropertyCollateralDetailViewModel);
                CustomerLoanAgainstPropertyCollateralDetailMakerChecker customerLoanAgainstPropertyCollateralDetailMakerChecker = Mapper.Map<CustomerLoanAgainstPropertyCollateralDetailMakerChecker>(_customerLoanAgainstPropertyCollateralDetailViewModel);
                customerLoanAgainstPropertyCollateralDetail.CenterPrmKey = personDetailRepository.GetCenterPrmKeyById(_customerLoanAgainstPropertyCollateralDetailViewModel.CenterId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerLoanAgainstPropertyCollateralDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerLoanAgainstPropertyCollateralDetails.Attach(customerLoanAgainstPropertyCollateralDetail);
                    context.Entry(customerLoanAgainstPropertyCollateralDetail).State = entityState;
                    customerLoanAccount.CustomerLoanAgainstPropertyCollateralDetails.Add(customerLoanAgainstPropertyCollateralDetail);

                    context.CustomerLoanAgainstPropertyCollateralDetailMakerCheckers.Attach(customerLoanAgainstPropertyCollateralDetailMakerChecker);
                    context.Entry(customerLoanAgainstPropertyCollateralDetailMakerChecker).State = EntityState.Added;
                    customerLoanAgainstPropertyCollateralDetail.CustomerLoanAgainstPropertyCollateralDetailMakerCheckers.Add(customerLoanAgainstPropertyCollateralDetailMakerChecker);
                }
                else
                {
                    context.CustomerLoanAgainstPropertyCollateralDetailMakerCheckers.Attach(customerLoanAgainstPropertyCollateralDetailMakerChecker);
                    context.Entry(customerLoanAgainstPropertyCollateralDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerBusinessLoanCollateralDetailData(CustomerBusinessLoanCollateralDetailViewModel _customerBusinessLoanCollateralDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerBusinessLoanCollateralDetailViewModel, _entryType);

                CustomerBusinessLoanCollateralDetail customerBusinessLoanCollateralDetail = Mapper.Map<CustomerBusinessLoanCollateralDetail>(_customerBusinessLoanCollateralDetailViewModel);
                CustomerBusinessLoanCollateralDetailMakerChecker customerBusinessLoanCollateralDetailMakerChecker = Mapper.Map<CustomerBusinessLoanCollateralDetailMakerChecker>(_customerBusinessLoanCollateralDetailViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerBusinessLoanCollateralDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerBusinessLoanCollateralDetails.Attach(customerBusinessLoanCollateralDetail);
                    context.Entry(customerBusinessLoanCollateralDetail).State = entityState;
                    customerLoanAccount.CustomerBusinessLoanCollateralDetails.Add(customerBusinessLoanCollateralDetail);

                    context.CustomerBusinessLoanCollateralDetailMakerCheckers.Attach(customerBusinessLoanCollateralDetailMakerChecker);
                    context.Entry(customerBusinessLoanCollateralDetailMakerChecker).State = EntityState.Added;
                    customerBusinessLoanCollateralDetail.CustomerBusinessLoanCollateralDetailMakerCheckers.Add(customerBusinessLoanCollateralDetailMakerChecker);
                }
                else
                {
                    context.CustomerBusinessLoanCollateralDetailMakerCheckers.Attach(customerBusinessLoanCollateralDetailMakerChecker);
                    context.Entry(customerBusinessLoanCollateralDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerLoanAgainstDepositCollateralDetailData(CustomerLoanAgainstDepositCollateralDetailViewModel _customerLoanAgainstDepositCollateralDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerLoanAgainstDepositCollateralDetailViewModel, _entryType);

                CustomerLoanAgainstDepositCollateralDetail customerLoanAgainstDepositCollateralDetail = Mapper.Map<CustomerLoanAgainstDepositCollateralDetail>(_customerLoanAgainstDepositCollateralDetailViewModel);

                CustomerLoanAgainstDepositCollateralDetailMakerChecker customerLoanAgainstDepositCollateralDetailMakerChecker = Mapper.Map<CustomerLoanAgainstDepositCollateralDetailMakerChecker>(_customerLoanAgainstDepositCollateralDetailViewModel);

                customerLoanAgainstDepositCollateralDetail.CustomerDepositAccountPrmKey = accountDetailRepository.GetCustomerDepositAccountPrmKeyByCustomerAccountId(_customerLoanAgainstDepositCollateralDetailViewModel.DepositAccountId);

                if (_entryType == StringLiteralValue.Create)
                {
                    customerLoanAgainstDepositCollateralDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerLoanAgainstDepositCollateralDetails.Attach(customerLoanAgainstDepositCollateralDetail);
                    context.Entry(customerLoanAgainstDepositCollateralDetail).State = EntityState.Added;
                    customerLoanAccount.CustomerLoanAgainstDepositCollateralDetails.Add(customerLoanAgainstDepositCollateralDetail);

                    context.CustomerLoanAgainstDepositCollateralDetailMakerCheckers.Attach(customerLoanAgainstDepositCollateralDetailMakerChecker);
                    context.Entry(customerLoanAgainstDepositCollateralDetailMakerChecker).State = EntityState.Added;
                    customerLoanAgainstDepositCollateralDetail.CustomerLoanAgainstDepositCollateralDetailMakerCheckers.Add(customerLoanAgainstDepositCollateralDetailMakerChecker);
                }
                else
                {
                    context.CustomerLoanAgainstDepositCollateralDetailMakerCheckers.Attach(customerLoanAgainstDepositCollateralDetailMakerChecker);
                    context.Entry(customerLoanAgainstDepositCollateralDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountPhotoSignData(CustomerAccountPhotoSignViewModel _customerAccountPhotoSignViewModel, string _entryType)
        {
            try
            {
                long customerAccountPhotoSignPrmKey = _customerAccountPhotoSignViewModel.CustomerAccountPhotoSignPrmKey;
                configurationDetailRepository.SetDefaultValues(_customerAccountPhotoSignViewModel, _entryType);

                CustomerAccountPhotoSign customerAccountPhotoSign = Mapper.Map<CustomerAccountPhotoSign>(_customerAccountPhotoSignViewModel);
                CustomerAccountPhotoSignMakerChecker customerAccountPhotoSignMakerChecker = Mapper.Map<CustomerAccountPhotoSignMakerChecker>(_customerAccountPhotoSignViewModel);


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerAccountPhotoSign.CustomerAccountPrmKey = customerAccountPrmKey;
                    string oldPhotoFileName = GetOldPhotoFileName(customerAccountPhotoSignPrmKey);
                    string oldSignFileName = GetOldSignFileName(customerAccountPhotoSignPrmKey);
                    string oldPhotoLocalStoragePath = GetOldPhotoLocalStoragePath(customerAccountPhotoSignPrmKey);
                    string oldSignLocalStoragePath = GetOldSignLocalStoragePath(customerAccountPhotoSignPrmKey);

                    if (oldPhotoFileName != customerAccountPhotoSign.PhotoNameOfFile && oldPhotoLocalStoragePath != null)
                    {
                        string serverDestinationPath = " ";
                        // Check if the destination path contains a tilde ('~') operator.
                        if (oldPhotoLocalStoragePath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(oldPhotoLocalStoragePath);
                        }

                        //oldRecord.Add("OldRecord");
                        deletePhotoForDeletedRecord.Add(serverDestinationPath);
                        //httpPostedFileBaseListForOldRecord.Add(_customerAccountPhotoSignViewModel.PhotoPath);
                    }
                    if (oldSignFileName != customerAccountPhotoSign.SignNameOfFile && oldSignLocalStoragePath != null)
                    {
                        string serverDestinationPath = " ";
                        // Check if the destination path contains a tilde ('~') operator.
                        if (oldSignLocalStoragePath.IndexOf('~') > -1)
                        {
                            serverDestinationPath = HttpContext.Current.Server.MapPath(oldSignLocalStoragePath);
                        }
                        //oldRecord.Add("OldRecord");
                        deletePhotoForDeletedRecord.Add(serverDestinationPath);
                        //httpPostedFileBaseListForOldRecord.Add(_customerAccountPhotoSignViewModel.SignPath);
                    }
                    context.CustomerAccountPhotoSigns.Attach(customerAccountPhotoSign);
                    context.Entry(customerAccountPhotoSign).State = entityState;
                    customerAccount.CustomerAccountPhotoSigns.Add(customerAccountPhotoSign);

                    context.CustomerAccountPhotoSignMakerCheckers.Attach(customerAccountPhotoSignMakerChecker);
                    context.Entry(customerAccountPhotoSignMakerChecker).State = EntityState.Added;
                    customerAccountPhotoSign.CustomerAccountPhotoSignMakerCheckers.Add(customerAccountPhotoSignMakerChecker);
                }
                else
                {
                    context.CustomerAccountPhotoSignMakerCheckers.Attach(customerAccountPhotoSignMakerChecker);
                    context.Entry(customerAccountPhotoSignMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //Get Old PhotoFileName 
        string GetOldPhotoFileName(long _customerAccountPhotoSignPrmKey)
        {
            return context.CustomerAccountPhotoSigns
                   .Where(c => c.PrmKey == _customerAccountPhotoSignPrmKey)
                   .Select(c => c.PhotoNameOfFile).FirstOrDefault();
        }

        //Get Old SignFileName
        string GetOldSignFileName(long _customerAccountPhotoSignPrmKey)
        {
            return context.CustomerAccountPhotoSigns
                   .Where(c => c.PrmKey == _customerAccountPhotoSignPrmKey)
                   .Select(c => c.SignNameOfFile).FirstOrDefault();
        }

        //Get Old PhotoLocalStoragePath
        string GetOldPhotoLocalStoragePath(long _customerAccountPhotoSignPrmKey)
        {
            return context.CustomerAccountPhotoSigns
                   .Where(c => c.PrmKey == _customerAccountPhotoSignPrmKey)
                   .Select(c => c.PhotoLocalStoragePath).FirstOrDefault();
        }

        //Get Old SignLocalStoragePath
        string GetOldSignLocalStoragePath(long _customerAccountPhotoSignPrmKey)
        {
            return context.CustomerAccountPhotoSigns
                   .Where(c => c.PrmKey == _customerAccountPhotoSignPrmKey)
                   .Select(c => c.SignLocalStoragePath).FirstOrDefault();
        }

        public bool AttachCustomerAccountReferencePersonDetailData(CustomerAccountReferencePersonDetailViewModel _customerAccountReferencePersonDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountReferencePersonDetailViewModel, _entryType);

                CustomerAccountReferencePersonDetail customerAccountReferencePersonDetail = Mapper.Map<CustomerAccountReferencePersonDetail>(_customerAccountReferencePersonDetailViewModel);

                CustomerAccountReferencePersonDetailMakerChecker customerAccountReferencePersonDetailMakerChecker = Mapper.Map<CustomerAccountReferencePersonDetailMakerChecker>(_customerAccountReferencePersonDetailViewModel);

                customerAccountReferencePersonDetail.CustomerAccountNumber = accountDetailRepository.GetCustomerAccountNumberById(_customerAccountReferencePersonDetailViewModel.CustomerAccountId);

                if (_entryType == StringLiteralValue.Create)
                {
                    customerAccountReferencePersonDetail.CustomerAccountPrmKey = customerAccountPrmKey;

                    context.CustomerAccountReferencePersonDetails.Attach(customerAccountReferencePersonDetail);
                    context.Entry(customerAccountReferencePersonDetail).State = EntityState.Added;
                    customerAccount.CustomerAccountReferencePersonDetails.Add(customerAccountReferencePersonDetail);

                    context.CustomerAccountReferencePersonDetailMakerCheckers.Attach(customerAccountReferencePersonDetailMakerChecker);
                    context.Entry(customerAccountReferencePersonDetailMakerChecker).State = EntityState.Added;
                    customerAccountReferencePersonDetail.CustomerAccountReferencePersonDetailMakerCheckers.Add(customerAccountReferencePersonDetailMakerChecker);
                }
                else
                {
                    context.CustomerAccountReferencePersonDetailMakerCheckers.Attach(customerAccountReferencePersonDetailMakerChecker);
                    context.Entry(customerAccountReferencePersonDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountStandingInstructionData(CustomerAccountStandingInstructionViewModel _customerAccountStandingInstructionViewModel, string _entryType, string _instructionType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountStandingInstructionViewModel, _entryType);

                CustomerAccountStandingInstruction customerAccountStandingInstruction = Mapper.Map<CustomerAccountStandingInstruction>(_customerAccountStandingInstructionViewModel);
                CustomerAccountStandingInstructionMakerChecker customerAccountStandingInstructionMakerChecker = Mapper.Map<CustomerAccountStandingInstructionMakerChecker>(_customerAccountStandingInstructionViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    customerAccountStandingInstruction.CustomerAccountPrmKey = customerAccountPrmKey;
                    customerAccountStandingInstruction.InstructionFor = _instructionType;

                    // AUTO DEBIT
                    if (_instructionType == StringLiteralValue.DebitAccount)
                        customerAccountStandingInstruction.CustomerAccountNumber = accountDetailRepository.GetCustomerAccountNumberById(_customerAccountStandingInstructionViewModel.DebitCustomerAccountNumberId);

                    // CREDIT ACCOUNT
                    if (_instructionType == StringLiteralValue.CreditAccount)
                        customerAccountStandingInstruction.CustomerAccountNumber = accountDetailRepository.GetCustomerAccountNumberById(_customerAccountStandingInstructionViewModel.CreditCustomerAccountNumberId);

                    // INTEREST CREDIT ACCOUNT
                    if (_instructionType == StringLiteralValue.CreditInterestAccount)
                        customerAccountStandingInstruction.CustomerAccountNumber = accountDetailRepository.GetCustomerAccountNumberById(_customerAccountStandingInstructionViewModel.InterestCustomerAccountNumberId);

                    context.CustomerAccountStandingInstructions.Attach(customerAccountStandingInstruction);
                    context.Entry(customerAccountStandingInstruction).State = EntityState.Added;
                    customerAccount.CustomerAccountStandingInstructions.Add(customerAccountStandingInstruction);

                    context.CustomerAccountStandingInstructionMakerCheckers.Attach(customerAccountStandingInstructionMakerChecker);
                    context.Entry(customerAccountStandingInstructionMakerChecker).State = EntityState.Added;
                    customerAccountStandingInstruction.CustomerAccountStandingInstructionMakerCheckers.Add(customerAccountStandingInstructionMakerChecker);
                }
                else
                {
                    context.CustomerAccountStandingInstructionMakerCheckers.Attach(customerAccountStandingInstructionMakerChecker);
                    context.Entry(customerAccountStandingInstructionMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountSweepDetailData(CustomerAccountSweepDetailViewModel _customerAccountSweepDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerAccountSweepDetailViewModel, _entryType);

                CustomerAccountSweepDetail customerAccountSweepDetail = Mapper.Map<CustomerAccountSweepDetail>(_customerAccountSweepDetailViewModel);
                CustomerAccountSweepDetailMakerChecker customerAccountSweepDetailMakerChecker = Mapper.Map<CustomerAccountSweepDetailMakerChecker>(_customerAccountSweepDetailViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerAccountSweepDetail.CustomerAccountPrmKey = customerAccountPrmKey;
                    customerAccountSweepDetail.SweepOutFrequencyPrmKey = accountDetailRepository.GetSweepOutFrequencyPrmKeyById(_customerAccountSweepDetailViewModel.SweepOutFrequencyId);

                    context.CustomerAccountSweepDetails.Attach(customerAccountSweepDetail);
                    context.Entry(customerAccountSweepDetail).State = entityState;
                    customerAccount.CustomerAccountSweepDetails.Add(customerAccountSweepDetail);

                    context.CustomerAccountSweepDetailMakerCheckers.Attach(customerAccountSweepDetailMakerChecker);
                    context.Entry(customerAccountSweepDetailMakerChecker).State = EntityState.Added;
                    customerAccountSweepDetail.CustomerAccountSweepDetailMakerCheckers.Add(customerAccountSweepDetailMakerChecker);
                }
                else
                {
                    context.CustomerAccountSweepDetailMakerCheckers.Attach(customerAccountSweepDetailMakerChecker);
                    context.Entry(customerAccountSweepDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerDepositAccountAgentData(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerDepositAccountAgentViewModel, _entryType);

                CustomerDepositAccountAgent customerDepositAccountAgent = Mapper.Map<CustomerDepositAccountAgent>(_customerDepositAccountAgentViewModel);
                CustomerDepositAccountAgentMakerChecker customerDepositAccountAgentMakerChecker = Mapper.Map<CustomerDepositAccountAgentMakerChecker>(_customerDepositAccountAgentViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    customerDepositAccountAgent.CustomerDepositAccountPrmKey = customerDepositAccountPrmKey;
                    customerDepositAccountAgent.AgentPrmKey = personDetailRepository.GetAgentPrmKeyById(_customerDepositAccountAgentViewModel.AgentId);

                    context.CustomerDepositAccountAgents.Attach(customerDepositAccountAgent);
                    context.Entry(customerDepositAccountAgent).State = EntityState.Added;
                    customerDepositAccount.CustomerDepositAccountAgents.Add(customerDepositAccountAgent);

                    context.CustomerDepositAccountAgentMakerCheckers.Attach(customerDepositAccountAgentMakerChecker);
                    context.Entry(customerDepositAccountAgentMakerChecker).State = EntityState.Added;
                    customerDepositAccountAgent.CustomerDepositAccountAgentMakerCheckers.Add(customerDepositAccountAgentMakerChecker);
                }
                else
                {
                    context.CustomerDepositAccountAgentMakerCheckers.Attach(customerDepositAccountAgentMakerChecker);
                    context.Entry(customerDepositAccountAgentMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerDepositAccountData(CustomerDepositAccountViewModel _customerDepositAccountViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerDepositAccountViewModel, _entryType);

                CustomerDepositAccount customerDepositAccount = Mapper.Map<CustomerDepositAccount>(_customerDepositAccountViewModel);
                CustomerDepositAccountMakerChecker customerDepositAccountMakerChecker = Mapper.Map<CustomerDepositAccountMakerChecker>(_customerDepositAccountViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerDepositAccount.CustomerAccountPrmKey = customerAccountPrmKey;
                    customerDepositAccount.AccountOperationModePrmKey = accountDetailRepository.GetAccountOperationModePrmKeyById(_customerDepositAccountViewModel.AccountOperationModeId);
                    customerDepositAccount.InstallmentFrequencyPrmKey = accountDetailRepository.GetInstallmentFrequencyPrmKeyById(_customerDepositAccountViewModel.InstallmentFrequencyId);

                    context.CustomerDepositAccounts.Attach(customerDepositAccount);
                    context.Entry(customerDepositAccount).State = entityState;
                    customerAccount.CustomerDepositAccounts.Add(customerDepositAccount);

                    context.CustomerDepositAccountMakerCheckers.Attach(customerDepositAccountMakerChecker);
                    context.Entry(customerDepositAccountMakerChecker).State = EntityState.Added;
                    customerDepositAccount.CustomerDepositAccountMakerCheckers.Add(customerDepositAccountMakerChecker);
                }
                else
                {
                    context.CustomerDepositAccountMakerCheckers.Attach(customerDepositAccountMakerChecker);
                    context.Entry(customerDepositAccountMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerTermDepositAccountDetailData(CustomerTermDepositAccountDetailViewModel _customerTermDepositAccountDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerTermDepositAccountDetailViewModel, _entryType);

                CustomerTermDepositAccountDetail customerTermDepositAccountDetail = Mapper.Map<CustomerTermDepositAccountDetail>(_customerTermDepositAccountDetailViewModel);
                CustomerTermDepositAccountDetailMakerChecker customerTermDepositAccountDetailMakerChecker = Mapper.Map<CustomerTermDepositAccountDetailMakerChecker>(_customerTermDepositAccountDetailViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerTermDepositAccountDetail.CustomerDepositAccountPrmKey = customerDepositAccountPrmKey;
                    customerTermDepositAccountDetail.RenewTypePrmKey = accountDetailRepository.GetRenewTypePrmKeyById(_customerTermDepositAccountDetailViewModel.RenewTypeId);

                    context.CustomerTermDepositAccountDetails.Attach(customerTermDepositAccountDetail);
                    context.Entry(customerTermDepositAccountDetail).State = entityState;
                    customerDepositAccount.CustomerTermDepositAccountDetails.Add(customerTermDepositAccountDetail);

                    context.CustomerTermDepositAccountDetailMakerCheckers.Attach(customerTermDepositAccountDetailMakerChecker);
                    context.Entry(customerTermDepositAccountDetailMakerChecker).State = EntityState.Added;
                    customerTermDepositAccountDetail.CustomerTermDepositAccountDetailMakerCheckers.Add(customerTermDepositAccountDetailMakerChecker);
                }
                else
                {
                    context.CustomerTermDepositAccountDetailMakerCheckers.Attach(customerTermDepositAccountDetailMakerChecker);
                    context.Entry(customerTermDepositAccountDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachDepositCustomerAccountData(DepositCustomerAccountViewModel _depositCustomerAccountViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_depositCustomerAccountViewModel, _entryType);

                CustomerAccount customerAccount = Mapper.Map<CustomerAccount>(_depositCustomerAccountViewModel);
                CustomerAccountMakerChecker customerAccountMakerChecker = Mapper.Map<CustomerAccountMakerChecker>(_depositCustomerAccountViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerAccountPrmKey = _depositCustomerAccountViewModel.CustomerAccountPrmKey;
                    customerDepositAccountPrmKey = _depositCustomerAccountViewModel.CustomerDepositAccountViewModel.CustomerDepositAccountPrmKey;

                    customerAccount.PrmKey = customerAccountPrmKey;

                    context.CustomerAccounts.Attach(customerAccount);
                    context.Entry(customerAccount).State = entityState;

                    context.CustomerAccountMakerCheckers.Attach(customerAccountMakerChecker);
                    context.Entry(customerAccountMakerChecker).State = EntityState.Added;
                    customerAccount.CustomerAccountMakerCheckers.Add(customerAccountMakerChecker);
                }
                else
                {
                    context.CustomerAccountMakerCheckers.Attach(customerAccountMakerChecker);
                    context.Entry(customerAccountMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPhotoDocumentInDatabaseStorage(CustomerAccountPhotoSignViewModel _customerAccountPhotoSignViewModel, CustomerAccountPhotoSignViewModel customerAccountPhotoSignViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _customerAccountPhotoSignViewModel.PhotoPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _customerAccountPhotoSignViewModel.PhotoCopy = imagecode;

                    _customerAccountPhotoSignViewModel.PhotoNameOfFile = "None";
                    _customerAccountPhotoSignViewModel.PhotoLocalStoragePath = "None";
                }
                else
                {
                    _customerAccountPhotoSignViewModel.PhotoCopy = customerAccountPhotoSignViewModel.PhotoCopy;
                    _customerAccountPhotoSignViewModel.PhotoNameOfFile = customerAccountPhotoSignViewModel.SignNameOfFile;
                    _customerAccountPhotoSignViewModel.PhotoLocalStoragePath = customerAccountPhotoSignViewModel.SignLocalStoragePath;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPhotoDocumentInLocalStorage(CustomerAccountPhotoSignViewModel _customerAccountPhotoSignViewModel, string _localStoragePath, CustomerAccountPhotoSignViewModel customerAccountPhotoSignViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    //if (_entryType == StringLiteralValue.Amend && _customerAccountPhotoSignViewModel.PhotoPath != null)
                    //{
                    //    oldRecord.Add("OldRecord");
                    //    localStorageFilePathListForOldRecord.Add(customerAccountPhotoSignViewModel.PhotoLocalStoragePath);
                    //    httpPostedFileBaseListForOldRecord.Add(customerAccountPhotoSignViewModel.PhotoPath);
                    //}

                    string serverDestinationPath = " ";

                    // Get Destination Path From Database
                    string destinationPath = _localStoragePath;

                    // Encrypt Filename With Extension
                    _customerAccountPhotoSignViewModel.PhotoNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_customerAccountPhotoSignViewModel.PhotoPath.FileName);

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _customerAccountPhotoSignViewModel.PhotoNameOfFile);

                    // Add New Uploaded Path In filePathList
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_customerAccountPhotoSignViewModel.PhotoPath);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _customerAccountPhotoSignViewModel.PhotoNameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _customerAccountPhotoSignViewModel.PhotoLocalStoragePath = localStoragePath;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSignDocumentInDatabaseStorage(CustomerAccountPhotoSignViewModel _customerAccountPhotoSignViewModel, CustomerAccountPhotoSignViewModel customerAccountPhotoSignViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {
                    Stream photostream = _customerAccountPhotoSignViewModel.SignPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _customerAccountPhotoSignViewModel.SignPhotoCopy = imagecode;


                    _customerAccountPhotoSignViewModel.SignNameOfFile = "None";
                    _customerAccountPhotoSignViewModel.SignLocalStoragePath = "None";
                }
                else
                {
                    _customerAccountPhotoSignViewModel.SignPhotoCopy = customerAccountPhotoSignViewModel.PhotoCopy;
                    _customerAccountPhotoSignViewModel.SignNameOfFile = customerAccountPhotoSignViewModel.SignNameOfFile;
                    _customerAccountPhotoSignViewModel.SignLocalStoragePath = customerAccountPhotoSignViewModel.SignLocalStoragePath;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSignDocumentInLocalStorage(CustomerAccountPhotoSignViewModel _customerAccountPhotoSignViewModel, string _localStoragePath, CustomerAccountPhotoSignViewModel customerAccountPhotoSignViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    ////Delete Old Records
                    //if (_entryType == StringLiteralValue.Amend && _customerAccountPhotoSignViewModel.SignPath != null)
                    //{
                    //    oldRecord.Add("OldRecord");
                    //    localStorageFilePathListForOldRecord.Add(customerAccountPhotoSignViewModel.SignLocalStoragePath);
                    //    httpPostedFileBaseListForOldRecord.Add(customerAccountPhotoSignViewModel.SignPath);
                    //}

                    string serverDestinationPath = " ";
                    // Get Destination Path From Database

                    string destinationPath = _localStoragePath;

                    // Encrypt Filename With Extension
                    _customerAccountPhotoSignViewModel.SignNameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_customerAccountPhotoSignViewModel.SignPath.FileName);

                    // Check if the destination path contains a tilde ('~') operator.
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _customerAccountPhotoSignViewModel.SignNameOfFile);

                    // Add New Uploaded Path In filePathList
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_customerAccountPhotoSignViewModel.SignPath);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _customerAccountPhotoSignViewModel.SignNameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _customerAccountPhotoSignViewModel.SignLocalStoragePath = localStoragePath;
                }
                
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //CustomerLoanAccount
        public bool AttachCustomerLoanAccountData(CustomerLoanAccountViewModel _customerLoanAccountViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerLoanAccountViewModel, _entryType);

                CustomerLoanAccount customerLoanAccount = Mapper.Map<CustomerLoanAccount>(_customerLoanAccountViewModel);

                CustomerLoanAccountMakerChecker customerLoanAccountMakerChecker = Mapper.Map<CustomerLoanAccountMakerChecker>(_customerLoanAccountViewModel);

                CustomerLoanAccountTranslation customerLoanAccountTranslation = Mapper.Map<CustomerLoanAccountTranslation>(_customerLoanAccountViewModel);

                CustomerLoanAccountTranslationMakerChecker customerLoanAccountTranslationMakerChecker = Mapper.Map<CustomerLoanAccountTranslationMakerChecker>(_customerLoanAccountViewModel);

                if (customerLoanAccount.DeductionRemark == null)
                {
                    customerLoanAccount.DeductionRemark = "None";
                }

                //customerLoanAccountGuarantorDetail.CustomerAccountPrmKey = accountDetailRepository.GetCustomerAccountPrmKeyById(_customerLoanAccountGuarantorDetailViewModel.CustomerAccountId);
                customerLoanAccount.MinuteOfMeetingAgendaPrmKey = accountDetailRepository.GetMinuteOfMeetingAgendaPrmKeyById(_customerLoanAccountViewModel.MinuteOfMeetingAgendaId);
                customerLoanAccount.LoanReasonPrmKey = accountDetailRepository.GetLoanReasonPrmKeyById(_customerLoanAccountViewModel.LoanReasonId);
                customerLoanAccount.OccupationPrmKey = personDetailRepository.GetOccupationPrmKeyById(_customerLoanAccountViewModel.OccupationId);
                
                // customerLoanAccount.TenureListPrmKey = accountDetailRepository.GetTenureListPrmKeyById(_customerLoanAccountViewModel.TenureListId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;
                    customerLoanAccountTranslation.PrmKey = _customerLoanAccountViewModel.CustomerLoanAccountTranslationPrmKey;

                    context.CustomerLoanAccounts.Attach(customerLoanAccount);
                    context.Entry(customerLoanAccount).State = entityState;
                    customerAccount.CustomerLoanAccounts.Add(customerLoanAccount);

                    context.CustomerLoanAccountMakerCheckers.Attach(customerLoanAccountMakerChecker);
                    context.Entry(customerLoanAccountMakerChecker).State = EntityState.Added;
                    customerLoanAccount.CustomerLoanAccountMakerCheckers.Add(customerLoanAccountMakerChecker);

                    context.CustomerLoanAccountTranslations.Attach(customerLoanAccountTranslation);
                    context.Entry(customerLoanAccountTranslation).State = entityState;
                    customerLoanAccount.CustomerLoanAccountTranslations.Add(customerLoanAccountTranslation);

                    context.CustomerLoanAccountTranslationMakerCheckers.Attach(customerLoanAccountTranslationMakerChecker);
                    context.Entry(customerLoanAccountTranslationMakerChecker).State = EntityState.Added;
                    customerLoanAccountTranslation.CustomerLoanAccountTranslationMakerCheckers.Add(customerLoanAccountTranslationMakerChecker);
                }
                else
                {
                    context.CustomerLoanAccountMakerCheckers.Attach(customerLoanAccountMakerChecker);
                    context.Entry(customerLoanAccountMakerChecker).State = EntityState.Added;

                    context.CustomerLoanAccountTranslationMakerCheckers.Attach(customerLoanAccountTranslationMakerChecker);
                    context.Entry(customerLoanAccountTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachLoanCustomerAccountData(LoanCustomerAccountViewModel _loanCustomerAccountViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_loanCustomerAccountViewModel, _entryType);

                CustomerAccount customerAccount = Mapper.Map<CustomerAccount>(_loanCustomerAccountViewModel);
                CustomerAccountMakerChecker customerAccountMakerChecker = Mapper.Map<CustomerAccountMakerChecker>(_loanCustomerAccountViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerAccountPrmKey = _loanCustomerAccountViewModel.CustomerAccountPrmKey;
                    customerLoanAccountPrmKey = _loanCustomerAccountViewModel.CustomerLoanAccountViewModel.CustomerLoanAccountPrmKey;

                    customerAccount.PrmKey = customerAccountPrmKey;

                    context.CustomerAccounts.Attach(customerAccount);
                    context.Entry(customerAccount).State = entityState;

                    context.CustomerAccountMakerCheckers.Attach(customerAccountMakerChecker);
                    context.Entry(customerAccountMakerChecker).State = EntityState.Added;
                    customerAccount.CustomerAccountMakerCheckers.Add(customerAccountMakerChecker);

                }
                else
                {
                    context.CustomerAccountMakerCheckers.Attach(customerAccountMakerChecker);
                    context.Entry(customerAccountMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachPersonEmploymentDetailData(PersonEmploymentDetailViewModel _personEmploymentDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_personEmploymentDetailViewModel, _entryType);

                _personEmploymentDetailViewModel.EmploymentTypePrmKey = managementDetailRepository.GetEmploymentTypePrmKeyById(_personEmploymentDetailViewModel.EmploymentTypeId);
                _personEmploymentDetailViewModel.EmployerNaturePrmKey = managementDetailRepository.GetEmployerNaturePrmKeyById(_personEmploymentDetailViewModel.NatureOfEmployerId);
                _personEmploymentDetailViewModel.DesignationPrmKey = managementDetailRepository.GetDesignationPrmKeyById(_personEmploymentDetailViewModel.DesignationId);
                _personEmploymentDetailViewModel.EmployerCityPrmKey = personDetailRepository.GetCenterPrmKeyById(_personEmploymentDetailViewModel.CityId);
                
                PersonEmploymentDetail personEmploymentDetail = Mapper.Map<PersonEmploymentDetail>(_personEmploymentDetailViewModel);
                PersonEmploymentDetailMakerChecker personEmploymentDetailMakerChecker = Mapper.Map<PersonEmploymentDetailMakerChecker>(_personEmploymentDetailViewModel);

                PersonEmploymentDetailTranslation personEmploymentDetailTranslation = Mapper.Map<PersonEmploymentDetailTranslation>(_personEmploymentDetailViewModel);
                PersonEmploymentDetailTranslationMakerChecker personEmploymentDetailTranslationMakerChecker = Mapper.Map<PersonEmploymentDetailTranslationMakerChecker>(_personEmploymentDetailViewModel);

                CustomerAccountEmploymentDetail customerAccountEmploymentDetail = Mapper.Map<CustomerAccountEmploymentDetail>(_personEmploymentDetailViewModel);
                CustomerAccountEmploymentDetailMakerChecker customerAccountEmploymentDetailMakerChecker = Mapper.Map<CustomerAccountEmploymentDetailMakerChecker>(_personEmploymentDetailViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    if (personEmploymentDetail.PersonPrmKey == 0)
                    {
                        personEmploymentDetail.PersonPrmKey = personPrmKey;
                    }

                    customerAccountEmploymentDetail.CustomerAccountPrmKey = customerAccountPrmKey;
                    personEmploymentDetailTranslation.PrmKey = _personEmploymentDetailViewModel.PersonEmploymentDetailTranslationPrmKey;
                    customerAccountEmploymentDetail.PrmKey = _personEmploymentDetailViewModel.CustomerAccountEmploymentDetailPrmKey;

                    context.PersonEmploymentDetails.Attach(personEmploymentDetail);
                    context.Entry(personEmploymentDetail).State = entityState;

                    context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerChecker);
                    context.Entry(personEmploymentDetailMakerChecker).State = EntityState.Added;
                    personEmploymentDetail.PersonEmploymentDetailMakerCheckers.Add(personEmploymentDetailMakerChecker);

                    context.PersonEmploymentDetailTranslations.Attach(personEmploymentDetailTranslation);
                    context.Entry(personEmploymentDetailTranslation).State = entityState;
                    personEmploymentDetail.PersonEmploymentDetailTranslations.Add(personEmploymentDetailTranslation);

                    context.PersonEmploymentDetailTranslationMakerCheckers.Attach(personEmploymentDetailTranslationMakerChecker);
                    context.Entry(personEmploymentDetailTranslationMakerChecker).State = EntityState.Added;
                    personEmploymentDetailTranslation.PersonEmploymentDetailTranslationMakerCheckers.Add(personEmploymentDetailTranslationMakerChecker);

                    context.CustomerAccountEmploymentDetailMakerCheckers.Attach(customerAccountEmploymentDetailMakerChecker);
                    context.Entry(customerAccountEmploymentDetailMakerChecker).State = EntityState.Added;
                    customerAccountEmploymentDetail.CustomerAccountEmploymentDetailMakerCheckers.Add(customerAccountEmploymentDetailMakerChecker);

                    context.CustomerAccountEmploymentDetails.Attach(customerAccountEmploymentDetail);
                    context.Entry(customerAccountEmploymentDetail).State = entityState;
                    personEmploymentDetail.CustomerAccountEmploymentDetails.Add(customerAccountEmploymentDetail);

                }
                else
                {
                    context.PersonEmploymentDetailMakerCheckers.Attach(personEmploymentDetailMakerChecker);
                    context.Entry(personEmploymentDetailMakerChecker).State = EntityState.Added;

                    context.PersonEmploymentDetailTranslationMakerCheckers.Attach(personEmploymentDetailTranslationMakerChecker);
                    context.Entry(personEmploymentDetailTranslationMakerChecker).State = EntityState.Added;

                    context.CustomerAccountEmploymentDetailMakerCheckers.Attach(customerAccountEmploymentDetailMakerChecker);
                    context.Entry(customerAccountEmploymentDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerLoanAccountGuarantorDetailData
        public bool AttachCustomerLoanAccountGuarantorDetailData(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerLoanAccountGuarantorDetailViewModel, _entryType);

                CustomerLoanAccountGuarantorDetail customerLoanAccountGuarantorDetail = Mapper.Map<CustomerLoanAccountGuarantorDetail>(_customerLoanAccountGuarantorDetailViewModel);

                CustomerLoanAccountGuarantorDetailMakerChecker customerLoanAccountGuarantorDetailMakerChecker = Mapper.Map<CustomerLoanAccountGuarantorDetailMakerChecker>(_customerLoanAccountGuarantorDetailViewModel);
                
                customerLoanAccountGuarantorDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;
                customerLoanAccountGuarantorDetail.PersonPrmKey = personDetailRepository.GetPersonPrmKeyById(_customerLoanAccountGuarantorDetailViewModel.PersonId);

                if (_entryType == StringLiteralValue.Create)
                {
                    context.CustomerLoanAccountGuarantorDetails.Attach(customerLoanAccountGuarantorDetail);
                    context.Entry(customerLoanAccountGuarantorDetail).State = EntityState.Added;
                    customerLoanAccountGuarantorDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerLoanAccountGuarantorDetailMakerCheckers.Attach(customerLoanAccountGuarantorDetailMakerChecker);
                    context.Entry(customerLoanAccountGuarantorDetailMakerChecker).State = EntityState.Added;
                    customerLoanAccountGuarantorDetail.CustomerLoanAccountGuarantorDetailMakerCheckers.Add(customerLoanAccountGuarantorDetailMakerChecker);
                }
                else
                {
                    context.CustomerLoanAccountGuarantorDetailMakerCheckers.Attach(customerLoanAccountGuarantorDetailMakerChecker);
                    context.Entry(customerLoanAccountGuarantorDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerVehicleLoanCollateralDetailData
        public bool AttachCustomerVehicleLoanCollateralDetailData(CustomerVehicleLoanCollateralDetailViewModel _customerVehicleLoanCollateralDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerVehicleLoanCollateralDetailViewModel, _entryType);

                CustomerVehicleLoanCollateralDetail customerVehicleLoanCollateralDetail = Mapper.Map<CustomerVehicleLoanCollateralDetail>(_customerVehicleLoanCollateralDetailViewModel);

                CustomerVehicleLoanCollateralDetailMakerChecker customerVehicleLoanCollateralDetailMakerChecker = Mapper.Map<CustomerVehicleLoanCollateralDetailMakerChecker>(_customerVehicleLoanCollateralDetailViewModel);

                customerVehicleLoanCollateralDetail.VehicleSupplierPrmKey = accountDetailRepository.GetVehicleSupplierPrmKeyById(_customerVehicleLoanCollateralDetailViewModel.VehicleSupplierId);
                customerVehicleLoanCollateralDetail.VehicleVariantPrmKey = accountDetailRepository.GetVehicleVariantPrmKeyById(_customerVehicleLoanCollateralDetailViewModel.VehicleVariantId);
                customerVehicleLoanCollateralDetail.ColourPrmKey = accountDetailRepository.GetVehicleColourPrmKeyById(_customerVehicleLoanCollateralDetailViewModel.ColourId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;
                    customerVehicleLoanCollateralDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerVehicleLoanCollateralDetails.Attach(customerVehicleLoanCollateralDetail);
                    context.Entry(customerVehicleLoanCollateralDetail).State = entityState;
                    customerLoanAccount.CustomerVehicleLoanCollateralDetails.Add(customerVehicleLoanCollateralDetail);

                    context.CustomerVehicleLoanCollateralDetailMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                    context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;
                    customerVehicleLoanCollateralDetail.CustomerVehicleLoanCollateralDetailMakerCheckers.Add(customerVehicleLoanCollateralDetailMakerChecker);
                }
                else
                {
                    context.CustomerVehicleLoanCollateralDetailMakerCheckers.Attach(customerVehicleLoanCollateralDetailMakerChecker);
                    context.Entry(customerVehicleLoanCollateralDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerPreOwnedVehicleLoanInspectionData
        public bool AttachCustomerPreOwnedVehicleLoanInspectionData(CustomerPreOwnedVehicleLoanInspectionViewModel _customerPreOwnedVehicleLoanInspectionViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerPreOwnedVehicleLoanInspectionViewModel, _entryType);

                CustomerPreOwnedVehicleLoanInspection customerPreOwnedVehicleLoanInspection = Mapper.Map<CustomerPreOwnedVehicleLoanInspection>(_customerPreOwnedVehicleLoanInspectionViewModel);

                CustomerPreOwnedVehicleLoanInspectionMakerChecker customerPreOwnedVehicleLoanInspectionMakerChecker = Mapper.Map<CustomerPreOwnedVehicleLoanInspectionMakerChecker>(_customerPreOwnedVehicleLoanInspectionViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;
                    
                    customerPreOwnedVehicleLoanInspection.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerPreOwnedVehicleLoanInspections.Attach(customerPreOwnedVehicleLoanInspection);
                    context.Entry(customerPreOwnedVehicleLoanInspection).State = entityState;
                    customerLoanAccount.CustomerPreOwnedVehicleLoanInspections.Add(customerPreOwnedVehicleLoanInspection);

                    context.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Attach(customerPreOwnedVehicleLoanInspectionMakerChecker);
                    context.Entry(customerPreOwnedVehicleLoanInspectionMakerChecker).State = EntityState.Added;
                    customerPreOwnedVehicleLoanInspection.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Add(customerPreOwnedVehicleLoanInspectionMakerChecker);
                }
                else
                {
                    context.CustomerPreOwnedVehicleLoanInspectionMakerCheckers.Attach(customerPreOwnedVehicleLoanInspectionMakerChecker);
                    context.Entry(customerPreOwnedVehicleLoanInspectionMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerVehicleLoanInsuranceDetailData
        public bool AttachCustomerLoanAccountVehicleInsuranceDetailData(CustomerVehicleLoanInsuranceDetailViewModel _CustomerVehicleLoanInsuranceDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_CustomerVehicleLoanInsuranceDetailViewModel, _entryType);

                CustomerVehicleLoanInsuranceDetail customerVehicleLoanInsuranceDetail = Mapper.Map<CustomerVehicleLoanInsuranceDetail>(_CustomerVehicleLoanInsuranceDetailViewModel);

                CustomerVehicleLoanInsuranceDetailMakerChecker customerVehicleLoanInsuranceDetailMakerChecker = Mapper.Map<CustomerVehicleLoanInsuranceDetailMakerChecker>(_CustomerVehicleLoanInsuranceDetailViewModel);
                customerVehicleLoanInsuranceDetail.InsuranceCompanyPrmKey = personDetailRepository.GetInsuranceCompanyPrmKeyById(_CustomerVehicleLoanInsuranceDetailViewModel.InsuranceCompanyId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerVehicleLoanInsuranceDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerVehicleLoanInsuranceDetails.Attach(customerVehicleLoanInsuranceDetail);
                    context.Entry(customerVehicleLoanInsuranceDetail).State = entityState;
                    customerLoanAccount.CustomerVehicleLoanInsuranceDetails.Add(customerVehicleLoanInsuranceDetail);

                    context.CustomerVehicleLoanInsuranceDetailMakerCheckers.Attach(customerVehicleLoanInsuranceDetailMakerChecker);
                    context.Entry(customerVehicleLoanInsuranceDetailMakerChecker).State = EntityState.Added;
                    customerVehicleLoanInsuranceDetail.CustomerVehicleLoanInsuranceDetailMakerCheckers.Add(customerVehicleLoanInsuranceDetailMakerChecker);
                }
                else
                {
                    context.CustomerVehicleLoanInsuranceDetailMakerCheckers.Attach(customerVehicleLoanInsuranceDetailMakerChecker);
                    context.Entry(customerVehicleLoanInsuranceDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerVehicleLoanPermitDetail
        public bool AttachCustomerVehicleLoanPermitDetail(CustomerVehicleLoanPermitDetailViewModel _customerVehicleLoanPermitDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerVehicleLoanPermitDetailViewModel, _entryType);

                CustomerVehicleLoanPermitDetail customerVehicleLoanPermitDetail = Mapper.Map<CustomerVehicleLoanPermitDetail>(_customerVehicleLoanPermitDetailViewModel);

                CustomerVehicleLoanPermitDetailMakerChecker customerVehicleLoanPermitDetailMakerChecker = Mapper.Map<CustomerVehicleLoanPermitDetailMakerChecker>(_customerVehicleLoanPermitDetailViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;
                    customerVehicleLoanPermitDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerVehicleLoanPermitDetails.Attach(customerVehicleLoanPermitDetail);
                    context.Entry(customerVehicleLoanPermitDetail).State = entityState;
                    customerLoanAccount.CustomerVehicleLoanPermitDetails.Add(customerVehicleLoanPermitDetail);

                    context.CustomerVehicleLoanPermitDetailMakerCheckers.Attach(customerVehicleLoanPermitDetailMakerChecker);
                    context.Entry(customerVehicleLoanPermitDetailMakerChecker).State = EntityState.Added;
                    customerVehicleLoanPermitDetail.CustomerVehicleLoanPermitDetailMakerCheckers.Add(customerVehicleLoanPermitDetailMakerChecker);
                }
                else
                {
                    context.CustomerVehicleLoanPermitDetailMakerCheckers.Attach(customerVehicleLoanPermitDetailMakerChecker);
                    context.Entry(customerVehicleLoanPermitDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerVehicleLoanContractDetailData
        public bool AttachCustomerVehicleLoanContractDetailData(CustomerVehicleLoanContractDetailViewModel _customerVehicleLoanContractDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerVehicleLoanContractDetailViewModel, _entryType);

                CustomerVehicleLoanContractDetail customerVehicleLoanContractDetail = Mapper.Map<CustomerVehicleLoanContractDetail>(_customerVehicleLoanContractDetailViewModel);

                CustomerVehicleLoanContractDetailMakerChecker customerVehicleLoanContractDetailMakerChecker = Mapper.Map<CustomerVehicleLoanContractDetailMakerChecker>(_customerVehicleLoanContractDetailViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;
                    customerVehicleLoanContractDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerVehicleLoanContractDetails.Attach(customerVehicleLoanContractDetail);
                    context.Entry(customerVehicleLoanContractDetail).State = entityState;
                    customerLoanAccount.CustomerVehicleLoanContractDetails.Add(customerVehicleLoanContractDetail);

                    context.CustomerVehicleLoanContractDetailMakerCheckers.Attach(customerVehicleLoanContractDetailMakerChecker);
                    context.Entry(customerVehicleLoanContractDetailMakerChecker).State = EntityState.Added;
                    customerVehicleLoanContractDetail.CustomerVehicleLoanContractDetailMakerCheckers.Add(customerVehicleLoanContractDetailMakerChecker);
                }
                else
                {
                    context.CustomerVehicleLoanContractDetailMakerCheckers.Attach(customerVehicleLoanContractDetailMakerChecker);
                    context.Entry(customerVehicleLoanContractDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerVehicleLoanPhotoData
        public bool AttachCustomerVehicleLoanPhotoData(CustomerVehicleLoanPhotoViewModel _customerVehicleLoanPhotoViewModel, string _storagePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerVehicleLoanPhotoViewModel, _entryType);

                CustomerVehicleLoanPhoto customerVehicleLoanPhoto = Mapper.Map<CustomerVehicleLoanPhoto>(_customerVehicleLoanPhotoViewModel);
                CustomerVehicleLoanPhotoMakerChecker customerVehicleLoanPhotoMakerChecker = Mapper.Map<CustomerVehicleLoanPhotoMakerChecker>(_customerVehicleLoanPhotoViewModel);

                customerVehicleLoanPhoto.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;
                if (_entryType == StringLiteralValue.Create)
                {
                    context.CustomerVehicleLoanPhotos.Attach(customerVehicleLoanPhoto);
                    context.Entry(customerVehicleLoanPhoto).State = EntityState.Added;
                    customerLoanAccount.CustomerVehicleLoanPhotos.Add(customerVehicleLoanPhoto);

                    context.CustomerVehicleLoanPhotoMakerCheckers.Attach(customerVehicleLoanPhotoMakerChecker);
                    context.Entry(customerVehicleLoanPhotoMakerChecker).State = EntityState.Added;
                    customerVehicleLoanPhoto.CustomerVehicleLoanPhotoMakerCheckers.Add(customerVehicleLoanPhotoMakerChecker);

                    ////Delete Old Image When New Image Uploaded Or Deleted Existing Image when PhotoUpload is Optional.
                    //if ((_oldFileName != _customerVehicleLoanPhotoViewModel.NameOfFile && _oldFileName != "None")|| _customerVehicleLoanPhotoViewModel.PhotoCaption=="NotApplicable")
                    //{
                    //    string serverDestinationPath = " ";

                    //    // Get Destination Path From Database
                    //    string destinationPath = _storagePath;

                    //    // Check if the destination path contains a tilde ('~') operator.
                    //    if (destinationPath.IndexOf('~') > -1)
                    //    {
                    //        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    //    }

                    //    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    //    _customerVehicleLoanPhotoViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                    //    oldRecord.Add("OldRecord");
                    //    localStorageFilePathListForOldRecord.Add(_customerVehicleLoanPhotoViewModel.LocalStoragePath);
                    //    httpPostedFileBaseListForOldRecord.Add(_customerVehicleLoanPhotoViewModel.PhotoPath);
                    //}
                }
                else
                {
                    context.CustomerVehicleLoanPhotoMakerCheckers.Attach(customerVehicleLoanPhotoMakerChecker);
                    context.Entry(customerVehicleLoanPhotoMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerPreOwnedVehicleLoanPhotoInLocalStorage
        public bool AttachCustomerVehicleLoanPhotoInLocalStorage(CustomerVehicleLoanPhotoViewModel _customerVehicleLoanPhotoViewModel, string _storagePath, IEnumerable<CustomerVehicleLoanPhotoViewModel> _customerVehicleLoanPhotoViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {

                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _customerVehicleLoanPhotoViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_customerVehicleLoanPhotoViewModel.PhotoPath.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _storagePath;

                    // Check if the destination path contains a tilde ('~')
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _customerVehicleLoanPhotoViewModel.NameOfFile);

                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_customerVehicleLoanPhotoViewModel.PhotoPath);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _customerVehicleLoanPhotoViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _customerVehicleLoanPhotoViewModel.LocalStoragePath = localStoragePath;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerVehicleLoanPhotoInDatabaseStorage
        public bool AttachCustomerVehicleLoanPhotoInDatabaseStorage(CustomerVehicleLoanPhotoViewModel _customerVehicleLoanPhotoViewModel, IEnumerable<CustomerVehicleLoanPhotoViewModel> _customerVehicleLoanPhotoViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {

                    Stream photostream = _customerVehicleLoanPhotoViewModel.PhotoPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _customerVehicleLoanPhotoViewModel.PhotoCopy = imagecode;

                    _customerVehicleLoanPhotoViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_customerVehicleLoanPhotoViewModel.PhotoPath.FileName);

                    _customerVehicleLoanPhotoViewModel.LocalStoragePath = "None";
                }
                //else
                //{
                //    IEnumerable<CustomerVehicleLoanPhotoViewModel> customerVehicleLoanPhotoViewModels = (from a in _customerVehicleLoanPhotoViewModelList
                //                                                                                         where a.CustomerVehicleLoanPhotoPrmKey == _customerVehicleLoanPhotoViewModel.CustomerVehicleLoanPhotoPrmKey
                //                                                                                         select a).ToList();

                //    foreach (CustomerVehicleLoanPhotoViewModel customerVehicleLoanPhotoViewModel in customerVehicleLoanPhotoViewModels)
                //    {
                //        _customerVehicleLoanPhotoViewModel.PhotoCopy = customerVehicleLoanPhotoViewModel.PhotoCopy;
                //        _customerVehicleLoanPhotoViewModel.NameOfFile = customerVehicleLoanPhotoViewModel.NameOfFile;
                //        _customerVehicleLoanPhotoViewModel.LocalStoragePath = customerVehicleLoanPhotoViewModel.LocalStoragePath;
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerGoldLoanCollateralDetailData
        public bool AttachCustomerGoldLoanCollateralDetailData(CustomerGoldLoanCollateralDetailViewModel _customerGoldLoanCollateralDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerGoldLoanCollateralDetailViewModel, _entryType);

                CustomerGoldLoanCollateralDetail customerGoldLoanCollateralDetail = Mapper.Map<CustomerGoldLoanCollateralDetail>(_customerGoldLoanCollateralDetailViewModel);

                CustomerGoldLoanCollateralDetailMakerChecker customerGoldLoanCollateralDetailMakerChecker = Mapper.Map<CustomerGoldLoanCollateralDetailMakerChecker>(_customerGoldLoanCollateralDetailViewModel);

                customerGoldLoanCollateralDetail.JewelAssayerPrmKey = personDetailRepository.GetJewelAssayerPrmKeyById(_customerGoldLoanCollateralDetailViewModel.JewelAssayerId);
                customerGoldLoanCollateralDetail.GoldOrnamentPrmKey = accountDetailRepository.GetGoldOrnamentPrmKeyById(_customerGoldLoanCollateralDetailViewModel.GoldOrnamentId);
                customerGoldLoanCollateralDetail.GoldLoanRatePrmKey = accountDetailRepository.GetCurrentGoldLoanRatePrmKey(customerGoldLoanCollateralDetail.MetalPurity);

                customerGoldLoanCollateralDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;
                
                if (_entryType == StringLiteralValue.Create)
                {
                    context.CustomerGoldLoanCollateralDetails.Attach(customerGoldLoanCollateralDetail);
                    context.Entry(customerGoldLoanCollateralDetail).State = EntityState.Added;
                    customerLoanAccount.CustomerGoldLoanCollateralDetails.Add(customerGoldLoanCollateralDetail);

                    context.CustomerGoldLoanCollateralDetailMakerCheckers.Attach(customerGoldLoanCollateralDetailMakerChecker);
                    context.Entry(customerGoldLoanCollateralDetailMakerChecker).State = EntityState.Added;
                    customerGoldLoanCollateralDetail.CustomerGoldLoanCollateralDetailMakerCheckers.Add(customerGoldLoanCollateralDetailMakerChecker);
                }
                else
                {
                    context.CustomerGoldLoanCollateralDetailMakerCheckers.Attach(customerGoldLoanCollateralDetailMakerChecker);
                    context.Entry(customerGoldLoanCollateralDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerConsumerLoanCollateralDetailData
        public bool AttachCustomerConsumerLoanCollateralDetailData(CustomerConsumerLoanCollateralDetailViewModel _customerConsumerLoanCollateralDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerConsumerLoanCollateralDetailViewModel, _entryType);

                CustomerConsumerLoanCollateralDetail customerConsumerLoanCollateralDetail = Mapper.Map<CustomerConsumerLoanCollateralDetail>(_customerConsumerLoanCollateralDetailViewModel);

                CustomerConsumerLoanCollateralDetailMakerChecker customerConsumerLoanCollateralDetailMakerChecker = Mapper.Map<CustomerConsumerLoanCollateralDetailMakerChecker>(_customerConsumerLoanCollateralDetailViewModel);

                customerConsumerLoanCollateralDetail.PersonPrmKey = personDetailRepository.GetPersonPrmKeyById(_customerConsumerLoanCollateralDetailViewModel.PersonId); 
                customerConsumerLoanCollateralDetail.ConsumerDurableItemBrandPrmKey = accountDetailRepository.GetConsumerDurableItemBrandPrmKeyById(_customerConsumerLoanCollateralDetailViewModel.ConsumerDurableItemBrandId);
                customerConsumerLoanCollateralDetail.ConsumerDurableItemPrmKey = accountDetailRepository.GetConsumerDurableItemPrmKeyById(_customerConsumerLoanCollateralDetailViewModel.ConsumerDurableItemId);
                customerConsumerLoanCollateralDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;
                
                if (_entryType == StringLiteralValue.Create)
                {
                    context.CustomerConsumerLoanCollateralDetails.Attach(customerConsumerLoanCollateralDetail);
                    context.Entry(customerConsumerLoanCollateralDetail).State = EntityState.Added;
                    customerLoanAccount.CustomerConsumerLoanCollateralDetails.Add(customerConsumerLoanCollateralDetail);

                    context.CustomerConsumerLoanCollateralDetailMakerCheckers.Attach(customerConsumerLoanCollateralDetailMakerChecker);
                    context.Entry(customerConsumerLoanCollateralDetailMakerChecker).State = EntityState.Added;
                    customerConsumerLoanCollateralDetail.CustomerConsumerLoanCollateralDetailMakerCheckers.Add(customerConsumerLoanCollateralDetailMakerChecker);
                }
                else
                {
                    context.CustomerConsumerLoanCollateralDetailMakerCheckers.Attach(customerConsumerLoanCollateralDetailMakerChecker);
                    context.Entry(customerConsumerLoanCollateralDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //CustomerGoldLoanCollateralPhotoViewModel
        public bool AttachCustomerGoldLoanCollateralPhotoData(CustomerGoldLoanCollateralPhotoViewModel _customerGoldLoanCollateralPhotoViewModel, string _storagePath, string _oldFileName, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerGoldLoanCollateralPhotoViewModel, _entryType);

                CustomerGoldLoanCollateralPhoto customerGoldLoanCollateralPhoto = Mapper.Map<CustomerGoldLoanCollateralPhoto>(_customerGoldLoanCollateralPhotoViewModel);
                CustomerGoldLoanCollateralPhotoMakerChecker customerGoldLoanCollateralPhotoMakerChecker = Mapper.Map<CustomerGoldLoanCollateralPhotoMakerChecker>(_customerGoldLoanCollateralPhotoViewModel);

                customerGoldLoanCollateralPhoto.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;
                if (_entryType == StringLiteralValue.Create)
                {

                    // personBankDetail.PersonPrmKey = personPrmKey;

                    context.CustomerGoldLoanCollateralPhotos.Attach(customerGoldLoanCollateralPhoto);
                    context.Entry(customerGoldLoanCollateralPhoto).State = EntityState.Added;
                    customerLoanAccount.CustomerGoldLoanCollateralPhotos.Add(customerGoldLoanCollateralPhoto);

                    context.CustomerGoldLoanCollateralPhotoMakerCheckers.Attach(customerGoldLoanCollateralPhotoMakerChecker);
                    context.Entry(customerGoldLoanCollateralPhotoMakerChecker).State = EntityState.Added;
                    customerGoldLoanCollateralPhoto.CustomerGoldLoanCollateralPhotoMakerCheckers.Add(customerGoldLoanCollateralPhotoMakerChecker);

                    ////Delete Old Image When New Image Uploaded Or Deleted Existing Image when PhotoUpload is Optional.
                    //if ((_oldFileName != _customerGoldLoanCollateralPhotoViewModel.NameOfFile && _oldFileName != "None") || _customerGoldLoanCollateralPhotoViewModel.PhotoCaption == "NotApplicable")
                    //{
                    //    string serverDestinationPath = " ";

                    //    // Get Destination Path From Database
                    //    string destinationPath = _storagePath;

                    //    // Check if the destination path contains a tilde ('~') operator.
                    //    if (destinationPath.IndexOf('~') > -1)
                    //    {
                    //        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    //    }

                    //    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    //    _customerGoldLoanCollateralPhotoViewModel.LocalStoragePath = Path.Combine(serverDestinationPath, _oldFileName);

                    //    oldRecord.Add("OldRecord");
                    //    localStorageFilePathListForOldRecord.Add(_customerGoldLoanCollateralPhotoViewModel.LocalStoragePath);
                    //    httpPostedFileBaseListForOldRecord.Add(_customerGoldLoanCollateralPhotoViewModel.PhotoPath);
                    //}
                }

                else
                {
                    context.CustomerGoldLoanCollateralPhotoMakerCheckers.Attach(customerGoldLoanCollateralPhotoMakerChecker);
                    context.Entry(customerGoldLoanCollateralPhotoMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerGoldLoanCollateralPhotoInLocalStorage(CustomerGoldLoanCollateralPhotoViewModel _customerGoldLoanCollateralPhotoViewModel, string _storagePath, IEnumerable<CustomerGoldLoanCollateralPhotoViewModel> _customerGoldLoanCollateralPhotoViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {

                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _customerGoldLoanCollateralPhotoViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_customerGoldLoanCollateralPhotoViewModel.PhotoPath.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _storagePath;

                    // Check if the destination path contains a tilde ('~')
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _customerGoldLoanCollateralPhotoViewModel.NameOfFile);

                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_customerGoldLoanCollateralPhotoViewModel.PhotoPath);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _customerGoldLoanCollateralPhotoViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _customerGoldLoanCollateralPhotoViewModel.LocalStoragePath = localStoragePath;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerGoldLoanCollateralPhotoInDatabaseStorage(CustomerGoldLoanCollateralPhotoViewModel _customerGoldLoanCollateralPhotoViewModel, IEnumerable<CustomerGoldLoanCollateralPhotoViewModel> _customerGoldLoanCollateralPhotoViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {

                    Stream photostream = _customerGoldLoanCollateralPhotoViewModel.PhotoPath.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _customerGoldLoanCollateralPhotoViewModel.PhotoCopy = imagecode;

                    _customerGoldLoanCollateralPhotoViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_customerGoldLoanCollateralPhotoViewModel.PhotoPath.FileName);

                    _customerGoldLoanCollateralPhotoViewModel.LocalStoragePath = "None";
                }
                //else
                //{
                //    IEnumerable<CustomerGoldLoanCollateralPhotoViewModel> customerGoldLoanCollateralPhotoViewModels = (from a in _customerGoldLoanCollateralPhotoViewModelList
                //                                                                                                       where a.CustomerGoldLoanCollateralPhotoPrmKey == _customerGoldLoanCollateralPhotoViewModel.CustomerGoldLoanCollateralPhotoPrmKey
                //                                                                                                       select a).ToList();

                //    foreach (CustomerGoldLoanCollateralPhotoViewModel customerGoldLoanCollateralPhotoViewModel in customerGoldLoanCollateralPhotoViewModels)
                //    {
                //        _customerGoldLoanCollateralPhotoViewModel.PhotoCopy = customerGoldLoanCollateralPhotoViewModel.PhotoCopy;
                //        _customerGoldLoanCollateralPhotoViewModel.NameOfFile = customerGoldLoanCollateralPhotoViewModel.NameOfFile;
                //        _customerGoldLoanCollateralPhotoViewModel.LocalStoragePath = customerGoldLoanCollateralPhotoViewModel.LocalStoragePath;
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerLoanFieldInvestigationData
        public bool AttachCustomerLoanFieldInvestigationData(CustomerLoanFieldInvestigationViewModel _customerLoanFieldInvestigationViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerLoanFieldInvestigationViewModel, _entryType);

                CustomerLoanFieldInvestigation customerLoanFieldInvestigation = Mapper.Map<CustomerLoanFieldInvestigation>(_customerLoanFieldInvestigationViewModel);

                CustomerLoanFieldInvestigationMakerChecker customerLoanFieldInvestigationMakerChecker = Mapper.Map<CustomerLoanFieldInvestigationMakerChecker>(_customerLoanFieldInvestigationViewModel);

                customerLoanFieldInvestigation.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                customerLoanFieldInvestigation.EmployeePrmKey = managementDetailRepository.GetEmployeePrmKeyById(_customerLoanFieldInvestigationViewModel.InvestigationOfficerId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    context.CustomerLoanFieldInvestigations.Attach(customerLoanFieldInvestigation);
                    context.Entry(customerLoanFieldInvestigation).State = entityState;
                    customerLoanAccount.CustomerLoanFieldInvestigations.Add(customerLoanFieldInvestigation);

                    context.CustomerLoanFieldInvestigationMakerCheckers.Attach(customerLoanFieldInvestigationMakerChecker);
                    context.Entry(customerLoanFieldInvestigationMakerChecker).State = EntityState.Added;
                    customerLoanFieldInvestigation.CustomerLoanFieldInvestigationMakerCheckers.Add(customerLoanFieldInvestigationMakerChecker);
                }
                else
                {
                    context.CustomerLoanFieldInvestigationMakerCheckers.Attach(customerLoanFieldInvestigationMakerChecker);
                    context.Entry(customerLoanFieldInvestigationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerLoanAccountDebtToIncomeRatio
        public bool AttachCustomerLoanAccountDebtToIncomeRatioData(CustomerLoanAccountDebtToIncomeRatioViewModel _customerLoanAccountDebtToIncomeRatioViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerLoanAccountDebtToIncomeRatioViewModel, _entryType);

                CustomerLoanAccountDebtToIncomeRatio customerLoanAccountDebtToIncomeRatio = Mapper.Map<CustomerLoanAccountDebtToIncomeRatio>(_customerLoanAccountDebtToIncomeRatioViewModel);

                CustomerLoanAccountDebtToIncomeRatioMakerChecker customerLoanAccountDebtToIncomeRatioMakerChecker = Mapper.Map<CustomerLoanAccountDebtToIncomeRatioMakerChecker>(_customerLoanAccountDebtToIncomeRatioViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerLoanAccountDebtToIncomeRatio.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerLoanAccountDebtToIncomeRatios.Attach(customerLoanAccountDebtToIncomeRatio);
                    context.Entry(customerLoanAccountDebtToIncomeRatio).State = entityState;
                    customerLoanAccount.CustomerLoanAccountDebtToIncomeRatios.Add(customerLoanAccountDebtToIncomeRatio);

                    context.CustomerLoanAccountDebtToIncomeRatioMakerCheckers.Attach(customerLoanAccountDebtToIncomeRatioMakerChecker);
                    context.Entry(customerLoanAccountDebtToIncomeRatioMakerChecker).State = EntityState.Added;
                    customerLoanAccountDebtToIncomeRatio.CustomerLoanAccountDebtToIncomeRatioMakerCheckers.Add(customerLoanAccountDebtToIncomeRatioMakerChecker);
                }
                else
                {
                    context.CustomerLoanAccountDebtToIncomeRatioMakerCheckers.Attach(customerLoanAccountDebtToIncomeRatioMakerChecker);
                    context.Entry(customerLoanAccountDebtToIncomeRatioMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerCashCreditLoanAccountData(CustomerCashCreditLoanAccountViewModel _customerCashCreditLoanAccountViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerCashCreditLoanAccountViewModel, _entryType);

                CustomerCashCreditLoanAccount customerCashCreditLoanAccount = Mapper.Map<CustomerCashCreditLoanAccount>(_customerCashCreditLoanAccountViewModel);

                CustomerCashCreditLoanAccountMakerChecker customerCashCreditLoanAccountMakerChecker = Mapper.Map<CustomerCashCreditLoanAccountMakerChecker>(_customerCashCreditLoanAccountViewModel);

                //customerLoanAccountGuarantorDetail.CustomerAccountPrmKey = accountDetailRepository.GetCustomerAccountPrmKeyById(_customerLoanAccountGuarantorDetailViewModel.CustomerAccountId);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerCashCreditLoanAccount.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerCashCreditLoanAccounts.Attach(customerCashCreditLoanAccount);
                    context.Entry(customerCashCreditLoanAccount).State = entityState;
                    customerLoanAccount.CustomerCashCreditLoanAccounts.Add(customerCashCreditLoanAccount);
                    
                    context.CustomerCashCreditLoanAccountMakerCheckers.Attach(customerCashCreditLoanAccountMakerChecker);
                    context.Entry(customerCashCreditLoanAccountMakerChecker).State = EntityState.Added;
                    customerCashCreditLoanAccount.CustomerCashCreditLoanAccountMakerCheckers.Add(customerCashCreditLoanAccountMakerChecker);
                }
                else
                {
                    context.CustomerCashCreditLoanAccountMakerCheckers.Attach(customerCashCreditLoanAccountMakerChecker);
                    context.Entry(customerCashCreditLoanAccountMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

       public bool AttachCustomerEducationalLoanDetailData(CustomerEducationalLoanDetailViewModel _customerEducationalLoanDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerEducationalLoanDetailViewModel, _entryType);

                CustomerEducationalLoanDetail customerEducationalLoanDetail = Mapper.Map<CustomerEducationalLoanDetail>(_customerEducationalLoanDetailViewModel);

                CustomerEducationalLoanDetailMakerChecker customerEducationalLoanDetailMakerChecker = Mapper.Map<CustomerEducationalLoanDetailMakerChecker>(_customerEducationalLoanDetailViewModel);

                CustomerEducationalLoanDetailTranslation customerEducationalLoanDetailTranslation = Mapper.Map<CustomerEducationalLoanDetailTranslation>(_customerEducationalLoanDetailViewModel);

                CustomerEducationalLoanDetailTranslationMakerChecker customerEducationalLoanDetailTranslationMakerChecker = Mapper.Map<CustomerEducationalLoanDetailTranslationMakerChecker>(_customerEducationalLoanDetailViewModel);

                //customerLoanAccountGuarantorDetail.CustomerAccountPrmKey = accountDetailRepository.GetCustomerAccountPrmKeyById(_customerLoanAccountGuarantorDetailViewModel.CustomerAccountId);
                customerEducationalLoanDetail.InstitutePrmKey = accountDetailRepository.GetInstitutePrmKeyById(_customerEducationalLoanDetailViewModel.InstituteId);
                customerEducationalLoanDetail.EducationalCoursePrmKey = accountDetailRepository.GetEducationalCoursePrmKeyById(_customerEducationalLoanDetailViewModel.EducationalCourseId);
                customerEducationalLoanDetail.CityPrmKey = personDetailRepository.GetCenterPrmKeyById(_customerEducationalLoanDetailViewModel.CenterId);


                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    customerEducationalLoanDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;
                    customerEducationalLoanDetailTranslation.PrmKey = _customerEducationalLoanDetailViewModel.CustomerEducationalLoanDetailTranslationPrmKey;

                    context.CustomerEducationalLoanDetails.Attach(customerEducationalLoanDetail);
                    context.Entry(customerEducationalLoanDetail).State = entityState;
                    customerLoanAccount.CustomerEducationalLoanDetails.Add(customerEducationalLoanDetail);
                    
                    context.CustomerEducationalLoanDetailMakerCheckers.Attach(customerEducationalLoanDetailMakerChecker);
                    context.Entry(customerEducationalLoanDetailMakerChecker).State = EntityState.Added;
                    customerEducationalLoanDetail.CustomerEducationalLoanDetailMakerCheckers.Add(customerEducationalLoanDetailMakerChecker);
                
                    context.CustomerEducationalLoanDetailTranslations.Attach(customerEducationalLoanDetailTranslation);
                    context.Entry(customerEducationalLoanDetailTranslation).State = entityState;
                    customerLoanAccount.CustomerEducationalLoanDetails.Add(customerEducationalLoanDetail);
                    
                    context.CustomerEducationalLoanDetailTranslationMakerCheckers.Attach(customerEducationalLoanDetailTranslationMakerChecker);
                    context.Entry(customerEducationalLoanDetailTranslationMakerChecker).State = EntityState.Added;
                    customerEducationalLoanDetailTranslation.CustomerEducationalLoanDetailTranslationMakerCheckers.Add(customerEducationalLoanDetailTranslationMakerChecker);
                }
                else
                {
                    context.CustomerEducationalLoanDetailMakerCheckers.Attach(customerEducationalLoanDetailMakerChecker);
                    context.Entry(customerEducationalLoanDetailMakerChecker).State = EntityState.Added;
                 
                    context.CustomerEducationalLoanDetailTranslationMakerCheckers.Attach(customerEducationalLoanDetailTranslationMakerChecker);
                    context.Entry(customerEducationalLoanDetailTranslationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerAccountDocumentPhotoData

        public bool AttachCustomerAccountDocumentInLocalStorage(CustomerAccountDocumentViewModel _customerAccountDocumentViewModel, string _storagePath, IEnumerable<CustomerAccountDocumentViewModel> _customerAccountDocumentViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {

                    string serverDestinationPath = " ";

                    // Encrypt Filename With Extension
                    _customerAccountDocumentViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_customerAccountDocumentViewModel.FileUploader.FileName);

                    // Get Destination Path From Database
                    string destinationPath = _storagePath;

                    // Check if the destination path contains a tilde ('~')
                    if (destinationPath.IndexOf('~') > -1)
                    {
                        serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
                    }
                    // Combine Destination Path with the encrypted file name to get the Full Destination Path
                    string fullDestinationPath = Path.Combine(serverDestinationPath, _customerAccountDocumentViewModel.NameOfFile);

                    // Add New Uploaded Path to filePathList for tracking
                    filePathList.Add("NewUpload");

                    // Add PhotoPath Object Value In httpPostedFileBaseList
                    httpPostedFileBaseList.Add(_customerAccountDocumentViewModel.FileUploader);

                    // Added Local Storage Path In List Object (i.e. localStorageFilePathList)
                    localStorageFilePathList.Add(fullDestinationPath);

                    string localStoragePath = destinationPath + "/" + _customerAccountDocumentViewModel.NameOfFile;

                    // Save the **virtual path** to the database or DataTable
                    _customerAccountDocumentViewModel.LocalStoragePath = localStoragePath;

                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachCustomerAccountDocumentInDatabaseStorage(CustomerAccountDocumentViewModel _customerAccountDocumentViewModel, IEnumerable<CustomerAccountDocumentViewModel> _customerAccountDocumentViewModelList, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Create)
                {

                    Stream photostream = _customerAccountDocumentViewModel.FileUploader.InputStream;
                    BinaryReader photobinaryreader = new BinaryReader(photostream);
                    byte[] imagecode = photobinaryreader.ReadBytes((Int32)photostream.Length);
                    _customerAccountDocumentViewModel.DocumentPhotoCopy = imagecode;

                    _customerAccountDocumentViewModel.NameOfFile = cryptoAlgorithmRepository.EncryptFileNameByAsymmetricKey() + Path.GetExtension(_customerAccountDocumentViewModel.FileUploader.FileName);

                    _customerAccountDocumentViewModel.LocalStoragePath = "None";
                }
                //else
                //{
                //    IEnumerable<CustomerAccountDocumentViewModel> customerAccountDocumentViewModels = (from a in _customerAccountDocumentViewModelList
                //                                                                                       where a.CustomerAccountDocumentPrmKey == _customerAccountDocumentViewModel.CustomerAccountDocumentPrmKey
                //                                                                                       select a).ToList();

                //    foreach (CustomerAccountDocumentViewModel customerAccountDocumentViewModel in customerAccountDocumentViewModels)
                //    {
                //        _customerAccountDocumentViewModel.DocumentPhotoCopy = customerAccountDocumentViewModel.DocumentPhotoCopy;
                //        _customerAccountDocumentViewModel.NameOfFile = customerAccountDocumentViewModel.NameOfFile;
                //        _customerAccountDocumentViewModel.LocalStoragePath = customerAccountDocumentViewModel.LocalStoragePath;
                //    }
                //}
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        //AttachCustomerLoanAcquaintanceDetail
        public bool AttachCustomerLoanAcquaintanceDetail(CustomerLoanAcquaintanceDetailViewModel _customerLoanAcquaintanceDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_customerLoanAcquaintanceDetailViewModel, _entryType);

                CustomerLoanAcquaintanceDetail customerLoanAcquaintanceDetail = Mapper.Map<CustomerLoanAcquaintanceDetail>(_customerLoanAcquaintanceDetailViewModel);

                CustomerLoanAcquaintanceDetailMakerChecker customerLoanAcquaintanceDetailMakerChecker = Mapper.Map<CustomerLoanAcquaintanceDetailMakerChecker>(_customerLoanAcquaintanceDetailViewModel);

                customerLoanAcquaintanceDetail.RelationPrmKey = personDetailRepository.GetRelationPrmKeyById(_customerLoanAcquaintanceDetailViewModel.RelationId);

                if (_entryType == StringLiteralValue.Create)
                {
                    customerLoanAcquaintanceDetail.CustomerLoanAccountPrmKey = customerLoanAccountPrmKey;

                    context.CustomerLoanAcquaintanceDetails.Attach(customerLoanAcquaintanceDetail);
                    context.Entry(customerLoanAcquaintanceDetail).State = EntityState.Added;
                    customerLoanAccount.CustomerLoanAcquaintanceDetails.Add(customerLoanAcquaintanceDetail);

                    context.CustomerLoanAcquaintanceDetailMakerCheckers.Attach(customerLoanAcquaintanceDetailMakerChecker);
                    context.Entry(customerLoanAcquaintanceDetailMakerChecker).State = EntityState.Added;
                    customerLoanAcquaintanceDetail.CustomerLoanAcquaintanceDetailMakerCheckers.Add(customerLoanAcquaintanceDetailMakerChecker);
                }
                else
                {
                    context.CustomerLoanAcquaintanceDetailMakerCheckers.Attach(customerLoanAcquaintanceDetailMakerChecker);
                    context.Entry(customerLoanAcquaintanceDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }


        //To Collect Deleted Records From Local Storage.
        public bool DeletePhotoForDeletedRecord(string _photoPathToDelete)
        {
            deletePhotoForDeletedRecord.Add(_photoPathToDelete);

            return true;
        }

        //To Delete Old Records From Local Storage
        public bool DeletePhotoOfDeletedRecord()
        {
            try
            {
                for (byte i = 0; i < deletePhotoForDeletedRecord.Count; i++)
                {
                    if (File.Exists(deletePhotoForDeletedRecord[i]))
                        File.Delete(deletePhotoForDeletedRecord[i]);
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }


        private bool DeleteLocalStorageDocument()
        {
            try
            {
                for (byte i = 0; i < filePathList.Count; i++)
                {
                    //If New File Uploaded
                    if (filePathList[i] == "NewUpload")
                    {
                        if (File.Exists(localStorageFilePathList[i]))
                            File.Delete(localStorageFilePathList[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }

        private bool SaveLocalStorageDocument()
        {
            try
            {
                for (byte i = 0; i < filePathList.Count; i++)
                {
                    //If New File Uploaded
                    if (filePathList[i] == "NewUpload")
                    {
                        //New Uploaded File Copy Uploaded File To Destination Folder
                        httpPostedFileBaseList[i].SaveAs(localStorageFilePathList[i]);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                return false;
            }
        }


        // Create List For New Uploaded Files
        List<HttpPostedFileBase> httpPostedFileBaseList = new List<HttpPostedFileBase>();
        List<string> filePathList = new List<string>();

        // Create List For Local Storage Path (Which Stored In Database) Of Above Files (i.e. filePathList)
        // It Is Mandatory To Maintain Same Sequence Of filePathList Or localStorageFilePathList To Get Accurate Record.

        List<string> localStorageFilePathList = new List<string>();

        // Create List For Local Storage Path (Which Deleted In Database) 
        List<string> deletePhotoForDeletedRecord = new List<string>();

        //List<string> oldRecord = new List<string>();
        //List<HttpPostedFileBase> httpPostedFileBaseListForOldRecord = new List<HttpPostedFileBase>();
        //List<string> localStorageFilePathListForOldRecord = new List<string>();

        public string GetFullFilePath(string _fullPath, string _nameOfFile)
        {
            string serverDestinationPath = " ";
            // Get Destination Path From Database
            string destinationPath = _fullPath;

            // Check if the destination path contains a tilde ('~') operator.
            if (destinationPath.IndexOf('~') > -1)
            {
                serverDestinationPath = HttpContext.Current.Server.MapPath(destinationPath);
            }

            return serverDestinationPath;
        }

        public bool FileExist(string _fullFilePath)
        {
            bool result = false;

            if (File.Exists(_fullFilePath))
            {
                result = true;
            }
            return result;
        }

        public async Task<bool> SaveData()
        {
            try
            {
                SaveLocalStorageDocument();
                await context.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                string error = ex.Message;
                DeleteLocalStorageDocument();
                return false;
            }
        }

    }
}
