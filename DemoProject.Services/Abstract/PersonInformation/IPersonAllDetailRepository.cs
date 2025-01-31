using DemoProject.Services.ViewModel.PersonInformation;
using System.Threading.Tasks;

namespace DemoProject.Services.Abstract.PersonInformation
{
    public interface IPersonAllDetailRepository
    {
        Task<bool> Save(PersonViewModel _personViewModel);
    }
}
