using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Parameter;

namespace DemoProject.Services.Abstract.Account.Parameter
{
    public interface IIncomeTaxActParameterRepository
    {
        Task<IncomeTaxActParameterViewModel> GetActiveEntry();
    }
}
