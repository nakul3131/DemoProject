using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface IVillageTownCityRepository
    {
        // Amend Delete Entry - If Entry Rejected
        Task<bool> Amend(VillageTownCityViewModel _villageViewModel);

        // Delete - Only For Rejected Entry
        Task<bool> Delete(VillageTownCityViewModel _villageViewModel);

        // Return Rejected Entries
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List Which Are Not Authorized
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List For Modification
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<VillageTownCityViewModel> GetRejectedEntry(Guid _centerId);

        bool GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey);

        //  Return Record From Table By Given Parameter (i.e. _centerId)
        Task<VillageTownCityViewModel> GetUnVerifiedEntry(Guid _centerId);

        // Return Record From Table By Given Parameter (i.e. _centerId)
        Task<VillageTownCityViewModel> GetVerifiedEntry(Guid _centerId);

        // Reject Entry
        Task<bool> Reject(VillageTownCityViewModel _villageViewModel);

        //  Save New Entry
        Task<bool> Save(VillageTownCityViewModel _villageViewModel);

        // Save Modification New Entry
        Task<bool> Modify(VillageTownCityViewModel _villageViewModel);

        // Verify Entry
        Task<bool> Verify(VillageTownCityViewModel _villageViewModel);
    }
}