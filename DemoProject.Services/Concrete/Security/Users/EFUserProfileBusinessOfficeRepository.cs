using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.Abstract.Enterprise;
using DemoProject.Services.Abstract.Security;
using DemoProject.Services.Abstract.Security.Users;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Security.Users;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Security.Users
{
    public class EFUserProfileBusinessOfficeRepository : IUserProfileBusinessOfficeRepository
    {
        private readonly EFDbContext context;
        private readonly IUserProfileDetailRepository userProfileDetailRepository;
        private readonly IEnterpriseDetailRepository enterpriseDetailRepository;
        private readonly ISecurityDetailRepository securityDetailRepository;

        public EFUserProfileBusinessOfficeRepository(RepositoryConnection _connection, IUserProfileDetailRepository _userProfileDetailRepository, IEnterpriseDetailRepository _enterpriseDetailRepository, ISecurityDetailRepository _securityDetailRepository)
        {
            context = _connection.EFDbContext;
            userProfileDetailRepository = _userProfileDetailRepository;
            enterpriseDetailRepository = _enterpriseDetailRepository;
            securityDetailRepository = _securityDetailRepository;

        }

        public async Task<IEnumerable<UserProfileBusinessOfficeViewModel>> GetRejectedUserProfileBusinessOfficeEntry(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileBusinessOfficeViewModel>("SELECT * FROM dbo.GetUserProfileBusinessOfficeEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileBusinessOfficeIndexViewModel>> GetRejectedUserProfileBusinessOfficeEntries(string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileBusinessOfficeIndexViewModel>("SELECT * FROM dbo.GetUserProfileBusinessOfficeEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<IEnumerable<UserProfileBusinessOfficeIndexViewModel>> GetUnverifiedUserProfileBusinessOfficeEntries( string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileBusinessOfficeIndexViewModel>("SELECT * FROM dbo.GetUserProfileBusinessOfficeEntries (@UserProfilePrmKey,  @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileBusinessOfficeViewModel>> GetUnverifiedUserProfileBusinessOfficeEntry(short _userProfilePrmKey, string _entryType)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileBusinessOfficeViewModel>("SELECT * FROM dbo.GetUserProfileBusinessOfficeEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<UserProfileBusinessOfficeViewModel>> GetVerifiedUserProfileBusinessOfficeEntries(short _userProfilePrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<UserProfileBusinessOfficeViewModel>("SELECT * FROM dbo.GetUserProfileBusinessOfficeEntriesByUserProfilePrmKey (@UserProfilePrmKey, @SelectedUserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@SelectedUserProfilePrmKey", _userProfilePrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<bool> Modify(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel)
        {
           try
            {
                List<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModelList = (List<UserProfileBusinessOfficeViewModel>)HttpContext.Current.Session["UserProfileBusinessOffice"];

                foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModelList)
                {
                    if (viewModel.UserProfilePrmKey == 0)
                    {
                        viewModel.ActivationStatus = StringLiteralValue.Active;
                        viewModel.EntryDateTime = DateTime.Now;
                        viewModel.EntryStatus = StringLiteralValue.Create;
                        viewModel.UserProfilePrmKey = _userProfileBusinessOfficeViewModel.UserProfilePrmKey;
                        viewModel.Note = _userProfileBusinessOfficeViewModel.Note;

                        if (viewModel.Note == null)
                            viewModel.Note = "None";

                        viewModel.Remark = "None";

                        viewModel.UserAction = StringLiteralValue.Create;
                       
                        //Set Deafult Value
                        //userProfileDetailRepository.GetUserProfileBusinessOfficeDefaultValues(viewModel, StringLiteralValue.Create);

                        //Get Prmkey By Id
                        viewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(viewModel.BusinessOfficeId);

                        UserProfileBusinessOffice userProfileBusinessOffice = Mapper.Map<UserProfileBusinessOffice>(viewModel);
                        UserProfileBusinessOfficeMakerChecker userProfileBusinessOfficeMakerChecker = Mapper.Map<UserProfileBusinessOfficeMakerChecker>(viewModel);
                        //userProfileBusinessOfficeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        context.UserProfileBusinessOffices.Attach(userProfileBusinessOffice);
                        context.Entry(userProfileBusinessOffice).State = EntityState.Added;

                        userProfileBusinessOfficeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                        context.UserProfileBusinessOfficeMakerCheckers.Attach(userProfileBusinessOfficeMakerChecker);
                        context.Entry(userProfileBusinessOfficeMakerChecker).State = EntityState.Added;
                        userProfileBusinessOffice.UserProfileBusinessOfficeMakerCheckers.Add(userProfileBusinessOfficeMakerChecker);
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

        public async Task<bool> Verify(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel)
        {
            try
            {
                // Modify Old UserProfileBusinessOffice
                IEnumerable<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModelListForModify = await userProfileDetailRepository.GetBusinessOfficeEntries(_userProfileBusinessOfficeViewModel.UserProfilePrmKey, StringLiteralValue.Verify);
                foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModelListForModify)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;

                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    UserProfileBusinessOfficeMakerChecker userProfileBusinessOfficeMakerCheckerForModify = Mapper.Map<UserProfileBusinessOfficeMakerChecker>(viewModel);

                    //UserProfileBusinessOfficeMakerChecker
                    context.UserProfileBusinessOfficeMakerCheckers.Attach(userProfileBusinessOfficeMakerCheckerForModify);
                    context.Entry(userProfileBusinessOfficeMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify Record
                List<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModelList = new List<UserProfileBusinessOfficeViewModel>();
                userProfileBusinessOfficeViewModelList = (List<UserProfileBusinessOfficeViewModel>)HttpContext.Current.Session["UserProfileBusinessOffice"];

                foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModelList)
                {
                    //Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.EntryStatus = StringLiteralValue.Verify;
                    viewModel.PrmKey = 0;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    UserProfileBusinessOfficeMakerChecker userProfileBusinessOfficeMakerChecker = Mapper.Map<UserProfileBusinessOfficeMakerChecker>(viewModel);
                    context.UserProfileBusinessOfficeMakerCheckers.Attach(userProfileBusinessOfficeMakerChecker);
                    context.Entry(userProfileBusinessOfficeMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Reject(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel)
        {
            try
            {
                //UserProfileBusinessOffice
                List<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModelList = (List<UserProfileBusinessOfficeViewModel>)HttpContext.Current.Session["UserProfileBusinessOffice"];

                foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModelList)
                {
                    // Set Deafult Value

                    userProfileDetailRepository.GetUserProfileBusinessOfficeDefaultValues(viewModel, StringLiteralValue.Reject);

                    UserProfileBusinessOfficeMakerChecker userProfileBusinessOfficeMakerChecker = Mapper.Map<UserProfileBusinessOfficeMakerChecker>(viewModel);
                    userProfileBusinessOfficeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    context.UserProfileBusinessOfficeMakerCheckers.Attach(userProfileBusinessOfficeMakerChecker);
                    context.Entry(userProfileBusinessOfficeMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Amend(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel)
        {
            try
            {

                userProfileDetailRepository.GetUserProfileBusinessOfficeDefaultValues(_userProfileBusinessOfficeViewModel, StringLiteralValue.Amend);


                // Amend Old UserRoleProfileMenu
                IEnumerable<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModelListForAmend = await userProfileDetailRepository.GetBusinessOfficeEntries(_userProfileBusinessOfficeViewModel.UserProfilePrmKey, StringLiteralValue.Reject);
                foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModelListForAmend)
                {
                    // Set Deafult Value
                    viewModel.PrmKey = 0;
                    userProfileDetailRepository.GetUserProfileBusinessOfficeDefaultValues(viewModel, StringLiteralValue.Amend);

                    UserProfileBusinessOfficeMakerChecker userProfileBusinessOfficeMakerCheckerForAmend = Mapper.Map<UserProfileBusinessOfficeMakerChecker>(viewModel);
                    userProfileBusinessOfficeMakerCheckerForAmend.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    context.UserProfileBusinessOfficeMakerCheckers.Attach(userProfileBusinessOfficeMakerCheckerForAmend);
                    context.Entry(userProfileBusinessOfficeMakerCheckerForAmend).State = EntityState.Added;
                }

                //Get UserRoleProfile From Session Object New Added Record / Updated Record
                List<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModelList = (List<UserProfileBusinessOfficeViewModel>)HttpContext.Current.Session["UserProfileBusinessOffice"];

                foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModelList)
                {
                    // Set Deafult Value
                    viewModel.PrmKey = 0;
                    viewModel.UserProfileBusinessOfficePrmKey = 0;
                    
                    userProfileDetailRepository.GetUserProfileBusinessOfficeDefaultValues(viewModel, StringLiteralValue.Create);

                    // Get Prmkey By Id
                     viewModel.BusinessOfficePrmKey = enterpriseDetailRepository.GetBusinessOfficePrmKeyById(viewModel.BusinessOfficeId);
                    //viewModel.BusinessOfficePrmKey = securityDetailRepository.GetRoleProfilePrmKeyById(viewModel.BusinessOfficeId);

                    UserProfileBusinessOffice userProfileBusinessOffice = Mapper.Map<UserProfileBusinessOffice>(viewModel);
                    userProfileBusinessOffice.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    UserProfileBusinessOfficeMakerChecker userProfileBusinessOfficeMakerChecker = Mapper.Map<UserProfileBusinessOfficeMakerChecker>(viewModel);
                    userProfileBusinessOfficeMakerChecker.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    context.UserProfileBusinessOfficeMakerCheckers.Attach(userProfileBusinessOfficeMakerChecker);
                    context.Entry(userProfileBusinessOfficeMakerChecker).State = EntityState.Added;
                    userProfileBusinessOffice.UserProfileBusinessOfficeMakerCheckers.Add(userProfileBusinessOfficeMakerChecker);

                    context.UserProfileBusinessOffices.Attach(userProfileBusinessOffice);
                    context.Entry(userProfileBusinessOffice).State = EntityState.Added;
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

        public async Task<bool> Delete(UserProfileBusinessOfficeViewModel _userProfileBusinessOfficeViewModel)
        {
            
            try
            {
                // Set Default Value
                userProfileDetailRepository.GetUserProfileBusinessOfficeDefaultValues(_userProfileBusinessOfficeViewModel, StringLiteralValue.Delete);

                //UserRoleProfile
                List<UserProfileBusinessOfficeViewModel> userProfileBusinessOfficeViewModellList = (List<UserProfileBusinessOfficeViewModel>)HttpContext.Current.Session["UserProfileBusinessOffice"];

                foreach (UserProfileBusinessOfficeViewModel viewModel in userProfileBusinessOfficeViewModellList)
                {
                    // Set Default Value
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                    userProfileDetailRepository.GetUserProfileBusinessOfficeDefaultValues(viewModel, StringLiteralValue.Delete);

                    UserProfileBusinessOfficeMakerChecker userProfileBusinessOfficeMakerChecker = Mapper.Map<UserProfileBusinessOfficeMakerChecker>(viewModel);

                    context.UserProfileBusinessOfficeMakerCheckers.Attach(userProfileBusinessOfficeMakerChecker);
                    context.Entry(userProfileBusinessOfficeMakerChecker).State = EntityState.Added;
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
