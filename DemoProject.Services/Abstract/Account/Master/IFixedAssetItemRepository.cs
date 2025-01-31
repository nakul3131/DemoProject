using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Account.Master;

namespace DemoProject.Services.Abstract.Account.Master
{
    public interface IFixedAssetItemRepository
    {
        // Amend Designation Delete Entry - If Entry Rejected
        Task<bool> Amend(FixedAssetItemViewModel _fixedAssetItemViewModel);

        // Delete Designation - Only For Rejected Entry
        Task<bool> Delete(FixedAssetItemViewModel _fixedAssetItemViewModel);

        // Return Rejected Entries
        Task<IEnumerable<FixedAssetItemViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From Designation Table Which Are Not Authorized
        Task<IEnumerable<FixedAssetItemViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From Designation Table For Modification
        Task<IEnumerable<FixedAssetItemViewModel>> GetIndexOfVerifiedEntries();

        short GetPrmKeyById(Guid _fixedAssetItemId);

        // Return Rejected Entry
        Task<FixedAssetItemViewModel> GetRejectedEntry(Guid _fixedAssetItemId);

        // Return Record From Designation Table By Given Parameter (i.e. DesignationId)
        Task<FixedAssetItemViewModel> GetUnVerifiedEntry(Guid _fixedAssetItemId);

        // Return Record From Designation Table By Given Parameter (i.e. DesignationId)
        Task<FixedAssetItemViewModel> GetVerifiedEntry(Guid _fixedAssetItemId);

        // Save Designation Modification New Entry
        Task<bool> Modify(FixedAssetItemViewModel _fixedAssetItemViewModel);

        // Reject Designation Entry
        Task<bool> Reject(FixedAssetItemViewModel _fixedAssetItemViewModel);

        // Save Designation New Entry
        Task<bool> Save(FixedAssetItemViewModel _fixedAssetItemViewModel);

        // Authorize Designation Entry
        Task<bool> Verify(FixedAssetItemViewModel _fixedAssetItemViewModel);
    }
}
