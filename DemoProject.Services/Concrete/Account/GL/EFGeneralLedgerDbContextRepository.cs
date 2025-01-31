using AutoMapper;
using DemoProject.Domain.Entities.Account.GL;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.GL;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace DemoProject.Services.Concrete.Account.GL
{
    public class EFGeneralLedgerDbContextRepository : IGeneralLedgerDbContextRepository
    {
        private readonly EFDbContext context;
        private readonly IGeneralLedgerDetailRepository generalLedgerDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public EFGeneralLedgerDbContextRepository(EFDbContext _context, IGeneralLedgerDetailRepository _generalLedgerDetailRepository, IAccountDetailRepository _accountDetailRepository, IConfigurationDetailRepository _configurationDetailRepository)
        {
            context = _context;
            generalLedgerDetailRepository = _generalLedgerDetailRepository;
            accountDetailRepository = _accountDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
        }

        private EntityState entityState;
        private GeneralLedger generalLedger = new GeneralLedger();
        private short generalLedgerPrmKey = 0;

        public bool AttachGeneralLedgerData(GeneralLedgerViewModel _generalLedgerViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_generalLedgerViewModel, _entryType);

                // Get PrmKey By Id Of All Dropdowns
                _generalLedgerViewModel.AccountClassPrmKey = accountDetailRepository.GetAccountClassPrmKeyById(_generalLedgerViewModel.AccountClassId);
                _generalLedgerViewModel.ParentGLPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_generalLedgerViewModel.ParentGLId);

                // Mapping
                // GeneralLedger
                GeneralLedger generalLedger = Mapper.Map<GeneralLedger>(_generalLedgerViewModel);
                GeneralLedgerMakerChecker generalLedgerMakerChecker = Mapper.Map<GeneralLedgerMakerChecker>(_generalLedgerViewModel);

                // GeneralLedgerTranslation
                GeneralLedgerTranslation generalLedgerTranslation = Mapper.Map<GeneralLedgerTranslation>(_generalLedgerViewModel);
                GeneralLedgerTranslationMakerChecker generalLedgerTranslationMakerChecker = Mapper.Map<GeneralLedgerTranslationMakerChecker>(_generalLedgerViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    generalLedgerPrmKey = _generalLedgerViewModel.GeneralLedgerPrmKey;
                    generalLedger.PrmKey = generalLedgerPrmKey;
                    generalLedgerTranslation.PrmKey = _generalLedgerViewModel.GeneralLedgerTranslationPrmKey;

                    context.GeneralLedgers.Attach(generalLedger);
                    context.Entry(generalLedger).State = entityState;

                    context.GeneralLedgerMakerCheckers.Attach(generalLedgerMakerChecker);
                    context.Entry(generalLedgerMakerChecker).State = EntityState.Added;
                    generalLedger.GeneralLedgerMakerCheckers.Add(generalLedgerMakerChecker);

                    context.GeneralLedgerTranslations.Attach(generalLedgerTranslation);
                    context.Entry(generalLedgerTranslation).State = entityState;
                    generalLedger.GeneralLedgerTranslations.Add(generalLedgerTranslation);

                    context.GeneralLedgerTranslationMakerCheckers.Attach(generalLedgerTranslationMakerChecker);
                    context.Entry(generalLedgerTranslationMakerChecker).State = EntityState.Added;
                    generalLedgerTranslation.GeneralLedgerTranslationMakerCheckers.Add(generalLedgerTranslationMakerChecker);
                }
                else
                {
                    context.GeneralLedgerMakerCheckers.Attach(generalLedgerMakerChecker);
                    context.Entry(generalLedgerMakerChecker).State = EntityState.Added;

                    context.GeneralLedgerTranslationMakerCheckers.Attach(generalLedgerTranslationMakerChecker);
                    context.Entry(generalLedgerTranslationMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachGeneralLedgerModificationData(GeneralLedgerViewModel _generalLedgerViewModel, string _entryType)
        {
            try
            {
                configurationDetailRepository.SetDefaultValues(_generalLedgerViewModel, _entryType);

                // Get PrmKey By Id Of All Dropdowns
                _generalLedgerViewModel.AccountClassPrmKey = accountDetailRepository.GetAccountClassPrmKeyById(_generalLedgerViewModel.AccountClassId);
                _generalLedgerViewModel.ParentGLPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_generalLedgerViewModel.ParentGLId);

                // Mapping
                GeneralLedgerModification generalLedgerModification = Mapper.Map<GeneralLedgerModification>(_generalLedgerViewModel);
                GeneralLedgerModificationMakerChecker generalLedgerModificationMakerChecker = Mapper.Map<GeneralLedgerModificationMakerChecker>(_generalLedgerViewModel);

                GeneralLedgerTranslation generalLedgerTranslation = Mapper.Map<GeneralLedgerTranslation>(_generalLedgerViewModel);
                GeneralLedgerTranslationMakerChecker generalLedgerTranslationMakerChecker = Mapper.Map<GeneralLedgerTranslationMakerChecker>(_generalLedgerViewModel);

                if (_entryType == StringLiteralValue.Create || _entryType == StringLiteralValue.Amend)
                {
                    entityState = (_entryType == StringLiteralValue.Amend) ? EntityState.Modified : EntityState.Added;

                    generalLedgerPrmKey = _generalLedgerViewModel.GeneralLedgerPrmKey;
                    generalLedger.PrmKey = generalLedgerPrmKey;
                    generalLedgerModification.PrmKey = _generalLedgerViewModel.GeneralLedgerModificationPrmKey;
                    generalLedgerTranslation.PrmKey = _generalLedgerViewModel.GeneralLedgerTranslationPrmKey;

                    context.GeneralLedgerModifications.Attach(generalLedgerModification);
                    context.Entry(generalLedgerModification).State = entityState;

                    context.GeneralLedgerModificationMakerCheckers.Attach(generalLedgerModificationMakerChecker);
                    context.Entry(generalLedgerModificationMakerChecker).State = EntityState.Added;
                    generalLedgerModification.GeneralLedgerModificationMakerCheckers.Add(generalLedgerModificationMakerChecker);

                    context.GeneralLedgerTranslations.Attach(generalLedgerTranslation);
                    context.Entry(generalLedgerTranslation).State = entityState;
                    generalLedger.GeneralLedgerTranslations.Add(generalLedgerTranslation);

                    context.GeneralLedgerTranslationMakerCheckers.Attach(generalLedgerTranslationMakerChecker);
                    context.Entry(generalLedgerTranslationMakerChecker).State = EntityState.Added;
                    generalLedgerTranslation.GeneralLedgerTranslationMakerCheckers.Add(generalLedgerTranslationMakerChecker);
                }
                else
                {
                    context.GeneralLedgerModificationMakerCheckers.Attach(generalLedgerModificationMakerChecker);
                    context.Entry(generalLedgerModificationMakerChecker).State = EntityState.Added;

                    context.GeneralLedgerTranslationMakerCheckers.Attach(generalLedgerTranslationMakerChecker);
                    context.Entry(generalLedgerTranslationMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachGeneralLedgerBusinessOfficeData(GeneralLedgerBusinessOfficeViewModel _generalLedgerBusinessOfficeViewModel, string _entryType)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_generalLedgerBusinessOfficeViewModel, _entryType);

                GeneralLedgerBusinessOffice generalLedgerBusinessOffice = Mapper.Map<GeneralLedgerBusinessOffice>(_generalLedgerBusinessOfficeViewModel);
                GeneralLedgerBusinessOfficeMakerChecker generalLedgerBusinessOfficeMakerChecker = Mapper.Map<GeneralLedgerBusinessOfficeMakerChecker>(_generalLedgerBusinessOfficeViewModel);

                generalLedgerBusinessOffice.BusinessOfficePrmKey = generalLedgerDetailRepository.GetBusinessOfficePrmKeyById(_generalLedgerBusinessOfficeViewModel.BusinessOfficeId);


                if (_entryType == StringLiteralValue.Create)
                {
                    generalLedgerBusinessOffice.GeneralLedgerPrmKey = generalLedgerPrmKey;

                    context.GeneralLedgerBusinessOffices.Attach(generalLedgerBusinessOffice);
                    context.Entry(generalLedgerBusinessOffice).State = EntityState.Added;
                    generalLedger.GeneralLedgerBusinessOffices.Add(generalLedgerBusinessOffice);

                    context.GeneralLedgerBusinessOfficeMakerCheckers.Attach(generalLedgerBusinessOfficeMakerChecker);
                    context.Entry(generalLedgerBusinessOfficeMakerChecker).State = EntityState.Added;
                    generalLedgerBusinessOffice.GeneralLedgerBusinessOfficeMakerCheckers.Add(generalLedgerBusinessOfficeMakerChecker);
                }
                else
                {
                    context.GeneralLedgerBusinessOfficeMakerCheckers.Attach(generalLedgerBusinessOfficeMakerChecker);
                    context.Entry(generalLedgerBusinessOfficeMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }

        public bool AttachGeneralLedgerCurrencyData(GeneralLedgerCurrencyViewModel _generalLedgerCurrencyViewModel, string _entryType)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_generalLedgerCurrencyViewModel, _entryType);

                GeneralLedgerCurrency generalLedgerCurrency = Mapper.Map<GeneralLedgerCurrency>(_generalLedgerCurrencyViewModel);
                GeneralLedgerCurrencyMakerChecker generalLedgerCurrencyMakerChecker = Mapper.Map<GeneralLedgerCurrencyMakerChecker>(_generalLedgerCurrencyViewModel);

                generalLedgerCurrency.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_generalLedgerCurrencyViewModel.CurrencyId);

                if (_entryType == StringLiteralValue.Create)
                {
                    generalLedgerCurrency.GeneralLedgerPrmKey = generalLedgerPrmKey;

                    context.GeneralLedgerCurrencies.Attach(generalLedgerCurrency);
                    context.Entry(generalLedgerCurrency).State = EntityState.Added;
                    generalLedger.GeneralLedgerCurrencies.Add(generalLedgerCurrency);

                    context.GeneralLedgerCurrencyMakerCheckers.Attach(generalLedgerCurrencyMakerChecker);
                    context.Entry(generalLedgerCurrencyMakerChecker).State = EntityState.Added;
                    generalLedgerCurrency.GeneralLedgerCurrencyMakerCheckers.Add(generalLedgerCurrencyMakerChecker);
                }
                else
                {
                    context.GeneralLedgerCurrencyMakerCheckers.Attach(generalLedgerCurrencyMakerChecker);
                    context.Entry(generalLedgerCurrencyMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }


        }

        public bool AttachGeneralLedgerTransactionTypeData(GeneralLedgerTransactionTypeViewModel _generalLedgerTransactionTypeViewModel, string _entryType)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_generalLedgerTransactionTypeViewModel, _entryType);

                _generalLedgerTransactionTypeViewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_generalLedgerTransactionTypeViewModel.TransactionTypeId);

                GeneralLedgerTransactionType generalLedgerTransactionType = Mapper.Map<GeneralLedgerTransactionType>(_generalLedgerTransactionTypeViewModel);
                GeneralLedgerTransactionTypeMakerChecker generalLedgerTransactionTypeMakerChecker = Mapper.Map<GeneralLedgerTransactionTypeMakerChecker>(_generalLedgerTransactionTypeViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    generalLedgerTransactionType.GeneralLedgerPrmKey = generalLedgerPrmKey;

                    context.GeneralLedgerTransactionTypes.Attach(generalLedgerTransactionType);
                    context.Entry(generalLedgerTransactionType).State = EntityState.Added;
                    generalLedger.GeneralLedgerTransactionTypes.Add(generalLedgerTransactionType);

                    context.GeneralLedgerTransactionTypeMakerCheckers.Attach(generalLedgerTransactionTypeMakerChecker);
                    context.Entry(generalLedgerTransactionTypeMakerChecker).State = EntityState.Added;
                    generalLedgerTransactionType.GeneralLedgerTransactionTypeMakerCheckers.Add(generalLedgerTransactionTypeMakerChecker);
                }
                else
                {
                    context.GeneralLedgerTransactionTypeMakerCheckers.Attach(generalLedgerTransactionTypeMakerChecker);
                    context.Entry(generalLedgerTransactionTypeMakerChecker).State = EntityState.Added;
                }
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }


        }

        public bool AttachGeneralLedgerCustomerTypeData(GeneralLedgerCustomerTypeViewModel _generalLedgerCustomerTypeViewModel, string _entryType)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_generalLedgerCustomerTypeViewModel, _entryType);

                _generalLedgerCustomerTypeViewModel.CustomerTypePrmKey = accountDetailRepository.GetCustomerTypePrmKeyById(_generalLedgerCustomerTypeViewModel.CustomerTypeId);

                GeneralLedgerCustomerType generalLedgerCustomerType = Mapper.Map<GeneralLedgerCustomerType>(_generalLedgerCustomerTypeViewModel);
                GeneralLedgerCustomerTypeMakerChecker generalLedgerCustomerTypeMakerChecker = Mapper.Map<GeneralLedgerCustomerTypeMakerChecker>(_generalLedgerCustomerTypeViewModel);

                if (_entryType == StringLiteralValue.Create)
                {
                    generalLedgerCustomerType.GeneralLedgerPrmKey = generalLedgerPrmKey;

                    context.GeneralLedgerCustomerTypes.Attach(generalLedgerCustomerType);
                    context.Entry(generalLedgerCustomerType).State = EntityState.Added;
                    generalLedger.GeneralLedgerCustomerTypes.Add(generalLedgerCustomerType);

                    context.GeneralLedgerCustomerTypeMakerCheckers.Attach(generalLedgerCustomerTypeMakerChecker);
                    context.Entry(generalLedgerCustomerTypeMakerChecker).State = EntityState.Added;
                    generalLedgerCustomerType.GeneralLedgerCustomerTypeMakerCheckers.Add(generalLedgerCustomerTypeMakerChecker);

                }
                else
                {
                    context.GeneralLedgerCustomerTypeMakerCheckers.Attach(generalLedgerCustomerTypeMakerChecker);
                    context.Entry(generalLedgerCustomerTypeMakerChecker).State = EntityState.Added;
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
