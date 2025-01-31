using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerAccountTurnOverLimitRepository : ICustomerAccountTurnOverLimitRepository
    {
        private readonly EFDbContext context;

        public EFCustomerAccountTurnOverLimitRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<CustomerAccountTurnOverLimitViewModel>> GetRejectedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountTurnOverLimitViewModel>("SELECT * FROM dbo.GetCustomerAccountTurnOverLimitEntriesByCustomerPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("@EntryType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountTurnOverLimitViewModel>> GetUnVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountTurnOverLimitViewModel>("SELECT * FROM dbo.GetCustomerAccountTurnOverLimitEntriesByCustomerPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntryType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountTurnOverLimitViewModel>> GetVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountTurnOverLimitViewModel>("SELECT * FROM dbo.GetCustomerAccountTurnOverLimitEntriesByCustomerPrmKey (@CustomerAccountPrmkey, @EntryType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("@EntryType", StringLiteralValue.Verify)).ToListAsync();
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
