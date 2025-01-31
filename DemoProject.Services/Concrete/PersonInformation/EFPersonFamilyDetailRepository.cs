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
    public class EFPersonFamilyDetailRepository : IPersonFamilyDetailRepository
    {

        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonFamilyDetailRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }

        bool result = true;

        public async Task<bool> Amend(PersonFamilyDetailViewModel _personFamilyDetailViewModel)
        {
            try
            {
                IEnumerable<PersonFamilyDetailViewModel> personFamilyDetailsViewModelList = await GetEntries(_personFamilyDetailViewModel.PersonPrmKey,StringLiteralValue.Reject);

                if (personFamilyDetailsViewModelList != null)
                {
                    foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailsViewModelList)
                    {
                        result = personDbContextRepository.AttachPersonFamilyDetailData(viewModel, StringLiteralValue.Amend);

                    }
                }

                // Get person FamilyDetail Details From Session Object
                List<PersonFamilyDetailViewModel> personFamilyDetailViewModelList = new List<PersonFamilyDetailViewModel>();

                personFamilyDetailViewModelList = (List<PersonFamilyDetailViewModel>)HttpContext.Current.Session["FamilyDetail"];

                if (personFamilyDetailViewModelList != null)
                {
                    foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailViewModelList)
                    {
                        viewModel.Remark = _personFamilyDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personFamilyDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonFamilyDetailData(viewModel, StringLiteralValue.Create);
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

        public async Task<bool> Modify(PersonFamilyDetailViewModel _personFamilyDetailViewModel)
        {
            try
            {
                // Get Trading Entity Details From Session Object
                List<PersonFamilyDetailViewModel> personFamilyDetailViewModelList = new List<PersonFamilyDetailViewModel>();
                personFamilyDetailViewModelList = (List<PersonFamilyDetailViewModel>)HttpContext.Current.Session["FamilyDetail"];

                if (personFamilyDetailViewModelList != null)
                {
                    foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailViewModelList)
                    {
                        viewModel.Remark = _personFamilyDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personFamilyDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonFamilyDetailData(viewModel, StringLiteralValue.Create);

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

        public async Task<bool> VerifyRejectDelete(PersonFamilyDetailViewModel _personFamilyDetailViewModel,string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Entry 
                    IEnumerable<PersonFamilyDetailViewModel> personFamilyDetailsViewModelList = await GetEntries(_personFamilyDetailViewModel.PersonPrmKey, StringLiteralValue.Verify);

                    if (personFamilyDetailsViewModelList != null)
                    {
                        foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailsViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonFamilyDetailData(viewModel, StringLiteralValue.Modify);

                        }
                    }
                }
                // Verify New Record
                List<PersonFamilyDetailViewModel> personFamilyDetailViewModelList = new List<PersonFamilyDetailViewModel>();
                personFamilyDetailViewModelList = (List<PersonFamilyDetailViewModel>)HttpContext.Current.Session["FamilyDetail"];

                if (personFamilyDetailViewModelList != null)
                {
                    foreach (PersonFamilyDetailViewModel viewModel in personFamilyDetailViewModelList)
                    {
                        viewModel.Remark = _personFamilyDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personFamilyDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonFamilyDetailData(viewModel, _entryType);

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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonFamilyDetail ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonFamilyDetailViewModel>> GetEntries(long _personPrmKey , string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonFamilyDetailViewModel>("SELECT * FROM dbo.GetPersonFamilyDetailEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
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
            int count = await context.PersonFamilyDetails
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
