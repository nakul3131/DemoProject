using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.PersonInformation;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
//Modified By Dhanashri Wagh 23/09/20224
namespace DemoProject.Services.Concrete.PersonInformation
{
    public class EFPersonAdditionalIncomeDetailRepository : IPersonAdditionalIncomeDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonAdditionalIncomeDetailRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }

        bool result = true;
        public async Task<bool> Amend(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel)
        {
            try
            {
                IEnumerable<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailsViewModelList = await GetEntries(_personAdditionalIncomeDetailViewModel.PersonPrmKey,StringLiteralValue.Reject);

                if (personAdditionalIncomeDetailsViewModelList != null)
                {
                    foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailsViewModelList)
                    {
                        result = personDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Amend);
                    }
                }
                // Get person BankDetail Details From Session Object
                List<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModelList = new List<PersonAdditionalIncomeDetailViewModel>();

                personAdditionalIncomeDetailViewModelList = (List<PersonAdditionalIncomeDetailViewModel>)HttpContext.Current.Session["AdditionalIncomeDetail"];

                if (personAdditionalIncomeDetailViewModelList != null)
                {
                    foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModelList)
                    {
                        viewModel.Remark = _personAdditionalIncomeDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personAdditionalIncomeDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Create);
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

        public async Task<bool> Modify(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel)
        {
            try
            {
                // Get Trading Entity Details From Session Object
                List<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModels = new List<PersonAdditionalIncomeDetailViewModel>();
                personAdditionalIncomeDetailViewModels = (List<PersonAdditionalIncomeDetailViewModel>)HttpContext.Current.Session["AdditionalIncomeDetail"];
                if (personAdditionalIncomeDetailViewModels != null)
                {
                    foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModels)
                    {
                        viewModel.Remark = _personAdditionalIncomeDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personAdditionalIncomeDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Create);

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

        public async Task<bool> VerifyRejectDelete(PersonAdditionalIncomeDetailViewModel _personAdditionalIncomeDetailViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Entry 
                    IEnumerable<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailViewModelList = await GetEntries(_personAdditionalIncomeDetailViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    if (personAdditionalIncomeDetailViewModelList != null)
                    {
                        foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, StringLiteralValue.Modify);
                        }
                    }
                }

                // Verify New Record
                List<PersonAdditionalIncomeDetailViewModel> personAdditionalIncomeDetailsViewModelList = new List<PersonAdditionalIncomeDetailViewModel>();
                personAdditionalIncomeDetailsViewModelList = (List<PersonAdditionalIncomeDetailViewModel>)HttpContext.Current.Session["AdditionalIncomeDetail"];

                if (personAdditionalIncomeDetailsViewModelList != null)
                {
                    foreach (PersonAdditionalIncomeDetailViewModel viewModel in personAdditionalIncomeDetailsViewModelList)
                    {
                        viewModel.Remark = _personAdditionalIncomeDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personAdditionalIncomeDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonAdditionalIncomeDetailData(viewModel, _entryType);
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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonAdditionalIncomeDetail ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Verified Entries By PersonPrmKey
        public async Task<IEnumerable<PersonAdditionalIncomeDetailViewModel>> GetEntries(long _personPrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonAdditionalIncomeDetailViewModel>("SELECT * FROM dbo.GetPersonAdditionalIncomeDetailEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
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
            int count = await context.PersonAdditionalIncomeDetails
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
