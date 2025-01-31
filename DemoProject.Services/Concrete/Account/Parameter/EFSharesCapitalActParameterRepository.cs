using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DemoProject.Services.Abstract.Account.Parameter;
using DemoProject.Services.Constants;
using DemoProject.Services.Wrapper;

namespace DemoProject.Services.Concrete.Account.Parameter
{
    public class EFSharesCapitalActParameterRepository : ISharesCapitalActParameterRepository
    {
        private readonly EFDbContext context;

        public EFSharesCapitalActParameterRepository(RepositoryConnection _connection)
        {
            context = _connection.EFDbContext;
        }

        public async Task<byte> GetMinorAge()
        {
            return await context.SharesCapitalActParameters
                        .Where(s => s.EntryStatus == StringLiteralValue.Verify)
                        .Select(s => s.ValidPersonAgeForMembership).FirstOrDefaultAsync();
        }

        public async Task<decimal> GetMaximumMemberSharesHoldingAmountLimit()
        {
            return await context.SharesCapitalActParameters
                        .Where(s => s.EntryStatus == StringLiteralValue.Verify)
                        .Select(s => s.MaximumSharesHoldingAmountOtherThanGov).FirstOrDefaultAsync();
        }
    }
}
