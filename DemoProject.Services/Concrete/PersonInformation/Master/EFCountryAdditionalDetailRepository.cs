using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation.Master;
using DemoProject.Services.Abstract.PersonInformation.Master;
using DemoProject.Domain.Entities.PersonInformation.Master;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.PersonInformation;

namespace DemoProject.Services.Concrete.PersonInformation.Master
{
    public class EFCountryAdditionalDetailRepository : ICountryAdditionalDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;

        public EFCountryAdditionalDetailRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IPersonDetailRepository _personDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            personDetailRepository = _personDetailRepository;
        }

        public async Task<bool> Amend(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel)
        {
            try
            {
                // Set Default Value
                _countryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryAdditionalDetailViewModel.EntryStatus = StringLiteralValue.Amend;
                _countryAdditionalDetailViewModel.ReasonForModification = "None";
                _countryAdditionalDetailViewModel.UserAction = StringLiteralValue.Amend;
                _countryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id of All Dropdown
                _countryAdditionalDetailViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_countryAdditionalDetailViewModel.CurrencyId);
                _countryAdditionalDetailViewModel.WorldWideTimeZonePrmKey = personDetailRepository.GetWorldWideTimeZonePrmKeyById(_countryAdditionalDetailViewModel.WorldWideTimeZoneId);

                // Mapping 
                CountryAdditionalDetail countryAdditionalDetail = Mapper.Map<CountryAdditionalDetail>(_countryAdditionalDetailViewModel);
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryAdditionalDetailViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                countryAdditionalDetail.PrmKey = _countryAdditionalDetailViewModel.CountryAdditionalDetailPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods

                // CountryAdditionalDetail
                context.CountryAdditionalDetails.Attach(countryAdditionalDetail);
                context.Entry(countryAdditionalDetail).State = EntityState.Modified;

                //CountryAdditionalDetailMakerChecker
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;
                countryAdditionalDetail.CountryAdditionalDetailMakerCheckers.Add(countryAdditionalDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel)
        {
            try
            {
                // Set Default Value
                _countryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryAdditionalDetailViewModel.UserAction = StringLiteralValue.Delete;
                _countryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryAdditionalDetailViewModel);

                // CountryAdditionalDetailMakerChecker
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<CountryAdditionalDetailViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesForCountryAdditionalDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CountryAdditionalDetailViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesForCountryAdditionalDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CountryAdditionalDetailViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesForCountryAdditionalDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CountryAdditionalDetailViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCenterEntriesForCountryAdditionalDetailCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CountryAdditionalDetailViewModel> GetViewModelForCreate(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCenterCountryAdditionalDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CountryAdditionalDetailViewModel> GetViewModelForReject(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCenterCountryAdditionalDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CountryAdditionalDetailViewModel> GetViewModelForUnverified(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCenterCountryAdditionalDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CountryAdditionalDetailViewModel> GetViewModelForVerified(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCenterCountryAdditionalDetailEntriesByCenterPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _centerPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel)
        {
            try
            {
                // Set Default Value
                _countryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryAdditionalDetailViewModel.UserAction = StringLiteralValue.Reject;
                _countryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryAdditionalDetailViewModel);

                // CountryAdditionalDetailMakerChecker
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel)
        {
            try
            {
                // Set Default Value
                _countryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryAdditionalDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _countryAdditionalDetailViewModel.ReasonForModification = "None";
                _countryAdditionalDetailViewModel.Remark = "None";
                _countryAdditionalDetailViewModel.UserAction = StringLiteralValue.Create;
                _countryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _countryAdditionalDetailViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_countryAdditionalDetailViewModel.CurrencyId);
                _countryAdditionalDetailViewModel.WorldWideTimeZonePrmKey = personDetailRepository.GetWorldWideTimeZonePrmKeyById(_countryAdditionalDetailViewModel.WorldWideTimeZoneId);

                // Mapping 
                CountryAdditionalDetail countryAdditionalDetail = Mapper.Map<CountryAdditionalDetail>(_countryAdditionalDetailViewModel);
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryAdditionalDetailViewModel);

                // CountryAdditionalDetail
                context.CountryAdditionalDetails.Attach(countryAdditionalDetail);
                context.Entry(countryAdditionalDetail).State = EntityState.Added;

                //CountryAdditionalDetailMakerChecker
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;
                countryAdditionalDetail.CountryAdditionalDetailMakerCheckers.Add(countryAdditionalDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel)
        {
            try
            {
                // Set Default Value
                _countryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryAdditionalDetailViewModel.EntryStatus = StringLiteralValue.Create;
                _countryAdditionalDetailViewModel.ReasonForModification = "None";
                _countryAdditionalDetailViewModel.Remark = "None";
                _countryAdditionalDetailViewModel.UserAction = StringLiteralValue.Create;
                _countryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id
                _countryAdditionalDetailViewModel.CurrencyPrmKey = accountDetailRepository.GetCurrencyPrmKeyById(_countryAdditionalDetailViewModel.CurrencyId);
                _countryAdditionalDetailViewModel.WorldWideTimeZonePrmKey = personDetailRepository.GetWorldWideTimeZonePrmKeyById(_countryAdditionalDetailViewModel.WorldWideTimeZoneId);

                // Mapping 
                CountryAdditionalDetail countryAdditionalDetail = Mapper.Map<CountryAdditionalDetail>(_countryAdditionalDetailViewModel);
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryAdditionalDetailViewModel);

                // CountryAdditionalDetail
                context.CountryAdditionalDetails.Attach(countryAdditionalDetail);
                context.Entry(countryAdditionalDetail).State = EntityState.Added;

                // CountryAdditionalDetailMakerChecker
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;
                countryAdditionalDetail.CountryAdditionalDetailMakerCheckers.Add(countryAdditionalDetailMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel)
        {
            try
            {
                // Assign MDF Status To EntryStatus For Performing Modify Operation
                CountryAdditionalDetailViewModel countryAdditionalDetailViewModelForModify = await GetViewModelForVerified(_countryAdditionalDetailViewModel.CenterPrmKey);

                if (countryAdditionalDetailViewModelForModify != null)
                {

                    // set Default Value
                    countryAdditionalDetailViewModelForModify.EntryDateTime = DateTime.Now;
                    countryAdditionalDetailViewModelForModify.UserAction = StringLiteralValue.Modify;
                    countryAdditionalDetailViewModelForModify.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping 
                    CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerCheckerForModify = Mapper.Map<CountryAdditionalDetailMakerChecker>(countryAdditionalDetailViewModelForModify);

                    // CountryAdditionalDetailMakerChecker
                    context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerCheckerForModify);
                    context.Entry(countryAdditionalDetailMakerCheckerForModify).State = EntityState.Added;

                }
                //Verify new Record
                // Set Default Value
                _countryAdditionalDetailViewModel.EntryDateTime = DateTime.Now;
                _countryAdditionalDetailViewModel.UserAction = StringLiteralValue.Verify;
                _countryAdditionalDetailViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping 
                CountryAdditionalDetailMakerChecker countryAdditionalDetailMakerChecker = Mapper.Map<CountryAdditionalDetailMakerChecker>(_countryAdditionalDetailViewModel);

                // CountryAdditionalDetailMakerChecker
                context.CountryAdditionalDetailMakerCheckers.Attach(countryAdditionalDetailMakerChecker);
                context.Entry(countryAdditionalDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<CountryAdditionalDetailViewModel> GetRejectedEntry(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCountryAdditionalDetailEntryByCenterPrmKey (@CenterPrmKey, @EntryType)", new SqlParameter("@CenterPrmKey", _centerPrmKey), new SqlParameter("EntryType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CountryAdditionalDetailViewModel> GetUnverifiedEntry(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCountryAdditionalDetailEntryByCenterPrmKey (@CenterPrmKey, @EntryType)", new SqlParameter("@CenterPrmKey", _centerPrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CountryAdditionalDetailViewModel> GetVerifiedEntry(short _centerPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CountryAdditionalDetailViewModel>("SELECT * FROM dbo.GetCountryAdditionalDetailEntryByCenterPrmKey (@CenterPrmKey, @EntryType)", new SqlParameter("@CenterPrmKey", _centerPrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
    }
}
