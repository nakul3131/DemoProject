using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
//Modified By Dhanashri Wagh 23/09/20224
namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonAdditionalDetailRepository : IPersonAdditionalDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonAdditionalDetailRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result = true;
        public int GetlistofOccupation(Guid OccupationId)
        {
            return context.Occupations.Where(c => c.OccupationId == OccupationId).Select(c => c.ParentOccupationPrmKey).FirstOrDefault();
        }

        public string GetListOfMaritalStatus(Guid MaritalStatusId)
        {
            return context.MaritalStatuses.Where(c => c.MaritalStatusId == MaritalStatusId).Select(c => c.SysNameOfMaritalStatus).FirstOrDefault();
        }

        public async Task<bool> Amend(PersonAdditionalDetailViewModel _personAdditionalDetailViewModel)
        {
            try
            {
                result = personDbContextRepository.AttachPersonAdditionalDetailData(_personAdditionalDetailViewModel, StringLiteralValue.Amend);

                //if (occupation == "SLRD")
                //    result = personDbContextRepository.AttachPersonEmployementDetailData(_personViewModel.PersonEmployementDetailViewModel, StringLiteralValue.Amend);
                //else
                //{
                //    _personViewModel.PersonEmployementDetailViewModel.NameOfEmployer = "None";
                //    _personViewModel.PersonEmployementDetailViewModel.TransNameOfEmployer = "None";
                //    _personViewModel.PersonEmployementDetailViewModel.EPFNumber = "None";
                //    _personViewModel.PersonEmployementDetailViewModel.TransEPFNumber = "None";

                //}

                if (result)
                {
                    result = await personDbContextRepository.SaveData();
                }


                return true;
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
                return await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonAdditionalDetail (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
        
        public async Task<PersonAdditionalDetailViewModel> GetViewModelForCreate(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonAdditionalDetailViewModel>("SELECT * FROM dbo.GetPersonAdditionalDetailEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonAdditionalDetailViewModel> GetViewModelForReject(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonAdditionalDetailViewModel>("SELECT * FROM dbo.GetPersonAdditionalDetailEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonAdditionalDetailViewModel> GetViewModelForUnverified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonAdditionalDetailViewModel>("SELECT * FROM dbo.GetPersonAdditionalDetailEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<PersonAdditionalDetailViewModel> GetViewModelForVerified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<PersonAdditionalDetailViewModel>("SELECT * FROM dbo.GetPersonAdditionalDetailEntryByPersonPrmKey (@PersonPrmKey, @EntriesType)", new SqlParameter("@PersonPrmKey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public long GetPrmKeyById(Guid _personId)
        {
            return context.People
                    .Where(c => c.PersonId == _personId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public bool IsVIPCustomer(Guid _personId)
        {
            long _personPrmKey = GetPrmKeyById(_personId);

            var vipRank = context.PersonAdditionalDetails
                            .Where(a => a.PersonPrmKey == _personPrmKey && a.EntryStatus == StringLiteralValue.Verify)
                            .Select(a => a.VIPRank).FirstOrDefault();

            if (vipRank > 0)
                return true;
            else
                return false;
        }

        public async Task<bool> Modify(PersonAdditionalDetailViewModel _personAdditionalDetailViewModel)
        {
            try
            {
                result = personDbContextRepository.AttachPersonAdditionalDetailData(_personAdditionalDetailViewModel, StringLiteralValue.Create);


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

        public async Task<bool> VerifyRejectDelete(PersonAdditionalDetailViewModel _personAdditionalDetailViewModel,string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Assign MDF Status To EntryStatus For Performing Modify Operation
                    PersonAdditionalDetailViewModel personAdditionalDetailViewModelForModify = await GetViewModelForVerified(_personAdditionalDetailViewModel.PersonPrmKey);
                    if (personAdditionalDetailViewModelForModify != null)
                    {
                        result = personDbContextRepository.AttachPersonAdditionalDetailData(_personAdditionalDetailViewModel, StringLiteralValue.Modify);
                    }
                
                }
                // Verify New Record

                if (result)
                {
                    result = personDbContextRepository.AttachPersonAdditionalDetailData(_personAdditionalDetailViewModel,_entryType);
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

    }
}
