using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Abstract.Account.SystemEntity;
using DemoProject.Services.Constants;
using DemoProject.Services.ViewModel.Account.Customer;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerAccountDetailRepository : ICustomerAccountDetailRepository
    {
        private readonly EFDbContext context;

        public readonly IAccountDetailRepository accountDetailRepository;

        public EFCustomerAccountDetailRepository(RepositoryConnection _connection, IAccountDetailRepository _accountDetailRepository)
        {
            context = _connection.EFDbContext;
            accountDetailRepository = _accountDetailRepository;
        }

        public short GetSchemePrmKey(Guid _customerAccountId)
        {
            long customerAccountPrmKey = accountDetailRepository.GetCustomerAccountPrmKeyById(_customerAccountId);

            return context.CustomerAccountDetails
                           .Where(d => d.CustomerAccountPrmKey == customerAccountPrmKey && d.EntryStatus == StringLiteralValue.Verify)
                           .Select(d => d.SchemePrmKey).FirstOrDefault();
        }


        public async Task<CustomerAccountDetailViewModel> GetRejectedEntry(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerAccountDetailEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Reject)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerAccountDetailViewModel> GetUnVerifiedEntry(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerAccountDetailEntryByCustomerAccountPrmKey(@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Unverified)).FirstOrDefaultAsync();
                return a;
            }
            catch (Exception ex)
            {
                string error = ex.Message;

                return null;
            }
        }

        public async Task<CustomerAccountDetailViewModel> GetVerifiedEntry(long _customerAccountPrmKey)
        {
            try
            {
                var a = await context.Database.SqlQuery<CustomerAccountDetailViewModel>("SELECT * FROM dbo.GetCustomerAccountDetailEntryByCustomerAccountPrmKey (@CustomerAccountPrmkey, @EntriesType)", new SqlParameter("@CustomerAccountPrmkey", _customerAccountPrmKey), new SqlParameter("EntriesType", StringLiteralValue.Verify)).FirstOrDefaultAsync();
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
