using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface IDivisionRepository
    {
        // Amend Delete Entry - If Entry Rejected
        Task<bool> Amend(DivisionViewModel _divisionViewModel);

        // Delete - Only For Rejected Entry
        Task<bool> Delete(DivisionViewModel _divisionViewModel);

        // Return Rejected Entries
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List Which Are Not Authorized
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List For Modification
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<DivisionViewModel> GetRejectedEntry(Guid _centerId);

        bool GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey);

        //  Return Record From Table By Given Parameter (i.e. _centerId)
        Task<DivisionViewModel> GetUnverifiedEntry(Guid _centerId);

        // Return Record From Table By Given Parameter (i.e. _centerId)
        Task<DivisionViewModel> GetVerifiedEntry(Guid _centerId);

        // Reject Entry
        Task<bool> Reject(DivisionViewModel _divisionViewModel);

        // Save Modification New Entry
        Task<bool> Modify(DivisionViewModel _divisionViewModel);

        //  Save New Entry
        Task<bool> Save(DivisionViewModel _divisionViewModel);

        // Verify Entry
        Task<bool> Verify(DivisionViewModel _divisionViewModel);
    }
}