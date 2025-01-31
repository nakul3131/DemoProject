using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.PersonInformation;
using System.Linq;
//Modified By Dhanashri Wagh 23/09/20224
namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonCommoditiesAssetRepository : IPersonCommoditiesAssetRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonCommoditiesAssetRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result = true;
        public async Task<bool> Amend(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel)
        {
            try
            {
                result = personDbContextRepository.AttachPersonCommoditiesAssetData(_personCommoditiesAssetViewModel, StringLiteralValue.Amend);

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

        public async Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonCommoditiesAsset ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<PersonCommoditiesAssetViewModel> GetEntry(long _personPrmKey ,string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonCommoditiesAssetViewModel>("SELECT * FROM dbo.GetPersonCommoditiesAssetEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel)
        {
            try
            {
                result = personDbContextRepository.AttachPersonCommoditiesAssetData(_personCommoditiesAssetViewModel, StringLiteralValue.Create);

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
        
        public async Task<bool> VerifyRejectDelete(PersonCommoditiesAssetViewModel _personCommoditiesAssetViewModel , string _entryType)
        {
            try
            {if (_entryType == StringLiteralValue.Verify)
                {
                    // Assign MDF Status To EntryStatus For Performing Modify Operation
                    PersonCommoditiesAssetViewModel personCommoditiesAssetViewModelForModify = await GetEntry(_personCommoditiesAssetViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    if (personCommoditiesAssetViewModelForModify.PrmKey > 0)
                    {
                        result = personDbContextRepository.AttachPersonCommoditiesAssetData(personCommoditiesAssetViewModelForModify, StringLiteralValue.Modify);
                    }
                }

                // Verify New Record
                result = personDbContextRepository.AttachPersonCommoditiesAssetData(_personCommoditiesAssetViewModel, _entryType);

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

        public async Task<bool> IsAnyAuthorizationPending(long personPrmKey)
        {
            //check waiting for response and rejected entries count
            int count = await context.PersonCommoditiesAssets
                        .Where(u => (u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject || u.EntryStatus == StringLiteralValue.Amend) && u.PersonPrmKey == personPrmKey)
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
    }
}
