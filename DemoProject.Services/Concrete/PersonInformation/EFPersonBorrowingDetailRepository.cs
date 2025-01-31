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
    public class EFPersonBorrowingDetailRepository : IPersonBorrowingDetailRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonBorrowingDetailRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result = true;

        public async Task<bool> Amend(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel)
        {
            try
            {
                IEnumerable<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelList = await GetEntries(_personBorrowingDetailViewModel.PersonPrmKey,StringLiteralValue.Reject);
                if (personBorrowingDetailViewModelList != null)
                {
                    foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelList)
                    {
                        result = personDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Amend);
                    }
                }
                // Get person BankDetail Details From Session Object
                List<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelsList = new List<PersonBorrowingDetailViewModel>();

                personBorrowingDetailViewModelsList = (List<PersonBorrowingDetailViewModel>)HttpContext.Current.Session["BorrowingDetail"];
                    if (personBorrowingDetailViewModelsList != null)
                    {
                        foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelsList)
                        {
                           viewModel.Remark = _personBorrowingDetailViewModel.Remark;
                           viewModel.PersonPrmKey = _personBorrowingDetailViewModel.PersonPrmKey;
                            result = personDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Create);
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
        
        public async Task<bool> Modify(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel)
        {
            try
            {
                // Get Trading Entity Details From Session Object
                List<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelList = new List<PersonBorrowingDetailViewModel>();
                personBorrowingDetailViewModelList = (List<PersonBorrowingDetailViewModel>)HttpContext.Current.Session["BorrowingDetail"];
                if (personBorrowingDetailViewModelList != null)
                {
                    foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelList)
                    {
                        viewModel.Remark = _personBorrowingDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personBorrowingDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Create);
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
        
        public async Task<bool> VerifyRejectDelete(PersonBorrowingDetailViewModel _personBorrowingDetailViewModel, string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Entry 
                    IEnumerable<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelList = await GetEntries(_personBorrowingDetailViewModel.PersonPrmKey,StringLiteralValue.Verify);
                    if (personBorrowingDetailViewModelList != null)
                    {
                        foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonBorrowingDetailData(viewModel, StringLiteralValue.Modify);
                        }
                    }

                }
                // Verify New Record
                List<PersonBorrowingDetailViewModel> personBorrowingDetailViewModelsList = new List<PersonBorrowingDetailViewModel>();
                personBorrowingDetailViewModelsList = (List<PersonBorrowingDetailViewModel>)HttpContext.Current.Session["BorrowingDetail"];
                if (personBorrowingDetailViewModelsList != null)
                {
                    foreach (PersonBorrowingDetailViewModel viewModel in personBorrowingDetailViewModelsList)
                    {
                        viewModel.Remark = _personBorrowingDetailViewModel.Remark;
                        viewModel.PersonPrmKey = _personBorrowingDetailViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonBorrowingDetailData(viewModel, _entryType);
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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonBorrowingDetail ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonBorrowingDetailViewModel>> GetEntries(long _personPrmKey,string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonBorrowingDetailViewModel>("SELECT * FROM dbo.GetPersonBorrowingDetailEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
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
            int count = await context.PersonBorrowingDetails
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
