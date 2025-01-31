using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.Account.Transaction;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Abstract.Account.Transaction;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Transaction;
using DemoProject.Services.Wrapper;
using DemoProject.Services.Abstract.Configuration;

namespace DemoProject.Services.Concrete.Account.Transaction
{
    public class EFSharesApplicationRepository : ISharesApplicationRepository
    {
        private readonly EFDbContext context;
        private readonly IAccountDetailRepository accountDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly IPersonDetailRepository personDetailRepository;
        private readonly IConfigurationDetailRepository configurationDetailRepository;

        public EFSharesApplicationRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository,
                                             IPersonDetailRepository _personDetailRepository, IConfigurationDetailRepository _configurationDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            personDetailRepository = _personDetailRepository;
            configurationDetailRepository = _configurationDetailRepository;
        }

        public async Task<bool> Amend(SharesApplicationViewModel _sharesApplicationViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel, StringLiteralValue.Amend);

                // Set Other Default Value
                _sharesApplicationViewModel.ApplicationStatus = "PEN";
                _sharesApplicationViewModel.StatusReason = "None";
                _sharesApplicationViewModel.TransStatusReason = "None";

                // Get PrmKey By Id
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.PersonPrmKey = personDetailRepository.GetPersonPrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.PersonId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.TransactionTypeId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.BusinessOfficeId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.MemberTypePrmKey = accountDetailRepository.GetMemberTypePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.MemberTypeId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.SchemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.SchemeId);

                // Mapping 
                // SharesApplication
                SharesApplication sharesApplicationForAmend = Mapper.Map<SharesApplication>(_sharesApplicationViewModel);
                SharesApplicationMakerChecker sharesApplicationMakerCheckerForAmend = Mapper.Map<SharesApplicationMakerChecker>(_sharesApplicationViewModel);

                // SharesApplicationModification
                SharesApplicationModification sharesApplicationModificationForAmend = Mapper.Map<SharesApplicationModification>(_sharesApplicationViewModel);
                SharesApplicationModificationMakerChecker sharesApplicationModificationMakerCheckerForAmend = Mapper.Map<SharesApplicationModificationMakerChecker>(_sharesApplicationViewModel);

                // SharesApplicationTranslation
                SharesApplicationTranslation sharesApplicationTranslationForAmend = Mapper.Map<SharesApplicationTranslation>(_sharesApplicationViewModel);
                SharesApplicationTranslationMakerChecker sharesApplicationTranslationMakerCheckerForAmend = Mapper.Map<SharesApplicationTranslationMakerChecker>(_sharesApplicationViewModel);

                // SharesApplicationDetail
                configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel.SharesApplicationDetailViewModel, StringLiteralValue.Amend);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.SharesApplicationPrmKey = _sharesApplicationViewModel.SharesApplicationPrmKey;
                SharesApplicationDetail sharesApplicationDetailForAmend = Mapper.Map<SharesApplicationDetail>(_sharesApplicationViewModel.SharesApplicationDetailViewModel);
                SharesApplicationDetailMakerChecker sharesApplicationDetailMakerCheckerForAmend = Mapper.Map<SharesApplicationDetailMakerChecker>(_sharesApplicationViewModel.SharesApplicationDetailViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                sharesApplicationForAmend.PrmKey = _sharesApplicationViewModel.SharesApplicationPrmKey;
                sharesApplicationModificationForAmend.PrmKey = _sharesApplicationViewModel.SharesApplicationModificationPrmKey;
                sharesApplicationTranslationForAmend.PrmKey = _sharesApplicationViewModel.SharesApplicationTranslationPrmKey;
                sharesApplicationDetailForAmend.PrmKey = _sharesApplicationViewModel.SharesApplicationDetailViewModel.SharesApplicationDetailPrmKey;

                // Save Data In Appropriate Tables By Entity Framework Methods
                // Check Entry Existance In Modification Table Or Main Table
                if (_sharesApplicationViewModel.SharesApplicationModificationPrmKey == 0)
                {
                    // SharesApplication
                    context.SharesApplicationMakerCheckers.Attach(sharesApplicationMakerCheckerForAmend);
                    context.Entry(sharesApplicationMakerCheckerForAmend).State = EntityState.Added;
                    sharesApplicationForAmend.SharesApplicationMakerCheckers.Add(sharesApplicationMakerCheckerForAmend);

                    context.SharesApplications.Attach(sharesApplicationForAmend);
                    context.Entry(sharesApplicationForAmend).State = EntityState.Modified;
                }
                else
                {
                    // SharesApplication Modification 
                    context.SharesApplicationModificationMakerCheckers.Attach(sharesApplicationModificationMakerCheckerForAmend);
                    context.Entry(sharesApplicationModificationMakerCheckerForAmend).State = EntityState.Added;
                    sharesApplicationModificationForAmend.SharesApplicationModificationMakerCheckers.Add(sharesApplicationModificationMakerCheckerForAmend);

                    context.SharesApplicationModifications.Attach(sharesApplicationModificationForAmend);
                    context.Entry(sharesApplicationModificationForAmend).State = EntityState.Modified;
                }

                // SharesApplicationTranslation
                context.SharesApplicationTranslationMakerCheckers.Attach(sharesApplicationTranslationMakerCheckerForAmend);
                context.Entry(sharesApplicationTranslationMakerCheckerForAmend).State = EntityState.Added;
                sharesApplicationTranslationForAmend.SharesApplicationTranslationMakerCheckers.Add(sharesApplicationTranslationMakerCheckerForAmend);

                context.SharesApplicationTranslations.Attach(sharesApplicationTranslationForAmend);
                context.Entry(sharesApplicationTranslationForAmend).State = EntityState.Modified;

                // SharesApplicationDetail
                context.SharesApplicationDetailMakerCheckers.Attach(sharesApplicationDetailMakerCheckerForAmend);
                context.Entry(sharesApplicationDetailMakerCheckerForAmend).State = EntityState.Added;
                sharesApplicationDetailForAmend.SharesApplicationDetailMakerCheckers.Add(sharesApplicationDetailMakerCheckerForAmend);

                context.SharesApplicationDetails.Attach(sharesApplicationDetailForAmend);
                context.Entry(sharesApplicationDetailForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(SharesApplicationViewModel _sharesApplicationViewModel)
        {
            try
            {
                // Set Default Value
                _sharesApplicationViewModel.EntryDateTime = DateTime.Now;
                _sharesApplicationViewModel.ReasonForModification = "None";
                _sharesApplicationViewModel.Remark = "None";
                _sharesApplicationViewModel.UserAction = StringLiteralValue.Delete;
                _sharesApplicationViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                SharesApplicationMakerChecker sharesApplicationMakerChecker = Mapper.Map<SharesApplicationMakerChecker>(_sharesApplicationViewModel);
                SharesApplicationModificationMakerChecker sharesApplicationModificationMakerChecker = Mapper.Map<SharesApplicationModificationMakerChecker>(_sharesApplicationViewModel);
                SharesApplicationTranslationMakerChecker sharesApplicationTranslationMakerChecker = Mapper.Map<SharesApplicationTranslationMakerChecker>(_sharesApplicationViewModel);
                SharesApplicationDetailMakerChecker sharesApplicationDetailMakerChecker = Mapper.Map<SharesApplicationDetailMakerChecker>(_sharesApplicationViewModel);

                if (_sharesApplicationViewModel.SharesApplicationModificationPrmKey == 0)
                {
                    // SharesApplication
                    context.SharesApplicationMakerCheckers.Attach(sharesApplicationMakerChecker);
                    context.Entry(sharesApplicationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // SharesApplicationModification  
                    context.SharesApplicationModificationMakerCheckers.Attach(sharesApplicationModificationMakerChecker);
                    context.Entry(sharesApplicationModificationMakerChecker).State = EntityState.Added;
                }

                // SharesApplicationTranslation
                context.SharesApplicationTranslationMakerCheckers.Attach(sharesApplicationTranslationMakerChecker);
                context.Entry(sharesApplicationTranslationMakerChecker).State = EntityState.Added;

                // SharesApplicationDetail
                context.SharesApplicationDetailMakerCheckers.Attach(sharesApplicationDetailMakerChecker);
                context.Entry(sharesApplicationDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<SharesApplicationViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetSharesApplicationEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SharesApplicationViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetSharesApplicationEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SharesApplicationViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                IEnumerable <SharesApplicationViewModel> sharesApplicationViewModels= await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetSharesApplicationEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
                return sharesApplicationViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesApplicationViewModel> GetViewModelForCreate(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetPersonFinancialAssetEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesApplicationViewModel> GetViewModelForReject(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetPersonFinancialAssetEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesApplicationViewModel> GetViewModelForUnverified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetPersonFinancialAssetEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesApplicationViewModel> GetViewModelForVerified(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetPersonFinancialAssetEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public int GetPrmKeyByNumber(long _sharesApplicationNumber)
        {
            return context.SharesApplications
                    .Where(c => c.ApplicationNumber == _sharesApplicationNumber)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<SharesApplicationViewModel> GetRejectedEntry(long _sharesApplicationNumber)
        {
            try
            {
                SharesApplicationViewModel sharesApplicationViewModel = await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetSharesApplicationEntry (@SharesApplicationId, @EntriesType)", new SqlParameter("@SharesApplicationId", _sharesApplicationNumber), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
                sharesApplicationViewModel.SharesApplicationDetailViewModel = await context.Database.SqlQuery<SharesApplicationDetailViewModel>("SELECT * FROM dbo.GetSharesApplicationDetailEntryBySharesApplicationPrmKey (@SharesApplicationPrmKey, @EntriesType)", new SqlParameter("@SharesApplicationPrmKey", sharesApplicationViewModel.SharesApplicationPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
                return sharesApplicationViewModel;
               
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesApplicationViewModel> GetUnVerifiedEntry(long _sharesApplicationNumber)
        {
            try
            {
                SharesApplicationViewModel sharesApplicationViewModel  =await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetSharesApplicationEntry (@SharesApplicationId, @EntriesType)", new SqlParameter("@SharesApplicationId", _sharesApplicationNumber), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                sharesApplicationViewModel.SharesApplicationDetailViewModel = await context.Database.SqlQuery<SharesApplicationDetailViewModel>("SELECT * FROM dbo.GetSharesApplicationDetailEntryBySharesApplicationPrmKey (@SharesApplicationPrmKey, @EntriesType)", new SqlParameter("@SharesApplicationPrmKey", sharesApplicationViewModel.SharesApplicationPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return sharesApplicationViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<SharesApplicationViewModel> GetVerifiedEntry(long _sharesApplicationNumber)
        {
            try
            {
                SharesApplicationViewModel sharesApplicationViewModel = await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetSharesApplicationEntry (@SharesApplicationId, @EntriesType)", new SqlParameter("@SharesApplicationId", _sharesApplicationNumber), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
                sharesApplicationViewModel.SharesApplicationDetailViewModel = await context.Database.SqlQuery<SharesApplicationDetailViewModel>("SELECT * FROM dbo.GetSharesApplicationDetailEntryBySharesApplicationPrmKey (@SharesApplicationPrmKey, @EntriesType)", new SqlParameter("@SharesApplicationPrmKey", sharesApplicationViewModel.SharesApplicationPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
                return sharesApplicationViewModel;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Modify(SharesApplicationViewModel _sharesApplicationViewModel)
        {
            try
            {
                // Set Default Value
                // Get PrmKey By Id
                int sharesApplicationPrmKey = _sharesApplicationViewModel.SharesApplicationPrmKey;

                _sharesApplicationViewModel.SharesApplicationDetailViewModel.PersonPrmKey = personDetailRepository.GetPersonPrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.PersonId);

                //_sharesApplicationViewModel.NomineePersonInformationNumber = personRepository.GetPrmKeyById(_sharesApplicationViewModel.NomineePersonInformationNumber);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.TransactionTypeId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.BusinessOfficeId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.MemberTypePrmKey = accountDetailRepository.GetMemberTypePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.MemberTypeId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.SchemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.SchemeId);

                // Mapping
                // SharesApplicationModification
                configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel, StringLiteralValue.Create);

                // Set Other Default Value
                _sharesApplicationViewModel.ApplicationStatus = "PEN";
                _sharesApplicationViewModel.StatusReason = "None";
                _sharesApplicationViewModel.TransStatusReason = "None";

                SharesApplicationModification sharesApplicationModification = Mapper.Map<SharesApplicationModification>(_sharesApplicationViewModel);
                sharesApplicationModification.SharesApplicationPrmKey = sharesApplicationPrmKey;
                SharesApplicationModificationMakerChecker sharesApplicationModificationMakerChecker = Mapper.Map<SharesApplicationModificationMakerChecker>(_sharesApplicationViewModel);
                // SharesApplicationTranslation
                SharesApplicationTranslation sharesApplicationTranslation = Mapper.Map<SharesApplicationTranslation>(_sharesApplicationViewModel);
                sharesApplicationTranslation.SharesApplicationPrmKey = sharesApplicationPrmKey;
                SharesApplicationTranslationMakerChecker sharesApplicationTranslationMakerChecker = Mapper.Map<SharesApplicationTranslationMakerChecker>(_sharesApplicationViewModel);
                
                // SharesApplicationDetail
                configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel.SharesApplicationDetailViewModel, StringLiteralValue.Create);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.SharesApplicationPrmKey = sharesApplicationPrmKey;
                SharesApplicationDetail sharesApplicationDetail = Mapper.Map<SharesApplicationDetail>(_sharesApplicationViewModel.SharesApplicationDetailViewModel);
                SharesApplicationDetailMakerChecker sharesApplicationDetailMakerChecker = Mapper.Map<SharesApplicationDetailMakerChecker>(_sharesApplicationViewModel.SharesApplicationDetailViewModel);

                
                // SharesApplicationModification
                context.SharesApplicationModificationMakerCheckers.Attach(sharesApplicationModificationMakerChecker);
                context.Entry(sharesApplicationModificationMakerChecker).State = EntityState.Added;
                sharesApplicationModification.SharesApplicationModificationMakerCheckers.Add(sharesApplicationModificationMakerChecker);

                context.SharesApplicationModifications.Attach(sharesApplicationModification);
                context.Entry(sharesApplicationModification).State = EntityState.Added;

                // SharesApplicationTranslation
                context.SharesApplicationTranslationMakerCheckers.Attach(sharesApplicationTranslationMakerChecker);
                context.Entry(sharesApplicationTranslationMakerChecker).State = EntityState.Added;
                sharesApplicationTranslation.SharesApplicationTranslationMakerCheckers.Add(sharesApplicationTranslationMakerChecker);

                context.SharesApplicationTranslations.Attach(sharesApplicationTranslation);
                context.Entry(sharesApplicationTranslation).State = EntityState.Added;

                // SharesApplicationDetail
                context.SharesApplicationDetailMakerCheckers.Attach(sharesApplicationDetailMakerChecker);
                context.Entry(sharesApplicationDetailMakerChecker).State = EntityState.Added;
                sharesApplicationDetail.SharesApplicationDetailMakerCheckers.Add(sharesApplicationDetailMakerChecker);

                context.SharesApplicationDetails.Attach(sharesApplicationDetail);
                context.Entry(sharesApplicationDetail).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
               string error = ex.Message;
                return false;
            }
        }

        public async Task<IEnumerable<SharesApplicationViewModel>> GetRejectedEntries(long _personPrmKey)
        {
            try
            {
                IEnumerable<SharesApplicationViewModel> sharesApplicationViewModels;

                sharesApplicationViewModels = await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetPersonFinancialAssetEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();

                return sharesApplicationViewModels;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SharesApplicationViewModel>> GetUnverifiedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetPersonFinancialAssetEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<SharesApplicationViewModel>> GetVerifiedEntries(long _personPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<SharesApplicationViewModel>("SELECT * FROM dbo.GetPersonFinancialAssetEntriesByPersonPrmKey (@UserProfilePrmKey, @CenterPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CenterPrmkey", _personPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(SharesApplicationViewModel _sharesApplicationViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel, StringLiteralValue.Reject);
                // Mapping
                SharesApplicationMakerChecker sharesApplicationMakerChecker = Mapper.Map<SharesApplicationMakerChecker>(_sharesApplicationViewModel);
                SharesApplicationModificationMakerChecker sharesApplicationModificationMakerChecker = Mapper.Map<SharesApplicationModificationMakerChecker>(_sharesApplicationViewModel);
                SharesApplicationTranslationMakerChecker sharesApplicationTranslationMakerChecker = Mapper.Map<SharesApplicationTranslationMakerChecker>(_sharesApplicationViewModel);
                configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel.SharesApplicationDetailViewModel, StringLiteralValue.Reject);
                SharesApplicationDetailMakerChecker sharesApplicationDetailMakerChecker = Mapper.Map<SharesApplicationDetailMakerChecker>(_sharesApplicationViewModel.SharesApplicationDetailViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods

                // Check Entry Existance In Modification Table Or Main Table
                if (_sharesApplicationViewModel.SharesApplicationModificationPrmKey == 0)
                {
                    // SharesApplicationMakerChecker
                    context.SharesApplicationMakerCheckers.Attach(sharesApplicationMakerChecker);
                    context.Entry(sharesApplicationMakerChecker).State = EntityState.Added;
                }
                else
                {
                    // SharesApplicationModificationMakerChecker
                    context.SharesApplicationModificationMakerCheckers.Attach(sharesApplicationModificationMakerChecker);
                    context.Entry(sharesApplicationModificationMakerChecker).State = EntityState.Added;
                }

                // SharesApplicationTranslationMakerChecker
                context.SharesApplicationTranslationMakerCheckers.Attach(sharesApplicationTranslationMakerChecker);
                context.Entry(sharesApplicationTranslationMakerChecker).State = EntityState.Added;

                // SharesApplicationDetailMakerChecker
                context.SharesApplicationDetailMakerCheckers.Attach(sharesApplicationDetailMakerChecker);
                context.Entry(sharesApplicationDetailMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        //  *** Transaction Table Entries Are Modified After Verification, So For Current Operation (i.e. Create / Modify) Not Required Modify Old Entries ***
        public async Task<bool> Save(SharesApplicationViewModel _sharesApplicationViewModel)
        {
            try
            {
                // Set Default Value
                configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel, StringLiteralValue.Create);

                // Set Other Default Values
                _sharesApplicationViewModel.ApplicationStatus = StringLiteralValue.Pending;
                _sharesApplicationViewModel.StatusReason = "None";
                _sharesApplicationViewModel.TransStatusReason = "None";

                // Get PrmKey By Id
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.BusinessOfficeId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.MemberTypePrmKey = accountDetailRepository.GetMemberTypePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.MemberTypeId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.PersonPrmKey = personDetailRepository.GetPersonPrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.PersonId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.TransactionTypePrmKey = accountDetailRepository.GetTransactionTypePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.TransactionTypeId);
                _sharesApplicationViewModel.SharesApplicationDetailViewModel.SchemePrmKey = accountDetailRepository.GetSchemePrmKeyById(_sharesApplicationViewModel.SharesApplicationDetailViewModel.SchemeId);

                // Mapping      
                // SharesApplication
                SharesApplication sharesApplication = Mapper.Map<SharesApplication>(_sharesApplicationViewModel);
                SharesApplicationMakerChecker sharesApplicationMakerChecker = Mapper.Map<SharesApplicationMakerChecker>(_sharesApplicationViewModel);

                // SharesApplicationTranslation
                SharesApplicationTranslation sharesApplicationTranslation = Mapper.Map<SharesApplicationTranslation>(_sharesApplicationViewModel);
                SharesApplicationTranslationMakerChecker sharesApplicationTranslationMakerChecker = Mapper.Map<SharesApplicationTranslationMakerChecker>(_sharesApplicationViewModel);

                // SharesApplicationTranslation
                configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel.SharesApplicationDetailViewModel, StringLiteralValue.Create);
                SharesApplicationDetail sharesApplicationDetail = Mapper.Map<SharesApplicationDetail>(_sharesApplicationViewModel.SharesApplicationDetailViewModel);
                SharesApplicationDetailMakerChecker sharesApplicationDetailMakerChecker = Mapper.Map<SharesApplicationDetailMakerChecker>(_sharesApplicationViewModel.SharesApplicationDetailViewModel);

                // SharesApplicationMakerChecker
                context.SharesApplicationMakerCheckers.Attach(sharesApplicationMakerChecker);
                context.Entry(sharesApplicationMakerChecker).State = EntityState.Added;
                sharesApplication.SharesApplicationMakerCheckers.Add(sharesApplicationMakerChecker);

                context.SharesApplications.Attach(sharesApplication);
                context.Entry(sharesApplication).State = EntityState.Added;

                // SharesApplicationTranslationMakerChecker
                context.SharesApplicationTranslationMakerCheckers.Attach(sharesApplicationTranslationMakerChecker);
                context.Entry(sharesApplicationTranslationMakerChecker).State = EntityState.Added;
                sharesApplicationTranslation.SharesApplicationTranslationMakerCheckers.Add(sharesApplicationTranslationMakerChecker);

                context.SharesApplicationTranslations.Attach(sharesApplicationTranslation);
                context.Entry(sharesApplicationTranslation).State = EntityState.Added;
                sharesApplication.SharesApplicationTranslations.Add(sharesApplicationTranslation);

                // SharesApplicationDetailMakerChecker
                context.SharesApplicationDetailMakerCheckers.Attach(sharesApplicationDetailMakerChecker);
                context.Entry(sharesApplicationDetailMakerChecker).State = EntityState.Added;
                sharesApplicationDetail.SharesApplicationDetailMakerCheckers.Add(sharesApplicationDetailMakerChecker);

                context.SharesApplicationDetails.Attach(sharesApplicationDetail);
                context.Entry(sharesApplicationDetail).State = EntityState.Added;
                sharesApplication.SharesApplicationDetails.Add(sharesApplicationDetail);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(SharesApplicationViewModel _sharesApplicationViewModel)
        {
            try
            {
                 // Set Default Value
                 configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel, StringLiteralValue.Verify);

                if (_sharesApplicationViewModel.SharesApplicationModificationPrmKey > 0)
                {
                    // Modify Old Record 
                    SharesApplicationViewModel sharesApplicationViewModelForModify = await GetVerifiedEntry(_sharesApplicationViewModel.ApplicationNumber);
                    // Set Default Value
                    configurationDetailRepository.SetDefaultValues(sharesApplicationViewModelForModify, StringLiteralValue.Modify);
                    
                    // SharesApplicationTranslation
                    SharesApplicationTranslationMakerChecker sharesApplicationTranslationMakerCheckerForModify = Mapper.Map<SharesApplicationTranslationMakerChecker>(sharesApplicationViewModelForModify);
                    context.SharesApplicationTranslationMakerCheckers.Attach(sharesApplicationTranslationMakerCheckerForModify);
                    context.Entry(sharesApplicationTranslationMakerCheckerForModify).State = EntityState.Added;

                    // Save Data In Appropriate Tables By Entity Framework Methods

                    // Check Entry Existance In Modification Table Or Main Table
                    if (sharesApplicationViewModelForModify.IsModified == true)
                    {
                        SharesApplicationModificationMakerChecker sharesApplicationModificationMakerCheckerForModify = Mapper.Map<SharesApplicationModificationMakerChecker>(sharesApplicationViewModelForModify);

                        context.SharesApplicationModificationMakerCheckers.Attach(sharesApplicationModificationMakerCheckerForModify);
                        context.Entry(sharesApplicationModificationMakerCheckerForModify).State = EntityState.Added;
                    }

                    // Verify New Record
                    // Set Default Value
                    configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel.SharesApplicationDetailViewModel, StringLiteralValue.Verify);
                    _sharesApplicationViewModel.SharesApplicationTranslationPrmKey = _sharesApplicationViewModel.SharesApplicationTranslationPrmKey;
                    SharesApplicationModificationMakerChecker sharesApplicationModificationMakerChecker = Mapper.Map<SharesApplicationModificationMakerChecker>(_sharesApplicationViewModel);
                    SharesApplicationTranslationMakerChecker sharesApplicationTranslationMakerChecker = Mapper.Map<SharesApplicationTranslationMakerChecker>(_sharesApplicationViewModel);
                    SharesApplicationDetailMakerChecker sharesApplicationDetailMakerChecker = Mapper.Map<SharesApplicationDetailMakerChecker>(_sharesApplicationViewModel.SharesApplicationDetailViewModel);

                    // SharesApplicationModificationMakerChecker
                    context.SharesApplicationModificationMakerCheckers.Attach(sharesApplicationModificationMakerChecker);
                    context.Entry(sharesApplicationModificationMakerChecker).State = EntityState.Added;

                    // SharesApplicationTranslationMakerChecker
                    context.SharesApplicationTranslationMakerCheckers.Attach(sharesApplicationTranslationMakerChecker);
                    context.Entry(sharesApplicationTranslationMakerChecker).State = EntityState.Added;

                    // SharesApplicationDetailMakerChecker
                    context.SharesApplicationDetailMakerCheckers.Attach(sharesApplicationDetailMakerChecker);
                    context.Entry(sharesApplicationDetailMakerChecker).State = EntityState.Added;
                }
                else
                {
                    configurationDetailRepository.SetDefaultValues(_sharesApplicationViewModel.SharesApplicationDetailViewModel, StringLiteralValue.Verify);
                    SharesApplicationMakerChecker sharesApplicationMakerChecker = Mapper.Map<SharesApplicationMakerChecker>(_sharesApplicationViewModel);
                    SharesApplicationTranslationMakerChecker sharesApplicationTranslationMakerChecker = Mapper.Map<SharesApplicationTranslationMakerChecker>(_sharesApplicationViewModel);
                    SharesApplicationDetailMakerChecker sharesApplicationDetailMakerChecker = Mapper.Map<SharesApplicationDetailMakerChecker>(_sharesApplicationViewModel.SharesApplicationDetailViewModel);

                    // SharesApplicationMakerChecker
                    context.SharesApplicationMakerCheckers.Attach(sharesApplicationMakerChecker);
                    context.Entry(sharesApplicationMakerChecker).State = EntityState.Added;

                    // SharesApplicationTranslationMakerChecker
                    context.SharesApplicationTranslationMakerCheckers.Attach(sharesApplicationTranslationMakerChecker);
                    context.Entry(sharesApplicationTranslationMakerChecker).State = EntityState.Added;

                    // SharesApplicationDetailMakerChecker
                    context.SharesApplicationDetailMakerCheckers.Attach(sharesApplicationDetailMakerChecker);
                    context.Entry(sharesApplicationDetailMakerChecker).State = EntityState.Added;
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
