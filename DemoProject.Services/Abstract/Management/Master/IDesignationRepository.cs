using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IDesignationRepository
    {
        // Amend Designation Delete Entry - If Entry Rejected
        Task<bool> Amend(DesignationViewModel _designationViewModel);

        // Delete Designation - Only For Rejected Entry
        Task<bool> Delete(DesignationViewModel _designationViewModel);

        // Return Rejected Entries
        Task<IEnumerable<DesignationViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From Designation Table Which Are Not Authorized
        Task<IEnumerable<DesignationViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From Designation Table For Modification
        Task<IEnumerable<DesignationViewModel>> GetIndexOfVerifiedEntries();

        // Return Rejected Entry
        Task<DesignationViewModel> GetRejectedEntry(Guid _designationId);

        bool GetUniqueDesignationName(string _nameOfDesignation);

        Guid GetDesignationIdByPrmKey(int _prmKey);

        // Return Record From Designation Table By Given Parameter (i.e. DesignationId)
        Task<DesignationViewModel> GetUnVerifiedEntry(Guid _designationId);

        // Return Record From Designation Table By Given Parameter (i.e. DesignationId)
        Task<DesignationViewModel> GetVerifiedEntry(Guid _designationId);

        // Save Designation Modification New Entry
        Task<bool> Modify(DesignationViewModel _designationViewModel);

        // Reject Designation Entry
        Task<bool> Reject(DesignationViewModel _designationViewModel);

        // Save Designation New Entry
        Task<bool> Save(DesignationViewModel _designationViewModel);

        // Authorize Designation Entry
        Task<bool> Verify(DesignationViewModel _designationViewModel);
    }
}
