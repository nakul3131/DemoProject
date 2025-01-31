using AutoMapper;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Domain.Entities.PersonInformation.Parameter;
using DemoProject.Services.Abstract.PersonInformation.Parameter;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.Concrete.PersonInformation.Parameter
{
    public class EFPersonInformationParameterRepository : IPersonInformationParameterRepository
    {
        private readonly EFDbContext context;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;

        public EFPersonInformationParameterRepository(RepositoryConnection _connection, IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository, IConfigurationDetailRepository _configurationDetailRepository)
        {
            context = _connection.EFDbContext;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
        }

        public async Task<bool> Amend(PersonInformationParameterViewModel _personInformationParameterViewModel)
        {
            try
            {
                // Set Default Value
                personInformationParameterDetailRepository.GetPersonInformationParameterAllDefaultValues(_personInformationParameterViewModel, StringLiteralValue.Amend);

                // Mapping 
                // PersonInformationParameter
                PersonInformationParameter personInformationParameter = Mapper.Map<PersonInformationParameter>(_personInformationParameterViewModel);
                PersonInformationParameterMakerChecker personInformationParameterMakerChecker = Mapper.Map<PersonInformationParameterMakerChecker>(_personInformationParameterViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                // PersonInformationParameter
                context.PersonInformationParameterMakerCheckers.Attach(personInformationParameterMakerChecker);
                context.Entry(personInformationParameterMakerChecker).State = EntityState.Added;
                personInformationParameter.PersonInformationParameterMakerCheckers.Add(personInformationParameterMakerChecker);

                context.PersonInformationParameters.Attach(personInformationParameter);
                context.Entry(personInformationParameter).State = EntityState.Modified;

                //Amend Old PersonInformationParameterDocumentType
                IEnumerable<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelListForAmend = await personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeEntries(StringLiteralValue.Reject);
                if (personInformationParameterDocumentTypeViewModelListForAmend != null)
                {
                    foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelListForAmend)
                    {
                        // Set Default Value
                        personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Amend, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                        // Mapping
                        PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerCheckerForAmend = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                        // PersonInformationParameterDocumentType
                        context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerCheckerForAmend);
                        context.Entry(personInformationParameterDocumentTypeMakerCheckerForAmend).State = EntityState.Added;
                    }
                }

                // Amend Old PersonInformationParameterNoticeType
                IEnumerable<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelListForAmend = await personInformationParameterDetailRepository.GetPersonInformationParameterNoticeTypeEntries(StringLiteralValue.Reject);

                if (personInformationParameterNoticeTypeViewModelListForAmend != null)
                {
                    foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelListForAmend)
                    {
                        // Set Default Value
                        personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Amend, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                        // Mapping
                        PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerCheckerForAmend = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                        // PersonInformationParameterNoticeType
                        context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerCheckerForAmend);
                        context.Entry(personInformationParameterNoticeTypeMakerCheckerForAmend).State = EntityState.Added;
                    }
                }

                // INSERT New Updated/Amended Record
                if (_personInformationParameterViewModel.KYCDocumentUpload != "D")
                {
                    // Get PersonInformationParameterDocumentType From Session Object
                    List<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelList = new List<PersonInformationParameterDocumentTypeViewModel>();
                    personInformationParameterDocumentTypeViewModelList = (List<PersonInformationParameterDocumentTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterDocumentType"];

                    if (personInformationParameterDocumentTypeViewModelList != null)
                    {
                        foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelList)
                        {
                            personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Create, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                            // Mapping
                            // PersonInformationParameterDocumentType
                            PersonInformationParameterDocumentType personInformationParameterDocumentType = Mapper.Map<PersonInformationParameterDocumentType>(viewModel);
                            PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerChecker = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                            // Save Data In Appropriate Tables By Entity Framework Methods
                            // PersonInformationParameterDocumentType
                            context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerChecker);
                            context.Entry(personInformationParameterDocumentTypeMakerChecker).State = EntityState.Added;
                            personInformationParameterDocumentType.PersonInformationParameterDocumentTypeMakerCheckers.Add(personInformationParameterDocumentTypeMakerChecker);

                            context.PersonInformationParameterDocumentTypes.Attach(personInformationParameterDocumentType);
                            context.Entry(personInformationParameterDocumentType).State = EntityState.Added;
                        }

                    }
                }

                // SMS ALERT
                if (_personInformationParameterViewModel.EnableSMSAlert)
                {
                    // Get PersonInformationParameterNoticeType From Session Object
                    List<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelList = new List<PersonInformationParameterNoticeTypeViewModel>();
                    personInformationParameterNoticeTypeViewModelList = (List<PersonInformationParameterNoticeTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterNoticeType"];
                    foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelList)
                    {
                        // Set Default Value
                        personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Create, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                        // Mapping
                        // PersonInformationParameterNoticeType
                        PersonInformationParameterNoticeType personInformationParameterNoticeType = Mapper.Map<PersonInformationParameterNoticeType>(viewModel);
                        PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerChecker = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                        // Save Data In Appropriate Tables By Entity Framework Methods
                        // PersonInformationParameterNoticeType
                        context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerChecker);
                        context.Entry(personInformationParameterNoticeTypeMakerChecker).State = EntityState.Added;
                        personInformationParameterNoticeType.PersonInformationParameterNoticeTypeMakerCheckers.Add(personInformationParameterNoticeTypeMakerChecker);

                        context.PersonInformationParameterNoticeTypes.Attach(personInformationParameterNoticeType);
                        context.Entry(personInformationParameterNoticeType).State = EntityState.Added;
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

        public async Task<bool> Delete(PersonInformationParameterViewModel _personInformationParameterViewModel)
        {
            try
            {
                // Set Default Value
                personInformationParameterDetailRepository.GetPersonInformationParameterAllDefaultValues(_personInformationParameterViewModel, StringLiteralValue.Delete);

                // Mapping
                PersonInformationParameterMakerChecker personInformationParameterMakerChecker = Mapper.Map<PersonInformationParameterMakerChecker>(_personInformationParameterViewModel);

                // PersonInformationParameter
                context.PersonInformationParameterMakerCheckers.Attach(personInformationParameterMakerChecker);
                context.Entry(personInformationParameterMakerChecker).State = EntityState.Added;

                // PersonInformationParameterDocumentType
                if (_personInformationParameterViewModel.KYCDocumentUpload != StringLiteralValue.Disable)
                {
                    IEnumerable<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelListForDelete = await personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeEntries(StringLiteralValue.Reject);

                    if (personInformationParameterDocumentTypeViewModelListForDelete != null)
                    {
                        foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelListForDelete)
                        {
                            // Set Default Value
                            personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Delete, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                            // Mapping
                            PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerChecker = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                            // PersonInformationParameterDocumentType
                            context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerChecker);
                            context.Entry(personInformationParameterDocumentTypeMakerChecker).State = EntityState.Added;
                        }
                    }
                }

                // PersonInformationParameterNoticeType
                if (_personInformationParameterViewModel.EnableSMSAlert)
                {
                    IEnumerable<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelListForDelete = await personInformationParameterDetailRepository.GetPersonInformationParameterNoticeTypeEntries(StringLiteralValue.Reject);

                    if (personInformationParameterNoticeTypeViewModelListForDelete != null)
                    {
                        foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelListForDelete)
                        {
                            // Set Default Value
                            personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Delete, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                            // Mapping
                            PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerChecker = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                            // PersonInformationParameterNoticeType
                            context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerChecker);
                            context.Entry(personInformationParameterNoticeTypeMakerChecker).State = EntityState.Added;
                        }
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

        public async Task<bool> GetSessionValues(string _entryType)
        {
            try
            {
                if (personInformationParameterDetailRepository.IsRequiredKYCDocumentUpload())
                {
                    HttpContext.Current.Session["PersonInformationParameterDocumentType"] = await personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeEntries(_entryType);

                    if (HttpContext.Current.Session["PersonInformationParameterDocumentType"] == null)
                        return false;
                }

                //if (personInformationParameterDetailRepository.IsEnableSMSAlert())
                //{
                    HttpContext.Current.Session["PersonInformationParameterNoticeType"] = await personInformationParameterDetailRepository.GetPersonInformationParameterNoticeTypeEntries(_entryType);

                    if (HttpContext.Current.Session["PersonInformationParameterNoticeType"] == null)
                        return false;
                //}

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<PersonInformationParameterViewModel> GetPersonInformationParameterEntry(string _entryType)
        {
            try
            {
                return await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(_entryType);
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonInformationParameterViewModel>> GetPersonInformationParameterIndex()
        {
            try
            {
                return await context.Database.SqlQuery<PersonInformationParameterViewModel>("SELECT * FROM dbo.GetPersonInformationParameterEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> IsAnyAuthorizationPending()
        {
            // check waiting for response and rejected entries count
            int count = await context.PersonInformationParameters
                        .Where(p => p.EntryStatus == StringLiteralValue.Amend || p.EntryStatus == StringLiteralValue.Create || p.EntryStatus == StringLiteralValue.Reject)
                        .Select(p => p.PrmKey).CountAsync();

            // check waiting for response and rejected entries count
            int dCount = await context.PersonInformationParameterDocumentTypes
                        .Where(pd => pd.EntryStatus == StringLiteralValue.Create || pd.EntryStatus == StringLiteralValue.Reject)
                        .Select(pd => pd.PrmKey).CountAsync();

            // check waiting for response and rejected entries count
            int nCount = await context.PersonInformationParameterNoticeTypes
                        .Where(pn => pn.EntryStatus == StringLiteralValue.Create || pn.EntryStatus == StringLiteralValue.Reject)
                        .Select(pn => pn.PrmKey).CountAsync();

            if (count > 0 || dCount > 0 || nCount > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Reject(PersonInformationParameterViewModel _personInformationParameterViewModel)
        {
            try
            {
                // Set Default Value
                personInformationParameterDetailRepository.GetPersonInformationParameterAllDefaultValues(_personInformationParameterViewModel, StringLiteralValue.Reject);

                // Mapping
                PersonInformationParameterMakerChecker personInformationParameterMakerChecker = Mapper.Map<PersonInformationParameterMakerChecker>(_personInformationParameterViewModel);

                // PersonInformationParameter
                context.PersonInformationParameterMakerCheckers.Attach(personInformationParameterMakerChecker);
                context.Entry(personInformationParameterMakerChecker).State = EntityState.Added;

                if (_personInformationParameterViewModel.KYCDocumentUpload != StringLiteralValue.Disable)
                {
                    // PersonInformationParameterDocumentType
                    List<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelList = new List<PersonInformationParameterDocumentTypeViewModel>();
                    personInformationParameterDocumentTypeViewModelList = (List<PersonInformationParameterDocumentTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterDocumentType"];

                    if (personInformationParameterDocumentTypeViewModelList != null)
                    {
                        foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelList)
                        {
                            // Set Default Value
                            personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Reject, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                            // Mapping
                            PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerChecker = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                            // PersonInformationParameterDocumentType
                            context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerChecker);
                            context.Entry(personInformationParameterDocumentTypeMakerChecker).State = EntityState.Added;
                        }
                    }
                }

                if (_personInformationParameterViewModel.EnableSMSAlert)
                {
                    // PersonInformationParameterNoticeType
                    List<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelList = new List<PersonInformationParameterNoticeTypeViewModel>();
                    personInformationParameterNoticeTypeViewModelList = (List<PersonInformationParameterNoticeTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterNoticeType"];

                    if (personInformationParameterNoticeTypeViewModelList != null)
                    {
                        foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelList)
                        {
                            // Set Default Value
                            personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Reject, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                            // Mapping
                            PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerChecker = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                            // PersonInformationParameterNoticeType
                            context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerChecker);
                            context.Entry(personInformationParameterNoticeTypeMakerChecker).State = EntityState.Added;
                        }
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

        public async Task<bool> Save(PersonInformationParameterViewModel _personInformationParameterViewModel)
        {
            try
            {
                // Set Default Value        
                personInformationParameterDetailRepository.GetPersonInformationParameterAllDefaultValues(_personInformationParameterViewModel, StringLiteralValue.Create);

                // Mapping
                PersonInformationParameter personInformationParameter = Mapper.Map<PersonInformationParameter>(_personInformationParameterViewModel);
                PersonInformationParameterMakerChecker personInformationParameterMakerChecker = Mapper.Map<PersonInformationParameterMakerChecker>(_personInformationParameterViewModel);

                // PersonInformationParameter
                context.PersonInformationParameterMakerCheckers.Attach(personInformationParameterMakerChecker);
                context.Entry(personInformationParameterMakerChecker).State = EntityState.Added;
                personInformationParameter.PersonInformationParameterMakerCheckers.Add(personInformationParameterMakerChecker);

                context.PersonInformationParameters.Attach(personInformationParameter);
                context.Entry(personInformationParameter).State = EntityState.Added;

                if (_personInformationParameterViewModel.KYCDocumentUpload != StringLiteralValue.Disable)
                {
                    List<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelList = (List<PersonInformationParameterDocumentTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterDocumentType"];

                    if (personInformationParameterDocumentTypeViewModelList != null)
                    {
                        foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelList)
                        {
                            // Set Default Value
                            personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Create, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                            // Mapping
                            // PersonInformationParameterDocumentType
                            PersonInformationParameterDocumentType personInformationParameterDocumentType = Mapper.Map<PersonInformationParameterDocumentType>(viewModel);
                            PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerChecker = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                            // PersonInformationParameterDocumentType
                            context.PersonInformationParameterDocumentTypes.Attach(personInformationParameterDocumentType);
                            context.Entry(personInformationParameterDocumentType).State = EntityState.Added;

                            context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerChecker);
                            context.Entry(personInformationParameterDocumentTypeMakerChecker).State = EntityState.Added;
                            personInformationParameterDocumentType.PersonInformationParameterDocumentTypeMakerCheckers.Add(personInformationParameterDocumentTypeMakerChecker);
                        }
                    }
                }

                if (_personInformationParameterViewModel.EnableSMSAlert)
                {
                    // Get PersonInformationParameterNoticeType From Session Object
                    List<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelList = (List<PersonInformationParameterNoticeTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterNoticeType"];

                    if (personInformationParameterNoticeTypeViewModelList != null)
                    {
                        foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelList)
                        {
                            // Set Default Value
                            personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Create, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                            // Mapping
                            // PersonInformationParameterNoticeType
                            PersonInformationParameterNoticeType personInformationParameterNoticeType = Mapper.Map<PersonInformationParameterNoticeType>(viewModel);
                            PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerChecker = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                            // PersonInformationParameterNoticeType
                            context.PersonInformationParameterNoticeTypes.Attach(personInformationParameterNoticeType);
                            context.Entry(personInformationParameterNoticeType).State = EntityState.Added;

                            context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerChecker);
                            context.Entry(personInformationParameterNoticeTypeMakerChecker).State = EntityState.Added;
                            personInformationParameterNoticeType.PersonInformationParameterNoticeTypeMakerCheckers.Add(personInformationParameterNoticeTypeMakerChecker);
                        }
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

        public async Task<bool> Verify(PersonInformationParameterViewModel _personInformationParameterViewModel)
        {
            try
            {
                // Modify Old Record      
                //PersonInformationParameterViewModel personInformationParameterViewModelOfOldEntry = await personInformationParameterDetailRepository.GetPersonInformationParameterEntry(StringLiteralValue.Verify);
                PersonInformationParameterViewModel personInformationParameterViewModelOfOldEntry = await GetActiveEntry();

                if(personInformationParameterViewModelOfOldEntry != null)
                {
                    if (personInformationParameterViewModelOfOldEntry.PrmKey > 0)
                    {
                        // Set Default Value
                        //personInformationParameterDetailRepository.GetPersonInformationParameterAllDefaultValues(personInformationParameterViewModelOfOldEntry, StringLiteralValue.Modify);
                        configurationDetailRepository.SetDefaultValues(personInformationParameterViewModelOfOldEntry, StringLiteralValue.Modify);

                        // Mapping
                        PersonInformationParameterMakerChecker personInformationParameterMakerCheckerForModify = Mapper.Map<PersonInformationParameterMakerChecker>(personInformationParameterViewModelOfOldEntry);

                        // PersonInformationParameter
                        context.PersonInformationParameterMakerCheckers.Attach(personInformationParameterMakerCheckerForModify);
                        context.Entry(personInformationParameterMakerCheckerForModify).State = EntityState.Added;

                        // Modify Old PersonInformationParameterDocumentType
                        IEnumerable<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelListForModify = await personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeEntries(StringLiteralValue.Verify);

                        if (personInformationParameterDocumentTypeViewModelListForModify != null)
                        {
                            foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelListForModify)
                            {
                                // Set Default Value
                                personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Modify, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                                // Mapping
                                PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerCheckerForModify = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                                // PersonInformationParameterDocumentType
                                context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerCheckerForModify);
                                context.Entry(personInformationParameterDocumentTypeMakerCheckerForModify).State = EntityState.Added;
                            }
                        }

                        // Modify Old PersonInformationParameterNoticeType
                        IEnumerable<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelListForModify = await personInformationParameterDetailRepository.GetPersonInformationParameterNoticeTypeEntries(StringLiteralValue.Verify);

                        if (personInformationParameterNoticeTypeViewModelListForModify != null)
                        {
                            foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelListForModify)
                            {
                                // Set Default Value
                                personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Modify, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                                // Mapping
                                PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerCheckerForModify = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                                // PersonInformationParameterNoticeType
                                context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerCheckerForModify);
                                context.Entry(personInformationParameterNoticeTypeMakerCheckerForModify).State = EntityState.Added;
                            }
                        }
                    }
                }

                // Verify Record
                // Set Default Value
                //personInformationParameterDetailRepository.GetPersonInformationParameterAllDefaultValues(_personInformationParameterViewModel, StringLiteralValue.Verify);
                configurationDetailRepository.SetDefaultValues(_personInformationParameterViewModel, StringLiteralValue.Verify);

                // Mapping
                PersonInformationParameterMakerChecker personInformationParameterMakerChecker = Mapper.Map<PersonInformationParameterMakerChecker>(_personInformationParameterViewModel);

                // PersonInformationParameter
                context.PersonInformationParameterMakerCheckers.Attach(personInformationParameterMakerChecker);
                context.Entry(personInformationParameterMakerChecker).State = EntityState.Added;

                // PersonInformationParameterDocumentType
                List<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelList = (List<PersonInformationParameterDocumentTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterDocumentType"];

                if (personInformationParameterDocumentTypeViewModelList != null)
                {
                    foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelList)
                    {
                        // Set Default Value
                        personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Verify, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                        // Mapping
                        PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerChecker = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                        // PersonInformationParameterDocumentType
                        context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerChecker);
                        context.Entry(personInformationParameterDocumentTypeMakerChecker).State = EntityState.Added;
                    }
                }

                // PersonInformationParameterNoticeType
                List<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelList = (List<PersonInformationParameterNoticeTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterNoticeType"];

                if (personInformationParameterNoticeTypeViewModelList != null)
                {
                    foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelList)
                    {
                        // Set Default Value
                        personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Verify, _personInformationParameterViewModel.PersonInformationParameterPrmKey);

                        // Mapping
                        PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerChecker = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                        // PersonInformationParameterNoticeType
                        context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerChecker);
                        context.Entry(personInformationParameterNoticeTypeMakerChecker).State = EntityState.Added;
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

        public async Task<PersonInformationParameterViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<PersonInformationParameterViewModel>("SELECT * FROM dbo.GetPersonInformationParameterEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }
    }
}