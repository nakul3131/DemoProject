using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IPowerAndFunctionRepository
    {
        // Amend PowerAndFunction Entry - If Entry Rejected
        Task<bool> Amend(PowerAndFunctionViewModel _powerAndFunctionViewModel);

        // Delete PowerAndFunction Entry - Only For Rejected Entry
        Task<bool> Delete(PowerAndFunctionViewModel _powerAndFunctionViewModel);

        Task<PowerAndFunctionViewModel> GetActiveEntry();

        // Return Rejected Entries
        Task<IEnumerable<PowerAndFunctionViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List Which Are Not Authorized
        Task<IEnumerable<PowerAndFunctionViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List For Modification
        Task<IEnumerable<PowerAndFunctionViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<PowerAndFunctionViewModel> GetRejectedEntry(Guid _PowerAndFunctionId);

        // Return UnAuthorized Entry
        Task<PowerAndFunctionViewModel> GetUnverifiedEntry(Guid _PowerAndFunctionId);

        int GetPrmKeyById(Guid _ParentId);

        List<SelectListItem> PowerAndFunctionDropdownList{ get; }

        List<SelectListItem> Parents { get; }

        // Reject PowerAndFunction Entry
        Task<bool> Reject(PowerAndFunctionViewModel _powerAndFunctionViewModel);

        // Save PowerAndFunction New Entry
        Task<bool> Save(PowerAndFunctionViewModel _powerAndFunctionViewModel);

        // Authorize PowerAndFunction Entry
        Task<bool> Verify(PowerAndFunctionViewModel _powerAndFunctionViewModel);

    }
}


