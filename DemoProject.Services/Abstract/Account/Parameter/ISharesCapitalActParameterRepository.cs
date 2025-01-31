using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.Account.Parameter
{
    public interface ISharesCapitalActParameterRepository
    {
        // Return Adult Age
        Task<byte> GetMinorAge();

        // Return Individual Shares Holding Amount
        Task<decimal> GetMaximumMemberSharesHoldingAmountLimit();
    }
}
