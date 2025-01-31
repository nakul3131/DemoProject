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

//Modified By Dhanashri Wagh 19/09/20224
namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonContactDetailsRepository : IPersonContactDetailsRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonContactDetailsRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        public async Task<bool> Amend(PersonContactDetailViewModel _personContactDetailViewModel)
        {
            bool result=true;
            try
            {
                IEnumerable<PersonContactDetailViewModel> personContactDetailsViewModelListForAmend = await GetEntries(_personContactDetailViewModel.PersonPrmKey,StringLiteralValue.Reject);

                if (personContactDetailsViewModelListForAmend != null)
                {
                    foreach (PersonContactDetailViewModel viewModel in personContactDetailsViewModelListForAmend)
                    {
                        result = personDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // Get Trading Entity Details From Session Object
                List<PersonContactDetailViewModel> personContactDetailViewModelList = new List<PersonContactDetailViewModel>();

                personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["ContactDetail"];

                if (personContactDetailViewModelList != null)
                {
                    foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                    {
                        viewModel.Remark = _personContactDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personContactDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Create);
                    }
                }

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


        public async Task<bool> Modify(PersonContactDetailViewModel _personContactDetailViewModel)
        {
            
            try
            {
                bool result=true;
                // Get Trading Entity Details From Session Object
                List<PersonContactDetailViewModel> personContactDetailViewModelList = new List<PersonContactDetailViewModel>();
                personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["ContactDetail"];

                if (personContactDetailViewModelList != null)
                {
                    foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                    {
                        viewModel.Remark = _personContactDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personContactDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Create);
                    }
                }

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

        public async Task<bool> VerifyRejectDelete(PersonContactDetailViewModel _personContactDetailViewModel, string _entryType)
        {
            bool result=true;
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Entry
                    IEnumerable<PersonContactDetailViewModel> personContactDetailsViewModelForModify = await GetEntries(_personContactDetailViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    if (personContactDetailsViewModelForModify != null)
                    {
                        foreach (PersonContactDetailViewModel viewModel in personContactDetailsViewModelForModify)
                        {
                            result = personDbContextRepository.AttachPersonContactDetailData(viewModel, StringLiteralValue.Modify);
                        }
                    }
                }

                // Verify New Entry
                List<PersonContactDetailViewModel> personContactDetailViewModelList = new List<PersonContactDetailViewModel>();
                personContactDetailViewModelList = (List<PersonContactDetailViewModel>)HttpContext.Current.Session["ContactDetail"];

                if (personContactDetailViewModelList != null)
                {
                    foreach (PersonContactDetailViewModel viewModel in personContactDetailViewModelList)
                    {
                        viewModel.Remark = _personContactDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personContactDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonContactDetailData(viewModel, _entryType);
                    }
                }

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

        //Get  Index
        public async Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfContactDetail ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonContactDetailViewModel>> GetEntries(long _personPrmKey ,string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonContactDetailViewModel>("SELECT * FROM dbo.GetPersonContactDetailEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

      
        //Check Any Authorization Is Pending Or Not
        public async Task<bool> IsAnyAuthorizationPending(long personPrmKey)
        {
            //check waiting for response and rejected entries count
            int count = await context.PersonContactDetails
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
