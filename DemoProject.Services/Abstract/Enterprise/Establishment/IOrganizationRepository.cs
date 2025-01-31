using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DemoProject.Services.ViewModel.Enterprise.Establishment;

namespace DemoProject.Services.Abstract.Enterprise.Establishment
{
    public interface IOrganizationRepository
    {
        // Amend Organization Delete Entry - If Entry Rejected
        Task<bool> Amend(OrganizationViewModel _organizationViewModel);

        // Delete Organization - Only For Rejected Entry
        Task<bool> RejectDelete(OrganizationViewModel _organizationViewModel, string _entryType);

        // Return Current Active Entry
        Task<OrganizationViewModel> GetActiveEntry();

        // Return Verified Entry
        Task<OrganizationViewModel> GetEntryById(Guid _organizationId);

        // Return Organization Verified Entries
        Task<IEnumerable<OrganizationIndexViewModel>> GetOrganizationIndex();

        // Return Rejected Entry
        Task<OrganizationViewModel> GetOrganizationEntry(string _entryType);

        Task<bool> GetSessionValues(string _entryType);

        // Return True If Any Verification Pending
        Task<bool> IsAnyAuthorizationPending();

        // Save Organization New Entry
        Task<bool> Save(OrganizationViewModel _organizationViewModel);

        // Verify Organization Entry
        Task<bool> Verify(OrganizationViewModel _organizationViewModel);
    }

}
