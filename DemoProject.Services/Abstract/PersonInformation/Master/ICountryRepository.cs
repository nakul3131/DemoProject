using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface ICountryRepository
    {
        // Amend Delete Entry - If Entry Rejected
        Task<bool> Amend(CountryViewModel _countryViewModel);

        // Delete - Only For Rejected Entry
        Task<bool> Delete(CountryViewModel _countryViewModel);

        // Return Rejected Entries
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List Which Are Not Authorized
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List For Modification
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<CountryViewModel> GetRejectedEntry(Guid _centerId);

        bool GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey);

        Guid GetCenterIdByPrmKey(int _prmKey);

        //  Return Record From Table By Given Parameter (i.e. _centerId)
        Task<CountryViewModel> GetUnVerifiedEntry(Guid _centerId);

        // Return Record From Table By Given Parameter (i.e. _centerId)
        Task<CountryViewModel> GetVerifiedEntry(Guid _centerId);

        // Reject Entry
        Task<bool> Reject(CountryViewModel _countryViewModel);

        //  Save New Entry
        Task<bool> Save(CountryViewModel _countryViewModel);

        // Save Modification New Entry
        Task<bool> Modify(CountryViewModel _countryViewModel);

        // Verify Entry
        Task<bool> Verify(CountryViewModel _countryViewModel);
    }
}