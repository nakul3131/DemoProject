using AutoMapper;
using DemoProject.Domain.Entities.Account.GL;
using DemoProject.Services.Abstract.Account.GL;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Management;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.GL;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace DemoProject.Services.Concrete.Account.GL
{
    public class EFGeneralLedgerRepository : IGeneralLedgerRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IManagementDetailRepository managementDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IGeneralLedgerBusinessOfficeRepository generalLedgerBusinessOfficeRepository;
        private readonly IGeneralLedgerCurrencyRepository generalLedgerCurrencyRepository;
        private readonly IGeneralLedgerTransactionTypeRepository generalLedgerTransactionTypeRepository;
        private readonly IGeneralLedgerCustomerTypeRepository generalLedgerCustomerTypeRepository;
        private readonly IGeneralLedgerDetailRepository generalLedgerDetailRepository;
        private readonly IGeneralLedgerDbContextRepository generalLedgerDbContextRepository;

        public EFGeneralLedgerRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IManagementDetailRepository _managementDetailRepository,
                                        IPersonDetailRepository _personDetailsRepository, IGeneralLedgerBusinessOfficeRepository _generalLedgerBusinessOfficeRepository, IGeneralLedgerCurrencyRepository _generalLedgerCurrencyRepository,
                                        IGeneralLedgerTransactionTypeRepository _generalLedgerTransactionTypeRepository, IGeneralLedgerCustomerTypeRepository _generalLedgerCustomerTypeRepository, IGeneralLedgerDetailRepository _generalLedgerDetailRepository, IGeneralLedgerDbContextRepository _generalLedgerDbContextRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            managementDetailRepository = _managementDetailRepository;
            personDetailRepository = _personDetailsRepository;
            generalLedgerBusinessOfficeRepository = _generalLedgerBusinessOfficeRepository;
            generalLedgerCurrencyRepository = _generalLedgerCurrencyRepository;
            generalLedgerTransactionTypeRepository = _generalLedgerTransactionTypeRepository;
            generalLedgerCustomerTypeRepository = _generalLedgerCustomerTypeRepository;
            generalLedgerDetailRepository = _generalLedgerDetailRepository;
            generalLedgerDbContextRepository = _generalLedgerDbContextRepository;
        }

        public async Task<bool> Amend(GeneralLedgerViewModel _generalLedgerViewModel)
        {
            try
            {
                bool result = true;
                if (result)
                {
                    // Check Entry Existance In Modification Table Or Main Table
                    if (_generalLedgerViewModel.GeneralLedgerModificationPrmKey == 0)
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerData(_generalLedgerViewModel, StringLiteralValue.Amend);
                    else
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerModificationData(_generalLedgerViewModel, StringLiteralValue.Amend);
                }

                // Amend GeneralLedgerBusinessOffice
                if (result)
                {
                    IEnumerable<GeneralLedgerBusinessOfficeViewModel> GeneralLedgerBusinessOfficeListForAmend = await generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeEntries(_generalLedgerViewModel.GeneralLedgerPrmKey, StringLiteralValue.Reject);

                    foreach (GeneralLedgerBusinessOfficeViewModel viewModel in GeneralLedgerBusinessOfficeListForAmend)
                    {
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerBusinessOfficeData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // Create New GeneralLedgerBusinessOffice
                if (result)
                {
                    List<GeneralLedgerBusinessOfficeViewModel> generalLedgerBusinessOfficeViewModelList = (List<GeneralLedgerBusinessOfficeViewModel>)HttpContext.Current.Session["GeneralLedgerBusinessOffice"];
                    if (generalLedgerBusinessOfficeViewModelList != null)
                    {
                        foreach (GeneralLedgerBusinessOfficeViewModel viewModel in generalLedgerBusinessOfficeViewModelList)
                        {
                            result = generalLedgerDbContextRepository.AttachGeneralLedgerBusinessOfficeData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }


                // Amend GeneralLedgerCurrency 
                if (result)
                {
                    IEnumerable<GeneralLedgerCurrencyViewModel> generalLedgerCurrencyListForAmend = await generalLedgerDetailRepository.GetGeneralLedgerCurrencyEntries(_generalLedgerViewModel.GeneralLedgerPrmKey, StringLiteralValue.Reject);

                    foreach (GeneralLedgerCurrencyViewModel viewModel in generalLedgerCurrencyListForAmend)
                    {
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerCurrencyData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Create New GeneralLedgerCurrency 
                if (result)
                {
                    List<GeneralLedgerCurrencyViewModel> generalLedgerCurrencyViewModelList = (List<GeneralLedgerCurrencyViewModel>)HttpContext.Current.Session["GeneralLedgerCurrency"];
                    if (generalLedgerCurrencyViewModelList != null)
                    {
                        foreach (GeneralLedgerCurrencyViewModel viewModel in generalLedgerCurrencyViewModelList)
                        {
                            result = generalLedgerDbContextRepository.AttachGeneralLedgerCurrencyData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // Amend GeneralLedgerTransactionType
                if (result)
                {
                    IEnumerable<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeViewModelForAmend = await generalLedgerDetailRepository.GetGeneralLedgerTransactionTypeEntries(_generalLedgerViewModel.GeneralLedgerPrmKey, StringLiteralValue.Reject);

                    foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeViewModelForAmend)
                    {
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerTransactionTypeData(viewModel, StringLiteralValue.Amend);

                    }
                }

                //Create New GeneralLedgerTransactionType
                if (result)
                {
                    List<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeViewModelList = (List<GeneralLedgerTransactionTypeViewModel>)HttpContext.Current.Session["GeneralLedgerTransactionType"];

                    if (generalLedgerTransactionTypeViewModelList != null)
                    {
                        foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeViewModelList)
                        {
                            result = generalLedgerDbContextRepository.AttachGeneralLedgerTransactionTypeData(viewModel, StringLiteralValue.Create);

                        }
                    }
                }

                // Amend GeneralLedgerCustomerType 
                if (result)
                {
                    IEnumerable<GeneralLedgerCustomerTypeViewModel> generalLedgerCustomerTypeViewModelForAmend = await generalLedgerDetailRepository.GetGeneralLedgerCustomerTypeEntries(_generalLedgerViewModel.GeneralLedgerPrmKey, StringLiteralValue.Reject);

                    foreach (GeneralLedgerCustomerTypeViewModel viewModel in generalLedgerCustomerTypeViewModelForAmend)
                    {
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerCustomerTypeData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Create New GeneralLedgerCustomerType
                if (result)
                {
                    List<GeneralLedgerCustomerTypeViewModel> generalLedgerCustomerTypeViewModelList = (List<GeneralLedgerCustomerTypeViewModel>)HttpContext.Current.Session["GeneralLedgerCustomerType"];

                    if (generalLedgerCustomerTypeViewModelList != null)
                    {
                        foreach (GeneralLedgerCustomerTypeViewModel viewModel in generalLedgerCustomerTypeViewModelList)
                        {
                            result = generalLedgerDbContextRepository.AttachGeneralLedgerCustomerTypeData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                if (result)
                    result = await generalLedgerDbContextRepository.SaveData();

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

        public async Task<bool> GetSessionValues(short _generalLedgerPrmKey, string _entryType)
        {
            try
            {
                HttpContext.Current.Session["GeneralLedgerBusinessOffice"] = await generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeEntries(_generalLedgerPrmKey, _entryType);

                HttpContext.Current.Session["GeneralLedgerCurrency"] = await generalLedgerDetailRepository.GetGeneralLedgerCurrencyEntries(_generalLedgerPrmKey, _entryType);

                HttpContext.Current.Session["GeneralLedgerTransactionType"] = await generalLedgerDetailRepository.GetGeneralLedgerTransactionTypeEntries(_generalLedgerPrmKey, _entryType);

                HttpContext.Current.Session["GeneralLedgerCustomerType"] = await generalLedgerDetailRepository.GetGeneralLedgerCustomerTypeEntries(_generalLedgerPrmKey, _entryType);

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<GeneralLedgerViewModel> GetGeneralLedgerEntry(Guid _generalLedgerId, string _entryType)
        {
            try
            {
                GeneralLedgerViewModel generalLedgerViewModel = await context.Database.SqlQuery<GeneralLedgerViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntry (@GeneralLedgerId, @EntryType)", new SqlParameter("@GeneralLedgerId", _generalLedgerId), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                return generalLedgerViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        public async Task<GeneralLedgerViewModel> GetGeneralLedgerEntryByPrmKey(short _generalLedgerPrmKey, string _entryType)
        {
            try
            {
                GeneralLedgerViewModel generalLedgerViewModel = await context.Database.SqlQuery<GeneralLedgerViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntry (@GeneralLedgerPrmKey, @EntryType)", new SqlParameter("@GeneralLedgerPrmKey", _generalLedgerPrmKey), new SqlParameter("EntryType", _entryType)).FirstOrDefaultAsync();

                return generalLedgerViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        public async Task<IEnumerable<GeneralLedgerIndexViewModel>> GetGeneralLedgerIndex(string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<GeneralLedgerIndexViewModel>("SELECT * FROM dbo.GetGeneralLedgerEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        public List<SelectListItem> GLParentDropdownList
        {
            get
            {
                // Get Regional Language PrmKey Session Object In Local Variable To Avoid Repetation
                short regionalLanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];

                // If Regional Language Other Than English i.e. (LanguagePrmKey = 1)
                if (regionalLanguagePrmKey != 1)
                {
                    return (from h in context.GeneralLedgers
                            join mf in context.GeneralLedgerModifications on h.PrmKey equals mf.GeneralLedgerPrmKey into hm
                            from mf in hm.DefaultIfEmpty()
                            join t in context.GeneralLedgerTranslations on h.PrmKey equals t.GeneralLedgerPrmKey into ht
                            from t in ht.DefaultIfEmpty()

                            where (h.EntryStatus.Equals(StringLiteralValue.Verify))
                                    && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                    || (h.EntryStatus == StringLiteralValue.Verify)
                                    && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                    && (t.EntryStatus.Equals(StringLiteralValue.Verify) || t.EntryStatus.Equals(null))
                                    && (t.LanguagePrmKey == regionalLanguagePrmKey)
                                    && (h.IsModified.Equals(false))
                            orderby h.NameOfGL
                            select new SelectListItem
                            {
                                Value = h.GeneralLedgerId.ToString(),
                                Text = ((mf.NameOfGL.Equals(null)) ? h.NameOfGL.Trim() + " ---> " + (t.TransNameOfGL.Equals(null) ? " " : t.TransNameOfGL.Trim()) : mf.NameOfGL + " ---> " + (t.TransNameOfGL.Equals(null) ? " " : t.TransNameOfGL.Trim()))
                            }).ToList();
                }

                // Default List In Default Language (i.e. English)
                return (from h in context.GeneralLedgers
                        join mf in context.GeneralLedgerModifications on h.PrmKey equals mf.GeneralLedgerPrmKey into hm
                        from mf in hm.DefaultIfEmpty()
                        join t in context.GeneralLedgerTranslations on h.PrmKey equals t.GeneralLedgerPrmKey into ht
                        from t in ht.DefaultIfEmpty()
                        where (h.EntryStatus.Equals(StringLiteralValue.Verify))
                                && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (mf.EntryStatus.Equals(StringLiteralValue.Verify) || mf.EntryStatus.Equals(null))
                                || (h.EntryStatus == StringLiteralValue.Verify)
                                && (h.ActivationStatus.Equals(StringLiteralValue.Active))
                                && (h.IsModified.Equals(false))
                        orderby h.NameOfGL
                        select new SelectListItem
                        {
                            Value = h.GeneralLedgerId.ToString(),
                            Text = ((mf.NameOfGL.Equals(null)) ? h.NameOfGL.Trim() : mf.NameOfGL.Trim())
                        }).ToList();
            }
        }

        public short GetCashGeneralLedgerPrmKey()
        {
            return context.GeneralLedgers
                    .Where(gl => gl.AccountClassPrmKey == 10001)
                    .Select(gl => gl.PrmKey).FirstOrDefault();
        }


        public short GetPrmKeyById(Guid _GeneralLedgerId)
        {
            return context.GeneralLedgers
                    .Where(c => c.GeneralLedgerId == _GeneralLedgerId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<bool> Modify(GeneralLedgerViewModel _generalLedgerViewModel)
        {
            try
            {
                // Set Default Value
                generalLedgerDetailRepository.GetGeneralLedgerAllDefaultValues(_generalLedgerViewModel, StringLiteralValue.Create);
                _generalLedgerViewModel.GeneralLedgerTranslationPrmKey = 0;

                // Get PrmKey By Id Of All Dropdowns
                _generalLedgerViewModel.AccountClassPrmKey = accountDetailRepository.GetAccountClassPrmKeyById(_generalLedgerViewModel.AccountClassId);
                _generalLedgerViewModel.ParentGLPrmKey = accountDetailRepository.GetGeneralLedgerPrmKeyById(_generalLedgerViewModel.ParentGLId);

                // GeneralLedgerModification
                GeneralLedgerModification generalLedgerModification = Mapper.Map<GeneralLedgerModification>(_generalLedgerViewModel);
                GeneralLedgerModificationMakerChecker generalLedgerModificationMakerChecker = Mapper.Map<GeneralLedgerModificationMakerChecker>(_generalLedgerViewModel);

                // GeneralLedgerTranslation
                GeneralLedgerTranslation generalLedgerTranslation = Mapper.Map<GeneralLedgerTranslation>(_generalLedgerViewModel);
                GeneralLedgerTranslationMakerChecker generalLedgerTranslationMakerChecker = Mapper.Map<GeneralLedgerTranslationMakerChecker>(_generalLedgerViewModel);

                // GeneralLedgerModification
                context.GeneralLedgerModificationMakerCheckers.Attach(generalLedgerModificationMakerChecker);
                context.Entry(generalLedgerModificationMakerChecker).State = EntityState.Added;
                generalLedgerModification.GeneralLedgerModificationMakerCheckers.Add(generalLedgerModificationMakerChecker);

                context.GeneralLedgerModifications.Attach(generalLedgerModification);
                context.Entry(generalLedgerModification).State = EntityState.Added;

                // GeneralLedgerTranslation
                context.GeneralLedgerTranslationMakerCheckers.Attach(generalLedgerTranslationMakerChecker);
                context.Entry(generalLedgerTranslationMakerChecker).State = EntityState.Added;
                generalLedgerTranslation.GeneralLedgerTranslationMakerCheckers.Add(generalLedgerTranslationMakerChecker);

                context.GeneralLedgerTranslations.Attach(generalLedgerTranslation);
                context.Entry(generalLedgerTranslation).State = EntityState.Added;

                // GeneralLedgerBusinessOffice
                List<GeneralLedgerBusinessOfficeViewModel> generalLedgerBusinessOfficeViewModelList = (List<GeneralLedgerBusinessOfficeViewModel>)HttpContext.Current.Session["GeneralLedgerBusinessOffice"];
                if (generalLedgerBusinessOfficeViewModelList != null)
                {
                    foreach (GeneralLedgerBusinessOfficeViewModel viewModel in generalLedgerBusinessOfficeViewModelList)
                    {
                        generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeDefaultValues(viewModel, StringLiteralValue.Create, _generalLedgerViewModel.GeneralLedgerPrmKey);

                        GeneralLedgerBusinessOffice generalLedgerBusinessOffice = Mapper.Map<GeneralLedgerBusinessOffice>(viewModel);
                        GeneralLedgerBusinessOfficeMakerChecker generalLedgerBusinessOfficeMakerChecker = Mapper.Map<GeneralLedgerBusinessOfficeMakerChecker>(viewModel);

                        // Set ReferenceKey As PrmKey To Every Object
                        generalLedgerBusinessOffice.BusinessOfficePrmKey = generalLedgerDetailRepository.GetBusinessOfficePrmKeyById(viewModel.BusinessOfficeId);

                        context.GeneralLedgerBusinessOfficeMakerCheckers.Attach(generalLedgerBusinessOfficeMakerChecker);
                        context.Entry(generalLedgerBusinessOfficeMakerChecker).State = EntityState.Added;
                        generalLedgerBusinessOffice.GeneralLedgerBusinessOfficeMakerCheckers.Add(generalLedgerBusinessOfficeMakerChecker);

                        context.GeneralLedgerBusinessOffices.Attach(generalLedgerBusinessOffice);
                        context.Entry(generalLedgerBusinessOffice).State = EntityState.Added;
                    }
                }

                // GeneralLedgerCurrency
                List<GeneralLedgerCurrencyViewModel> generalLedgerCurrencyViewModelList = (List<GeneralLedgerCurrencyViewModel>)HttpContext.Current.Session["GeneralLedgerCurrency"];
                if (generalLedgerCurrencyViewModelList != null)
                {
                    foreach (GeneralLedgerCurrencyViewModel viewModel in generalLedgerCurrencyViewModelList)
                    {
                        generalLedgerDetailRepository.GetGeneralLedgerCurrencyDefaultValues(viewModel, StringLiteralValue.Create, _generalLedgerViewModel.GeneralLedgerPrmKey);

                        GeneralLedgerCurrency generalLedgerCurrency = Mapper.Map<GeneralLedgerCurrency>(viewModel);
                        GeneralLedgerCurrencyMakerChecker generalLedgerCurrencyMakerChecker = Mapper.Map<GeneralLedgerCurrencyMakerChecker>(viewModel);

                        // Set ReferenceKey As PrmKey To Every Object
                        generalLedgerCurrency.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(viewModel.CurrencyId);

                        context.GeneralLedgerCurrencyMakerCheckers.Attach(generalLedgerCurrencyMakerChecker);
                        context.Entry(generalLedgerCurrencyMakerChecker).State = EntityState.Added;
                        generalLedgerCurrency.GeneralLedgerCurrencyMakerCheckers.Add(generalLedgerCurrencyMakerChecker);

                        context.GeneralLedgerCurrencies.Attach(generalLedgerCurrency);
                        context.Entry(generalLedgerCurrency).State = EntityState.Added;
                    }
                }

                // GeneralLedgerTransactionType
                List<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeViewModelList = (List<GeneralLedgerTransactionTypeViewModel>)HttpContext.Current.Session["GeneralLedgerTransactionType"];

                if (generalLedgerTransactionTypeViewModelList != null)
                {
                    foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeViewModelList)
                    {
                        generalLedgerDetailRepository.GetGeneralLedgerTransactionTypeDefaultValues(viewModel, StringLiteralValue.Create, _generalLedgerViewModel.GeneralLedgerPrmKey);

                        viewModel.GeneralLedgerPrmKey = _generalLedgerViewModel.GeneralLedgerPrmKey;

                        viewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(viewModel.TransactionTypeId);

                        GeneralLedgerTransactionType generalLedgerTransactionType = Mapper.Map<GeneralLedgerTransactionType>(viewModel);
                        GeneralLedgerTransactionTypeMakerChecker generalLedgerTransactionTypeMakerChecker = Mapper.Map<GeneralLedgerTransactionTypeMakerChecker>(viewModel);

                        // Set ReferenceKey As PrmKey To Every Object
                        generalLedgerTransactionType.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(viewModel.TransactionTypeId);

                        context.GeneralLedgerTransactionTypeMakerCheckers.Attach(generalLedgerTransactionTypeMakerChecker);
                        context.Entry(generalLedgerTransactionTypeMakerChecker).State = EntityState.Added;
                        generalLedgerTransactionType.GeneralLedgerTransactionTypeMakerCheckers.Add(generalLedgerTransactionTypeMakerChecker);

                        context.GeneralLedgerTransactionTypes.Attach(generalLedgerTransactionType);
                        context.Entry(generalLedgerTransactionType).State = EntityState.Added;
                    }
                }

                // GeneralLedgerCustomerType
                List<GeneralLedgerCustomerTypeViewModel> generalLedgerCustomerTypeViewModelList = (List<GeneralLedgerCustomerTypeViewModel>)HttpContext.Current.Session["GeneralLedgerCustomerType"];

                if (generalLedgerCustomerTypeViewModelList != null)
                {
                    foreach (GeneralLedgerCustomerTypeViewModel viewModel in generalLedgerCustomerTypeViewModelList)
                    {
                        generalLedgerDetailRepository.GetGeneralLedgerCustomerTypeDefaultValues(viewModel, StringLiteralValue.Create, _generalLedgerViewModel.GeneralLedgerPrmKey);

                        viewModel.GeneralLedgerPrmKey = _generalLedgerViewModel.GeneralLedgerPrmKey;

                        viewModel.CustomerTypePrmKey = accountDetailRepository.GetCustomerTypePrmKeyById(viewModel.CustomerTypeId);

                        GeneralLedgerCustomerType generalLedgerCustomerType = Mapper.Map<GeneralLedgerCustomerType>(viewModel);
                        GeneralLedgerCustomerTypeMakerChecker generalLedgerCustomerTypeMakerChecker = Mapper.Map<GeneralLedgerCustomerTypeMakerChecker>(viewModel);

                        // Set ReferenceKey As PrmKey To Every Object
                        generalLedgerCustomerType.CustomerTypePrmKey = accountDetailRepository.GetCustomerTypePrmKeyById(viewModel.CustomerTypeId);

                        context.GeneralLedgerCustomerTypeMakerCheckers.Attach(generalLedgerCustomerTypeMakerChecker);
                        context.Entry(generalLedgerCustomerTypeMakerChecker).State = EntityState.Added;
                        generalLedgerCustomerType.GeneralLedgerCustomerTypeMakerCheckers.Add(generalLedgerCustomerTypeMakerChecker);

                        context.GeneralLedgerCustomerTypes.Attach(generalLedgerCustomerType);
                        context.Entry(generalLedgerCustomerType).State = EntityState.Added;
                    }
                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> VerifyRejectDelete(GeneralLedgerViewModel _generalLedgerViewModel, string _entryType)
        {
            try
            {
                string entriesType;
                if (_entryType == StringLiteralValue.Verify || _entryType == StringLiteralValue.Reject)
                    entriesType = StringLiteralValue.Unverified;
                else
                    entriesType = StringLiteralValue.Reject;
                bool result = true;
                if (result)
                {
                    // Check Entry Existance In Modification Table Or Main Table
                    if (_generalLedgerViewModel.GeneralLedgerModificationPrmKey == 0)
                    {
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerData(_generalLedgerViewModel, _entryType);
                    }
                    else
                    {
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerModificationData(_generalLedgerViewModel, _entryType);
                    }
                }

                // GeneralLedgerBusinessOffice
                if (result)
                {
                    IEnumerable<GeneralLedgerBusinessOfficeViewModel> GeneralLedgerBusinessOfficeList = await generalLedgerDetailRepository.GetGeneralLedgerBusinessOfficeEntries(_generalLedgerViewModel.GeneralLedgerPrmKey, entriesType);

                    foreach (GeneralLedgerBusinessOfficeViewModel viewModel in GeneralLedgerBusinessOfficeList)
                    {
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerBusinessOfficeData(viewModel, _entryType);
                    }
                }

                // GeneralLedgerCurrency
                if (result)
                {
                    IEnumerable<GeneralLedgerCurrencyViewModel> GeneralLedgerCurrencyList = await generalLedgerDetailRepository.GetGeneralLedgerCurrencyEntries(_generalLedgerViewModel.GeneralLedgerPrmKey, entriesType);

                    foreach (GeneralLedgerCurrencyViewModel viewModel in GeneralLedgerCurrencyList)
                    {
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerCurrencyData(viewModel, _entryType);
                    }
                }

                // GeneralLedgerTransactionType
                if (result)
                {
                    IEnumerable<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeViewModelList = await generalLedgerDetailRepository.GetGeneralLedgerTransactionTypeEntries(_generalLedgerViewModel.GeneralLedgerPrmKey, entriesType);

                    foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeViewModelList)
                    {
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerTransactionTypeData(viewModel, _entryType);
                    }
                }

                // GeneralLedgerCustomerType
                if (result)
                {
                    IEnumerable<GeneralLedgerCustomerTypeViewModel> generalLedgerCustomerTypeViewModelList = await generalLedgerDetailRepository.GetGeneralLedgerCustomerTypeEntries(_generalLedgerViewModel.GeneralLedgerPrmKey, entriesType);

                    foreach (GeneralLedgerCustomerTypeViewModel viewModel in generalLedgerCustomerTypeViewModelList)
                    {
                        result = generalLedgerDbContextRepository.AttachGeneralLedgerCustomerTypeData(viewModel, _entryType);
                    }
                }

                if (result)
                    result = await generalLedgerDbContextRepository.SaveData();

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
        public async Task<bool> Save(GeneralLedgerViewModel _generalLedgerViewModel)
        {
            try
            {
                bool result = true;

                // GeneralLedger
                if (result)
                    generalLedgerDbContextRepository.AttachGeneralLedgerData(_generalLedgerViewModel, StringLiteralValue.Create);

                // GeneralLedgerBusinessOffice
                if (result)
                {
                    List<GeneralLedgerBusinessOfficeViewModel> generalLedgerBusinessOfficeViewModelList = (List<GeneralLedgerBusinessOfficeViewModel>)HttpContext.Current.Session["GeneralLedgerBusinessOffice"];
                    if (generalLedgerBusinessOfficeViewModelList != null)
                    {
                        foreach (GeneralLedgerBusinessOfficeViewModel viewModel in generalLedgerBusinessOfficeViewModelList)
                        {
                            generalLedgerDbContextRepository.AttachGeneralLedgerBusinessOfficeData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // GeneralLedgerCurrency
                if (result)
                {
                    List<GeneralLedgerCurrencyViewModel> generalLedgerCurrencyViewModelList = (List<GeneralLedgerCurrencyViewModel>)HttpContext.Current.Session["GeneralLedgerCurrency"];
                    if (generalLedgerCurrencyViewModelList != null)
                    {
                        foreach (GeneralLedgerCurrencyViewModel viewModel in generalLedgerCurrencyViewModelList)
                        {
                            generalLedgerDbContextRepository.AttachGeneralLedgerCurrencyData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // GeneralLedgerTransactionType
                if (result)
                {
                    List<GeneralLedgerTransactionTypeViewModel> generalLedgerTransactionTypeViewModelList = (List<GeneralLedgerTransactionTypeViewModel>)HttpContext.Current.Session["GeneralLedgerTransactionType"];
                    if (generalLedgerTransactionTypeViewModelList != null)
                    {
                        foreach (GeneralLedgerTransactionTypeViewModel viewModel in generalLedgerTransactionTypeViewModelList)
                        {
                            generalLedgerDbContextRepository.AttachGeneralLedgerTransactionTypeData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                // GeneralLedgerCustomerType
                if (result)
                {
                    List<GeneralLedgerCustomerTypeViewModel> generalLedgerCustomerTypeViewModelList = (List<GeneralLedgerCustomerTypeViewModel>)HttpContext.Current.Session["GeneralLedgerCustomerType"];
                    if (generalLedgerCustomerTypeViewModelList != null)
                    {
                        foreach (GeneralLedgerCustomerTypeViewModel viewModel in generalLedgerCustomerTypeViewModelList)
                        {
                            generalLedgerDbContextRepository.AttachGeneralLedgerCustomerTypeData(viewModel, StringLiteralValue.Create);
                        }
                    }
                }

                if (result)
                    result = await generalLedgerDbContextRepository.SaveData();

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