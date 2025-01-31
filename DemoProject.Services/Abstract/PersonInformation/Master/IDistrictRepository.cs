using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface IDistrictRepository
    {
        // Amend Delete Entry - If Entry Rejected
        Task<bool> Amend(DistrictViewModel _districtViewModel);

        // Delete - Only For Rejected Entry
        Task<bool> Delete(DistrictViewModel _districtViewModel);

        // Return Rejected Entries
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List Which Are Not Authorized
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List For Modification
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<DistrictViewModel> GetRejectedEntry(Guid _centerId);

        bool GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey);

        //  Return Record From Table By Given Parameter (i.e. _centerId)
        Task<DistrictViewModel> GetUnVerifiedEntry(Guid _centerId);

        // Return Record From Table By Given Parameter (i.e. _centerId)
        Task<DistrictViewModel> GetVerifiedEntry(Guid _centerId);

        // Reject Entry
        Task<bool> Reject(DistrictViewModel _districtViewModel);

        //  Save New Entry
        Task<bool> Save(DistrictViewModel _districtViewModel);

        // Save Modification New Entry
        Task<bool> Modify(DistrictViewModel _districtViewModel);

        // Verify Entry
        Task<bool> Verify(DistrictViewModel _districtViewModel);
    }
}