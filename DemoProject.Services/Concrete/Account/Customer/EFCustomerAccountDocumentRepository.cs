using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerAccountDocumentRepository : ICustomerAccountDocumentRepository
    {
        private readonly EFDbContext context;

        public EFCustomerAccountDocumentRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<IEnumerable<CustomerAccountDocumentViewModel>> GetRejectedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountDocumentViewModel>("SELECT * FROM dbo.GetCustomerAccountDocumentEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountDocumentViewModel>> GetUnVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountDocumentViewModel>("SELECT * FROM dbo.GetCustomerAccountDocumentEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).ToListAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<IEnumerable<CustomerAccountDocumentViewModel>> GetVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountDocumentViewModel>("SELECT * FROM dbo.GetCustomerAccountDocumentEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).ToListAsync();
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
