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
    public class EFPersonSMSAlertRepository: IPersonSMSAlertRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonMasterRepository personMasterRepository;
        private readonly IPersonDbContextRepository personDbContextRepository;

        public EFPersonSMSAlertRepository(RepositoryConnection _connection, IPersonMasterRepository _personMasterRepository, IPersonDbContextRepository _personDbContextRepository)
        {
            context = _connection.EFDbContext;
            personMasterRepository = _personMasterRepository;
            personDbContextRepository = _personDbContextRepository;
        }
        bool result = true;

        public async Task<bool> Amend(PersonSMSAlertViewModel _personSMSAlertViewModel)
        {
            try
            {
                IEnumerable<PersonSMSAlertViewModel> personSMSAlertsViewModelList = await GetEntries(_personSMSAlertViewModel.PersonPrmKey , StringLiteralValue.Reject);

                if (personSMSAlertsViewModelList != null)
                {
                    foreach (PersonSMSAlertViewModel viewModel in personSMSAlertsViewModelList)
                    {
                        result = personDbContextRepository.AttachPersonSMSAlertData(viewModel, StringLiteralValue.Amend);

                    }
                }
                // Get person BankDetail Details From Session Object
                List<PersonSMSAlertViewModel> personSMSAlertViewModelList = new List<PersonSMSAlertViewModel>();

                personSMSAlertViewModelList = (List<PersonSMSAlertViewModel>)HttpContext.Current.Session["SMSAlert"];

                if (personSMSAlertViewModelList != null)
                {
                    foreach (PersonSMSAlertViewModel viewModel in personSMSAlertViewModelList)
                    {
                        viewModel.Remark = _personSMSAlertViewModel.Remark;
                        viewModel.PersonPrmKey = _personSMSAlertViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonSMSAlertData(viewModel, StringLiteralValue.Create);

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


        public async Task<bool> Modify(PersonSMSAlertViewModel _personSMSAlertViewModel)
        {
            bool result = true;
            try
            {
                // Get Trading Entity Details From Session Object
                List<PersonSMSAlertViewModel> personSMSAlertViewModelList = new List<PersonSMSAlertViewModel>();
                personSMSAlertViewModelList = (List<PersonSMSAlertViewModel>)HttpContext.Current.Session["SMSAlert"];

                if (personSMSAlertViewModelList != null)
                {
                    foreach (PersonSMSAlertViewModel viewModel in personSMSAlertViewModelList)
                    {
                        viewModel.Remark = _personSMSAlertViewModel.Remark;
                        viewModel.PersonPrmKey = _personSMSAlertViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonSMSAlertData(viewModel, StringLiteralValue.Create);

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


        public async Task<bool> VerifyRejectDelete(PersonSMSAlertViewModel _personSMSAlertViewModel ,string _entryType)
        {
            bool result=true;
            try
            {
                if (_entryType == StringLiteralValue.Verify)
                {
                    // Modify Old Entry 
                    IEnumerable<PersonSMSAlertViewModel> personSMSAlertsViewModelList = await GetEntries(_personSMSAlertViewModel.PersonPrmKey,StringLiteralValue.Verify);

                    if (personSMSAlertsViewModelList != null)
                    {
                        foreach (PersonSMSAlertViewModel viewModel in personSMSAlertsViewModelList)
                        {
                            result = personDbContextRepository.AttachPersonSMSAlertData(viewModel, StringLiteralValue.Modify);
                        }
                    }
                }
                // Verify New Record
                List<PersonSMSAlertViewModel> personSMSAlertViewModelList = new List<PersonSMSAlertViewModel>();
                personSMSAlertViewModelList = (List<PersonSMSAlertViewModel>)HttpContext.Current.Session["SMSAlert"];

                if (personSMSAlertViewModelList != null)
                {
                    foreach (PersonSMSAlertViewModel viewModel in personSMSAlertViewModelList)
                    {
                        viewModel.Remark = _personSMSAlertViewModel.Remark;
                        viewModel.PersonPrmKey = _personSMSAlertViewModel.PersonPrmKey;
                        result = personDbContextRepository.AttachPersonSMSAlertData(viewModel, _entryType);
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
                var a = await context.Database.SqlQuery<PersonIndexViewModel>("SELECT * FROM dbo.GetPersonEntriesOfPersonSMSAlert ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //Get Entries By PersonPrmKey
        public async Task<IEnumerable<PersonSMSAlertViewModel>> GetEntries(long _personPrmKey , string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<PersonSMSAlertViewModel>("SELECT * FROM dbo.GetPersonSMSAlertEntriesByPersonPrmKey (@PersonPrmkey, @EntriesType)", new SqlParameter("@PersonPrmkey", _personPrmKey), new SqlParameter("EntriesType", _entryType)).ToListAsync();
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
            int count = await context.PersonSMSAlertes
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
