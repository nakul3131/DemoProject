using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Security.UserRoles;

namespace DemoProject.Services.Abstract.Security.UserRoles
{
    public interface IRoleProfileRepository
    {
        List<SelectListItem> GetModelEntries(Guid MenuId);

        // Amend GeneralLedger Delete Entry - If Entry Rejected
        Task<bool> Amend(RoleProfileViewModel _roleProfileViewModel);

        // Return Rejected Entries
        Task<IEnumerable<RoleProfileIndexViewModel>> GetRoleProfileIndex(string _entryType);

        Task<bool> GetSessionValues(short _roleProfilePrmKey, string _entryType);

        // Return Rejected Entry
        Task<RoleProfileViewModel> GetRoleProfileEntry(Guid _roleProfileId, string _entryType);

        RoleProfileViewModel GetRoleProfileAllowAllAccess(Guid _roleProfileId);

        bool GetUniqueRoleProfileName(string _nameOfRoleProfile);

        // Reject GeneralLedger Entry
        Task<bool> VerifyRejectDelete(RoleProfileViewModel _roleProfileViewModel, string _entryType);

        // Save GeneralLedger New Entry
        Task<bool> Save(RoleProfileViewModel _roleProfileViewModel);


    }
}