using System;
using System.Web;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.ViewModel.PersonInformation;
using System.Linq;
//Modified By Dhanashri Wagh 23/09/20224

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonSocialMediaRepository : IPersonSocialMediaRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonSocialMediaRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result = true;

        public async Task<bool> Amend(PersonSocialMediaViewModel _personSocialMediaViewModel)
        {
           
            try
            {
                // Amend Old PersonSocialMedia
                IEnumerable<PersonSocialMediaViewModel> personSocialMediaViewModelListForAmend = await GetEntries(_personSocialMediaViewModel.PersonPrmKey,StringLiteralValue.Reject);

                foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModelListForAmend)
                {
                    result = personDbContextRepository.AttachPersonSocialMediaData(viewModel, StringLiteralValue.Amend);
                }

                // Get Person Social Media From Session Object
                List<PersonSocialMediaViewModel> personSocialMediaViewModelList = new List<PersonSocialMediaViewModel>();

                personSocialMediaViewModelList = (List<PersonSocialMediaViewModel>)HttpContext.Current.Session["SocialMedia"];

                foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModelList)
                {
                    viewModel.Remark = _personSocialMediaViewModel.Remark;
                    viewModel.PersonPrmKey = _personSocialMediaViewModel.PersonPrmKey;
                    result = personDbContextRepository.AttachPersonSocialMediaData(viewModel, StringLiteralValue.Create);
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

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(PersonSocialMediaViewModel _personSocialMediaViewModel)
        {
            try
            {
                // Get Trading Entity Details From Session Object
                List<PersonSocialMediaViewModel> personSocialMediaViewModelList = new List<PersonSocialMediaViewModel>();

                personSocialMediaViewModelList = (List<PersonSocialMediaViewModel>)HttpContext.Current.Session["SocialMedia"];

                foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModelList)
                {
                    viewModel.Remark = _personSocialMediaViewModel.Remark;
                    viewModel.PersonPrmKey = _personSocialMediaViewModel.PersonPrmKey;
                    result = personDbContextRepository.AttachPersonSocialMediaData(viewModel, StringLiteralValue.Create);
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
      
        public async Task<bool> VerifyRejectDelete(PersonSocialMediaViewModel _personSocialMediaViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Entry
                    IEnumerable<PersonSocialMediaViewModel> personSocialMediaViewModelListForModify = await GetEntries(_personSocialMediaViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModelListForModify)
                    {
                        result = personDbContextRepository.AttachPersonSocialMediaData(viewModel, StringLiteralValue.Modify);
                    }
                }
                // Verify New Entry
                List<PersonSocialMediaViewModel> personSocialMediaViewModelList = new List<PersonSocialMediaViewModel>();

               personSocialMediaViewModelList = (List<PersonSocialMediaViewModel>)HttpContext.Current.Session["SocialMedia"];

                foreach (PersonSocialMediaViewModel viewModel in personSocialMediaViewModelList)
                {
                    viewModel.Remark = _personSocialMediaViewModel.Remark;
                    viewModel.PersonPrmKey = _personSocialMediaViewModel.PersonPrmKey;
                    result = personDbContextRepository.AttachPersonSocialMediaData(viewModel,_entryType);
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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonSocialMedia ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonSocialMediaViewModel>> GetEntries(long _personPrmKey , string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonSocialMediaViewModel>("SELECT * FROM dbo.GetPersonSocialMediaEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
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
            int count = await context.PersonSocialMedias
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