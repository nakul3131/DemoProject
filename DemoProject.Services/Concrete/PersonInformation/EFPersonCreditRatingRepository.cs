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
    public class EFPersonCreditRatingRepository : IPersonCreditRatingRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonCreditRatingRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }

        bool result = true;

        public async Task<bool> Amend(PersonCreditRatingViewModel _personCreditRatingViewModel)
        {
            try
            {
                IEnumerable<PersonCreditRatingViewModel> personCreditRatingsViewModelList = await GetEntries(_personCreditRatingViewModel.PersonPrmKey,StringLiteralValue.Reject);

                if (personCreditRatingsViewModelList != null)
                {
                    foreach (PersonCreditRatingViewModel viewModel in personCreditRatingsViewModelList)
                    {
                        result = personDbContextRepository.AttachPersonCreditRatingData(viewModel, StringLiteralValue.Amend);
                    }
                }
                // Get Trading Entity Details From Session Object
                List<PersonCreditRatingViewModel> personCreditRatingViewModelList = new List<PersonCreditRatingViewModel>();

                personCreditRatingViewModelList = (List<PersonCreditRatingViewModel>)HttpContext.Current.Session["CreditRating"];

                if (personCreditRatingViewModelList != null)
                {
                    foreach (PersonCreditRatingViewModel viewModel in personCreditRatingViewModelList)
                    {
                        viewModel.Remark = _personCreditRatingViewModel.Remark;
                        viewModel.PersonPrmKey = _personCreditRatingViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonCreditRatingData(viewModel, StringLiteralValue.Create);
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

        public async Task<bool> Modify(PersonCreditRatingViewModel _personCreditRatingViewModel)
        {
            try
            {
                List<PersonCreditRatingViewModel> personCreditRatingViewModelList = new List<PersonCreditRatingViewModel>();
                personCreditRatingViewModelList = (List<PersonCreditRatingViewModel>)HttpContext.Current.Session["CreditRating"];

                if (personCreditRatingViewModelList != null)
                {
                    foreach (PersonCreditRatingViewModel viewModel in personCreditRatingViewModelList)
                    {
                        viewModel.Remark = _personCreditRatingViewModel.Remark;
                        viewModel.PersonPrmKey = _personCreditRatingViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonCreditRatingData(viewModel, StringLiteralValue.Create);
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

        public async Task<bool> VerifyRejectDelete(PersonCreditRatingViewModel _personCreditRatingViewModel ,string _entryType)
        {
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Entry
                    IEnumerable<PersonCreditRatingViewModel> personCreditRatingsViewModelList = await GetEntries(_personCreditRatingViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    if (personCreditRatingsViewModelList != null)
                    {
                        foreach (PersonCreditRatingViewModel viewModel in personCreditRatingsViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonCreditRatingData(viewModel, StringLiteralValue.Modify);
                        }
                    }

                }

                // Verify New Entry
                List<PersonCreditRatingViewModel> personCreditRatingViewModelList = new List<PersonCreditRatingViewModel>();
                personCreditRatingViewModelList = (List<PersonCreditRatingViewModel>)HttpContext.Current.Session["CreditRating"];

                if (personCreditRatingViewModelList != null)
                {
                    foreach (PersonCreditRatingViewModel viewModel in personCreditRatingViewModelList)
                    {
                        viewModel.Remark = _personCreditRatingViewModel.Remark;
                        viewModel.PersonPrmKey = _personCreditRatingViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonCreditRatingData(viewModel, _entryType);
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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonCreditRating ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonCreditRatingViewModel>> GetEntries(long _personPrmKey ,string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonCreditRatingViewModel>("SELECT * FROM dbo.GetPersonCreditRatingEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
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
            int count = await context.PersonCreditRatings
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
