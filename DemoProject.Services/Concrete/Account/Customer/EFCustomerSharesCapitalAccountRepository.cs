using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerSharesCapitalAccountRepository : ICustomerSharesCapitalAccountRepository
    {
        private readonly EFDbContext context;

        public EFCustomerSharesCapitalAccountRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<CustomerSharesCapitalAccountViewModel> GetRejectedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerSharesCapitalAccountViewModel>("SELECT * FROM dbo.GetCustomerSharesCapitalAccountEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerSharesCapitalAccountViewModel> GetUnVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerSharesCapitalAccountViewModel>("SELECT * FROM dbo.GetCustomerSharesCapitalAccountEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerSharesCapitalAccountViewModel> GetVerifiedEntries(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerSharesCapitalAccountViewModel>("SELECT * FROM dbo.GetCustomerSharesCapitalAccountEntriesByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
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
