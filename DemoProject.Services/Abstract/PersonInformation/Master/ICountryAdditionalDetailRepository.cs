using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface ICountryAdditionalDetailRepository
    {
        // Amend CountryAdditionalDetail Delete Entry - If Entry Rejected
        Task<bool> Amend(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel);

        // Delete CountryAdditionalDetail - Only For Rejected Entry
        Task<bool> Delete(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel);

        // Return Empty CountryAdditionalDetail Table 
        Task<IEnumerable<CountryAdditionalDetailViewModel>> GetIndexWithCreateModifyOperationStatus();

        // Return Rejected Entries
        Task<IEnumerable<CountryAdditionalDetailViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From CountryAdditionalDetail Table Which Are Not Authorized
        Task<IEnumerable<CountryAdditionalDetailViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From CountryAdditionalDetail Table For Modification
        Task<IEnumerable<CountryAdditionalDetailViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty CountryAdditionalDetailViewModel (Used For Create)
        Task<CountryAdditionalDetailViewModel> GetViewModelForCreate(short _centerPrmKey);

        // Return CountryAdditionalDetail (Used For Reject View)
        Task<CountryAdditionalDetailViewModel> GetViewModelForReject(short _centerPrmKey);

        // Return CountryAdditionalDetail (Used For Unverified View)
        Task<CountryAdditionalDetailViewModel> GetViewModelForUnverified(short _centerPrmKey);

        // Return CountryAdditionalDetailViewModel (Used For Unverified View)
        Task<CountryAdditionalDetailViewModel> GetViewModelForVerified(short _centerPrmKey);

        // Reject CountryAdditionalDetail Entry
        Task<bool> Reject(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel);

        // Save CountryAdditionalDetail New Entry
        Task<bool> Save(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel);

        // Save CountryAdditionalDetail Modification New Entry
        Task<bool> Modify(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel);

        // Authorize CountryAdditionalDetail Entry
        Task<bool> Verify(CountryAdditionalDetailViewModel _countryAdditionalDetailViewModel);

        // Return GetRejectedEntry Entry
        Task<CountryAdditionalDetailViewModel> GetRejectedEntry(short _centerPrmKey);

        // Return GetUnVerifiedEntry Entry
        Task<CountryAdditionalDetailViewModel> GetUnverifiedEntry(short _centerPrmKey);

        // Return GetVerifiedEntry Entry
        Task<CountryAdditionalDetailViewModel> GetVerifiedEntry(short _centerPrmKey);
    }
}
