using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Security.Password;

namespace DemoProject.Services.Abstract.Security.Master
{
    public interface IPasswordPolicyRepository
    {
        // Amend Password Policy Delete Entry - If Entry Rejected
        Task<bool> Amend(PasswordPolicyViewModel passwordPolicyRejectedViewModel);

        // Delete Password Policy - Only For Rejected Entry
        Task<bool> Delete(PasswordPolicyViewModel passwordPolicyViewModel);

        //Return Rejected Entries
        Task<IEnumerable<PasswordPolicyViewModel>> GetIndexOfRejectedEntries();

        // Return UnAuthorized Entry
        Task<IEnumerable<PasswordPolicyViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Authorize Entry
        Task<IEnumerable<PasswordPolicyViewModel>> GetIndexOfVerifiedEntries();

        PasswordPolicyValueViewModel GetPasswordPolicyValues(short _userProfilePrmKey);

        // Return Rejected Entry
        Task<PasswordPolicyViewModel> GetRejectedEntry(Guid PasswordPolicyId);

        // Return UnAuthorized Entry
        Task<PasswordPolicyViewModel> GetUnVerifiedEntry(Guid PasswordPolicyId);

        // PasswordPolicyModification
        Task<PasswordPolicyViewModel> GetVerifiedEntry(Guid PasswordPolicyId);

        // Reject Password Policy Entry
        Task<bool> Reject(PasswordPolicyViewModel _passwordPolicyViewModel);

        // Save Password Policy New Entry
        Task<bool> Save(PasswordPolicyViewModel passwordPolicyViewModel);

        // Authorize Password Policy Entry
        Task<bool> Verify(PasswordPolicyViewModel _passwordPolicyViewModel);
    }
}
