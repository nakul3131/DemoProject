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

//Modified By Dhanashri Wagh 19/09/20224

namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonCourtCaseRepository : IPersonCourtCaseRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonCourtCaseRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result;

        public async Task<bool> Amend(PersonCourtCaseViewModel _personCourtCaseViewModel)
        {
            
            try
            {
                // Amend Old PersonCourtCase
                IEnumerable<PersonCourtCaseViewModel> personCourtCaseViewModelListForAmend = await GetEntries(_personCourtCaseViewModel.PersonPrmKey , StringLiteralValue.Reject);

                if (personCourtCaseViewModelListForAmend != null)
                {
                    foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelListForAmend)
                    {
                        result = personDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Amend);
                    }
                }

                // Get Person Social Media From Session Object
                List<PersonCourtCaseViewModel> personCourtCaseViewModelList = new List<PersonCourtCaseViewModel>();

                personCourtCaseViewModelList = (List<PersonCourtCaseViewModel>)HttpContext.Current.Session["CourtCase"];

                if (personCourtCaseViewModelList != null)
                {
                    foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelList)
                    {
                        viewModel.Remark = _personCourtCaseViewModel.Remark;
                        viewModel.PersonPrmKey = _personCourtCaseViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Create);
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

        public async Task<bool> Modify(PersonCourtCaseViewModel _personCourtCaseViewModel)
        {
            try
            {
                // Get Trading Entity Details From Session Object
                List<PersonCourtCaseViewModel> personCourtCaseViewModelList = new List<PersonCourtCaseViewModel>();

                personCourtCaseViewModelList = (List<PersonCourtCaseViewModel>)HttpContext.Current.Session["CourtCase"];

                if (personCourtCaseViewModelList != null)
                {
                    foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelList)
                    {
                        viewModel.Remark = _personCourtCaseViewModel.Remark;
                        viewModel.PersonPrmKey = _personCourtCaseViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(PersonCourtCaseViewModel _personCourtCaseViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Entry
                    IEnumerable<PersonCourtCaseViewModel> personCourtCaseViewModelListForModify = await GetEntries(_personCourtCaseViewModel.PersonPrmKey , StringLiteralValue.Verify);

                    if (personCourtCaseViewModelListForModify != null)
                    {
                        foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelListForModify)
                        {
                            result = personDbContextRepository.AttachPersonCourtCaseData(viewModel, StringLiteralValue.Modify);
                        }
                    }
                }
                // Verify New Entry
                List<PersonCourtCaseViewModel> personCourtCaseViewModelList = new List<PersonCourtCaseViewModel>();

                personCourtCaseViewModelList = (List<PersonCourtCaseViewModel>)HttpContext.Current.Session["CourtCase"];

                foreach (PersonCourtCaseViewModel viewModel in personCourtCaseViewModelList)
                {
                    viewModel.Remark = _personCourtCaseViewModel.Remark;
                    viewModel.PersonPrmKey = _personCourtCaseViewModel.PersonPrmKey;
                    result = personDbContextRepository.AttachPersonCourtCaseData(viewModel, _entryType);
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

        //Get Verified Index
        public async Task<IEnumerable<PersonIndexViewModel>> GetIndex(string _entryType)
        {
            try
            {
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonCourtCase ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonCourtCaseViewModel>> GetEntries(long _personPrmKey ,string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonCourtCaseViewModel>("SELECT * FROM dbo.GetPersonCourtCaseEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
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
            int count = await context.PersonCourtCases
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