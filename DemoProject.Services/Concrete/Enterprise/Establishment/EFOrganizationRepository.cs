using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Configuration;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Enterprise;

namespace DemoProject.Services.Concrete.Enterprise.Establishment
{
    public class EFOrganizationRepository : IOrganizationRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IOrganizationDetailRepository organizationDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IOrganizationDetailDbContextRepository organizationDetailDbContextRepository;

        public EFOrganizationRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IConfigurationDetailRepository _configurationDetailRepository,
                IEnterpriseDetailRepository _enterpriseDetailRepository, IOrganizationDetailRepository _organizationDetailRepository, IPersonDetailRepository _personDetailRepository, IOrganizationDetailDbContextRepository _organizationDetailDbContextRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            organizationDetailRepository = _organizationDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            organizationDetailRepository = _organizationDetailRepository;
            personDetailRepository = _personDetailRepository;
            organizationDetailDbContextRepository = _organizationDetailDbContextRepository;
        }


        public short GetMatchGSTNumber(Guid stateId)
        {
            var centerPrmKey = context.Centers.Where(c => c.CenterId == stateId).Select(c => c.PrmKey).FirstOrDefault();

            var iSONumericCode = context.CenterISOCodes.Where(c => c.CenterPrmKey == centerPrmKey).Select(c => c.ISONumericCode).FirstOrDefault();

            return iSONumericCode;
        }

        public async Task<bool> Amend(OrganizationViewModel _organizationViewModel)
        {
            try
            {
                bool result = true;
                if (result)
                    result = organizationDetailDbContextRepository.AttachOrganizationData(_organizationViewModel, StringLiteralValue.Amend);

                if (result)
                    result = organizationDetailDbContextRepository.AttachAuthorizedSharesCapitalData(_organizationViewModel.AuthorizedSharesCapitalViewModel, StringLiteralValue.Amend);


                // Amend Old Contact Detail
                if (result)
                {
                    IEnumerable<OrganizationContactDetailViewModel> organizationContactDetailViewModelListForAmend = await organizationDetailRepository.GetContactDetailEntries(StringLiteralValue.Reject);
                    foreach (OrganizationContactDetailViewModel viewModel in organizationContactDetailViewModelListForAmend)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationContactDetailData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Get Contact Details From Session Object
                if (result)
                {
                    List<OrganizationContactDetailViewModel> organizationContactDetailViewModelList = (List<OrganizationContactDetailViewModel>)HttpContext.Current.Session["OrganizationContactDetail"];
                    foreach (OrganizationContactDetailViewModel viewModel in organizationContactDetailViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationContactDetailData(viewModel, StringLiteralValue.Create);
                    }
                }

                // Amend OrganizationGSTRegistrationDetail
                if (result)
                {
                    IEnumerable<OrganizationGSTRegistrationDetailViewModel> organizationGSTRegistrationDetailViewModelListForAmend = await organizationDetailRepository.GetGSTRegistrationDetailEntries(StringLiteralValue.Reject);
                    foreach (OrganizationGSTRegistrationDetailViewModel viewModel in organizationGSTRegistrationDetailViewModelListForAmend)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationGSTRegistrationDetailData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Create New OrganizationGSTRegistrationDetail
                if (result)
                {
                    List<OrganizationGSTRegistrationDetailViewModel> organizationGSTRegistrationDetailViewModelList = (List<OrganizationGSTRegistrationDetailViewModel>)HttpContext.Current.Session["OrganizationGSTRegistrationDetail"];
                    foreach (OrganizationGSTRegistrationDetailViewModel viewModel in organizationGSTRegistrationDetailViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationGSTRegistrationDetailData(viewModel, StringLiteralValue.Create);
                    }
                }

                // Amend Old OrganizationFund
                if (result)
                {
                    IEnumerable<OrganizationFundViewModel> organizationFundViewModelListForAmend = await organizationDetailRepository.GetFundEntries(StringLiteralValue.Reject);
                    foreach (OrganizationFundViewModel viewModel in organizationFundViewModelListForAmend)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationFundData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Get OrganizationFund From Session Object
                if (result)
                {
                    List<OrganizationFundViewModel> organizationFundViewModelList = (List<OrganizationFundViewModel>)HttpContext.Current.Session["OrganizationFund"];
                    foreach (OrganizationFundViewModel viewModel in organizationFundViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationFundData(viewModel, StringLiteralValue.Create);
                    }
                }

                // Amend Old OrganizationLoanType
                if (result)
                {
                    IEnumerable<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelListForAmend = await organizationDetailRepository.GetLoanTypeEntries(StringLiteralValue.Reject);
                    foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelListForAmend)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationLoanTypeData(viewModel, StringLiteralValue.Amend);
                    }
                }

                //Get OrganizationLoanTypes From Session Object
                if (result)
                {
                    List<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelList = (List<OrganizationLoanTypeViewModel>)HttpContext.Current.Session["OrganizationLoanType"];
                    foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationLoanTypeData(viewModel, StringLiteralValue.Create);
                    }

                }

                if (result)
                    result = await organizationDetailDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> RejectDelete(OrganizationViewModel _organizationViewModel, string _entryType)
        {
            try
            {
                bool result = true;
                string entriesType;
                if (_entryType == StringLiteralValue.Reject)
                    entriesType = StringLiteralValue.Unverified;
                else
                    entriesType = StringLiteralValue.Reject;

                if (result)
                    result = organizationDetailDbContextRepository.AttachOrganizationData(_organizationViewModel, _entryType);

                if (result)
                    result = organizationDetailDbContextRepository.AttachAuthorizedSharesCapitalData(_organizationViewModel.AuthorizedSharesCapitalViewModel, _entryType);

                //OrganizationContactDetail
                if (result)
                {
                    IEnumerable<OrganizationContactDetailViewModel> organizationContactDetailViewModelList = await organizationDetailRepository.GetContactDetailEntries(entriesType);
                    foreach (OrganizationContactDetailViewModel viewModel in organizationContactDetailViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationContactDetailData(viewModel, _entryType);
                    }
                }

                //OrganizationGSTRegistrationDetail
                if (result)
                {
                    IEnumerable<OrganizationGSTRegistrationDetailViewModel> organizationGSTRegistrationDetailViewModelList = await organizationDetailRepository.GetGSTRegistrationDetailEntries(entriesType);
                    foreach (OrganizationGSTRegistrationDetailViewModel viewModel in organizationGSTRegistrationDetailViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationGSTRegistrationDetailData(viewModel, _entryType);
                    }
                }

                //OrganizationFund
                if (result)
                {
                    IEnumerable<OrganizationFundViewModel> organizationFundViewModelList = await organizationDetailRepository.GetFundEntries(entriesType);

                    foreach (OrganizationFundViewModel viewModel in organizationFundViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationFundData(viewModel, _entryType);
                    }
                }

                //OrganizationLoanType
                if (result)
                {
                    IEnumerable<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelList = await organizationDetailRepository.GetLoanTypeEntries(entriesType);

                    foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationLoanTypeData(viewModel, _entryType);
                    }
                }

                if (result)
                    result = await organizationDetailDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<OrganizationViewModel> GetActiveEntry()
        {
            try
            {
                return await context.Database.SqlQuery<OrganizationViewModel>("SELECT * FROM dbo.GetOrganizationEntry (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return null;
            }
        }

        public async Task<OrganizationViewModel> GetEntryById(Guid _organizationId)
        {
            try
            {
                return await context.Database.SqlQuery<OrganizationViewModel>("SELECT * FROM dbo.GetOrganizationEntryById (@OrganizationId)", new SqlParameter("@OrganizationId", _organizationId)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OrganizationIndexViewModel>> GetOrganizationIndex()
        {
            try
            {
                return await context.Database.SqlQuery<OrganizationIndexViewModel>("SELECT * FROM dbo.GetOrganizationEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<OrganizationViewModel> GetOrganizationEntry(string _entryType)
        {
            try
            {
                OrganizationViewModel organizationViewModel = await context.Database.SqlQuery<OrganizationViewModel>("SELECT * FROM dbo.GetOrganizationEntry (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", _entryType)).FirstOrDefaultAsync();

                organizationViewModel.AuthorizedSharesCapitalViewModel = await organizationDetailRepository.GetAuthorizedSharesCapitalEntry(_entryType);

                return organizationViewModel;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> GetSessionValues(string _entryType)
        {
            try
            {
                HttpContext.Current.Session["OrganizationContactDetail"] = await organizationDetailRepository.GetContactDetailEntries(_entryType);
                HttpContext.Current.Session["OrganizationGSTRegistrationDetail"] = await organizationDetailRepository.GetGSTRegistrationDetailEntries(_entryType);
                HttpContext.Current.Session["OrganizationFund"] = await organizationDetailRepository.GetFundEntries(_entryType);
                HttpContext.Current.Session["OrganizationLoanType"] = await organizationDetailRepository.GetLoanTypeEntries(_entryType);
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> IsAnyAuthorizationPending()
        {
            // Check Created, Amended and rejected entries count
            int count = await context.Organizations
                        .Where(u => u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Amend || u.EntryStatus == StringLiteralValue.Reject)
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


        public async Task<bool> Save(OrganizationViewModel _organizationViewModel)
        {
            try
            {
                bool result = true;

                if (result)
                    result = organizationDetailDbContextRepository.AttachOrganizationData(_organizationViewModel, StringLiteralValue.Create);

                if (result)
                    result = organizationDetailDbContextRepository.AttachAuthorizedSharesCapitalData(_organizationViewModel.AuthorizedSharesCapitalViewModel, StringLiteralValue.Create);


                // OrganizationContactDetail
                if (result)
                {
                    List<OrganizationContactDetailViewModel> organizationContactDetailViewModelList = (List<OrganizationContactDetailViewModel>)HttpContext.Current.Session["OrganizationContactDetail"];
                    foreach (OrganizationContactDetailViewModel viewModel in organizationContactDetailViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationContactDetailData(viewModel, StringLiteralValue.Create);
                    }
                }

                // OrganizationGSTRegistrationDetail
                if (result)
                {
                    List<OrganizationGSTRegistrationDetailViewModel> organizationGSTRegistrationDetailViewModelList = (List<OrganizationGSTRegistrationDetailViewModel>)HttpContext.Current.Session["OrganizationGSTRegistrationDetail"];

                    foreach (OrganizationGSTRegistrationDetailViewModel viewModel in organizationGSTRegistrationDetailViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationGSTRegistrationDetailData(viewModel, StringLiteralValue.Create);
                    }
                }

                // OrganizationFund
                if (result)
                {
                    List<OrganizationFundViewModel> organizationFundViewModelList = (List<OrganizationFundViewModel>)HttpContext.Current.Session["OrganizationFund"];
                    foreach (OrganizationFundViewModel viewModel in organizationFundViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationFundData(viewModel, StringLiteralValue.Create);
                    }
                }

                //Get OrganizationLoanType From Session Object
                if (result)
                {
                    List<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelList = (List<OrganizationLoanTypeViewModel>)HttpContext.Current.Session["OrganizationLoanType"];

                    foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationLoanTypeData(viewModel, StringLiteralValue.Create);
                    }
                }

                if (result)
                    result = await organizationDetailDbContextRepository.SaveData();

                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(OrganizationViewModel _organizationViewModel)
        {
            try
            {
                bool result = true;
                // First Modify Record - Get Active Record (i.e. Whose Entry Status is Verified)                 
                OrganizationViewModel organizationViewModelOldEntry = await GetOrganizationEntry(StringLiteralValue.Verify);

                // Skip First Entry
                if (organizationViewModelOldEntry.OrganizationPrmKey > 0)
                {
                    if (result)
                        result = organizationDetailDbContextRepository.AttachOrganizationData(organizationViewModelOldEntry, StringLiteralValue.Modify);

                    if (result)
                        result = organizationDetailDbContextRepository.AttachAuthorizedSharesCapitalData(organizationViewModelOldEntry.AuthorizedSharesCapitalViewModel, StringLiteralValue.Modify);

                    // Modify Old Contact Detail
                    if (result)
                    {
                        IEnumerable<OrganizationContactDetailViewModel> organizationContactDetailViewModelListForModify = await organizationDetailRepository.GetContactDetailEntries(StringLiteralValue.Verify);
                        foreach (OrganizationContactDetailViewModel viewModel in organizationContactDetailViewModelListForModify)
                        {
                            result = organizationDetailDbContextRepository.AttachOrganizationContactDetailData(viewModel, StringLiteralValue.Modify);
                        }
                    }

                    // Modify Old GSTRegistration Detail
                    if (result)
                    {
                        IEnumerable<OrganizationGSTRegistrationDetailViewModel> organizationGSTRegistrationDetailViewModelListForModify = await organizationDetailRepository.GetGSTRegistrationDetailEntries(StringLiteralValue.Verify);
                        foreach (OrganizationGSTRegistrationDetailViewModel viewModel in organizationGSTRegistrationDetailViewModelListForModify)
                        {
                            result = organizationDetailDbContextRepository.AttachOrganizationGSTRegistrationDetailData(viewModel, StringLiteralValue.Modify);
                        }
                    }

                    // Modify Old Organization Fund
                    if (result)
                    {
                        IEnumerable<OrganizationFundViewModel> organizationFundViewModelListForModify = await organizationDetailRepository.GetFundEntries(StringLiteralValue.Verify);

                        foreach (OrganizationFundViewModel viewModel in organizationFundViewModelListForModify)
                        {
                            result = organizationDetailDbContextRepository.AttachOrganizationFundData(viewModel, StringLiteralValue.Modify);
                        }
                    }

                    // Modify Old Organization LoanType
                    if (result)
                    {
                        IEnumerable<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelListForModify = await organizationDetailRepository.GetLoanTypeEntries(StringLiteralValue.Verify);
                        foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelListForModify)
                        {
                            result = organizationDetailDbContextRepository.AttachOrganizationLoanTypeData(viewModel, StringLiteralValue.Modify);
                        }
                    }
                }

                // Verify Record

                if (result)
                    result = organizationDetailDbContextRepository.AttachOrganizationData(_organizationViewModel, StringLiteralValue.Verify);

                if (result)
                    result = organizationDetailDbContextRepository.AttachAuthorizedSharesCapitalData(_organizationViewModel.AuthorizedSharesCapitalViewModel, StringLiteralValue.Verify);

                //OrganizationContactDetail
                if (result)
                {
                    List<OrganizationContactDetailViewModel> organizationContactDetailViewModelList = new List<OrganizationContactDetailViewModel>();
                    organizationContactDetailViewModelList = (List<OrganizationContactDetailViewModel>)HttpContext.Current.Session["OrganizationContactDetail"];

                    foreach (OrganizationContactDetailViewModel viewModel in organizationContactDetailViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationContactDetailData(viewModel, StringLiteralValue.Verify);
                    }
                }

                //OrganizationGSTRegistrationDetail
                if (result)
                {
                    List<OrganizationGSTRegistrationDetailViewModel> organizationGSTRegistrationDetailViewModelList = new List<OrganizationGSTRegistrationDetailViewModel>();
                    organizationGSTRegistrationDetailViewModelList = (List<OrganizationGSTRegistrationDetailViewModel>)HttpContext.Current.Session["OrganizationGSTRegistrationDetail"];

                    foreach (OrganizationGSTRegistrationDetailViewModel viewModel in organizationGSTRegistrationDetailViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationGSTRegistrationDetailData(viewModel, StringLiteralValue.Verify);
                    }
                }

                //OrganizationFund
                if (result)
                {
                    List<OrganizationFundViewModel> organizationFundViewModelList = new List<OrganizationFundViewModel>();
                    organizationFundViewModelList = (List<OrganizationFundViewModel>)HttpContext.Current.Session["OrganizationFund"];

                    foreach (OrganizationFundViewModel viewModel in organizationFundViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationFundData(viewModel, StringLiteralValue.Verify);
                    }
                }

                //OrganizationLoanType
                if (result)
                {
                    List<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelList = new List<OrganizationLoanTypeViewModel>();
                    organizationLoanTypeViewModelList = (List<OrganizationLoanTypeViewModel>)HttpContext.Current.Session["OrganizationLoanType"];

                    foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelList)
                    {
                        result = organizationDetailDbContextRepository.AttachOrganizationLoanTypeData(viewModel, StringLiteralValue.Verify);
                    }
                }

                if (result)
                    result = await organizationDetailDbContextRepository.SaveData();
                if (result)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}