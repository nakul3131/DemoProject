using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Domain.Entities.Security.Parameter;
using DemoProject.Services.ViewModel.Parameter.Security;

namespace DemoProject.Services.Abstract.Security.Parameter
{
    public interface IUserAuthenticationParameterRepository
    {
        // Amend User Authentication Parameter Delete Entry - If Entry Rejected
        Task<bool> Amend(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel);

        // Delete User Authentication Parameter - Only For Rejected Entry
        Task<bool> Delete(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel);

        // Return Current Active Entry
        Task<UserAuthenticationParameterViewModel> GetActiveEntry();
        
        // Return Rejected Entry
        Task<UserAuthenticationParameterViewModel> GetRejectedEntry();

        // Return UnAuthorized Entry
        Task<UserAuthenticationParameterViewModel> GetUnAuthorizedEntry();

        // Return UnAuthorized Entry
        Task<UserAuthenticationParameterViewModel> GetUnVerifiedEntry();

        // Return Authorized Entries
        Task<IEnumerable<UserAuthenticationParameterViewModel>> GetUserAuthenticationParameterIndex();

        // Return True If Any Authorization Pending
        Task<bool> IsAnyAuthorizationPending();

        // Save User Authentication Parameter New Entry
        Task<bool> Save(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel);

        // Authorize User Authentication Parameter Entry
        Task<bool> Verify(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel);

        // Reject User Authentication Parameter Entry
        Task<bool> Reject(UserAuthenticationParameterViewModel _userAuthenticationParameterViewModel);

        IEnumerable<UserAuthenticationParameter> UserAuthenticationParameters { get; }
    }
}
