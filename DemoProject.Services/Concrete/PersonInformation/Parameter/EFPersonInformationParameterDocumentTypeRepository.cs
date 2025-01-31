using AutoMapper;
using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Domain.Entities.PersonInformation.Parameter;
using DemoProject.Services.Abstract.PersonInformation.Parameter;

namespace DemoProject.Services.Concrete.PersonInformation.Parameter
{
    public class EFPersonInformationParameterDocumentTypeRepository : IPersonInformationParameterDocumentTypeRepository
    {
        private readonly EFDbContext context;

        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;

        public EFPersonInformationParameterDocumentTypeRepository(RepositoryConnection _connection, IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository)
        {
            context = _connection.EFDbContext;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
        }

        public async Task<bool> Amend(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel)
        {
            try
            {
                // Amend Old PersonInformationParameterDocumentType
                IEnumerable<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelListForAmend = await personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeEntries(StringLiteralValue.Reject);
                foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelListForAmend)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Amend, _personInformationParameterDocumentTypeViewModel.PersonInformationParameterPrmKey);

                    // Mapping
                    PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerCheckerForAmend = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                    // PersonInformationParameterDocumentType
                    context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerCheckerForAmend);
                    context.Entry(personInformationParameterDocumentTypeMakerCheckerForAmend).State = EntityState.Added;
                }

                // Get PersonInformationParameterDocumentType From Session Object
                List<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelList = (List<PersonInformationParameterDocumentTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterDocumentType"];
                foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelList)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Create, _personInformationParameterDocumentTypeViewModel.PersonInformationParameterPrmKey);

                    // Mapping
                    // PersonInformationParameterDocumentType
                    PersonInformationParameterDocumentType personInformationParameterDocumentType = Mapper.Map<PersonInformationParameterDocumentType>(viewModel);
                    PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerChecker = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                    // Save Data In Appropriate Tables By Entity Framework Methods
                    // PersonInformationParameterDocumentType
                    context.PersonInformationParameterDocumentTypes.Attach(personInformationParameterDocumentType);
                    context.Entry(personInformationParameterDocumentType).State = EntityState.Added;

                    context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerChecker);
                    context.Entry(personInformationParameterDocumentTypeMakerChecker).State = EntityState.Added;
                    personInformationParameterDocumentType.PersonInformationParameterDocumentTypeMakerCheckers.Add(personInformationParameterDocumentTypeMakerChecker);
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

        public async Task<bool> Delete(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel)
        {
            try
            {
                IEnumerable<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelListForDelete = await personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeEntries(StringLiteralValue.Reject);
                foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelListForDelete)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Delete, _personInformationParameterDocumentTypeViewModel.PersonInformationParameterPrmKey);

                    // Mapping
                    PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerChecker = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                    // PersonInformationParameterDocumentType
                    context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerChecker);
                    context.Entry(personInformationParameterDocumentTypeMakerChecker).State = EntityState.Added;
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
                HttpContext.Current.Session["PersonInformationParameterDocumentType"] = await personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeEntries(_entryType);
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<PersonInformationParameterDocumentTypeViewModel> GetPersonInformationParameterDocumentTypeEntry(string _entryType)
        {
            try
            {
                PersonInformationParameterDocumentTypeViewModel personInformationParameterViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeEntry(_entryType);

                return personInformationParameterViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonInformationParameterDocumentTypeViewModel>> GetPersonInformationParameterDocumentTypeIndex()
        {
            try
            {
                return await context.Database.SqlQuery<PersonInformationParameterDocumentTypeViewModel>("SELECT * FROM dbo.GetPersonInformationParameterDocumentTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
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
            int count = await context.PersonInformationParameterDocumentTypes
                        .Where(u => u.EntryStatus == StringLiteralValue.Amend || u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject)
                        .Select(u => u.PrmKey).CountAsync();

            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Reject(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel)
        {
            try
            {
                List<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelList = (List<PersonInformationParameterDocumentTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterDocumentType"];
                foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelList)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Reject, _personInformationParameterDocumentTypeViewModel.PersonInformationParameterPrmKey);

                    // Mapping
                    PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerChecker = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                    // PersonInformationParameterDocumentType
                    context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerChecker);
                    context.Entry(personInformationParameterDocumentTypeMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel)
        {
            try
            {
                // Get PersonInformationParameterDocumentType From Session Object
                List<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelList = (List<PersonInformationParameterDocumentTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterDocumentType"];
                foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelList)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Create, _personInformationParameterDocumentTypeViewModel.PersonInformationParameterPrmKey);

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

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(PersonInformationParameterDocumentTypeViewModel _personInformationParameterDocumentTypeViewModel)
        {
            try
            {
                // Modify Old PersonInformationParameterDocumentType
                IEnumerable<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelListForModify = await personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeEntries(StringLiteralValue.Verify);
                foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelListForModify)
                {
                    if (viewModel.PrmKey > 0)
                    {
                        // Set Default Value
                        personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Modify, _personInformationParameterDocumentTypeViewModel.PersonInformationParameterPrmKey);
                        // Mapping
                        PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerCheckerForModify = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                        // PersonInformationParameterDocumentType
                        context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerCheckerForModify);
                        context.Entry(personInformationParameterDocumentTypeMakerCheckerForModify).State = EntityState.Added;
                    }
                }

                // Verify Record
                // Set Default Value
                List<PersonInformationParameterDocumentTypeViewModel> personInformationParameterDocumentTypeViewModelList = (List<PersonInformationParameterDocumentTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterDocumentType"];
                foreach (PersonInformationParameterDocumentTypeViewModel viewModel in personInformationParameterDocumentTypeViewModelList)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterDocumentTypeDefaultValues(viewModel, StringLiteralValue.Verify, _personInformationParameterDocumentTypeViewModel.PersonInformationParameterPrmKey);

                    // Mapping
                    PersonInformationParameterDocumentTypeMakerChecker personInformationParameterDocumentTypeMakerChecker = Mapper.Map<PersonInformationParameterDocumentTypeMakerChecker>(viewModel);

                    // PersonInformationParameterDocumentType
                    context.PersonInformationParameterDocumentTypeMakerCheckers.Attach(personInformationParameterDocumentTypeMakerChecker);
                    context.Entry(personInformationParameterDocumentTypeMakerChecker).State = EntityState.Added;
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
    }
}