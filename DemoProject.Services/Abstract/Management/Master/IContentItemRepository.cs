using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IContentItemRepository
    {
        // Amend ContentItem Delete Entry - If Entry Rejected
        Task<bool> Amend(ContentItemViewModel _contentItemViewModel);

        List<SelectListItem> ContentItemDropdownList { get; }

        // Delete ContentItem - Only For Rejected Entry
        Task<bool> Delete(ContentItemViewModel _contentItemViewModel);

        // Return Rejected Entries
        Task<IEnumerable<ContentItemViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From ContentItem Table Which Are Not Authorized
        Task<IEnumerable<ContentItemViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From ContentItem Table For Modification
        Task<IEnumerable<ContentItemViewModel>> GetIndexOfVerifiedEntries();

        short GetPrmKeyById(Guid _ContentItemId);

        // Return Rejected Entry
        Task<ContentItemViewModel> GetRejectedEntry(Guid _contentItemId);

        bool GetUniqueContentItemName(string _nameOfContentItem);

        // Return Record From ContentItem Table By Given Parameter (i.e. ContentItemId)
        Task<ContentItemViewModel> GetUnVerifiedEntry(Guid _contentItemId);

        // Return Record From ContentItem Table By Given Parameter (i.e. ContentItemId)
        Task<ContentItemViewModel> GetVerifiedEntry(Guid _contentItemId);

        // Save ContentItem Modification New Entry
        Task<bool> Modify(ContentItemViewModel _contentItemViewModel);

        // Reject ContentItem Entry
        Task<bool> Reject(ContentItemViewModel _contentItemViewModel);

        // Save ContentItem New Entry
        Task<bool> Save(ContentItemViewModel _contentItemViewModel);

        // Authorize ContentItem Entry
        Task<bool> Verify(ContentItemViewModel _contentItemViewModel);

    }
}
