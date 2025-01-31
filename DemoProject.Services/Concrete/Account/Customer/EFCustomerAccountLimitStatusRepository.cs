using System.Linq;
using DemoProject.Services.Abstract.Account.Customer;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Customer
{
    public class EFCustomerAccountLimitStatusRepository : ICustomerAccountLimitStatusRepository
    {
        private readonly EFDbContext context;

        public EFCustomerAccountLimitStatusRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public bool IsReachedMaximumNumberOfTransactionLimit(long _customerAccountPrmKey)
        {
            return context.CustomerAccountLimitStatuses
                    .Where(c => c.CustomerAccountPrmKey == _customerAccountPrmKey)
                    .Select(c => c.IsReachedMaximumNumberOfTransactionLimit).FirstOrDefault();
        }
    }
}
