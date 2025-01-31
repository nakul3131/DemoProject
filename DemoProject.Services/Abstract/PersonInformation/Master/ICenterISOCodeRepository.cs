using System;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface ICenterISOCodeRepository
    {
        // Save CenterIsoCode New Entry
        Task<bool> ModifyCenterIsoCode(CenterIsoCodeViewModel _centerIsoCodeViewModel);

        short GetPrmKeyById(Guid _CenterId);

        // Return GetRejectedEntry Entry
        Task<CenterIsoCodeViewModel> GetRejectedEntry(short _centerPrmKey);

        // Return GetUnVerifiedEntry Entry
        Task<CenterIsoCodeViewModel> GetUnverifiedEntry(short _centerPrmKey);

        // Return GetVerifiedEntry Entry
        Task<CenterIsoCodeViewModel> GetVerifiedEntry(short _centerPrmKey);
    }
}
