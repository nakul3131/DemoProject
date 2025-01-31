using AutoMapper;
using DemoProject.Domain.Entities.Account.Customer;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerLoanAccountGuarantorDetailRepository: ICustomerLoanAccountGuarantorDetailRepository
    {
        private readonly EFDbContext context;


        public EFCustomerLoanAccountGuarantorDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountGuarantorDetail.EntryDateTime = DateTime.Now;
                _customerLoanAccountGuarantorDetail.EntryStatus = StringLiteralValue.Amend;
                _customerLoanAccountGuarantorDetail.ReasonForModification = "None";
                _customerLoanAccountGuarantorDetail.Remark = "None";
                _customerLoanAccountGuarantorDetail.UserAction = StringLiteralValue.Amend;
                _customerLoanAccountGuarantorDetail.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Amend First Old Record
                // CustomerJointAccount
                IEnumerable<CustomerLoanAccountGuarantorDetailViewModel> customerAccountJointAccountHolderViewModels = await GetRejectedEntries(_customerLoanAccountGuarantorDetail.CustomerLoanAccountPrmKey);

                foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerAccountJointAccountHolderViewModels)
                {
                    // Set Default Value
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Amend;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    CustomerLoanAccountGuarantorDetailMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerLoanAccountGuarantorDetailMakerChecker>(viewModel);

                    // CustomerLoanAccountGuarantorDetailMakerChecker
                    context.CustomerLoanAccountGuarantorDetailMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                }

                // Insert New Updated Record - Get Record From Session Object
                List<CustomerLoanAccountGuarantorDetailViewModel> customerAccountJointAccountHolderViewModelList = new List<CustomerLoanAccountGuarantorDetailViewModel>();
                customerAccountJointAccountHolderViewModelList = (List<CustomerLoanAccountGuarantorDetailViewModel>)HttpContext.Current.Session["CustomerLoanAccountGuarantorDetail"];

                foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerAccountJointAccountHolderViewModelList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerLoanAccountGuarantorDetail.Remark;
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id

                    CustomerLoanAccountGuarantorDetail customerAccountJointAccountHolder = Mapper.Map<CustomerLoanAccountGuarantorDetail>(viewModel);
                    customerAccountJointAccountHolder.CustomerLoanAccountPrmKey = _customerLoanAccountGuarantorDetail.CustomerLoanAccountPrmKey;

                    CustomerLoanAccountGuarantorDetailMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerLoanAccountGuarantorDetailMakerChecker>(viewModel);

                    context.CustomerLoanAccountGuarantorDetailMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                    customerAccountJointAccountHolder.CustomerLoanAccountGuarantorDetailMakerCheckers.Add(customerAccountJointAccountHolderMakerChecker);

                    context.CustomerLoanAccountGuarantorDetails.Attach(customerAccountJointAccountHolder);
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

        public async Task<bool> Delete(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountGuarantorDetail.EntryDateTime = DateTime.Now;
                _customerLoanAccountGuarantorDetail.ReasonForModification = "None";
                _customerLoanAccountGuarantorDetail.Remark = "None";
                _customerLoanAccountGuarantorDetail.UserAction = StringLiteralValue.Delete;
                _customerLoanAccountGuarantorDetail.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerJointAccount
                List<CustomerLoanAccountGuarantorDetailViewModel> customerAccountJointAccountHolderViewModelList = new List<CustomerLoanAccountGuarantorDetailViewModel>();
                customerAccountJointAccountHolderViewModelList = (List<CustomerLoanAccountGuarantorDetailViewModel>)HttpContext.Current.Session["CustomerLoanAccountGuarantorDetail"];

                foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerAccountJointAccountHolderViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerLoanAccountGuarantorDetail.Remark;
                    viewModel.UserAction = StringLiteralValue.Delete;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    CustomerLoanAccountGuarantorDetailMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerLoanAccountGuarantorDetailMakerChecker>(viewModel);

                    // CustomerLoanAccountGuarantorDetailMakerChecker
                    context.CustomerLoanAccountGuarantorDetailMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
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

        public async Task<bool> Modify(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountGuarantorDetail.EntryDateTime = DateTime.Now;
                _customerLoanAccountGuarantorDetail.EntryStatus = StringLiteralValue.Create;
                _customerLoanAccountGuarantorDetail.Remark = "None";
                _customerLoanAccountGuarantorDetail.UserAction = StringLiteralValue.Create;
                _customerLoanAccountGuarantorDetail.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerJointAccount
                List<CustomerLoanAccountGuarantorDetailViewModel> customerAccountJointAccountHolderList = new List<CustomerLoanAccountGuarantorDetailViewModel>();
                customerAccountJointAccountHolderList = (List<CustomerLoanAccountGuarantorDetailViewModel>)HttpContext.Current.Session["CustomerLoanAccountGuarantorDetail"];

                foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerAccountJointAccountHolderList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.CustomerLoanAccountPrmKey = _customerLoanAccountGuarantorDetail.CustomerLoanAccountPrmKey;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By 

                    // Mapping CustomerJointAccount
                    CustomerLoanAccountGuarantorDetail customerAccountJointAccountHolder = Mapper.Map<CustomerLoanAccountGuarantorDetail>(_customerLoanAccountGuarantorDetail);
                    CustomerLoanAccountGuarantorDetailMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerLoanAccountGuarantorDetailMakerChecker>(_customerLoanAccountGuarantorDetail);

                    // CustomerJointAccount
                    context.CustomerLoanAccountGuarantorDetails.Attach(customerAccountJointAccountHolder);
                    context.Entry(customerAccountJointAccountHolder).State = EntityState.Added;

                    // CustomerJointAccountMakerChecker
                    context.CustomerLoanAccountGuarantorDetailMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                    customerAccountJointAccountHolder.CustomerLoanAccountGuarantorDetailMakerCheckers.Add(customerAccountJointAccountHolderMakerChecker);
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

        public async Task<bool> Reject(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountGuarantorDetail.EntryDateTime = DateTime.Now;
                _customerLoanAccountGuarantorDetail.UserAction = StringLiteralValue.Reject;
                _customerLoanAccountGuarantorDetail.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerJointAccount
                List<CustomerLoanAccountGuarantorDetailViewModel> customerAccountJointAccountHolderViewModelList = new List<CustomerLoanAccountGuarantorDetailViewModel>();
                customerAccountJointAccountHolderViewModelList = (List<CustomerLoanAccountGuarantorDetailViewModel>)HttpContext.Current.Session["CustomerLoanAccountGuarantorDetail"];

                foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerAccountJointAccountHolderViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerLoanAccountGuarantorDetail.Remark;
                    viewModel.UserAction = StringLiteralValue.Reject;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    // Mapping
                    CustomerLoanAccountGuarantorDetailMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerLoanAccountGuarantorDetailMakerChecker>(viewModel);

                    // CustomerLoanAccountGuarantorDetailMakerCheckers
                    context.CustomerLoanAccountGuarantorDetailMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
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

        public async Task<bool> Save(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountGuarantorDetail.EntryDateTime = DateTime.Now;
                _customerLoanAccountGuarantorDetail.EntryStatus = StringLiteralValue.Create;
                _customerLoanAccountGuarantorDetail.ReasonForModification = "None";
                _customerLoanAccountGuarantorDetail.Remark = "None";
                _customerLoanAccountGuarantorDetail.UserAction = StringLiteralValue.Create;
                _customerLoanAccountGuarantorDetail.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerJointAccount
                List<CustomerLoanAccountGuarantorDetailViewModel> customerAccountJointAccountHolderList = new List<CustomerLoanAccountGuarantorDetailViewModel>();
                customerAccountJointAccountHolderList = (List<CustomerLoanAccountGuarantorDetailViewModel>)HttpContext.Current.Session["CustomerLoanAccountGuarantorDetail"];

                foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerAccountJointAccountHolderList)
                {
                    // Set Default Value
                    viewModel.EntryStatus = StringLiteralValue.Create;
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.CustomerLoanAccountPrmKey = _customerLoanAccountGuarantorDetail.CustomerLoanAccountPrmKey;
                    viewModel.Remark = "None";
                    viewModel.ReasonForModification = "None";
                    viewModel.UserAction = StringLiteralValue.Create;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Get PrmKey By Id

                    // Mapping CustomerJointAccount
                    CustomerLoanAccountGuarantorDetail customerAccountJointAccountHolder = Mapper.Map<CustomerLoanAccountGuarantorDetail>(_customerLoanAccountGuarantorDetail);
                    CustomerLoanAccountGuarantorDetailMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerLoanAccountGuarantorDetailMakerChecker>(_customerLoanAccountGuarantorDetail);

                    // CustomerJointAccount
                    context.CustomerLoanAccountGuarantorDetails.Attach(customerAccountJointAccountHolder);
                    context.Entry(customerAccountJointAccountHolder).State = EntityState.Added;

                    // CustomerJointAccountMakerChecker
                    context.CustomerLoanAccountGuarantorDetailMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                    customerAccountJointAccountHolder.CustomerLoanAccountGuarantorDetailMakerCheckers.Add(customerAccountJointAccountHolderMakerChecker);
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

        public async Task<bool> Verify(CustomerLoanAccountGuarantorDetailViewModel _customerLoanAccountGuarantorDetail)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountGuarantorDetail.EntryDateTime = DateTime.Now;
                _customerLoanAccountGuarantorDetail.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // CustomerJointAccount
                IEnumerable<CustomerLoanAccountGuarantorDetailViewModel> customerAccountJointAccountHolderViewModels = await GetVerifiedEntries(_customerLoanAccountGuarantorDetail.CustomerLoanAccountPrmKey);

                foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerAccountJointAccountHolderViewModels)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.UserAction = StringLiteralValue.Modify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CustomerLoanAccountGuarantorDetailMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerLoanAccountGuarantorDetailMakerChecker>(viewModel);

                    context.CustomerLoanAccountGuarantorDetailMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                }

                // Verify Record
                // CustomerJointAccount
                List<CustomerLoanAccountGuarantorDetailViewModel> customerAccountJointAccountHolderViewModelList = new List<CustomerLoanAccountGuarantorDetailViewModel>();
                customerAccountJointAccountHolderViewModelList = (List<CustomerLoanAccountGuarantorDetailViewModel>)HttpContext.Current.Session["CustomerLoanAccountGuarantorDetail"];

                foreach (CustomerLoanAccountGuarantorDetailViewModel viewModel in customerAccountJointAccountHolderViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerLoanAccountGuarantorDetail.Remark;
                    viewModel.UserAction = StringLiteralValue.Verify;
                    viewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    CustomerLoanAccountGuarantorDetailMakerChecker customerAccountJointAccountHolderMakerChecker = Mapper.Map<CustomerLoanAccountGuarantorDetailMakerChecker>(viewModel);

                    context.CustomerLoanAccountGuarantorDetailMakerCheckers.Attach(customerAccountJointAccountHolderMakerChecker);
                    context.Entry(customerAccountJointAccountHolderMakerChecker).State = EntityState.Added;
                }

                // CustomerNominee
                List<CustomerAccountNomineeViewModel> customerAccountNomineeViewModelList = new List<CustomerAccountNomineeViewModel>();
                customerAccountNomineeViewModelList = (List<CustomerAccountNomineeViewModel>)HttpContext.Current.Session["CustomerAccountNominee"];

                foreach (CustomerAccountNomineeViewModel viewModel in customerAccountNomineeViewModelList)
                {
                    viewModel.EntryDateTime = DateTime.Now;
                    viewModel.Remark = _customerLoanAccountGuarantorDetail.Remark;
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

        public async Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountGuarantorDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountGuarantorDetailEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountGuarantorDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountGuarantorDetailEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountGuarantorDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountGuarantorDetailEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetRejectedEntries(long _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountGuarantorDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountGuarantorDetailEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetUnVerifiedEntries(long _customerLoanAccountPrmKey)
        {
            try
            {
                var a= await context.Database.SqlQuery<CustomerLoanAccountGuarantorDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountGuarantorDetailEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanAccountGuarantorDetailViewModel>> GetVerifiedEntries(long _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountGuarantorDetailViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountGuarantorDetailEntriesByCustomerLoanAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

       
    }
}
