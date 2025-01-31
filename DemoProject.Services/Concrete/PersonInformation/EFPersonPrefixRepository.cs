using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.PersonInformation;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonPrefixRepository : IPersonPrefixRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonDetailRepository personDetailRepository;

        public EFPersonPrefixRepository(RepositoryConnection _connection, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;
            personDetailRepository = _personDetailRepository;
        }

        public async Task<bool> Amend(PersonPrefixViewModel _personPrefixViewModel)
        {
            try
            {
                // Set Default Value
                _personPrefixViewModel.EntryDateTime = DateTime.Now;
                _personPrefixViewModel.EntryStatus = StringLiteralValue.Amend;
                _personPrefixViewModel.ReasonForModification = "None";
                _personPrefixViewModel.UserAction = StringLiteralValue.Amend;
                _personPrefixViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _personPrefixViewModel.PrefixPrmKey = personDetailRepository.GetPrefixPrmKeyById(_personPrefixViewModel.PrefixId);

                //Mapping
                PersonPrefix personPrefix = Mapper.Map<PersonPrefix>(_personPrefixViewModel);
                PersonPrefixMakerChecker personPrefixMakerChecker = Mapper.Map<PersonPrefixMakerChecker>(_personPrefixViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                personPrefix.PrmKey = _personPrefixViewModel.PersonPrefixPrmKey;

                //PersonPrefix
                context.PersonPrefixes.Attach(personPrefix);
                context.Entry(personPrefix).State = EntityState.Modified;

                //PersonPrefixMakerChecker
                context.PersonPrefixMakerCheckers.Attach(personPrefixMakerChecker);
                context.Entry(personPrefixMakerChecker).State = EntityState.Added;
                personPrefix.PersonPrefixMakerCheckers.Add(personPrefixMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(PersonPrefixViewModel _personPrefixViewModel)
        {
            try
            {
                // Set Default Value
                _personPrefixViewModel.EntryDateTime = DateTime.Now;
                _personPrefixViewModel.UserAction = StringLiteralValue.Delete;
                _personPrefixViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                PersonPrefixMakerChecker personPrefixMakerChecker = Mapper.Map<PersonPrefixMakerChecker>(_personPrefixViewModel);

                //PersonPrefixMakerChecker
                context.PersonPrefixMakerCheckers.Attach(personPrefixMakerChecker);
                context.Entry(personPrefixMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<PersonPrefixViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonPrefixViewModel>("SELECT * FROM dbo.GetPersonEntriesForPersonPrefixCRUDOperation (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonPrefixViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<PersonPrefixViewModel>("SELECT * FROM dbo.GetPersonEntriesForPersonPrefixCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<PersonPrefixViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<PersonPrefixViewModel>("SELECT * FROM dbo.GetPersonEntriesForPersonPrefixCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonPrefixViewModel> GetViewModelForCreate(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonPrefixViewModel>("SELECT * FROM dbo.GetPersonPrefixEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonPrefixViewModel> GetViewModelForReject(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonPrefixViewModel>("SELECT * FROM dbo.GetPersonPrefixEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonPrefixViewModel> GetViewModelForUnverified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonPrefixViewModel>("SELECT * FROM dbo.GetPersonPrefixEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonPrefixViewModel> GetViewModelForVerified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonPrefixViewModel>("SELECT * FROM dbo.GetPersonPrefixEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(PersonPrefixViewModel _personPrefixViewModel)
        {
            try
            {
                // Set Default Value
                _personPrefixViewModel.EntryDateTime = DateTime.Now;
                _personPrefixViewModel.EntryStatus = StringLiteralValue.Create;
                _personPrefixViewModel.Remark = "None";
                _personPrefixViewModel.ReasonForModification = _personPrefixViewModel.ReasonForModification;
                _personPrefixViewModel.UserAction = StringLiteralValue.Create;
                _personPrefixViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _personPrefixViewModel.PrefixPrmKey = personDetailRepository.GetPrefixPrmKeyById(_personPrefixViewModel.PrefixId);

                //Mapping
                PersonPrefix personPrefix = Mapper.Map<PersonPrefix>(_personPrefixViewModel);
                PersonPrefixMakerChecker personPrefixMakerChecker = Mapper.Map<PersonPrefixMakerChecker>(_personPrefixViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //PersonPrefix
                context.PersonPrefixes.Attach(personPrefix);
                context.Entry(personPrefix).State = EntityState.Added;

                //PersonPrefixMakerChecker
                context.PersonPrefixMakerCheckers.Attach(personPrefixMakerChecker);
                context.Entry(personPrefixMakerChecker).State = EntityState.Added;
                personPrefix.PersonPrefixMakerCheckers.Add(personPrefixMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(PersonPrefixViewModel _personPrefixViewModel)
        {
            try
            {
                // Set Default Value
                _personPrefixViewModel.EntryDateTime = DateTime.Now;
                _personPrefixViewModel.UserAction = StringLiteralValue.Reject;
                _personPrefixViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                PersonPrefixMakerChecker personPrefixMakerChecker = Mapper.Map<PersonPrefixMakerChecker>(_personPrefixViewModel);

                //PersonPrefixMakerChecker
                context.PersonPrefixMakerCheckers.Attach(personPrefixMakerChecker);
                context.Entry(personPrefixMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(PersonPrefixViewModel _personPrefixViewModel)
        {
            try
            {
                _personPrefixViewModel.EntryDateTime = DateTime.Now;
                _personPrefixViewModel.EntryStatus = StringLiteralValue.Create;
                _personPrefixViewModel.ReasonForModification = "None";
                _personPrefixViewModel.Remark = "None";
                _personPrefixViewModel.UserAction = StringLiteralValue.Create;
                _personPrefixViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _personPrefixViewModel.PrefixPrmKey = personDetailRepository.GetPrefixPrmKeyById(_personPrefixViewModel.PrefixId);

                PersonPrefix personPrefix = Mapper.Map<PersonPrefix>(_personPrefixViewModel);

                PersonPrefixMakerChecker personPrefixMakerChecker = Mapper.Map<PersonPrefixMakerChecker>(_personPrefixViewModel);

                context.PersonPrefixMakerCheckers.Attach(personPrefixMakerChecker);
                context.Entry(personPrefixMakerChecker).State = EntityState.Added;
                personPrefix.PersonPrefixMakerCheckers.Add(personPrefixMakerChecker);

                context.PersonPrefixes.Attach(personPrefix);
                context.Entry(personPrefix).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(PersonPrefixViewModel _personPrefixViewModel)
        {
            try
            {
                // Assign MDF Status To EntryStatus For Performing Modify Operation
                PersonPrefixViewModel personPrefixViewModelForModify = await GetViewModelForVerified(_personPrefixViewModel.PersonPrmKey);
                if (personPrefixViewModelForModify != null)
                {
                    // Set Default Value
                    personPrefixViewModelForModify.EntryDateTime = DateTime.Now;
                    personPrefixViewModelForModify.UserAction = StringLiteralValue.Modify;
                    personPrefixViewModelForModify.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    PersonPrefixMakerChecker personPrefixMakerCheckerForModify = Mapper.Map<PersonPrefixMakerChecker>(personPrefixViewModelForModify);

                    //PersonPrefixMakerChecker
                    context.PersonPrefixMakerCheckers.Attach(personPrefixMakerCheckerForModify);
                    context.Entry(personPrefixMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify New Record
                // Set Default Value
                _personPrefixViewModel.EntryDateTime = DateTime.Now;
                _personPrefixViewModel.UserAction = StringLiteralValue.Verify;
                _personPrefixViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                PersonPrefixMakerChecker personPrefixMakerChecker = Mapper.Map<PersonPrefixMakerChecker>(_personPrefixViewModel);

                //PersonPrefixMakerCheckers
                context.PersonPrefixMakerCheckers.Attach(personPrefixMakerChecker);
                context.Entry(personPrefixMakerChecker).State = EntityState.Added;

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
