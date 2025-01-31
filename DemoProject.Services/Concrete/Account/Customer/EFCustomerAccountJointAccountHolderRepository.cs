using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using DemoProject.Domain.Entities.Account.Customer;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.PersonInformation;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerJointAccountHolderRepository : ICustomerJointAccountHolderRepository
    {
        private readonly EFDbContext context;
        private readonly IPersonRepository personRepository;


        public EFCustomerJointAccountHolderRepository(RepositoryConnection _connection, IPersonRepository _personRepository)
        {
            context = _connection.EFDbContext;
            personRepository = _personRepository;
        }

        public async Task<bool> Amend(CustomerJointAccountHolderViewModel _customerAccountJointAccountHolderViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountJointAccountHolderViewModel.ActivationStatus = StringLiteralValue.Active;
                _customerAccountJointAccountHolderViewModel.EntryDateTime = DateTime.Now;
                _customerAccountJointAccountHolderViewModel.EntryStatus = StringLiteralValue.Amend;
                _customerAccountJointAccountHolderViewModel.ReasonForModification = "None";
                _customerAccountJointAccountHolderViewModel.Remark = "None";
                _customerAccountJointAccountHolderViewModel.UserAction = StringLiteralValue.Amend;
                _customerAccountJointAccountHolderViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Amend First Old Record
                // CustomerJointAccount
                IEnumerable<CustomerJointAccountHolderViewModel> customerAccountJointAccountHolderViewModels = await GetRejectedEntries(_customerAccountJointAccountHolderViewModel.CustomerAccountPrmKey);

                foreach (CustomerJointAccountHolderViewModel viewModel in customerAccountJointAccountHolderViewModels)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    CustomerJointAccountHolderMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerJointAccountHolderMakerChecker>(viewModel);

                    // CustomerJointAccountHolderMakerChecker
                    context.CustomerJointAccountHolderMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                }

                // Insert New Updated Record - Get Record From Session Object
                List<CustomerJointAccountHolderViewModel> customerAccountJointAccountHolderViewModelList = new List<CustomerJointAccountHolderViewModel>();
                customerAccountJointAccountHolderViewModelList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["CustomerJointAccountHolder"];

                foreach (CustomerJointAccountHolderViewModel viewModel in customerAccountJointAccountHolderViewModelList)
                {
                    // Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerAccountJointAccountHolderViewModel.Remark;
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    //viewModel.PersonPrmKey = personRepository.GetPrmKeyById(viewModel.PersonId);

                    CustomerJointAccountHolder customerAccountJointAccountHolder = Mapper.Map<CustomerJointAccountHolder>(viewModel);
                    customerAccountJointAccountHolder.CustomerAccountPrmKey = _customerAccountJointAccountHolderViewModel.CustomerAccountPrmKey;

                    CustomerJointAccountHolderMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerJointAccountHolderMakerChecker>(viewModel);

                    context.CustomerJointAccountHolderMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                    customerAccountJointAccountHolder.CustomerJointAccountHolderMakerCheckers.Add(customerAccountJointAccountHolderMakerChecker);

                    context.CustomerJointAccountHolders.Attach(customerAccountJointAccountHolder);
                    context.Entry(customerAccountJointAccountHolder).State = EntityState.Added;
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

        public async Task<bool> Delete(CustomerJointAccountHolderViewModel _customerAccountJointAccountHolderViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountJointAccountHolderViewModel.EntryDateTime = DateTime.Now;
                _customerAccountJointAccountHolderViewModel.ReasonForModification = "None";
                _customerAccountJointAccountHolderViewModel.Remark = "None";
                _customerAccountJointAccountHolderViewModel.UserAction = StringLiteralValue.Delete;
                _customerAccountJointAccountHolderViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerJointAccount
                List<CustomerJointAccountHolderViewModel> customerAccountJointAccountHolderViewModelList = new List<CustomerJointAccountHolderViewModel>();
                customerAccountJointAccountHolderViewModelList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["CustomerJointAccountHolder"];

                foreach (CustomerJointAccountHolderViewModel viewModel in customerAccountJointAccountHolderViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerAccountJointAccountHolderViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    CustomerJointAccountHolderMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerJointAccountHolderMakerChecker>(viewModel);

                    // CustomerJointAccountHolderMakerChecker
                    context.CustomerJointAccountHolderMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
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
        public async Task<bool> Modify(CustomerJointAccountHolderViewModel _customerAccountJointAccountHolderViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountJointAccountHolderViewModel.EntryDateTime = DateTime.Now;
                _customerAccountJointAccountHolderViewModel.EntryStatus = StringLiteralValue.Create;
                _customerAccountJointAccountHolderViewModel.Remark = "None";
                _customerAccountJointAccountHolderViewModel.UserAction = StringLiteralValue.Create;
                _customerAccountJointAccountHolderViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerJointAccount
                List<CustomerJointAccountHolderViewModel> customerAccountJointAccountHolderList = new List<CustomerJointAccountHolderViewModel>();
                customerAccountJointAccountHolderList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["CustomerJointAccountHolder"];

                foreach (CustomerJointAccountHolderViewModel viewModel in customerAccountJointAccountHolderList)
                {
                    // Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.CustomerAccountPrmKey = _customerAccountJointAccountHolderViewModel.CustomerAccountPrmKey;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    //viewModel.PersonPrmKey = personRepository.GetPrmKeyById(viewModel.PersonId);

                    // Mapping CustomerJointAccount
                    CustomerJointAccountHolder customerAccountJointAccountHolder = Mapper.Map<CustomerJointAccountHolder>(_customerAccountJointAccountHolderViewModel);
                    CustomerJointAccountHolderMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerJointAccountHolderMakerChecker>(_customerAccountJointAccountHolderViewModel);

                    // CustomerJointAccount
                    context.CustomerJointAccountHolders.Attach(customerAccountJointAccountHolder);
                    context.Entry(customerAccountJointAccountHolder).State = EntityState.Added;

                    // CustomerJointAccountMakerChecker
                    context.CustomerJointAccountHolderMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                    customerAccountJointAccountHolder.CustomerJointAccountHolderMakerCheckers.Add(customerAccountJointAccountHolderMakerChecker);
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

        public async Task<bool> Reject(CustomerJointAccountHolderViewModel _customerAccountJointAccountHolderViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountJointAccountHolderViewModel.EntryDateTime = DateTime.Now;
                _customerAccountJointAccountHolderViewModel.UserAction = StringLiteralValue.Reject;
                _customerAccountJointAccountHolderViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerJointAccount
                List<CustomerJointAccountHolderViewModel> customerAccountJointAccountHolderViewModelList = new List<CustomerJointAccountHolderViewModel>();
                customerAccountJointAccountHolderViewModelList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["CustomerJointAccountHolder"];

                foreach (CustomerJointAccountHolderViewModel viewModel in customerAccountJointAccountHolderViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerAccountJointAccountHolderViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    CustomerJointAccountHolderMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerJointAccountHolderMakerChecker>(viewModel);

                    // CustomerJointAccountHolderMakerCheckers
                    context.CustomerJointAccountHolderMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
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

        public async Task<bool> Save(CustomerJointAccountHolderViewModel _customerAccountJointAccountHolderViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountJointAccountHolderViewModel.ActivationStatus = StringLiteralValue.Active;
                _customerAccountJointAccountHolderViewModel.EntryDateTime = DateTime.Now;
                _customerAccountJointAccountHolderViewModel.EntryStatus = StringLiteralValue.Create;
                _customerAccountJointAccountHolderViewModel.ReasonForModification = "None";
                _customerAccountJointAccountHolderViewModel.Remark = "None";
                _customerAccountJointAccountHolderViewModel.UserAction = StringLiteralValue.Create;
                _customerAccountJointAccountHolderViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerJointAccount
                List<CustomerJointAccountHolderViewModel> customerAccountJointAccountHolderList = new List<CustomerJointAccountHolderViewModel>();
                customerAccountJointAccountHolderList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["CustomerJointAccountHolder"];

                foreach (CustomerJointAccountHolderViewModel viewModel in customerAccountJointAccountHolderList)
                {
                    // Set Default Value
                    viewModel.ActivationStatus = StringLiteralValue.Active;
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.CustomerAccountPrmKey = _customerAccountJointAccountHolderViewModel.CustomerAccountPrmKey;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id
                    //viewModel.PersonPrmKey = personRepository.GetPrmKeyById(viewModel.PersonId);

                    // Mapping CustomerJointAccount
                    CustomerJointAccountHolder customerAccountJointAccountHolder = Mapper.Map<CustomerJointAccountHolder>(_customerAccountJointAccountHolderViewModel);
                    CustomerJointAccountHolderMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerJointAccountHolderMakerChecker>(_customerAccountJointAccountHolderViewModel);

                    // CustomerJointAccount
                    context.CustomerJointAccountHolders.Attach(customerAccountJointAccountHolder);
                    context.Entry(customerAccountJointAccountHolder).State = EntityState.Added;

                    // CustomerJointAccountMakerChecker
                    context.CustomerJointAccountHolderMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                    customerAccountJointAccountHolder.CustomerJointAccountHolderMakerCheckers.Add(customerAccountJointAccountHolderMakerChecker);
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

        public async Task<bool> Verify(CustomerJointAccountHolderViewModel _customerAccountJointAccountHolderViewModel)
        {
            try
            {
                // Set Default Value
                _customerAccountJointAccountHolderViewModel.EntryDateTime = DateTime.Now;
                _customerAccountJointAccountHolderViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerJointAccount
                IEnumerable<CustomerJointAccountHolderViewModel> customerAccountJointAccountHolderViewModels = await GetVerifiedEntries(_customerAccountJointAccountHolderViewModel.CustomerAccountPrmKey);

                foreach (CustomerJointAccountHolderViewModel viewModel in customerAccountJointAccountHolderViewModels)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CustomerJointAccountHolderMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerJointAccountHolderMakerChecker>(viewModel);

                    context.CustomerJointAccountHolderMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                }

                // Verify Record
                // CustomerJointAccount
                List<CustomerJointAccountHolderViewModel> customerAccountJointAccountHolderViewModelList = new List<CustomerJointAccountHolderViewModel>();
                customerAccountJointAccountHolderViewModelList = (List<CustomerJointAccountHolderViewModel>)HttpContext.Current.Session["CustomerJointAccountHolder"];

                foreach (CustomerJointAccountHolderViewModel viewModel in customerAccountJointAccountHolderViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerAccountJointAccountHolderViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CustomerJointAccountHolderMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerJointAccountHolderMakerChecker>(viewModel);

                    context.CustomerJointAccountHolderMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                }

                // CustomerNominee
                List<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = new List<CustomerAccountNomineeViewModel>();
                customerAccountNomineeViewModelList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];

                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerAccountJointAccountHolderViewModel.Remark;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CustomerAccountNomineeMakerChecker customerAccountNomineeMakerChecker = Mapper.Map<CustomerAccountNomineeMakerChecker>(viewModel);
                    CustomerAccountNomineeTranslationMakerChecker customerAccountNomineeTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeTranslationMakerChecker>(viewModel);
                    CustomerAccountNomineeGuardianMakerChecker customerAccountNomineeGuardianMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianMakerChecker>(viewModel);
                    CustomerAccountNomineeGuardianTranslationMakerChecker customerAccountNomineeGuardianTranslationMakerChecker = Mapper.Map<CustomerAccountNomineeGuardianTranslationMakerChecker>(viewModel);

                    context.CustomerAccountNomineeMakerCheckers.Attach(customerAccountNomineeMakerChecker);
                    context.Entry(customerAccountNomineeMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeTranslationMakerCheckers.Attach(customerAccountNomineeTranslationMakerChecker);
                    context.Entry(customerAccountNomineeTranslationMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeGuardianMakerCheckers.Attach(customerAccountNomineeGuardianMakerChecker);
                    context.Entry(customerAccountNomineeGuardianMakerChecker).State = EntityState.Added;

                    context.CustomerAccountNomineeGuardianTranslationMakerCheckers.Attach(customerAccountNomineeGuardianTranslationMakerChecker);
                    context.Entry(customerAccountNomineeGuardianTranslationMakerChecker).State = EntityState.Added;
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


        public async Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerJointAccountHolderViewModel>("SELECT * FROM dbo.GetCustomerJointAccountHolderEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerJointAccountHolderViewModel>("SELECT * FROM dbo.GetCustomerJointAccountHolderEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerJointAccountHolderViewModel>("SELECT * FROM dbo.GetCustomerJointAccountHolderEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetRejectedEntries(long _customerAccountPrmKey)
        {
            try
            {                
                var a = await context.Database.SqlQuery<CustomerJointAccountHolderViewModel>("SELECT * FROM dbo.GetCustomerJointAccountHolderEntriesByCustomerPrmKey (@UserProfilePrmKey, @CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetUnVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a= await context.Database.SqlQuery<CustomerJointAccountHolderViewModel>("SELECT * FROM dbo.GetCustomerJointAccountHolderEntriesByCustomerPrmKey (@UserProfilePrmKey, @CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerJointAccountHolderViewModel>> GetVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerJointAccountHolderViewModel>("SELECT * FROM dbo.GetCustomerJointAccountHolderEntriesByCustomerPrmKey (@UserProfilePrmKey, @CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

       
    }
}
