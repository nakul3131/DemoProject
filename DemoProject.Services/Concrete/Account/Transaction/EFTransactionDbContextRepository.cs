using AutoMapper;
using DemoProject.Domain.Entities.Account.Transaction;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Enterprise.Office;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.Wrapper;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DemoProject.Services.Concrete.Account.Transaction
{
    public  class EFTransactionDbContextRepository : ITransactionDbContextRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IBusinessOfficeRepository businessOfficeRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly ICustomerAccountRepository customerAccountRepository;
        private readonly IGeneralLedgerRepository generalLedgerRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ITransactionCustomerAccountRepository transactionCustomerAccountRepository;
        private readonly ITransactionGeneralLedgerRepository transactionGeneralLedgerRepository;
        private readonly ITransactionCashDenominationRepository transactionCashDenominationRepository;

        private EntityState entityState;

        public EFTransactionDbContextRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, ICustomerAccountRepository _customerAccountRepository, IBusinessOfficeRepository _businessOfficeRepository, IConfigurationDetailRepository _configurationDetailRepository, IGeneralLedgerRepository _generalLedgerRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ITransactionCustomerAccountRepository _transactionCustomerAccountRepository,
                                       ITransactionGeneralLedgerRepository _transactionGeneralLedgerRepository, ITransactionCashDenominationRepository _transactionCashDenominationRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            customerAccountRepository = _customerAccountRepository;
            businessOfficeRepository = _businessOfficeRepository;
            generalLedgerRepository = _generalLedgerRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            transactionCustomerAccountRepository = _transactionCustomerAccountRepository;
            transactionGeneralLedgerRepository = _transactionGeneralLedgerRepository;
            transactionCashDenominationRepository = _transactionCashDenominationRepository;
            configurationDetailRepository = _configurationDetailRepository;
        }
        public bool AttachTransactionData(TransactionViewModel _transactionViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_transactionViewModel, _entryType);

                _transactionViewModel.TokenNumber = "111";
                _transactionViewModel.TransactionNumber = 111;
                _transactionViewModel.Narration = "None";

                //Get Prm Key By Id
                _transactionViewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_transactionViewModel.TransactionTypeId);

                TransactionMaster transactionMaster = Mapper.Map<TransactionMaster>(_transactionViewModel);
                TransactionMasterMakerChecker transactionMasterMakerChecker = Mapper.Map<TransactionMasterMakerChecker>(_transactionViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    context.TransactionMasters.Attach(transactionMaster);
                    context.Entry(transactionMaster).State = entityState;

                    context.TransactionMasterMakerCheckers.Attach(transactionMasterMakerChecker);
                    context.Entry(transactionMasterMakerChecker).State = EntityState.Added;
                    transactionMaster.TransactionMasterMakerCheckers.Add(transactionMasterMakerChecker);

                }
                else
                {
                    context.TransactionMasterMakerCheckers.Attach(transactionMasterMakerChecker);
                    context.Entry(transactionMasterMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachTransactionCustomerAccountData(TransactionCustomerAccountViewModel _transactionCustomerAccountViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_transactionCustomerAccountViewModel, _entryType);

                // Get PrmKey By Id Of All Dropdowns
                _transactionCustomerAccountViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficeTypePrmKeyById(_transactionCustomerAccountViewModel.BusinessOfficeId);
                _transactionCustomerAccountViewModel.CustomerAccountPrmKey = accountDetailRepository.GetCustomerAccountPrmKeyById(_transactionCustomerAccountViewModel.TransactionCustomerAccountId);
               

                TransactionCustomerAccount transactionCustomerAccount = Mapper.Map<TransactionCustomerAccount>(_transactionCustomerAccountViewModel);
                TransactionCustomerAccountMakerChecker transactionCustomerAccountMakerChecker = Mapper.Map<TransactionCustomerAccountMakerChecker>(_transactionCustomerAccountViewModel);

                if (_entryType == StringLiteralValue.Create )
                {
                    context.TransactionCustomerAccounts.Attach(transactionCustomerAccount);
                    context.Entry(transactionCustomerAccount).State = EntityState.Added;

                    context.TransactionCustomerAccountMakerCheckers.Attach(transactionCustomerAccountMakerChecker);
                    context.Entry(transactionCustomerAccountMakerChecker).State = EntityState.Added;
                    transactionCustomerAccount.TransactionCustomerAccountMakerCheckers.Add(transactionCustomerAccountMakerChecker);
                }
                else
                {
                    context.TransactionCustomerAccountMakerCheckers.Attach(transactionCustomerAccountMakerChecker);
                    context.Entry(transactionCustomerAccountMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSharesCessationTransactionData(SharesCessationTransactionViewModel _sharesCessationTransactionViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_sharesCessationTransactionViewModel, _entryType);
                _sharesCessationTransactionViewModel.CessionReason = "NNN";
                SharesCessationTransaction sharesCessationTransaction = Mapper.Map<SharesCessationTransaction>(_sharesCessationTransactionViewModel);
                SharesCessationTransactionMakerChecker sharesCessationTransactionMakerChecker = Mapper.Map<SharesCessationTransactionMakerChecker>(_sharesCessationTransactionViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    context.SharesCessationTransactions.Attach(sharesCessationTransaction);
                    context.Entry(sharesCessationTransaction).State = EntityState.Added;

                    context.SharesCessationTransactionMakerCheckers.Attach(sharesCessationTransactionMakerChecker);
                    context.Entry(sharesCessationTransactionMakerChecker).State = EntityState.Added;
                    sharesCessationTransaction.SharesCessationTransactionMakerCheckers.Add(sharesCessationTransactionMakerChecker);
                }
                else
                {
                    context.SharesCessationTransactionMakerCheckers.Attach(sharesCessationTransactionMakerChecker);
                    context.Entry(sharesCessationTransactionMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachTransactionGeneralLedgerData(TransactionGeneralLedgerViewModel _transactionGeneralLedgerViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_transactionGeneralLedgerViewModel, _entryType);
               
                // Get PrmKey By Id Of All Dropdowns
                _transactionGeneralLedgerViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_transactionGeneralLedgerViewModel.BusinessOfficeId);
                _transactionGeneralLedgerViewModel.GeneralLedgerPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_transactionGeneralLedgerViewModel.GeneralLedgerId);
                _transactionGeneralLedgerViewModel.PersonPrmKey = accountDetailRepository.GetPersonPrmKeyByPersonId(_transactionGeneralLedgerViewModel.PersonId);

                TransactionGeneralLedger transactionGeneralLedger = Mapper.Map<TransactionGeneralLedger>(_transactionGeneralLedgerViewModel);
                TransactionGeneralLedgerMakerChecker transactionGeneralLedgerMakerChecker = Mapper.Map<TransactionGeneralLedgerMakerChecker>(_transactionGeneralLedgerViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    context.TransactionGeneralLedgers.Attach(transactionGeneralLedger);
                    context.Entry(transactionGeneralLedger).State = EntityState.Added;

                    context.TransactionGeneralLedgerMakerCheckers.Attach(transactionGeneralLedgerMakerChecker);
                    context.Entry(transactionGeneralLedgerMakerChecker).State = EntityState.Added;
                    transactionGeneralLedger.TransactionGeneralLedgerMakerCheckers.Add(transactionGeneralLedgerMakerChecker);

                }
                else
                {
                    context.TransactionGeneralLedgerMakerCheckers.Attach(transactionGeneralLedgerMakerChecker);
                    context.Entry(transactionGeneralLedgerMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

         public bool AttachTransactionGSTDetailData(TransactionGSTDetailViewModel _transactionGSTDetailViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_transactionGSTDetailViewModel, _entryType);

                TransactionGSTDetail transactionGSTDetail = Mapper.Map<TransactionGSTDetail>(_transactionGSTDetailViewModel);
                TransactionGSTDetailMakerChecker transactionGSTDetailMakerChecker = Mapper.Map<TransactionGSTDetailMakerChecker>(_transactionGSTDetailViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    context.TransactionGSTDetails.Attach(transactionGSTDetail);
                    context.Entry(transactionGSTDetail).State = EntityState.Added;

                    context.TransactionGSTDetailMakerCheckers.Attach(transactionGSTDetailMakerChecker);
                    context.Entry(transactionGSTDetailMakerChecker).State = EntityState.Added;
                    transactionGSTDetail.TransactionGSTDetailMakerCheckers.Add(transactionGSTDetailMakerChecker);

                }
                else
                {
                    context.TransactionGSTDetailMakerCheckers.Attach(transactionGSTDetailMakerChecker);
                    context.Entry(transactionGSTDetailMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachSharesTransactionData(SharesCapitalTransactionViewModel _sharesCapitalTransactionViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_sharesCapitalTransactionViewModel, _entryType);
                
                SharesCapitalTransaction sharesCapitalTransaction = Mapper.Map<SharesCapitalTransaction>(_sharesCapitalTransactionViewModel);
                SharesCapitalTransactionMakerChecker sharesCapitalTransactionMakerChecker = Mapper.Map<SharesCapitalTransactionMakerChecker>(_sharesCapitalTransactionViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    context.SharesCapitalTransactions.Attach(sharesCapitalTransaction);
                    context.Entry(sharesCapitalTransaction).State = EntityState.Added;
                    
                    context.SharesCapitalTransactionMakerCheckers.Attach(sharesCapitalTransactionMakerChecker);
                    context.Entry(sharesCapitalTransactionMakerChecker).State = EntityState.Added;
                    sharesCapitalTransaction.SharesCapitalTransactionMakerCheckers.Add(sharesCapitalTransactionMakerChecker);

                }
                else
                {
                    context.SharesCapitalTransactionMakerCheckers.Attach(sharesCapitalTransactionMakerChecker);
                    context.Entry(sharesCapitalTransactionMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachTransactionCashDenominationData(TransactionCashDenominationViewModel _transactionCashDenominationViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_transactionCashDenominationViewModel, _entryType);

                // Get PrmKey By Id Of All Dropdowns
                _transactionCashDenominationViewModel.DenominationPrmKey = accountDetailRepository.GetDenominationPrmKey(_transactionCashDenominationViewModel.DenominationId);

                TransactionCashDenomination transactionCashDenomination = Mapper.Map<TransactionCashDenomination>(_transactionCashDenominationViewModel);
                TransactionCashDenominationMakerChecker transactionCashDenominationMakerChecker = Mapper.Map<TransactionCashDenominationMakerChecker>(_transactionCashDenominationViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    context.TransactionCashDenominations.Attach(transactionCashDenomination);
                    context.Entry(transactionCashDenomination).State = EntityState.Added;

                    context.TransactionCashDenominationMakerCheckers.Attach(transactionCashDenominationMakerChecker);
                    context.Entry(transactionCashDenominationMakerChecker).State = EntityState.Added;
                    transactionCashDenomination.TransactionCashDenominationMakerCheckers.Add(transactionCashDenominationMakerChecker);

                }
                else
                {
                    context.TransactionCashDenominationMakerCheckers.Attach(transactionCashDenominationMakerChecker);
                    context.Entry(transactionCashDenominationMakerChecker).State = EntityState.Added;
                }

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public async Task<bool> SaveData()
        {
            try
            {
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }
    }
}
