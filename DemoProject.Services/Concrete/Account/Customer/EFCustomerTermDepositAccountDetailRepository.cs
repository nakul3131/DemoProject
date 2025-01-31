using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Web;
using DemoProject.Domain.Entities.Account.Customer;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerTermDepositAccountDetailRepository : ICustomerTermDepositAccountDetailRepository
    {
        private readonly EFDbContext context;

        public EFCustomerTermDepositAccountDetailRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(CustomerTermDepositAccountDetailViewModel _customerDepositAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerDepositAccountViewModel.EntryDateTime = DateTime.Now;
                _customerDepositAccountViewModel.EntryStatus = StringLiteralValue.Amend;
                _customerDepositAccountViewModel.Remark = "None";
                _customerDepositAccountViewModel.UserAction = StringLiteralValue.Amend;
                _customerDepositAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns

                // Mapping
                // CustomerTermDepositAccountDetail
                CustomerTermDepositAccountDetail customerDepositAccountForAmend = Mapper.Map<CustomerTermDepositAccountDetail>(_customerDepositAccountViewModel);
                CustomerTermDepositAccountDetailMakerChecker customerDepositAccountMakerCheckerForAmend = Mapper.Map<CustomerTermDepositAccountDetailMakerChecker>(_customerDepositAccountViewModel);


                // Set ReferenceKey As PrmKey To Every Object
                customerDepositAccountForAmend.PrmKey = _customerDepositAccountViewModel.CustomerTermDepositAccountDetailPrmKey;

                // Check Entry Existance In Modification Table Or Main Table
                // CustomerTermDepositAccountDetail
                context.CustomerTermDepositAccountDetailMakerCheckers.Attach(customerDepositAccountMakerCheckerForAmend);
                context.Entry(customerDepositAccountMakerCheckerForAmend).State = EntityState.Added;
                customerDepositAccountForAmend.CustomerTermDepositAccountDetailMakerCheckers.Add(customerDepositAccountMakerCheckerForAmend);

                context.CustomerTermDepositAccountDetails.Attach(customerDepositAccountForAmend);
                context.Entry(customerDepositAccountForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CustomerTermDepositAccountDetailViewModel _customerDepositAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerDepositAccountViewModel.EntryDateTime = DateTime.Now;
                _customerDepositAccountViewModel.Remark = "None";
                _customerDepositAccountViewModel.UserAction = StringLiteralValue.Delete;
                _customerDepositAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // Mapping
                CustomerTermDepositAccountDetailMakerChecker customerDepositAccountMakerChecker = Mapper.Map<CustomerTermDepositAccountDetailMakerChecker>(_customerDepositAccountViewModel);

                //CustomerTermDepositAccountDetailMakerChecker
                context.CustomerTermDepositAccountDetailMakerCheckers.Attach(customerDepositAccountMakerChecker);
                context.Entry(customerDepositAccountMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }


        public async Task<bool> Reject(CustomerTermDepositAccountDetailViewModel _customerDepositAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerDepositAccountViewModel.EntryDateTime = DateTime.Now;
                _customerDepositAccountViewModel.UserAction = StringLiteralValue.Reject;
                _customerDepositAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _customerDepositAccountViewModel.Remark = "None";

                // Mapping
                CustomerTermDepositAccountDetailMakerChecker customerDepositAccountMakerChecker = Mapper.Map<CustomerTermDepositAccountDetailMakerChecker>(_customerDepositAccountViewModel);

                //CustomerTermDepositAccountDetailMakerChecker
                context.CustomerTermDepositAccountDetailMakerCheckers.Attach(customerDepositAccountMakerChecker);
                context.Entry(customerDepositAccountMakerChecker).State = EntityState.Added;

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
        public async Task<bool> Save(CustomerTermDepositAccountDetailViewModel _customerDepositAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerDepositAccountViewModel.EntryDateTime = DateTime.Now;
                _customerDepositAccountViewModel.EntryStatus = StringLiteralValue.Create;
                _customerDepositAccountViewModel.Remark = "None";
                _customerDepositAccountViewModel.UserAction = StringLiteralValue.Create;
                _customerDepositAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns

                // Mapping
                // CustomerTermDepositAccountDetail
                CustomerTermDepositAccountDetail customerDepositAccount = Mapper.Map<CustomerTermDepositAccountDetail>(_customerDepositAccountViewModel);
                CustomerTermDepositAccountDetailMakerChecker customerDepositAccountMakerChecker = Mapper.Map<CustomerTermDepositAccountDetailMakerChecker>(_customerDepositAccountViewModel);

                // customerDepositAccount
                context.CustomerTermDepositAccountDetailMakerCheckers.Attach(customerDepositAccountMakerChecker);
                context.Entry(customerDepositAccountMakerChecker).State = EntityState.Added;
                customerDepositAccount.CustomerTermDepositAccountDetailMakerCheckers.Add(customerDepositAccountMakerChecker);

                context.CustomerTermDepositAccountDetails.Attach(customerDepositAccount);
                context.Entry(customerDepositAccount).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CustomerTermDepositAccountDetailViewModel _customerDepositAccountViewModel)
        {
            try
            {
                // Set Default Value
                _customerDepositAccountViewModel.EntryDateTime = DateTime.Now;
                _customerDepositAccountViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _customerDepositAccountViewModel.Remark = "None";

                _customerDepositAccountViewModel.UserAction = StringLiteralValue.Verify;

                CustomerTermDepositAccountDetailMakerChecker customerDepositAccountMakerChecker = Mapper.Map<CustomerTermDepositAccountDetailMakerChecker>(_customerDepositAccountViewModel);

                //CustomerTermDepositAccountDetailMakerChecker
                context.CustomerTermDepositAccountDetailMakerCheckers.Attach(customerDepositAccountMakerChecker);
                context.Entry(customerDepositAccountMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }


        public async Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerTermDepositAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerTermDepositAccountDetailEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerTermDepositAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerTermDepositAccountDetailEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerTermDepositAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerTermDepositAccountDetailEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetRejectedEntries(int _customerTermDepositAccountDetailPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerTermDepositAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerTermDepositAccountDetailEntriesByCustomerDepositAccountPrmKey (@CustomerTermDepositAccountDetailPrmKey, @EntriesType)", new SqlParameter("@CustomerTermDepositAccountDetailPrmKey", _customerTermDepositAccountDetailPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetUnVerifiedEntries(int _customerDepositAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerTermDepositAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerTermDepositAccountDetailEntriesByCustomerDepositAccountPrmKey (@CustomerTermDepositAccountDetailPrmKey, @EntriesType)", new SqlParameter("@CustomerTermDepositAccountDetailPrmKey", _customerDepositAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }
   
        public async Task<IEnumerable<CustomerTermDepositAccountDetailViewModel>> GetVerifiedEntries(int _customerDepositAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerTermDepositAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerTermDepositAccountDetailEntriesByCustomerDepositAccountPrmKey (@CustomerTermDepositAccountDetailPrmKey, @EntriesType)", new SqlParameter("@CustomerTermDepositAccountDetailPrmKey", _customerDepositAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
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
