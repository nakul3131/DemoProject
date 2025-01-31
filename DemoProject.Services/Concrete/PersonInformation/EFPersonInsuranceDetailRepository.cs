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
    public class EFPersonInsuranceDetailRepository : IPersonInsuranceDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonInsuranceDetailRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }

        bool result = true;
        public async Task<bool> Amend(PersonInsuranceDetailViewModel _personInsuranceDetailViewModel)
        {
            try
            {
                // Amend Old Fund
                IEnumerable<PersonInsuranceDetailViewModel> personInsuranceDetailViewModelListForAmend = await GetEntries(_personInsuranceDetailViewModel.PersonPrmKey,StringLiteralValue.Reject);

                foreach (PersonInsuranceDetailViewModel viewModel in personInsuranceDetailViewModelListForAmend)
                {
                    result = personDbContextRepository.AttachPersonInsuranceDetailData(viewModel, StringLiteralValue.Amend);
                }

                //Get Organization Fund From Session Object
                List<PersonInsuranceDetailViewModel> personAssetDetailViewModelList = (List<PersonInsuranceDetailViewModel>)HttpContext.Current.Session["InsuranceDetail"];
                if (personAssetDetailViewModelList != null)
                {
                    foreach (PersonInsuranceDetailViewModel viewModel in personAssetDetailViewModelList)
                    {
                        viewModel.PersonPrmKey = _personInsuranceDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonInsuranceDetailData(viewModel, StringLiteralValue.Create);
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

        public async Task<bool> Modify(PersonInsuranceDetailViewModel _personInsuranceDetailViewModel)
        {
            try
            {
                // Get Trading Entity Details From Session Object
                List<PersonInsuranceDetailViewModel> personInsuranceDetailViewModelList = new List<PersonInsuranceDetailViewModel>();
                personInsuranceDetailViewModelList = (List<PersonInsuranceDetailViewModel>)HttpContext.Current.Session["InsuranceDetail"];

                if (personInsuranceDetailViewModelList != null)
                {
                    foreach (PersonInsuranceDetailViewModel viewModel in personInsuranceDetailViewModelList)
                    {
                        viewModel.PersonPrmKey = _personInsuranceDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonInsuranceDetailData(viewModel, StringLiteralValue.Create);

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

        public async Task<bool> VerifyRejectDelete(PersonInsuranceDetailViewModel _personInsuranceDetailViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Organization Fund
                    IEnumerable<PersonInsuranceDetailViewModel> personInsuranceDetailViewModelListForModify = await GetEntries(_personInsuranceDetailViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    if (personInsuranceDetailViewModelListForModify != null)
                    {
                        foreach (PersonInsuranceDetailViewModel viewModel in personInsuranceDetailViewModelListForModify)
                        {
                            result = personDbContextRepository.AttachPersonInsuranceDetailData(viewModel, StringLiteralValue.Modify);

                        }
                    }
                }

                // Verify Record
               
                List<PersonInsuranceDetailViewModel> personInsuranceDetailViewModelList = new List<PersonInsuranceDetailViewModel>();
                personInsuranceDetailViewModelList = (List<PersonInsuranceDetailViewModel>)HttpContext.Current.Session["InsuranceDetail"];

                if (personInsuranceDetailViewModelList != null)
                {
                    foreach (PersonInsuranceDetailViewModel viewModel in personInsuranceDetailViewModelList)
                    {
                        viewModel.PersonPrmKey = _personInsuranceDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonInsuranceDetailData(viewModel, _entryType);

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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonInsuranceDetail ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonInsuranceDetailViewModel>> GetEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonInsuranceDetailViewModel>("SELECT * FROM dbo.GetPersonInsuranceDetailEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
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
            int count = await context.PersonInsuranceDetails
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
