using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface IStateRepository
    {
        // Amend Delete Entry - If Entry Rejected
        Task<bool> Amend(StateViewModel _stateViewModel);

        // Delete - Only For Rejected Entry
        Task<bool> Delete(StateViewModel _stateViewModel);

        // Return Rejected Entries
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List Which Are Not Authorized
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List For Modification
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<StateViewModel> GetRejectedEntry(Guid _centerId);

        bool GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey);

        //  Return Record From Table By Given Parameter (i.e. _centerId)
        Task<StateViewModel> GetUnVerifiedEntry(Guid _centerId);

        // Return Record From Table By Given Parameter (i.e. _centerId)
        Task<StateViewModel> GetVerifiedEntry(Guid _centerId);

        // Save Modification New Entry
        Task<bool> Modify(StateViewModel _stateViewModel);

        // Reject Entry
        Task<bool> Reject(StateViewModel _stateViewModel);

        //  Save New Entry
        Task<bool> Save(StateViewModel _stateViewModel);

        // Verify Entry
        Task<bool> Verify(StateViewModel _stateViewModel);
    }
}