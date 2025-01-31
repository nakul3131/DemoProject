using AutoMapper;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserRoleProfileRepository : IUserRoleProfileRepository
    {
        private readonly EFDbContext context;
        private readonly IUserProfileDetailRepository userProfileDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

      
        public EFUserRoleProfileRepository(RepositoryConnection _connection, IUserProfileDetailRepository _userProfileDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ISecurityDetailRepository _securityDetailRepository)
        {
            context = _connection.EFDbContext;
            userProfileDetailRepository = _userProfileDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            securityDetailRepository = _securityDetailRepository;
        }
        public async Task<bool> Amend(UserRoleProfileViewModel _userRoleProfileViewModel)
        {
            try
            {

                userProfileDetailRepository.GetUserRoleProfileDefaultValues(_userRoleProfileViewModel, StringLiteralValue.Amend);


                // Amend Old UserRoleProfileMenu
                IEnumerable<UserRoleProfileViewModel> userRoleProfileViewModelListForAmend = await userProfileDetailRepository.GetUserRoleProfileEntries(_userRoleProfileViewModel.UserProfilePrmKey, StringLiteralValue.Reject);
                foreach (UserRoleProfileViewModel viewModel in userRoleProfileViewModelListForAmend)
                {
                    // Set Deafult Value
                    viewModel.PrmKey = 0;
                    userProfileDetailRepository.GetUserRoleProfileDefaultValues(viewModel, StringLiteralValue.Amend);

                    UserRoleProfileMakerChecker userRoleProfileMakerCheckerForAmend = Mapper.Map<UserRoleProfileMakerChecker>(viewModel);
                    userRoleProfileMakerCheckerForAmend.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    context.UserRoleProfileMakerCheckers.Attach(userRoleProfileMakerCheckerForAmend);
                    context.Entry(userRoleProfileMakerCheckerForAmend).State = EntityState.Added;
                }

                //Get UserRoleProfile From Session Object New Added Record / Updated Record
                List<UserRoleProfileViewModel> UserRoleProfileViewModelList = (List<UserRoleProfileViewModel>)HttpContext.Current.Session["UserRoleProfile"];

                foreach (UserRoleProfileViewModel viewModel in UserRoleProfileViewModelList)
                {
                    // Set Deafult Value
                    viewModel.PrmKey = 0;
                    viewModel.UserRoleProfilePrmKey = 0;
                    userProfileDetailRepository.GetUserRoleProfileDefaultValues(viewModel, StringLiteralValue.Create);

                    // Get Prmkey By Id
                    viewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(viewModel.BusinessOfficeId);
                    viewModel.RoleProfilePrmKey = securityDetailRepository.GetRoleProfilePrmKeyById(viewModel.RoleProfileId);

                    UserRoleProfile userRoleProfile = Mapper.Map<UserRoleProfile>(viewModel);
                    userRoleProfile.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    UserRoleProfileMakerChecker userRoleProfileMakerChecker = Mapper.Map<UserRoleProfileMakerChecker>(viewModel);
                    userRoleProfileMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    context.UserRoleProfileMakerCheckers.Attach(userRoleProfileMakerChecker);
                    context.Entry(userRoleProfileMakerChecker).State = EntityState.Added;
                    userRoleProfile.UserRoleProfileMakerCheckers.Add(userRoleProfileMakerChecker);

                    context.UserRoleProfiles.Attach(userRoleProfile);
                    context.Entry(userRoleProfile).State = EntityState.Added;
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

        public async Task<bool> Delete(UserRoleProfileViewModel _userRoleProfileViewModel)
        {
            try
            {
                // Set Default Value
                userProfileDetailRepository.GetUserRoleProfileDefaultValues(_userRoleProfileViewModel, StringLiteralValue.Delete);

                //UserRoleProfile
                List<UserRoleProfileViewModel> userRoleProfileViewModelList = (List<UserRoleProfileViewModel>)HttpContext.Current.Session["UserRoleProfile"];

                foreach (UserRoleProfileViewModel viewModel in userRoleProfileViewModelList)
                {
                    // Set Default Value
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    userProfileDetailRepository.GetUserRoleProfileDefaultValues(viewModel, StringLiteralValue.Delete);

                    UserRoleProfileMakerChecker userRoleProfileMakerChecker = Mapper.Map<UserRoleProfileMakerChecker>(viewModel);

                    context.UserRoleProfileMakerCheckers.Attach(userRoleProfileMakerChecker);
                    context.Entry(userRoleProfileMakerChecker).State = EntityState.Added;
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

        public short GetPrmKeyById(Guid _userRoleProfileId)
        {
            return context.RoleProfiles
                    .Where(c => c.RoleProfileId == _userRoleProfileId)
                    .Select(c => c.PrmKey).FirstOrDefault();
        }

        public async Task<IEnumerable<UserRoleProfileViewModel>> GetRejectedUserRoleProfileEntry(short _userProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<UserRoleProfileViewModel>("SELECT * FROM dbo.GetUserRoleProfileEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserRoleProfileIndexViewModel>> GetRejectedUserRoleProfileEntries(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserRoleProfileIndexViewModel>("SELECT * FROM dbo.GetUserRoleProfileEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserRoleProfileViewModel>> GetUnverifiedUserRoleProfileEntry(short _userProfilePrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<UserRoleProfileViewModel>("SELECT * FROM dbo.GetUserRoleProfileEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserRoleProfileViewModel>> GetVerifiedUserRoleProfileEntries(short _userProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<UserRoleProfileViewModel>("SELECT * FROM dbo.GetUserRoleProfileEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(UserRoleProfileViewModel _userRoleProfileViewModel)
        {

            try
            {

                //userProfileDetailRepository.GetUserRoleProfileDefaultValues(_UserRoleProfileViewModel, StringLiteralValue.Create);

                //Get UserRoleProfile From Session Object New Added Record / Updated Record
                //Get UserRoleProfile From Session Object New Added Record / Updated Record
                List<UserRoleProfileViewModel> userRoleProfileViewModelList = (List<UserRoleProfileViewModel>)HttpContext.Current.Session["UserRoleProfile"];

                 foreach (UserRoleProfileViewModel viewModel in userRoleProfileViewModelList)
                {
                    if (viewModel.UserProfilePrmKey == 0)
                    {
                       viewModel.ActivationStatus = StringLiteralValue.Active;
                       viewModel.EntryDateTime = DateTime.Now;
                       viewModel.EntryStatus = StringLiteralValue.Create;
                       viewModel.UserProfilePrmKey = _userRoleProfileViewModel.UserProfilePrmKey;
                       viewModel.Note = _userRoleProfileViewModel.Note;

                    if (viewModel.Note == null)
                        viewModel.Note = "None";

                    viewModel.Remark = "None";
                  
                    viewModel.UserAction = StringLiteralValue.Create;


                    // Set Deafult Value
                    //userProfileDetailRepository.GetUserRoleProfileDefaultValues(viewModel, StringLiteralValue.Create);
                    
                        // Get Prmkey By Id
                        viewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(viewModel.BusinessOfficeId);
                        viewModel.RoleProfilePrmKey = securityDetailRepository.GetRoleProfilePrmKeyById(viewModel.RoleProfileId);

                        UserRoleProfile userRoleProfile = Mapper.Map<UserRoleProfile>(viewModel);
                        UserRoleProfileMakerChecker userRoleProfileMakerChecker = Mapper.Map<UserRoleProfileMakerChecker>(viewModel);
                        //UserRoleProfileMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        context.UserRoleProfiles.Attach(userRoleProfile);
                        context.Entry(userRoleProfile).State = EntityState.Added;

                        userRoleProfileMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        context.UserRoleProfileMakerCheckers.Attach(userRoleProfileMakerChecker);
                        context.Entry(userRoleProfileMakerChecker).State = EntityState.Added;
                        userRoleProfile.UserRoleProfileMakerCheckers.Add(userRoleProfileMakerChecker);
                    }

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

        public async Task<IEnumerable<UserRoleProfileIndexViewModel>> GetUserRoleProfileUnverifiedIndex(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserRoleProfileIndexViewModel>("SELECT * FROM dbo.GetUserRoleProfileEntries (@UserProfilePrmKey, @EntryType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntryType", _entryType)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Reject(UserRoleProfileViewModel _userRoleProfileViewModel)
        {
            try
            {
                //UserRoleProfile
                List<UserRoleProfileViewModel> userRoleProfileViewModelList = (List<UserRoleProfileViewModel>)HttpContext.Current.Session["UserRoleProfile"];

                foreach (UserRoleProfileViewModel viewModel in userRoleProfileViewModelList)
                {
                    // Set Deafult Value
                   
                    userProfileDetailRepository.GetUserRoleProfileDefaultValues(viewModel, StringLiteralValue.Reject);

                    UserRoleProfileMakerChecker userRoleProfileMakerChecker = Mapper.Map<UserRoleProfileMakerChecker>(viewModel);
                    userRoleProfileMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    context.UserRoleProfileMakerCheckers.Attach(userRoleProfileMakerChecker);
                    context.Entry(userRoleProfileMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Verify(UserRoleProfileViewModel _userRoleProfileViewModel)
        {
            try
            {
                // Modify Old UserRoleProfile
                IEnumerable<UserRoleProfileViewModel> userRoleProfileViewModelListForModify = await userProfileDetailRepository.GetUserRoleProfileEntries(_userRoleProfileViewModel.UserProfilePrmKey, StringLiteralValue.Verify);
                foreach (UserRoleProfileViewModel viewModel in userRoleProfileViewModelListForModify)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    UserRoleProfileMakerChecker userRoleProfileMakerCheckerForModify = Mapper.Map<UserRoleProfileMakerChecker>(viewModel);
                  
                    //OrganizationLoanType
                    context.UserRoleProfileMakerCheckers.Attach(userRoleProfileMakerCheckerForModify);
                    context.Entry(userRoleProfileMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify Record
                List<UserRoleProfileViewModel> userRoleProfileViewModelViewModelList = new List<UserRoleProfileViewModel>();
                userRoleProfileViewModelViewModelList = (List<UserRoleProfileViewModel>)HttpContext.Current.Session["UserRoleProfile"];

                foreach (UserRoleProfileViewModel viewModel in userRoleProfileViewModelViewModelList)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Verify;
                    viewModel.PrmKey = 0;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    UserRoleProfileMakerChecker userRoleProfileMakerChecker = Mapper.Map<UserRoleProfileMakerChecker>(viewModel);
                    context.UserRoleProfileMakerCheckers.Attach(userRoleProfileMakerChecker);
                    context.Entry(userRoleProfileMakerChecker).State = EntityState.Added;
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
