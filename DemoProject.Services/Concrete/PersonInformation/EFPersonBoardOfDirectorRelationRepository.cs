using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using System.Linq;
//Modified By Dhanashri Wagh 23/09/20224
namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonBoardOfDirectorRelationRepository : IPersonBoardOfDirectorRelationRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonBoardOfDirectorRelationRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result = true;

        public async Task<bool> Amend(PersonBoardOfDirectorRelationViewModel _personBoardOfDirectorRelationViewModel)
        {
            try
            {
                IEnumerable<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationsViewModelList = await GetEntries(_personBoardOfDirectorRelationViewModel.PersonPrmKey,StringLiteralValue.Reject);

                if (personBoardOfDirectorRelationsViewModelList != null)
                {
                    foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationsViewModelList)
                    {
                        result = personDbContextRepository.AttachPersonBoardOfDirectorRelationData(viewModel, StringLiteralValue.Amend);
                    }
                }
                // Get person BankDetail Details From Session Object
                List<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationViewModelList = new List<PersonBoardOfDirectorRelationViewModel>();

                personBoardOfDirectorRelationViewModelList = (List<PersonBoardOfDirectorRelationViewModel>)HttpContext.Current.Session["BoardOfDirectorRelation"];

                if (personBoardOfDirectorRelationViewModelList != null)
                    {
                        foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationViewModelList)
                        {
                        viewModel.Remark = _personBoardOfDirectorRelationViewModel.Remark;
                        viewModel.PersonPrmKey = _personBoardOfDirectorRelationViewModel.PersonPrmKey;
                            result = personDbContextRepository.AttachPersonBoardOfDirectorRelationData(viewModel, StringLiteralValue.Create);
                        }
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

        public async Task<bool> Modify(PersonBoardOfDirectorRelationViewModel _personBoardOfDirectorRelationViewModel)
        {
            try
            {
                // Get Trading Entity Details From Session Object
                List<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationViewModelList = new List<PersonBoardOfDirectorRelationViewModel>();
                personBoardOfDirectorRelationViewModelList = (List<PersonBoardOfDirectorRelationViewModel>)HttpContext.Current.Session["BoardOfDirectorRelation"];

                if (personBoardOfDirectorRelationViewModelList != null)
                {
                    foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationViewModelList)
                    {
                        viewModel.Remark = _personBoardOfDirectorRelationViewModel.Remark;
                        viewModel.PersonPrmKey = _personBoardOfDirectorRelationViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonBoardOfDirectorRelationData(viewModel, StringLiteralValue.Create);
                    }
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
        
        public async Task<bool> VerifyRejectDelete(PersonBoardOfDirectorRelationViewModel _personBoardOfDirectorRelationViewModel , string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Entry 
                    IEnumerable<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationsViewModelList = await GetEntries(_personBoardOfDirectorRelationViewModel.PersonPrmKey, StringLiteralValue.Verify);
                    if (personBoardOfDirectorRelationsViewModelList != null)
                    {
                        foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationsViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonBoardOfDirectorRelationData(viewModel, StringLiteralValue.Modify);
                        } }
                }
                // Verify New Record
                List<PersonBoardOfDirectorRelationViewModel> personBoardOfDirectorRelationViewModelList = new List<PersonBoardOfDirectorRelationViewModel>();
                personBoardOfDirectorRelationViewModelList = (List<PersonBoardOfDirectorRelationViewModel>)HttpContext.Current.Session["BoardOfDirectorRelation"];
                if (personBoardOfDirectorRelationViewModelList != null)
                {
                    foreach (PersonBoardOfDirectorRelationViewModel viewModel in personBoardOfDirectorRelationViewModelList)
                    {
                        viewModel.Remark = _personBoardOfDirectorRelationViewModel.Remark;
                        viewModel.PersonPrmKey = _personBoardOfDirectorRelationViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonBoardOfDirectorRelationData(viewModel, _entryType);
                    }
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


        //Get Verified Index
        public async Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonBoardOfDirectorRelation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonBoardOfDirectorRelationViewModel>> GetEntries(long _personPrmKey,string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonBoardOfDirectorRelationViewModel>("SELECT * FROM dbo.GetPersonBoardOfDirectorRelationEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        
        public async Task<bool> IsAnyAuthorizationPending(long personPrmKey)
        {
            //check waiting for response and rejected entries count
            int count = await context.PersonBoardOfDirectorRelations
                        .Where(u => (u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject) && u.PersonPrmKey == personPrmKey)
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
