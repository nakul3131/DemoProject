using System;
using System.Web;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.Wrapper;
using System.Linq;
using DemoProject.Domain.Entities.Enterprise.Establishment;
using System.Data.Entity;
using AutoMapper;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Concrete.Enterprise.Establishment
{
    public class EFOrganizationLoanTypeRepository : IOrganizationLoanTypeRepository
    {
        private readonly EFDbContext context;

        private readonly IAccountDetailRepository accountDetailRepository;

        private readonly IOrganizationDetailRepository organizationDetailRepository;

        public EFOrganizationLoanTypeRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IOrganizationDetailRepository _organizationDetailRepository)
        {
            context = _connection.EFDbContext;

            accountDetailRepository = _accountDetailRepository;
            organizationDetailRepository = _organizationDetailRepository;
        }

        public async Task<bool> Amend(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel)
        {
            try
            {
                // Amend Old LoanType
                IEnumerable<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelListForAmend = await organizationDetailRepository.GetLoanTypeEntries(StringLiteralValue.Reject);

                foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelListForAmend)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.Remark = "None";
                    viewModel.UserAction = StringLiteralValue.Amend;

                    //Mapping
                    OrganizationLoanTypeMakerChecker organizationLoanTypeMakerCheckerForAmend = Mapper.Map<OrganizationLoanTypeMakerChecker>(viewModel);
                    OrganizationLoanTypeTranslationMakerChecker organizationLoanTypeTranslationMakerCheckerForAmend = Mapper.Map<OrganizationLoanTypeTranslationMakerChecker>(viewModel);

                    //OrganizationLoanType
                    context.OrganizationLoanTypeMakerCheckers.Attach(organizationLoanTypeMakerCheckerForAmend);
                    context.Entry(organizationLoanTypeMakerCheckerForAmend).State = EntityState.Added;

                    //OrganizationLoanTypeTranslation
                    context.OrganizationLoanTypeTranslationMakerCheckers.Attach(organizationLoanTypeTranslationMakerCheckerForAmend);
                    context.Entry(organizationLoanTypeTranslationMakerCheckerForAmend).State = EntityState.Added;

                }

                //Get Contact Details From Session Object
                List<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelList = (List<OrganizationLoanTypeViewModel>)HttpContext.Current.Session["OrganizationLoanType"];

                foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelList)
                {
                    //Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.OrganizationLoanTypePrmKey = 0;
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                    viewModel.Note = _organizationLoanTypeViewModel.Note;
                    viewModel.Remark = _organizationLoanTypeViewModel.Remark;
                    viewModel.TransNote = _organizationLoanTypeViewModel.TransNote;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.LoanTypePrmKey = accountDetailRepository.GetLoanTypePrmKeyById(viewModel.LoanTypeId);

                    //Mapping
                    //OrganizationLoanType
                    OrganizationLoanType organizationLoanType = Mapper.Map<OrganizationLoanType>(viewModel);
                    OrganizationLoanTypeMakerChecker organizationLoanTypeMakerChecker = Mapper.Map<OrganizationLoanTypeMakerChecker>(viewModel);

                    //OrganizationLoanTypeTranslation
                    OrganizationLoanTypeTranslation organizationLoanTypeTranslation = Mapper.Map<OrganizationLoanTypeTranslation>(viewModel);
                    OrganizationLoanTypeTranslationMakerChecker organizationLoanTypeTranslationMakerChecker = Mapper.Map<OrganizationLoanTypeTranslationMakerChecker>(viewModel);

                    // Save Data In Appropriate Tables By Entity Framework Methods
                    //OrganizationLoanTypes
                    context.OrganizationLoanTypes.Attach(organizationLoanType);
                    context.Entry(organizationLoanType).State = EntityState.Added;

                    context.OrganizationLoanTypeMakerCheckers.Attach(organizationLoanTypeMakerChecker);
                    context.Entry(organizationLoanTypeMakerChecker).State = EntityState.Added;
                    organizationLoanType.OrganizationLoanTypeMakerCheckers.Add(organizationLoanTypeMakerChecker);

                    //OrganizationLoanTypeTranslations
                    context.OrganizationLoanTypeTranslations.Attach(organizationLoanTypeTranslation);
                    context.Entry(organizationLoanTypeTranslation).State = EntityState.Added;
                    organizationLoanType.OrganizationLoanTypeTranslations.Add(organizationLoanTypeTranslation);

                    context.OrganizationLoanTypeTranslationMakerCheckers.Attach(organizationLoanTypeTranslationMakerChecker);
                    context.Entry(organizationLoanTypeTranslationMakerChecker).State = EntityState.Added;
                    organizationLoanTypeTranslation.OrganizationLoanTypeTranslationMakerCheckers.Add(organizationLoanTypeTranslationMakerChecker);

                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<OrganizationLoanTypeViewModel>> GetByLawsLoanScheduleParameterIndex()
        {
            try
            {
                var a = await context.Database.SqlQuery<OrganizationLoanTypeViewModel>("SELECT * FROM dbo.GetOrganizationLoanTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OrganizationLoanTypeViewModel>> GetByLawsLoanScheduleParameterUnverifiedIndex()
        {
            try
            {
                var a = await context.Database.SqlQuery<OrganizationLoanTypeViewModel>("SELECT * FROM dbo.GetOrganizationLoanTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OrganizationLoanTypeViewModel>> GetByLawsLoanScheduleParameterRejectedIndex()
        {
            try
            {
                var a = await context.Database.SqlQuery<OrganizationLoanTypeViewModel>("SELECT * FROM dbo.GetOrganizationLoanTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<OrganizationLoanTypeViewModel>> GetloanSanctionAuthorityIndex()
        {
            try
            {
                var a = await context.Database.SqlQuery<OrganizationLoanTypeViewModel>("SELECT * FROM dbo.GetOrganizationLoanTypeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ByLawsLoanScheduleParameterViewModel> GetVerifiedEntries(byte _loanTypePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.GetByLawsLoanScheduleParameterEntryByLoanTypePrmKey (@LoanTypePrmKey, @EntryType)", new SqlParameter("@LoanTypePrmKey", _loanTypePrmKey), new SqlParameter("EntryType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<ByLawsLoanScheduleParameterViewModel> GetUnVerifiedEntries(byte _loanTypePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<ByLawsLoanScheduleParameterViewModel>("SELECT * FROM dbo.GetByLawsLoanScheduleParameterEntryByLoanTypePrmKey (@LoanTypePrmKey, @EntryType)", new SqlParameter("@LoanTypePrmKey", _loanTypePrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Delete(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel)
        {
            try
            {
                IEnumerable<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelListForDelete = await organizationDetailRepository.GetLoanTypeEntries(StringLiteralValue.Reject);

                foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelListForDelete)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.Remark = "None";
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    OrganizationLoanTypeMakerChecker organizationLoanTypeMakerChecker = Mapper.Map<OrganizationLoanTypeMakerChecker>(viewModel);
                    OrganizationLoanTypeTranslationMakerChecker organizationLoanTypeTranslationMakerChecker = Mapper.Map<OrganizationLoanTypeTranslationMakerChecker>(viewModel);

                    //OrganizationLoanType
                    context.OrganizationLoanTypeMakerCheckers.Attach(organizationLoanTypeMakerChecker);
                    context.Entry(organizationLoanTypeMakerChecker).State = EntityState.Added;

                    //OrganizationLoanTypeTranslation
                    context.OrganizationLoanTypeTranslationMakerCheckers.Attach(organizationLoanTypeTranslationMakerChecker);
                    context.Entry(organizationLoanTypeTranslationMakerChecker).State = EntityState.Added;

                }

                await context.SaveChangesAsync();

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
            //check waiting for response and rejected entries count
            int count = await context.OrganizationLoanTypes
                        .Where(u => u.EntryStatus == StringLiteralValue.Create || u.EntryStatus == StringLiteralValue.Reject)
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

        public async Task<bool> Reject(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel)
        {
            try
            {
                List<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelList = (List<OrganizationLoanTypeViewModel>)HttpContext.Current.Session["OrganizationLoanType"];

                foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelList)
                {
                    //Set Default Value
                    viewModel.PrmKey = 0;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.Remark = _organizationLoanTypeViewModel.Remark;

                    //Mapping
                    OrganizationLoanTypeMakerChecker organizationLoanTypeMakerChecker = Mapper.Map<OrganizationLoanTypeMakerChecker>(viewModel);
                    OrganizationLoanTypeTranslationMakerChecker loanTypeTranslationMakerChecker = Mapper.Map<OrganizationLoanTypeTranslationMakerChecker>(viewModel);

                    //OrganizationLoanType
                    context.OrganizationLoanTypeMakerCheckers.Attach(organizationLoanTypeMakerChecker);
                    context.Entry(organizationLoanTypeMakerChecker).State = EntityState.Added;

                    //OrganizationLoanTypeTranslation
                    context.OrganizationLoanTypeTranslationMakerCheckers.Attach(loanTypeTranslationMakerChecker);
                    context.Entry(loanTypeTranslationMakerChecker).State = EntityState.Added;

                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel)
        {
            try
            {
                //Get Organization LoanType From Session Object
                List<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelList = (List<OrganizationLoanTypeViewModel>)HttpContext.Current.Session["OrganizationLoanType"];

                foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelList)
                {
                    //Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                    viewModel.Note = _organizationLoanTypeViewModel.Note;
                    viewModel.Remark = "None";
                    viewModel.TransNote = _organizationLoanTypeViewModel.TransNote;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.LoanTypePrmKey = accountDetailRepository.GetLoanTypePrmKeyById(viewModel.LoanTypeId);

                    //Mapping
                    //OrganizationLoanType
                    OrganizationLoanType organizationLoanType = Mapper.Map<OrganizationLoanType>(viewModel);
                    OrganizationLoanTypeMakerChecker organizationLoanTypeMakerChecker = Mapper.Map<OrganizationLoanTypeMakerChecker>(viewModel);

                    //OrganizationLoanTypeTranslation
                    OrganizationLoanTypeTranslation organizationLoanTypeTranslation = Mapper.Map<OrganizationLoanTypeTranslation>(viewModel);
                    OrganizationLoanTypeTranslationMakerChecker organizationLoanTypeTranslationMakerChecker = Mapper.Map<OrganizationLoanTypeTranslationMakerChecker>(viewModel);

                    //OrganizationLoanTypes
                    context.OrganizationLoanTypes.Attach(organizationLoanType);
                    context.Entry(organizationLoanType).State = EntityState.Added;

                    context.OrganizationLoanTypeMakerCheckers.Attach(organizationLoanTypeMakerChecker);
                    context.Entry(organizationLoanTypeMakerChecker).State = EntityState.Added;
                    organizationLoanType.OrganizationLoanTypeMakerCheckers.Add(organizationLoanTypeMakerChecker);

                    //OrganizationLoanTypeTranslations
                    context.OrganizationLoanTypeTranslationMakerCheckers.Attach(organizationLoanTypeTranslationMakerChecker);
                    context.Entry(organizationLoanTypeTranslationMakerChecker).State = EntityState.Added;
                    organizationLoanTypeTranslation.OrganizationLoanTypeTranslationMakerCheckers.Add(organizationLoanTypeTranslationMakerChecker);

                    context.OrganizationLoanTypeTranslations.Attach(organizationLoanTypeTranslation);
                    context.Entry(organizationLoanTypeTranslation).State = EntityState.Added;
                    organizationLoanType.OrganizationLoanTypeTranslations.Add(organizationLoanTypeTranslation);
                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(OrganizationLoanTypeViewModel _organizationLoanTypeViewModel)
        {
            try
            {
                // Modify Old Organization LoanType
                IEnumerable<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelListForModify = await organizationDetailRepository.GetLoanTypeEntries(StringLiteralValue.Verify);
                foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelListForModify)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.Remark = _organizationLoanTypeViewModel.Remark;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    OrganizationLoanTypeMakerChecker organizationLoanTypeMakerCheckerForModify = Mapper.Map<OrganizationLoanTypeMakerChecker>(viewModel);
                    OrganizationLoanTypeTranslationMakerChecker organizationLoanTypeTranslationMakerCheckerForModify = Mapper.Map<OrganizationLoanTypeTranslationMakerChecker>(viewModel);

                    //OrganizationLoanType
                    context.OrganizationLoanTypeMakerCheckers.Attach(organizationLoanTypeMakerCheckerForModify);
                    context.Entry(organizationLoanTypeMakerCheckerForModify).State = EntityState.Added;

                    //OrganizationLoanTypeTranslation
                    context.OrganizationLoanTypeTranslationMakerCheckers.Attach(organizationLoanTypeTranslationMakerCheckerForModify);
                    context.Entry(organizationLoanTypeTranslationMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify Record
                List<OrganizationLoanTypeViewModel> organizationLoanTypeViewModelList = new List<OrganizationLoanTypeViewModel>();
                organizationLoanTypeViewModelList = (List<OrganizationLoanTypeViewModel>)HttpContext.Current.Session["OrganizationLoanType"];

                foreach (OrganizationLoanTypeViewModel viewModel in organizationLoanTypeViewModelList)
                {
                    //Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Verify;
                    viewModel.PrmKey = 0;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.Remark = _organizationLoanTypeViewModel.Remark;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    OrganizationLoanTypeMakerChecker organizationLoanTypeMakerChecker = Mapper.Map<OrganizationLoanTypeMakerChecker>(viewModel);
                    OrganizationLoanTypeTranslationMakerChecker organizationLoanTypeTranslationMakerChecker = Mapper.Map<OrganizationLoanTypeTranslationMakerChecker>(viewModel);

                    //OrganizationLoanTypeTranslation
                    context.OrganizationLoanTypeTranslationMakerCheckers.Attach(organizationLoanTypeTranslationMakerChecker);
                    context.Entry(organizationLoanTypeTranslationMakerChecker).State = EntityState.Added;

                    //OrganizationLoanType
                    context.OrganizationLoanTypeMakerCheckers.Attach(organizationLoanTypeMakerChecker);
                    context.Entry(organizationLoanTypeMakerChecker).State = EntityState.Added;
                }

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }
    }
}
