using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.ViewModel.PersonInformation.Parameter;
using DemoProject.Domain.Entities.PersonInformation.Parameter;
using DemoProject.Services.Abstract.PersonInformation.Parameter;

namespace DemoProject.Services.Concrete.PersonInformation.Parameter
{
    public class EFPersonInformationParameterNoticeTypeRepository : IPersonInformationParameterNoticeTypeRepository
    {
        private readonly EFDbContext context;

        private readonly IPersonInformationParameterDetailRepository personInformationParameterDetailRepository;

        public EFPersonInformationParameterNoticeTypeRepository(RepositoryConnection _connection, IPersonInformationParameterDetailRepository _personInformationParameterDetailRepository)
        {
            context = _connection.EFDbContext;
            personInformationParameterDetailRepository = _personInformationParameterDetailRepository;
        }

        public async Task<bool> Amend(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel)
        {
            try
            {
                // Amend Old PersonInformationParameterNoticeType
                IEnumerable<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelListForAmend = await personInformationParameterDetailRepository.GetPersonInformationParameterNoticeTypeEntries(StringLiteralValue.Reject);
                foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelListForAmend)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Amend, _personInformationParameterNoticeTypeViewModel.PersonInformationParameterPrmKey);
                  
                   // Mapping
                    PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerCheckerForAmend = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                    // PersonInformationParameterNoticeType
                    context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerCheckerForAmend);
                    context.Entry(personInformationParameterNoticeTypeMakerCheckerForAmend).State = EntityState.Added;
                }

                // Get PersonInformationParameterNoticeType From Session Object
                List<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelList = (List<PersonInformationParameterNoticeTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterNoticeType"];
                foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelList)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Create, _personInformationParameterNoticeTypeViewModel.PersonInformationParameterPrmKey);

                    // Mapping
                    // PersonInformationParameterNoticeType
                    PersonInformationParameterNoticeType personInformationParameterNoticeType = Mapper.Map<PersonInformationParameterNoticeType>(viewModel);
                    PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerChecker = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                    // Save Data In Appropriate Tables By Entity Framework Methods
                    // PersonInformationParameterNoticeType
                    context.PersonInformationParameterNoticeTypes.Attach(personInformationParameterNoticeType);
                    context.Entry(personInformationParameterNoticeType).State = EntityState.Added;

                    context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerChecker);
                    context.Entry(personInformationParameterNoticeTypeMakerChecker).State = EntityState.Added;
                    personInformationParameterNoticeType.PersonInformationParameterNoticeTypeMakerCheckers.Add(personInformationParameterNoticeTypeMakerChecker);
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

        public async Task<bool> Delete(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel)
        {
            try
            {
                IEnumerable<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelListForDelete = await personInformationParameterDetailRepository.GetPersonInformationParameterNoticeTypeEntries(StringLiteralValue.Reject);
                foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelListForDelete)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Delete, _personInformationParameterNoticeTypeViewModel.PersonInformationParameterPrmKey);

                    // Mapping
                    PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerChecker = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                    // PersonInformationParameterNoticeType
                    context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerChecker);
                    context.Entry(personInformationParameterNoticeTypeMakerChecker).State = EntityState.Added;
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
                HttpContext.Current.Session["PersonInformationParameterNoticeType"] = await personInformationParameterDetailRepository.GetPersonInformationParameterNoticeTypeEntries(_entryType);
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<PersonInformationParameterNoticeTypeViewModel> GetPersonInformationParameterNoticeTypeEntry(string _entryType)
        {
            try
            {
                PersonInformationParameterNoticeTypeViewModel personInformationParameterNoticeTypeViewModel = await personInformationParameterDetailRepository.GetPersonInformationParameterNoticeTypeEntry(_entryType);

                return personInformationParameterNoticeTypeViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonInformationParameterNoticeTypeViewModel>> GetPersonInformationParameterNoticeTypeIndex()
        {
            try
            {
                return await context.Database.SqlQuery<PersonInformationParameterNoticeTypeViewModel>("SELECT * FROM dbo.GetPersonInformationParameterNoticeTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
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
            int count = await context.PersonInformationParameterNoticeTypes
                        .Where(u => u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject)
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

        public async Task<bool> Reject(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel)
        {
            try
            {
                List<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelList = (List<PersonInformationParameterNoticeTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterNoticeType"];
                foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelList)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Reject, _personInformationParameterNoticeTypeViewModel.PersonInformationParameterPrmKey);

                    // Mapping
                    PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerChecker = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                    // PersonInformationParameterNoticeType
                    context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerChecker);
                    context.Entry(personInformationParameterNoticeTypeMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel)
        {
            try
            {
                // Get PersonInformationParameterNoticeType From Session Object
                List<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelList = (List<PersonInformationParameterNoticeTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterNoticeType"];

                foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelList)
                {
                    personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Create, _personInformationParameterNoticeTypeViewModel.PersonInformationParameterPrmKey);

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

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(PersonInformationParameterNoticeTypeViewModel _personInformationParameterNoticeTypeViewModel)
        {
            try
            {
                // Modify Old PersonInformationParameterNoticeType
                IEnumerable<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelListForModify = await personInformationParameterDetailRepository.GetPersonInformationParameterNoticeTypeEntries(StringLiteralValue.Verify);
                foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelListForModify)
                {
                    if (viewModel.PrmKey > 0)
                    {
                        // Set Default Value
                        personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Modify, _personInformationParameterNoticeTypeViewModel.PersonInformationParameterPrmKey);

                        // Mapping
                        PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerCheckerForModify = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                        // PersonInformationParameterNoticeType
                        context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerCheckerForModify);
                        context.Entry(personInformationParameterNoticeTypeMakerCheckerForModify).State = EntityState.Added;
                    }
                }

                // Verify Record
                // Set Default Value
                List<PersonInformationParameterNoticeTypeViewModel> personInformationParameterNoticeTypeViewModelList = (List<PersonInformationParameterNoticeTypeViewModel>)HttpContext.Current.Session["PersonInformationParameterNoticeType"];
                foreach (PersonInformationParameterNoticeTypeViewModel viewModel in personInformationParameterNoticeTypeViewModelList)
                {
                    // Set Default Value
                    personInformationParameterDetailRepository.GetPersonInformationParameterNoticeDefaultValues(viewModel, StringLiteralValue.Verify, _personInformationParameterNoticeTypeViewModel.PersonInformationParameterPrmKey);

                    // Mapping
                    PersonInformationParameterNoticeTypeMakerChecker personInformationParameterNoticeTypeMakerChecker = Mapper.Map<PersonInformationParameterNoticeTypeMakerChecker>(viewModel);

                    // PersonInformationParameterNoticeType
                    context.PersonInformationParameterNoticeTypeMakerCheckers.Attach(personInformationParameterNoticeTypeMakerChecker);
                    context.Entry(personInformationParameterNoticeTypeMakerChecker).State = EntityState.Added;
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