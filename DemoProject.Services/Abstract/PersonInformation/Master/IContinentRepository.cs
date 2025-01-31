using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface IContinentRepository
    {
        // Amend Delete Entry - If Entry Rejected
        Task<bool> Amend(ContinentViewModel _continentViewModel);

        // Delete - Only For Rejected Entry
        Task<bool> Delete(ContinentViewModel _continentViewModel);

        // Return Rejected Entries
        Task<IEnumerable<ContinentViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List Which Are Not Authorized
        Task<IEnumerable<ContinentViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List For Modification
        Task<IEnumerable<ContinentViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<ContinentViewModel> GetRejectedEntry(Guid _centerId);

        bool GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey);

        Guid GetContinentIdByPrmKey(int _prmKey);

        //  Return Record From Table By Given Parameter (i.e. _centerId)
        Task<ContinentViewModel> GetUnVerifiedEntry(Guid _centerId);

        // Return Record From Table By Given Parameter (i.e. _centerId)
        Task<ContinentViewModel> GetVerifiedEntry(Guid _centerId);

        // Reject Entry
        Task<bool> Reject(ContinentViewModel _continentViewModel);

        //  Save New Entry
        Task<bool> Save(ContinentViewModel _continentViewModel);

        // Save Modification New Entry
        Task<bool> Modify(ContinentViewModel _continentViewModel);

        // Verify Entry
        Task<bool> Verify(ContinentViewModel _continentViewModel);
    }
}