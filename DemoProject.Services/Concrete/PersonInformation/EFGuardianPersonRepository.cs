using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Configuration;
using System.Linq;
//Modified By Dhanashri Wagh 23/09/20224
namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFGuardianPersonRepository : IGuardianPersonRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        public EFGuardianPersonRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository, IConfigurationDetailRepository _configurationDetailRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
            configurationDetailRepository = _configurationDetailRepository;
        }

        bool result = true;
        public async Task<bool> Amend(GuardianPersonViewModel _guardianPersonViewModel)
        {
            try
            {
                int age = configurationDetailRepository.GetAge(GetDateOfBirth(_guardianPersonViewModel.PersonPrmKey));

                if (age < 18)
                {
                    result = personDbContextRepository.AttachGuardianPersonData(_guardianPersonViewModel, StringLiteralValue.Amend);
                }

                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }

                return result;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<GuardianPersonViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<GuardianPersonViewModel>("SELECT * FROM dbo.GetPersonEntriesOfGuardianPerson (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GuardianPersonViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<GuardianPersonViewModel>("SELECT * FROM dbo.GetPersonEntriesOfGuardianPerson ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<GuardianPersonViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<GuardianPersonViewModel>("SELECT * FROM dbo.GetGuardianPersonEntries ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<GuardianPersonViewModel> GetViewModelForCreate(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GuardianPersonViewModel>("SELECT * FROM dbo.GetGuardianPersonEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<GuardianPersonViewModel> GetViewModelForReject(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GuardianPersonViewModel>("SELECT * FROM dbo.GetGuardianPersonEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<GuardianPersonViewModel> GetViewModelForUnverified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GuardianPersonViewModel>("SELECT * FROM dbo.GetGuardianPersonEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<GuardianPersonViewModel> GetViewModelForVerified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<GuardianPersonViewModel>("SELECT * FROM dbo.GetGuardianPersonEntryByPersonPrmKey ( @PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(GuardianPersonViewModel _guardianPersonViewModel)
        {
            try
            {

                int age = configurationDetailRepository.GetAge(GetDateOfBirth(_guardianPersonViewModel.PersonPrmKey));

                if (age < 18)
                {
                    result = personDbContextRepository.AttachGuardianPersonData(_guardianPersonViewModel, StringLiteralValue.Create);
                }

                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }

                return result;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> VerifyRejectDelete(GuardianPersonViewModel _guardianPersonViewModel, string _entryType)
        {
            try
            {
                int age = configurationDetailRepository.GetAge(GetDateOfBirth(_guardianPersonViewModel.PersonPrmKey));

                if (_entryType == StringLiteralValue.Verify)
                {

                    if (age < 18)
                    {
                        result = personDbContextRepository.AttachGuardianPersonData(_guardianPersonViewModel, StringLiteralValue.Modify);
                    }
                }

                // Verify New Record

                if (age < 18)
                {
                    result = personDbContextRepository.AttachGuardianPersonData(_guardianPersonViewModel, _entryType);
                }

                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }

                return result;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }


        public DateTime GetDateOfBirth(long _personPrmKey)
        {
            return context.People
                        .Where(m => m.PrmKey == _personPrmKey)
                        .Select(m => m.DateOfBirth).FirstOrDefault();
        }
    }
}
