using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IBoardOfDirectorRepository
    {
        List<SelectListItem> BoardOfDirectorDropdownList { get; }

        // Amend BoardOfDirector Entry - If Entry Rejected
        Task<bool> Amend(BoardOfDirectorViewModel _boardOfDirectorViewModel);

        // Delete BoardOfDirector Entry - Only For Rejected Entry
        Task<bool> Delete(BoardOfDirectorViewModel _boardOfDirectorViewModel);

        Task<BoardOfDirectorViewModel> GetActiveEntry();

        // Get ChairmanPersonPrmKey
        long GetChairmanPersonPrmKey();

        // Get ViceChairmanPersonPrmKey
        long GetViceChairmanPersonPrmKey();

        // Return Rejected Entries
        Task<IEnumerable<BoardOfDirectorViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List Which Are Not Authorized
        Task<IEnumerable<BoardOfDirectorViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List For Modification
        Task<IEnumerable<BoardOfDirectorViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<BoardOfDirectorViewModel> GetRejectedEntry(Guid _BoardOfDirectorId);

        // Return UnAuthorized Entry
        Task<BoardOfDirectorViewModel> GetUnverifiedEntry(Guid _BoardOfDirectorId);

        // Return Verified Entry
        Task<BoardOfDirectorViewModel> GetVerifiedEntry(Guid _BoardOfDirectorId);

        // Reject BoardOfDirector Entry
        Task<bool> Reject(BoardOfDirectorViewModel _boardOfDirectorViewModel);

        // Save BoardOfDirector New Entry
        Task<bool> Save(BoardOfDirectorViewModel _boardOfDirectorViewModel);

        // Authorize BoardOfDirector Entry
        Task<bool> Verify(BoardOfDirectorViewModel _boardOfDirectorViewModel);
    }
}
