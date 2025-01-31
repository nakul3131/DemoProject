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
   public class EFCustomerLoanAccountRepository : ICustomerLoanAccountRepository
    {
        private readonly EFDbContext context;

        public EFCustomerLoanAccountRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(CustomerLoanAccountViewModel _customerLoanAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountViewModel.EntryDateTime = DateTime.Now;
                _customerLoanAccountViewModel.EntryStatus = StringLiteralValue.Amend;
                //_customerLoanAccountViewModel.ReasonForModification = "None";
                _customerLoanAccountViewModel.UserAction = StringLiteralValue.Amend;
                _customerLoanAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerLoanAccount customerLoanAccount = Mapper.Map<CustomerLoanAccount>(_customerLoanAccountViewModel);
                CustomerLoanAccountMakerChecker customerLoanAccountMakerChecker = Mapper.Map<CustomerLoanAccountMakerChecker>(_customerLoanAccountViewModel);

                // Set ReferenceKey As PrmKey To Every Object
                customerLoanAccount.PrmKey = _customerLoanAccountViewModel.CustomerLoanAccountPrmKey;

                //CustomerLoanAccount
                context.CustomerLoanAccounts.Attach(customerLoanAccount);
                context.Entry(customerLoanAccount).State = EntityState.Modified;

                //CustomerLoanAccountMakerChecker
                context.CustomerLoanAccountMakerCheckers.Attach(customerLoanAccountMakerChecker);
                context.Entry(customerLoanAccountMakerChecker).State = EntityState.Added;
                customerLoanAccount.CustomerLoanAccountMakerCheckers.Add(customerLoanAccountMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CustomerLoanAccountViewModel _customerLoanAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountViewModel.EntryDateTime = DateTime.Now;
                _customerLoanAccountViewModel.UserAction = StringLiteralValue.Delete;
                _customerLoanAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CustomerLoanAccountMakerChecker customerLoanAccountMakerChecker = Mapper.Map<CustomerLoanAccountMakerChecker>(_customerLoanAccountViewModel);

                //CustomerLoanAccountMakerChecker
                context.CustomerLoanAccountMakerCheckers.Attach(customerLoanAccountMakerChecker);
                context.Entry(customerLoanAccountMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Modify(CustomerLoanAccountViewModel _customerLoanAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountViewModel.EntryDateTime = DateTime.Now;
                _customerLoanAccountViewModel.EntryStatus = StringLiteralValue.Create;
                _customerLoanAccountViewModel.Remark = "None";
                _customerLoanAccountViewModel.UserAction = StringLiteralValue.Create;
                _customerLoanAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerLoanAccount customerLoanAccount = Mapper.Map<CustomerLoanAccount>(_customerLoanAccountViewModel);
                CustomerLoanAccountMakerChecker customerLoanAccountMakerChecker = Mapper.Map<CustomerLoanAccountMakerChecker>(_customerLoanAccountViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CustomerLoanAccount
                context.CustomerLoanAccounts.Attach(customerLoanAccount);
                context.Entry(customerLoanAccount).State = EntityState.Added;

                //CustomerLoanAccountMakerChecker
                context.CustomerLoanAccountMakerCheckers.Attach(customerLoanAccountMakerChecker);
                context.Entry(customerLoanAccountMakerChecker).State = EntityState.Added;
                customerLoanAccount.CustomerLoanAccountMakerCheckers.Add(customerLoanAccountMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }

            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(CustomerLoanAccountViewModel _customerLoanAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountViewModel.EntryDateTime = DateTime.Now;
                _customerLoanAccountViewModel.UserAction = StringLiteralValue.Reject;
                _customerLoanAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping 
                CustomerLoanAccountMakerChecker customerLoanAccountMakerChecker = Mapper.Map<CustomerLoanAccountMakerChecker>(_customerLoanAccountViewModel);

                //CustomerLoanAccountMakerChecker
                context.CustomerLoanAccountMakerCheckers.Attach(customerLoanAccountMakerChecker);
                context.Entry(customerLoanAccountMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Save(CustomerLoanAccountViewModel _customerLoanAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerLoanAccountViewModel.EntryDateTime = DateTime.Now;
                _customerLoanAccountViewModel.EntryStatus = StringLiteralValue.Create;
                _customerLoanAccountViewModel.Remark = "None";
                //_customerLoanAccountViewModel.ReasonForModification = "None";
                _customerLoanAccountViewModel.UserAction = StringLiteralValue.Create;
                _customerLoanAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerLoanAccount customerLoanAccount = Mapper.Map<CustomerLoanAccount>(_customerLoanAccountViewModel);
                CustomerLoanAccountMakerChecker customerLoanAccountMakerChecker = Mapper.Map<CustomerLoanAccountMakerChecker>(_customerLoanAccountViewModel);

                // Save Data In Appropriate Tables By Entity Framework Methods
                //CustomerLoanAccount
                context.CustomerLoanAccounts.Attach(customerLoanAccount);
                context.Entry(customerLoanAccount).State = EntityState.Added;

                //CustomerLoanAccountMakerChecker
                context.CustomerLoanAccountMakerCheckers.Attach(customerLoanAccountMakerChecker);
                context.Entry(customerLoanAccountMakerChecker).State = EntityState.Added;
                customerLoanAccount.CustomerLoanAccountMakerCheckers.Add(customerLoanAccountMakerChecker);

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CustomerLoanAccountViewModel _customerLoanAccountViewModel)
        {
            try
            {
                // Assign MDF Status To EntryStatus For Performing Modify Operation

                CustomerLoanAccountViewModel customerLoanAccountViewModelForModify = await GetVerifiedEntry(_customerLoanAccountViewModel.CustomerLoanAccountPrmKey);
                if (customerLoanAccountViewModelForModify != null)
                {
                    // Set Default Value
                    customerLoanAccountViewModelForModify.EntryDateTime = DateTime.Now;
                    customerLoanAccountViewModelForModify.UserAction = StringLiteralValue.Modify;
                    customerLoanAccountViewModelForModify.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                    //Mapping
                    CustomerLoanAccountMakerChecker customerLoanAccountMakerCheckerForModify = Mapper.Map<CustomerLoanAccountMakerChecker>(customerLoanAccountViewModelForModify);

                    //CustomerLoanAccountMakerChecker
                    context.CustomerLoanAccountMakerCheckers.Attach(customerLoanAccountMakerCheckerForModify);
                    context.Entry(customerLoanAccountMakerCheckerForModify).State = EntityState.Added;

                }

                // Verify New Record
                // Set Default Value
                _customerLoanAccountViewModel.EntryDateTime = DateTime.Now;
                _customerLoanAccountViewModel.UserAction = StringLiteralValue.Verify;
                _customerLoanAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                //Mapping
                CustomerLoanAccountMakerChecker customerLoanAccountMakerChecker = Mapper.Map<CustomerLoanAccountMakerChecker>(_customerLoanAccountViewModel);

                //CustomerLoanAccountMakerCheckers
                context.CustomerLoanAccountMakerCheckers.Attach(customerLoanAccountMakerChecker);
                context.Entry(customerLoanAccountMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }


        public async Task<IEnumerable<CustomerLoanAccountViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanAccountCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanAccountViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanAccountCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanAccountViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanAccountCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerLoanAccountViewModel>> GetIndexWithCreateModifyOperationStatus()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountViewModel>("SELECT * FROM dbo.GetSchemeEntriesForCustomerLoanAccountCRUDOperation ( @UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }


        public async Task<CustomerLoanAccountViewModel> GetRejectedEntry(long _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountEntriesByCustomerAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanAccountViewModel> GetUnVerifiedEntry(long _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountEntriesByCustomerAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanAccountViewModel> GetVerifiedEntry(long _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountEntriesByCustomerAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerLoanAccountViewModel> GetViewModelForCreate(long _customerLoanAccountPrmKey)
        {
            try
            {
                return await context.Database.SqlQuery<CustomerLoanAccountViewModel>("SELECT * FROM dbo.GetCustomerLoanAccountEntriesByCustomerAccountPrmKey (@UserProfilePrmKey, @CustomerLoanAccountPrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("@CustomerLoanAccountPrmKey", _customerLoanAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Initiated)).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }



    }
}
