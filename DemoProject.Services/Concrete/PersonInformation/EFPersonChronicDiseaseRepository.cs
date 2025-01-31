using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
//Modified By Dhanashri Wagh 19/09/20224
namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonChronicDiseaseRepository : IPersonChronicDiseaseRepository
    {

        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonChronicDiseaseRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
            bool result = true;

        public async Task<bool> Amend(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel)
        {
            try
            {
                IEnumerable<PersonChronicDiseaseViewModel> PersonChronicDiseasesViewModelList = await GetEntries(_personChronicDiseaseViewModel.PersonPrmKey,StringLiteralValue.Reject);
                if (PersonChronicDiseasesViewModelList != null)
                {
                    foreach (PersonChronicDiseaseViewModel viewModel in PersonChronicDiseasesViewModelList)
                    {
                        result = personDbContextRepository.AttachPersonChronicDiseaseData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // Get Trading Entity Details From Session Object
                List<PersonChronicDiseaseViewModel> personChronicDiseaseViewModelList = new List<PersonChronicDiseaseViewModel>();

                personChronicDiseaseViewModelList = (List<PersonChronicDiseaseViewModel>)HttpContext.Current.Session["ChronicDisease"];
                if (personChronicDiseaseViewModelList != null)
                {
                    foreach (PersonChronicDiseaseViewModel viewModel in personChronicDiseaseViewModelList)
                    {
                        viewModel.Remark = _personChronicDiseaseViewModel.Remark;
                        viewModel.PersonPrmKey = _personChronicDiseaseViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonChronicDiseaseData(viewModel, StringLiteralValue.Create);
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

        public async Task<bool> Modify(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel)
        {
            try
            {
                List<PersonChronicDiseaseViewModel> personChronicDiseaseViewModelList = new List<PersonChronicDiseaseViewModel>();
                personChronicDiseaseViewModelList = (List<PersonChronicDiseaseViewModel>)HttpContext.Current.Session["ChronicDisease"];
                if (personChronicDiseaseViewModelList != null)
                {
                    foreach (PersonChronicDiseaseViewModel viewModel in personChronicDiseaseViewModelList)
                    {
                        viewModel.Remark = _personChronicDiseaseViewModel.Remark;
                        viewModel.PersonPrmKey = _personChronicDiseaseViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonChronicDiseaseData(viewModel, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(PersonChronicDiseaseViewModel _personChronicDiseaseViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    _personChronicDiseaseViewModel.PersonPrmKey = personMasterRepository.GetPersonPrmKeyById(_personChronicDiseaseViewModel.PersonId);

                    // Modify Old Entry
                    IEnumerable<PersonChronicDiseaseViewModel> PersonChronicDiseasesViewModelList = await GetEntries(_personChronicDiseaseViewModel.PersonPrmKey,StringLiteralValue.Verify);
                    if (PersonChronicDiseasesViewModelList != null)
                    {
                        foreach (PersonChronicDiseaseViewModel viewModel in PersonChronicDiseasesViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonChronicDiseaseData(viewModel, StringLiteralValue.Modify);
                        }
                    }
                }

                // Verify New Entry
                List<PersonChronicDiseaseViewModel> personChronicDiseaseViewModelList = new List<PersonChronicDiseaseViewModel>();
                personChronicDiseaseViewModelList = (List<PersonChronicDiseaseViewModel>)HttpContext.Current.Session["ChronicDisease"];

                if (personChronicDiseaseViewModelList != null)
                {
                    foreach (PersonChronicDiseaseViewModel viewModel in personChronicDiseaseViewModelList)
                    {
                        viewModel.Remark = _personChronicDiseaseViewModel.Remark;
                        viewModel.PersonPrmKey = _personChronicDiseaseViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonChronicDiseaseData(viewModel, _entryType);
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

        //Get  Index
        public async Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonChronicDisease ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonChronicDiseaseViewModel>> GetEntries(long _personPrmKey ,string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonChronicDiseaseViewModel>("SELECT * FROM dbo.GetPersonChronicDiseaseEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
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
            int count = await context.PersonChronicDiseases
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
