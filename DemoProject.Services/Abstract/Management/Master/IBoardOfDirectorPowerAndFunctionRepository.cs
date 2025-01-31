using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IBoardOfDirectorPowerAndFunctionRepository
    {
        // Amend BoardOfDirectorPowerAndFunction Entry - If Entry Rejected
        Task<bool> Amend(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel);

        // Delete BoardOfDirectorPowerAndFunction Entry - Only For Rejected Entry
        Task<bool> Delete(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel);

        // Return Rejected Entries
        Task<IEnumerable<BoardOfDirectorPowerAndFunctionViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List Which Are Not Authorized
        Task<IEnumerable<BoardOfDirectorPowerAndFunctionViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List For Modification
        Task<IEnumerable<BoardOfDirectorPowerAndFunctionViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<BoardOfDirectorPowerAndFunctionViewModel> GetRejectedEntry(Guid _BoardOfDirectorPowerAndFunctionId);

        // Return UnAuthorized Entry
        Task<BoardOfDirectorPowerAndFunctionViewModel> GetUnverifiedEntry(Guid _BoardOfDirectorPowerAndFunctionId);

        // Reject BoardOfDirectorPowerAndFunction Entry
        Task<bool> Reject(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel);

        // Save BoardOfDirectorPowerAndFunction New Entry
        Task<bool> Save(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel);

        // Authorize BoardOfDirectorPowerAndFunction Entry
        Task<bool> Verify(BoardOfDirectorPowerAndFunctionViewModel _boardOfDirectorPowerAndFunctionViewModel);
    }
}
