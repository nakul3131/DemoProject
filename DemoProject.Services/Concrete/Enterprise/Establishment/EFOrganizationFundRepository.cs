using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.Enterprise.Establishment;
using DemoProject.Services.Abstract.Enterprise.Establishment;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Enterprise.Establishment;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Account.SystemEntity;

namespace DemoProject.Services.Concrete.Enterprise.Establishment
{
    public class EFOrganizationFundRepository : IOrganizationFundRepository
    {
        private readonly EFDbContext context;
        private readonly IOrganizationDetailRepository organizationDetailRepository;
        private readonly IAccountDetailRepository accountDetailRepository;

        public EFOrganizationFundRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IOrganizationDetailRepository _organizationDetailRepository)
        {
            context = _connection.EFDbContext;

            accountDetailRepository = _accountDetailRepository;
            organizationDetailRepository = _organizationDetailRepository;
        }

        public async Task<bool> Amend(OrganizationFundViewModel _organizationFundViewModel)
        {
            try
            {
                // Amend Old Fund
                IEnumerable<OrganizationFundViewModel> organizationFundViewModelListForAmend = await organizationDetailRepository.GetFundEntries(StringLiteralValue.Reject);
                foreach (OrganizationFundViewModel viewModel in organizationFundViewModelListForAmend)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.Remark = "None";

                    //Mapping
                    OrganizationFundMakerChecker organizationFundMakerCheckerForAmend = Mapper.Map<OrganizationFundMakerChecker>(viewModel);
                    OrganizationFundTranslationMakerChecker organizationFundTranslationMakerCheckerForAmend = Mapper.Map<OrganizationFundTranslationMakerChecker>(viewModel);

                    //OrganizationFund
                    context.OrganizationFundMakerCheckers.Attach(organizationFundMakerCheckerForAmend);
                    context.Entry(organizationFundMakerCheckerForAmend).State = EntityState.Added;

                    //OrganizationFundTranslation
                    context.OrganizationFundTranslationMakerCheckers.Attach(organizationFundTranslationMakerCheckerForAmend);
                    context.Entry(organizationFundTranslationMakerCheckerForAmend).State = EntityState.Added;

                }

                //Get Organization Fund From Session Object
                List<OrganizationFundViewModel> organizationFundViewModelList = (List<OrganizationFundViewModel>)HttpContext.Current.Session["OrganizationFund"];

                foreach (OrganizationFundViewModel viewModel in organizationFundViewModelList)
                {
                    //Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                    viewModel.Note = _organizationFundViewModel.Note;
                    viewModel.Remark = _organizationFundViewModel.Remark;
                    viewModel.TransNote = _organizationFundViewModel.TransNote;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKeyBy Id
                    viewModel.FundPrmKey = accountDetailRepository.GetFundPrmKeyById(viewModel.FundId);

                    //Mapping
                    //OrganizationFund
                    OrganizationFund organizationFund = Mapper.Map<OrganizationFund>(viewModel);
                    OrganizationFundMakerChecker organizationFundMakerChecker = Mapper.Map<OrganizationFundMakerChecker>(viewModel);

                    //OrganizationFundTranslation
                    OrganizationFundTranslation organizationFundTranslation = Mapper.Map<OrganizationFundTranslation>(viewModel);
                    OrganizationFundTranslationMakerChecker organizationFundTranslationMakerChecker = Mapper.Map<OrganizationFundTranslationMakerChecker>(viewModel);

                    // Save Data In Appropriate Tables By Entity Framework Methods
                    //OrganizationFunds
                    context.OrganizationFunds.Attach(organizationFund);
                    context.Entry(organizationFund).State = EntityState.Added;

                    context.OrganizationFundMakerCheckers.Attach(organizationFundMakerChecker);
                    context.Entry(organizationFundMakerChecker).State = EntityState.Added;
                    organizationFund.OrganizationFundMakerCheckers.Add(organizationFundMakerChecker);

                    //OrganizationFundTranslations
                    context.OrganizationFundTranslationMakerCheckers.Attach(organizationFundTranslationMakerChecker);
                    context.Entry(organizationFundTranslationMakerChecker).State = EntityState.Added;
                    organizationFundTranslation.OrganizationFundTranslationMakerCheckers.Add(organizationFundTranslationMakerChecker);

                    context.OrganizationFundTranslations.Attach(organizationFundTranslation);
                    context.Entry(organizationFundTranslation).State = EntityState.Added;
                    organizationFund.OrganizationFundTranslations.Add(organizationFundTranslation);
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

        public async Task<bool> Delete(OrganizationFundViewModel _organizationFundViewModel)
        {
            try
            {
                IEnumerable<OrganizationFundViewModel> organizationFundViewModelListForDelete = await organizationDetailRepository.GetFundEntries(StringLiteralValue.Reject);
                foreach (OrganizationFundViewModel viewModel in organizationFundViewModelListForDelete)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.Remark = "None";
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    OrganizationFundMakerChecker organizationFundMakerChecker = Mapper.Map<OrganizationFundMakerChecker>(viewModel);
                    OrganizationFundTranslationMakerChecker organizationFundTranslationMakerChecker = Mapper.Map<OrganizationFundTranslationMakerChecker>(viewModel);

                    //OrganizationFund
                    context.OrganizationFundMakerCheckers.Attach(organizationFundMakerChecker);
                    context.Entry(organizationFundMakerChecker).State = EntityState.Added;

                    //OrganizationFundTranslation
                    context.OrganizationFundTranslationMakerCheckers.Attach(organizationFundTranslationMakerChecker);
                    context.Entry(organizationFundTranslationMakerChecker).State = EntityState.Added;

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
            int count = await context.OrganizationFunds
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

        public async Task<bool> Reject(OrganizationFundViewModel _organizationFundViewModel)
        {
            try
            {
                List<OrganizationFundViewModel> organizationFundViewModelList = (List<OrganizationFundViewModel>)HttpContext.Current.Session["OrganizationFund"];
                foreach (OrganizationFundViewModel viewModel in organizationFundViewModelList)
                {
                    //Set Default Value
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.Remark = _organizationFundViewModel.Remark;

                    //Mapping
                    OrganizationFundMakerChecker organizationFundMakerChecker = Mapper.Map<OrganizationFundMakerChecker>(viewModel);
                    OrganizationFundTranslationMakerChecker organizationFundTranslationMakerChecker = Mapper.Map<OrganizationFundTranslationMakerChecker>(viewModel);

                    //OrganizationFund
                    context.OrganizationFundMakerCheckers.Attach(organizationFundMakerChecker);
                    context.Entry(organizationFundMakerChecker).State = EntityState.Added;

                    //OrganizationFundTranslation
                    context.OrganizationFundTranslationMakerCheckers.Attach(organizationFundTranslationMakerChecker);
                    context.Entry(organizationFundTranslationMakerChecker).State = EntityState.Added;

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

        public async Task<bool> Save(OrganizationFundViewModel _organizationFundViewModel)
        {
            try
            {
                //Get Organization Fund From Session Object
                List<OrganizationFundViewModel> organizationFundViewModelList = (List<OrganizationFundViewModel>)HttpContext.Current.Session["OrganizationFund"];
                foreach (OrganizationFundViewModel viewModel in organizationFundViewModelList)
                {
                    //Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.LanguagePrmKey = (short)HttpContext.Current.Session["RegionalLanguagePrmKey"];
                    viewModel.Note = _organizationFundViewModel.Note;
                    viewModel.Remark = "None";
                    viewModel.TransNote = _organizationFundViewModel.TransNote;
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    viewModel.FundPrmKey = accountDetailRepository.GetFundPrmKeyById(viewModel.FundId);

                    //Mapping
                    //OrganizationFund
                    OrganizationFund organizationFund = Mapper.Map<OrganizationFund>(viewModel);
                    OrganizationFundMakerChecker organizationFundMakerChecker = Mapper.Map<OrganizationFundMakerChecker>(viewModel);

                    //OrganizationFundTranslation
                    OrganizationFundTranslation organizationFundTranslation = Mapper.Map<OrganizationFundTranslation>(viewModel);
                    OrganizationFundTranslationMakerChecker organizationFundTranslationMakerChecker = Mapper.Map<OrganizationFundTranslationMakerChecker>(viewModel);

                    //OrganizationFund
                    context.OrganizationFunds.Attach(organizationFund);
                    context.Entry(organizationFund).State = EntityState.Added;

                    context.OrganizationFundMakerCheckers.Attach(organizationFundMakerChecker);
                    context.Entry(organizationFundMakerChecker).State = EntityState.Added;
                    organizationFund.OrganizationFundMakerCheckers.Add(organizationFundMakerChecker);

                    //OrganizationFundTranslation
                    context.OrganizationFundTranslations.Attach(organizationFundTranslation);
                    context.Entry(organizationFundTranslation).State = EntityState.Added;
                    organizationFund.OrganizationFundTranslations.Add(organizationFundTranslation);

                    context.OrganizationFundTranslationMakerCheckers.Attach(organizationFundTranslationMakerChecker);
                    context.Entry(organizationFundTranslationMakerChecker).State = EntityState.Added;
                    organizationFundTranslation.OrganizationFundTranslationMakerCheckers.Add(organizationFundTranslationMakerChecker);

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

        public async Task<bool> Verify(OrganizationFundViewModel _organizationFundViewModel)
        {
            try
            {
                // Modify Old Organization Fund
                IEnumerable<OrganizationFundViewModel> organizationFundViewModelListForModify = await organizationDetailRepository.GetFundEntries(StringLiteralValue.Verify);
                foreach (OrganizationFundViewModel viewModel in organizationFundViewModelListForModify)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.Remark = _organizationFundViewModel.Remark;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    OrganizationFundMakerChecker organizationFundMakerCheckerForModify = Mapper.Map<OrganizationFundMakerChecker>(viewModel);
                    OrganizationFundTranslationMakerChecker organizationFundTranslationMakerCheckerForModify = Mapper.Map<OrganizationFundTranslationMakerChecker>(viewModel);

                    //OrganizationFund
                    context.OrganizationFundMakerCheckers.Attach(organizationFundMakerCheckerForModify);
                    context.Entry(organizationFundMakerCheckerForModify).State = EntityState.Added;

                    //OrganizationFundTranslation
                    context.OrganizationFundTranslationMakerCheckers.Attach(organizationFundTranslationMakerCheckerForModify);
                    context.Entry(organizationFundTranslationMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify Record
                // Set Default Value
                List<OrganizationFundViewModel> organizationFundViewModelList = new List<OrganizationFundViewModel>();
                organizationFundViewModelList = (List<OrganizationFundViewModel>)HttpContext.Current.Session["OrganizationFund"];

                foreach (OrganizationFundViewModel viewModel in organizationFundViewModelList)
                {
                    //Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Verify;
                    viewModel.PrmKey = 0;
                    viewModel.Remark = _organizationFundViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    OrganizationFundMakerChecker organizationFundMakerChecker = Mapper.Map<OrganizationFundMakerChecker>(viewModel);
                    OrganizationFundTranslationMakerChecker organizationFundTranslationMakerChecker = Mapper.Map<OrganizationFundTranslationMakerChecker>(viewModel);

                    //OrganizationFund
                    context.OrganizationFundMakerCheckers.Attach(organizationFundMakerChecker);
                    context.Entry(organizationFundMakerChecker).State = EntityState.Added;

                    //OrganizationFundTranslation
                    context.OrganizationFundTranslationMakerCheckers.Attach(organizationFundTranslationMakerChecker);
                    context.Entry(organizationFundTranslationMakerChecker).State = EntityState.Added;

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
