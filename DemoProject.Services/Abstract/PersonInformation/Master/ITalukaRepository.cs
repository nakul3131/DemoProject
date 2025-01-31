using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.PersonInformation.Master;

namespace DemoProject.Services.Abstract.PersonInformation.Master
{
    public interface ITalukaRepository
    {
        // Amend Delete Entry - If Entry Rejected
        Task<bool> Amend(TalukaViewModel _talukaViewModel);

        // Delete - Only For Rejected Entry
        Task<bool> Delete(TalukaViewModel _talukaViewModel);

        // Return Rejected Entries
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List Which Are Not Authorized
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List For Modification
        Task<IEnumerable<CenterIndexViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<TalukaViewModel> GetRejectedEntry(Guid _centerId);

        bool GetUniqueCenterName(string NameOfCenter, byte CenterCategoryPrmKey);

        //  Return Record From Table By Given Parameter (i.e. _centerId)
        Task<TalukaViewModel> GetUnVerifiedEntry(Guid _centerId);

        // Return Record From Table By Given Parameter (i.e. _centerId)
        Task<TalukaViewModel> GetVerifiedEntry(Guid _centerId);

        // Reject Entry
        Task<bool> Reject(TalukaViewModel _talukaViewModel);

        //  Save New Entry
        Task<bool> Save(TalukaViewModel _talukaViewModel);

        // Save Modification New Entry
        Task<bool> Modify(TalukaViewModel _talukaViewModel);

        // Verify Entry
        Task<bool> Verify(TalukaViewModel _talukaViewModel);
    }
}