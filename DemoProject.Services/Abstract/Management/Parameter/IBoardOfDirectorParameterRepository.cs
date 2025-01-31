using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Parameter;

namespace DemoProject.Services.Abstract.Management.Parameter
{
    public interface IBoardOfDirectorParameterRepository
    {
        // Amend BoardOfDirectorParameter Delete Entry - If Entry Rejected
        Task<bool> Amend(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel);

        // Delete BoardOfDirectorParameter - Only For Rejected Entry
        Task<bool> Delete(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel);

        // Return Current Active Entry
        Task<BoardOfDirectorParameterViewModel> GetActiveEntry();

        // Return BoardOfDirectorParameter Verified Entries
        Task<IEnumerable<BoardOfDirectorParameterViewModel>> GetBoardOfDirectorParameterIndex();

        // Return Rejected Entry
        Task<BoardOfDirectorParameterViewModel> GetRejectedEntry();

        // // Return UnVerified Entry
        Task<BoardOfDirectorParameterViewModel> GetUnverifiedEntry();

        // Return True If Any Verification Pending
        Task<bool> IsAnyAuthorizationPending();

        // Reject BoardOfDirectorParameter Entry
        Task<bool> Reject(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel);

        // Save BoardOfDirectorParameter New Entry
        Task<bool> Save(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel);

        // Verify BoardOfDirectorParameter Entry
        Task<bool> Verify(BoardOfDirectorParameterViewModel _boardOfDirectorParameterViewModel);
    }
}
