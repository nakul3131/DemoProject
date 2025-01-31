using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Domain.Entities.Security.Users;
using DemoProject.Services.ViewModel.Security.Users;

namespace DemoProject.Services.Abstract.Security.Users
{
    public interface IUserProfileRepository
    {
        Task<bool> Amend(UserProfileViewModel _userProfileViewModel);

        Task<bool> GetSessionValues(short _userProfilePrmKey, string _entryType);

        IEnumerable<UserProfile> GetUserProfiles { get; }

        Task<IEnumerable<UserProfileIndexViewModel>> GetUserProfileIndex(string _entryType);

        Task<UserProfileViewModel> GetUserProfileEntry(Guid _userProfileId, string _entryType);

        bool GetUserProfilePasswords(short _userProfilePrmKey, string _inputedPassword);

        List<SelectListItem> GetMenuByHomeBranch(Guid homeBranchId);

        Task<bool> VerifyRejectDelete(UserProfileViewModel _userProfileViewModel, string _entryType);

        Task<bool> Save(UserProfileViewModel _userProfileViewModel);

    }
}
