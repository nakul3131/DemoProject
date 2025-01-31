using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using DemoProject.Services.ViewModel.Management.Master;

namespace DemoProject.Services.Abstract.Management.Master
{
    public interface IAgendaMeetingTypeRepository
    {

        // Amend AgendaMeetingType Entry - If Entry Rejected
        Task<bool> Amend(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel);

        // Delete AgendaMeetingType Entry - Only For Rejected Entry
        Task<bool> Delete(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel);

        // Closed BusinessOfficePasswordPolicy - Only For Verified Entry
        Task<bool> Closed(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel);

        // Return Rejected Entries
        Task<IEnumerable<AgendaMeetingTypeViewModel>> GetIndexOfRejectedEntries();

        // Return Valid List From AgendaMeetingType Table Which Are Not Authorized
        Task<IEnumerable<AgendaMeetingTypeViewModel>> GetIndexOfUnVerifiedEntries();

        // Return Valid List From AgendaMeetingType Table For Modification
        Task<IEnumerable<AgendaMeetingTypeViewModel>> GetIndexOfVerifiedEntries();

        // Return Empty AgendaMeetingType Table 
        Task<IEnumerable<AgendaMeetingTypeViewModel>> GetIndexWithCreateModifyOperationStatus();

        List<SelectListItem> GetModelEntries(Guid vehicleMakeId);

        // Return Rejected AgendaMeetingType Entries
        Task<IEnumerable<AgendaMeetingTypeViewModel>> GetRejectedEntries(int _agendaPrmKey);

        // Return UnVerified AgendaMeetingType Entries
        Task<IEnumerable<AgendaMeetingTypeViewModel>> GetUnverifiedEntries(int _agendaPrmKey);

        // Return Verified AgendaMeetingType Entries
        Task<IEnumerable<AgendaMeetingTypeViewModel>> GetVerifiedEntries(int _agendaPrmKey);

        // Return Empty AgendaMeetingTypeViewModel (Used For Create)
        Task<AgendaMeetingTypeViewModel> GetViewModelForCreate(int _agendaPrmKey);

        // Return AgendaMeetingTypeViewModel (Used For Reject View)
        Task<AgendaMeetingTypeViewModel> GetViewModelForReject(int _agendaPrmKey);

        // Return AgendaMeetingTypeViewModel (Used For Unverified View)
        Task<AgendaMeetingTypeViewModel> GetViewModelForUnverified(int _agendaPrmKey);

        // Reject AgendaMeetingType Entry
        Task<bool> Reject(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel);

        // Save AgendaMeetingType New Entry
        Task<bool> Save(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel);

        // Verify AgendaMeetingType Entry
        Task<bool> Verify(AgendaMeetingTypeViewModel _agendaMeetingTypeViewModel);

    }
}
