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
    public class EFCustomerDepositAccountAgentRepository : ICustomerDepositAccountAgentRepository
    {
        private readonly EFDbContext context;

        public EFCustomerDepositAccountAgentRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<bool> Amend(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel)
        {
            try
            {
                // Set Default Value
                _customerDepositAccountAgentViewModel.EntryDateTime = DateTime.Now;
                _customerDepositAccountAgentViewModel.EntryStatus = StringLiteralValue.Amend;
                _customerDepositAccountAgentViewModel.Remark = "None";
                _customerDepositAccountAgentViewModel.UserAction = StringLiteralValue.Amend;
                _customerDepositAccountAgentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns

                // Mapping
                // CustomerDepositAccountAgent
                CustomerDepositAccountAgent customerDepositAccountAgentForAmend = Mapper.Map<CustomerDepositAccountAgent>(_customerDepositAccountAgentViewModel);
                CustomerDepositAccountAgentMakerChecker customerDepositAccountAgentMakerCheckerForAmend = Mapper.Map<CustomerDepositAccountAgentMakerChecker>(_customerDepositAccountAgentViewModel);


                // Set ReferenceKey As PrmKey To Every Object
                customerDepositAccountAgentForAmend.PrmKey = _customerDepositAccountAgentViewModel.CustomerDepositAccountAgentPrmKey;

                // Check Entry Existance In Modification Table Or Main Table
                // CustomerDepositAccountAgent
                context.CustomerDepositAccountAgentMakerCheckers.Attach(customerDepositAccountAgentMakerCheckerForAmend);
                context.Entry(customerDepositAccountAgentMakerCheckerForAmend).State = EntityState.Added;
                customerDepositAccountAgentForAmend.CustomerDepositAccountAgentMakerCheckers.Add(customerDepositAccountAgentMakerCheckerForAmend);

                context.CustomerDepositAccountAgents.Attach(customerDepositAccountAgentForAmend);
                context.Entry(customerDepositAccountAgentForAmend).State = EntityState.Modified;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Delete(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel)
        {
            try
            {
                // Set Default Value
                _customerDepositAccountAgentViewModel.EntryDateTime = DateTime.Now;
                _customerDepositAccountAgentViewModel.Remark = "None";
                _customerDepositAccountAgentViewModel.UserAction = StringLiteralValue.Delete;
                _customerDepositAccountAgentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Mapping
                // Mapping
                CustomerDepositAccountAgentMakerChecker customerDepositAccountAgentMakerChecker = Mapper.Map<CustomerDepositAccountAgentMakerChecker>(_customerDepositAccountAgentViewModel);

                //CustomerDepositAccountAgentMakerChecker
                context.CustomerDepositAccountAgentMakerCheckers.Attach(customerDepositAccountAgentMakerChecker);
                context.Entry(customerDepositAccountAgentMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Reject(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel)
        {
            try
            {
                // Set Default Value
                _customerDepositAccountAgentViewModel.EntryDateTime = DateTime.Now;
                _customerDepositAccountAgentViewModel.UserAction = StringLiteralValue.Reject;
                _customerDepositAccountAgentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _customerDepositAccountAgentViewModel.Remark = "None";

                // Mapping
                CustomerDepositAccountAgentMakerChecker customerDepositAccountAgentMakerChecker = Mapper.Map<CustomerDepositAccountAgentMakerChecker>(_customerDepositAccountAgentViewModel);

                //CustomerDepositAccountAgentMakerChecker
                context.CustomerDepositAccountAgentMakerCheckers.Attach(customerDepositAccountAgentMakerChecker);
                context.Entry(customerDepositAccountAgentMakerChecker).State = EntityState.Added;

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
        public async Task<bool> Save(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel)
        {
            try
            {
                // Set Default Value
                _customerDepositAccountAgentViewModel.EntryDateTime = DateTime.Now;
                _customerDepositAccountAgentViewModel.EntryStatus = StringLiteralValue.Create;
                _customerDepositAccountAgentViewModel.Remark = "None";
                _customerDepositAccountAgentViewModel.UserAction = StringLiteralValue.Create;
                _customerDepositAccountAgentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];

                // Get PrmKey By Id Of All Dropdowns

                // Mapping
                // CustomerDepositAccountAgent
                CustomerDepositAccountAgent customerDepositAccountAgent = Mapper.Map<CustomerDepositAccountAgent>(_customerDepositAccountAgentViewModel);
                CustomerDepositAccountAgentMakerChecker customerDepositAccountAgentMakerChecker = Mapper.Map<CustomerDepositAccountAgentMakerChecker>(_customerDepositAccountAgentViewModel);

                // customerDepositAccountAgent
                context.CustomerDepositAccountAgentMakerCheckers.Attach(customerDepositAccountAgentMakerChecker);
                context.Entry(customerDepositAccountAgentMakerChecker).State = EntityState.Added;
                customerDepositAccountAgent.CustomerDepositAccountAgentMakerCheckers.Add(customerDepositAccountAgentMakerChecker);

                context.CustomerDepositAccountAgents.Attach(customerDepositAccountAgent);
                context.Entry(customerDepositAccountAgent).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<bool> Verify(CustomerDepositAccountAgentViewModel _customerDepositAccountAgentViewModel)
        {
            try
            {
                // Set Default Value
                _customerDepositAccountAgentViewModel.EntryDateTime = DateTime.Now;
                _customerDepositAccountAgentViewModel.UserProfilePrmKey = (short)HttpContext.Current.Session["UserProfilePrmKey"];
                _customerDepositAccountAgentViewModel.Remark = "None";

                _customerDepositAccountAgentViewModel.UserAction = StringLiteralValue.Verify;

                CustomerDepositAccountAgentMakerChecker customerDepositAccountAgentMakerChecker = Mapper.Map<CustomerDepositAccountAgentMakerChecker>(_customerDepositAccountAgentViewModel);

                //CustomerDepositAccountAgentMakerChecker
                context.CustomerDepositAccountAgentMakerCheckers.Attach(customerDepositAccountAgentMakerChecker);
                context.Entry(customerDepositAccountAgentMakerChecker).State = EntityState.Added;

                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return false;
            }
        }

        public async Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetIndexOfRejectedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerDepositAccountAgentViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountAgentEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetIndexOfUnVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerDepositAccountAgentViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountAgentEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetIndexOfVerifiedEntries()
        {
            try
            {
                return await context.Database.SqlQuery<CustomerDepositAccountAgentViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountAgentEntries (@UserProfilePrmKey, @EntriesType)", new SqlParameter("@UserProfilePrmKey", (short)HttpContext.Current.Session["UserProfilePrmKey"]), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetRejectedEntries(int _customerDepositAccountAgentId)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerDepositAccountAgentViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountAgentEntriesByCustomerDepositAccountPrmKey (@CustomerDepositAccountAgentId, @EntriesType)", new SqlParameter("@CustomerDepositAccountAgentId", _customerDepositAccountAgentId), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetUnVerifiedEntries(int _customerDepositAccountAgentId)
        {

            try
            {
                var a = await context.Database.SqlQuery<CustomerDepositAccountAgentViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountAgentEntriesByCustomerDepositAccountPrmKey (@CustomerDepositAccountAgentId, @EntriesType)", new SqlParameter("@CustomerDepositAccountAgentId", _customerDepositAccountAgentId), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;

            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerDepositAccountAgentViewModel>> GetVerifiedEntries(int _customerDepositAccountAgentId)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerDepositAccountAgentViewModel>("SELECT * FROM dbo.GetCustomerDepositAccountAgentEntriesByCustomerDepositAccountPrmKey (@CustomerDepositAccountAgentId, @EntriesType)", new SqlParameter("@CustomerDepositAccountAgentId", _customerDepositAccountAgentId), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
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
